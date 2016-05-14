using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure;
using CygX1.UI.WinForms.RecentFileMenu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class MainForm : Form
    {
        private RecentProjectMenu recentProjectMenu;
        private RegistrySettings registrySettings;
        private AppFacade application = new AppFacade();
        private ItemFormController<SnippetForm> formController;

        public MainForm()
        {
            InitializeComponent();
            this.formController = new ItemFormController<SnippetForm>(this);
            this.formController.ItemFormClosed += formController_SnippetFormClosed;
            this.registrySettings = new RegistrySettings(ConfigSettings.RegistryPath);
            this.application.CodeSyntaxFolderPath = ConfigSettings.SyntaxFilePath;
            InitializeIconImages();
            InitializeRecentProjectMenu();
            
            if (!LoadLastProject())
            {
                this.Text = WindowCaption();
                EnableControls();
            }
        }

        private void InitializeIconImages()
        {
            Resources.Namespace = "CygSoft.CodeCat.UI.WinForms.UiResource";
            Resources.ExecutingAssembly = Assembly.GetExecutingAssembly();

            openFileMenuItem.Image = Resources.GetImage(Constants.ImageKeys.OpenProject);
            createNewFileMenuItem.Image = Resources.GetImage(Constants.ImageKeys.NewProject);
            addSnippetMenuItem.Image = Resources.GetImage(Constants.ImageKeys.AddSnippet);
            deleteSnippetMenuItem.Image = Resources.GetImage(Constants.ImageKeys.DeleteSnippet);
            viewSnippetMenuItem.Image = Resources.GetImage(Constants.ImageKeys.EditSnippet);
            findButton.Image = Resources.GetImage(Constants.ImageKeys.FindSnippets);
        }


        private void formController_SnippetFormClosed(object sender, ItemFormEventArgs e)
        {
            this.application.CloseCodeSnippet(e.Id);
            ExecuteSearch(keywordsTextBox.Text);
            ListviewHelper.SelectItem(listView1, e.Id);
        }

        private void createNewFileMenuItem_Click(object sender, EventArgs e)
        {
            string filePath;
            DialogResult result = Dialogs.CreateIndexDialog(this, out filePath);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                CreateProject(filePath);
            }
        }

        private void openFileMenuItem_Click(object sender, EventArgs e)
        {
            string filePath;
            DialogResult result = Dialogs.OpenIndexDialog(this, out filePath);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                OpenProject(filePath);
            }
        }

        private void EnableControls()
        {
            bool projectLoaded = this.application.Loaded;
            bool itemSelected = this.listView1.SelectedItems.Count == 1;
            bool itemsSelected = this.listView1.SelectedItems.Count > 1;

            findButton.Enabled = projectLoaded;

            viewSnippetMenuItem.Enabled = projectLoaded && itemSelected;
            addSnippetMenuItem.Enabled = projectLoaded;
            deleteSnippetMenuItem.Enabled = projectLoaded && itemSelected;
            openFolderMenuItem.Enabled = projectLoaded;
            brokenLinksMenuItem.Enabled = projectLoaded;

            addKeywordsMenuItem.Enabled = projectLoaded && (itemSelected || itemsSelected);
            removeKeywordsMenuItem.Enabled = projectLoaded && (itemSelected || itemsSelected);

            copyIdentifierMenuItem.Enabled = projectLoaded && itemSelected;
            copyKeywordsMenuItem.Enabled = projectLoaded && (itemSelected || itemsSelected);
        }

        private void EnableContextMenuItems()
        {
            bool singleSelection = listView1.SelectedItems.Count == 1;

            menuContextCopyIdentifier.Enabled = singleSelection;
            menuContextCopyKeywords.Enabled = true;
            menuContextDelete.Enabled = singleSelection;
            menuContextViewSnippet.Enabled = singleSelection;
            menuContextAddKeywords.Enabled = true;
            menuContextRemoveKeywords.Enabled = true;
        }

        private void InitializeRecentProjectMenu()
        {
            RecentFiles recentFiles = new RecentFiles();
            recentFiles.MaxNoOfFiles = 15;
            recentFiles.RegistryPath = ConfigSettings.RegistryPath;
            recentFiles.RegistrySubFolder = RegistrySettings.RecentFilesFolder;
            recentFiles.MaxDisplayNameLength = 80;

            recentProjectMenu = new RecentProjectMenu(openRecentMenuItem, recentFiles);
            recentProjectMenu.RecentProjectOpened += recentProjectMenu_RecentProjectOpened;
        }

        private void recentProjectMenu_RecentProjectOpened(object sender, RecentProjectEventArgs e)
        {
            if (File.Exists(e.RecentFile.FullPath))
            {
                OpenProject(e.RecentFile.FullPath);
            }
            else
            {
                DialogResult result = Dialogs.RemoveRecentMenuItemDialogPrompt(this);
                if (result == System.Windows.Forms.DialogResult.Yes)
                    recentProjectMenu.Remove(e.RecentFile.FullPath);
            }
        }

        private void OpenProject(string filePath)
        {
            try
            {
                this.application.Open(filePath, ConfigSettings.CodeLibraryIndexFileVersion);
                this.Text = WindowCaption();
                keywordsTextBox.Text = string.Empty;
                ExecuteSearch(keywordsTextBox.Text);
                recentProjectMenu.Notify(filePath);
                recentProjectMenu.CurrentlyOpenedFile = filePath;
                ConfigSettings.LastProject = filePath;
                registrySettings.InitialDirectory = Path.GetDirectoryName(filePath);
                EnableControls();
            }
            catch (Exception exception)
            {
                Dialogs.ProjectFileLoadErrorNotification(this, exception);
            }
        }

        private void CreateProject(string filePath)
        {
            this.application.Create(filePath, ConfigSettings.CodeLibraryIndexFileVersion);
            this.Text = WindowCaption();
            keywordsTextBox.Text = string.Empty;
            ExecuteSearch(keywordsTextBox.Text);
            recentProjectMenu.Notify(filePath);
            recentProjectMenu.CurrentlyOpenedFile = filePath;
            ConfigSettings.LastProject = filePath;
            registrySettings.InitialDirectory = Path.GetDirectoryName(filePath);
            EnableControls();
        }

        private bool LoadLastProject()
        {
            try
            {
                string lastProject = ConfigSettings.LastProject;
                if (!string.IsNullOrEmpty(lastProject))
                {
                    OpenProject(lastProject);
                    return true;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void DeleteSnippet()
        {
            IKeywordIndexItem codeItem = ListviewHelper.SelectedItem(listView1);

            if (codeItem != null)
            {
                if (this.formController.ItemFormIsOpen(codeItem.Id))
                {
                    MessageBox.Show(this, "Cannot delete an open snippet.",
                        ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }

                DialogResult result = MessageBox.Show(this, "Sure you want to delete this snippet?",
                    ConfigSettings.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.application.RemoveCodeSnippet(codeItem.Id);
                    ListviewHelper.DeleteSelectedItem(listView1);
                    ExecuteSearch(keywordsTextBox.Text);
                }
            }
        }

        private void ViewSnippet()
        {
            IKeywordIndexItem codeItem = ListviewHelper.SelectedItem(listView1);
            if (codeItem != null)
            {
                formController.OpenCodeWindow(this.application.OpenCodeSnippet(codeItem), this.application);
            }
        }

        private void CopySelectedKeyphrases()
        {
            Clipboard.Clear();
            Clipboard.SetText(application.CopyAllKeywords(ListviewHelper.SelectedItems(listView1)));
        }

        private void CopyIdentifier()
        {
            Clipboard.Clear();
            Clipboard.SetText(ListviewHelper.SelectedItem(listView1).Id);
        }

        private string WindowCaption()
        {
            if (!this.application.Loaded)
                return ConfigSettings.ApplicationTitle;
            else
                return ConfigSettings.ApplicationTitle + " - [" + this.application.GetContextFileTitle() + "]";
        }

        private string ItemCountCaption(int foundItems)
        {
            if (!this.application.Loaded)
                return "No items loaded";
            else
                return string.Format("{0} of {1} available items found.", foundItems, this.application.GetIndexCount().ToString());
        }

        private void AddKeywordsToItems()
        {
            EnterKeywordsForm frm = new EnterKeywordsForm();
            DialogResult result = frm.ShowDialog(this);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string delimitedKeywordList = frm.Keywords;
                IKeywordIndexItem[] IndexItems = ListviewHelper.SelectedItems(listView1);
                application.AddKeywords(IndexItems, delimitedKeywordList);
            }
        }

        private void RemoveKeywordFromItems()
        {
            SelectKeywordsForm frm = new SelectKeywordsForm();
            frm.Text = "Remove Keywords";
            IKeywordIndexItem[] IndexItems = ListviewHelper.SelectedItems(listView1);
            frm.Keywords = application.AllKeywords(IndexItems);
            DialogResult result = frm.ShowDialog(this);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string[] keywords = frm.Keywords;
                application.RemoveKeywords(IndexItems, keywords);
            }
        }

        private void ViewKeywordFromItems()
        {
            SelectKeywordsForm frm = new SelectKeywordsForm();
            frm.Text = "View Keywords";
            IKeywordIndexItem[] IndexItems = ListviewHelper.SelectedItems(listView1);
            frm.Keywords = application.AllKeywords(IndexItems);
            DialogResult result = frm.ShowDialog(this);
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            ExecuteSearch(keywordsTextBox.Text);
        }

        private void ExecuteSearch(string delimitedKeywords)
        {
            IKeywordIndexItem[] IndexItems = this.application.FindIndeces(delimitedKeywords);
            ListviewHelper.ReloadListview(listView1, IndexItems);
            this.indexCountLabel.Text = ItemCountCaption(IndexItems.Length);
        }

        private void exitAppMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void addSnippetMenuItem_Click(object sender, EventArgs e)
        {
            formController.OpenCodeWindow(this.application.CreateCodeSnippet(), this.application);
        }

        private void viewSnippetMenuItem_Click(object sender, EventArgs e)
        {
            ViewSnippet();
        }

        private void deleteSnippetMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSnippet();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            IKeywordIndexItem codeItem = ListviewHelper.SelectedItem(listView1);
            if (codeItem != null)
            {
                formController.OpenCodeWindow(this.application.OpenCodeSnippet(codeItem), this.application);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (formController.OpenSnippetsExist
            //if (!formController.RequestExit())
            //{
            //    e.Cancel = true;
            //}
        }

        private void openFolderMenuItem_Click(object sender, EventArgs e)
        {
            this.application.OpenContextFolder();
        }

        private void brokenLinksMenuItem_Click(object sender, EventArgs e)
        {
            BrokenLinksForm frm = new BrokenLinksForm(this.application, this.formController);
            frm.ShowDialog(this);
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutBoxDialog dialog = new AboutBoxDialog();
            dialog.ShowDialog(this);
        }

        private void keywordsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.application.Loaded)
                ExecuteSearch(keywordsTextBox.Text);
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListviewHelper.SortColumn(listView1, e.Column);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableControls();
        }

        private void menuContextViewSnippet_Click(object sender, EventArgs e)
        {
            ViewSnippet();
        }

        private void menuContextDelete_Click(object sender, EventArgs e)
        {
            DeleteSnippet();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!this.application.Loaded)
                return;

            if (e.Button == MouseButtons.Right)
            {
                EnableContextMenuItems();
                if (listView1.FocusedItem.Bounds.Contains(e.Location) == true)
                    contextMenu.Show(Cursor.Position);
            } 
        }

        private void copyKeywordsMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedKeyphrases();
        }

        private void copyIdentifierMenuItem_Click(object sender, EventArgs e)
        {
            CopyIdentifier();
        }

        private void menuContextCopyKeywords_Click(object sender, EventArgs e)
        {
            CopySelectedKeyphrases();
        }

        private void menuContextCopyIdentifier_Click(object sender, EventArgs e)
        {
            CopyIdentifier();
        }

        private void menuContextAddKeywords_Click(object sender, EventArgs e)
        {
            AddKeywordsToItems();
        }

        private void menuContextRemoveKeywords_Click(object sender, EventArgs e)
        {
            RemoveKeywordFromItems();
        }

        private void addKeywordsMenuItem_Click(object sender, EventArgs e)
        {
            AddKeywordsToItems();
        }

        private void removeKeywordsMenuItem_Click(object sender, EventArgs e)
        {
            RemoveKeywordFromItems();
        }

        private void menuContextViewKeywords_Click(object sender, EventArgs e)
        {
            ViewKeywordFromItems();
        }
    }
}
