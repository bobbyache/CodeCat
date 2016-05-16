using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.UI.WinForms;
using CygX1.UI.WinForms.RecentFileMenu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class MainForm : Form
    {
        private RecentProjectMenu recentProjectMenu;
        private RegistrySettings registrySettings;
        private AppFacade application = new AppFacade();
        private SearchForm searchForm;

        public MainForm()
        {
            InitializeComponent();

            dockPanel.ContentRemoved += (s, e) => CreateSnippetDocumentIfNone();

            //dockPanel.SaveAsXml(
            //dockPanel.LoadFromXml(
            this.registrySettings = new RegistrySettings(ConfigSettings.RegistryPath);
            this.application.CodeSyntaxFolderPath = ConfigSettings.SyntaxFilePath;

            InitializeMenuClickEvents();
            InitializeRecentProjectMenu();
            InitializeSearchForm();

            EnableControls();

            searchForm.Activate();
        }

        private void InitializeMenuClickEvents()
        {
            mnuFileOpen.Click += mnuFileOpen_Click;
            mnuWindowKeywordSearch.Click += mnuWindowKeywordSearch_Click;
            mnuResultsDeleteSelection.Click += mnuResultsDeleteSelection_Click;
            mnuSnippetsAdd.Click += mnuSnippetsAdd_Click;
            mnuSnippetsViewModify.Click += mnuSnippetsViewModify_Click;
            mnuResultsAddKeywords.Click += mnuResultsAddKeywords_Click;
        }

        private void InitializeSearchForm()
        {
            searchForm = new SearchForm();
            searchForm.OpenSnippet += searchForm_OpenSnippet;
            searchForm.SearchExecuted += (s, e) => ExecuteSearch(e.Keywords);
            searchForm.SelectSnippet += (s, e) => EnableControls();
            searchForm.Show(dockPanel, DockState.DockLeft);
            //searchForm.CloseButton = false;
            //searchForm.CloseButtonVisible = false;
        }

        private void InitializeRecentProjectMenu()
        {
            RecentFiles recentFiles = new RecentFiles();
            recentFiles.MaxNoOfFiles = 15;
            recentFiles.RegistryPath = ConfigSettings.RegistryPath;
            recentFiles.RegistrySubFolder = RegistrySettings.RecentFilesFolder;
            recentFiles.MaxDisplayNameLength = 80;

            recentProjectMenu = new RecentProjectMenu(mnuFileOpenRecent, recentFiles);
            recentProjectMenu.RecentProjectOpened += RecentProjectOpened;
        }

        private void PromptOpenProject()
        {
            string filePath;
            DialogResult result = Dialogs.OpenIndexDialog(this, out filePath);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                OpenProject(filePath);
            }
        }

        private void PromptCreateProject()
        {
            string filePath;
            DialogResult result = Dialogs.CreateIndexDialog(this, out filePath);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                CreateProject(filePath);
            }
        }

        private void OpenProject(string filePath)
        {
            try
            {
                ClearSnippetDocuments();

                this.application.Open(filePath, ConfigSettings.CodeLibraryIndexFileVersion);
                this.Text = WindowCaption();

                searchForm.KeywordSearchText = string.Empty;
                ExecuteSearch(searchForm.KeywordSearchText);
                recentProjectMenu.Notify(filePath);
                recentProjectMenu.CurrentlyOpenedFile = filePath;
                ConfigSettings.LastProject = filePath;
                registrySettings.InitialDirectory = Path.GetDirectoryName(filePath);

                CreateSnippetDocumentIfNone();
                EnableControls();

                searchForm.Activate();
            }
            catch (Exception exception)
            {
                Dialogs.ProjectFileLoadErrorNotification(this, exception);
            }
        }

        private void CreateProject(string filePath)
        {
            ClearSnippetDocuments();

            this.application.Create(filePath, ConfigSettings.CodeLibraryIndexFileVersion);
            this.Text = WindowCaption();
            searchForm.KeywordSearchText = string.Empty;
            ExecuteSearch(searchForm.KeywordSearchText);
            recentProjectMenu.Notify(filePath);
            recentProjectMenu.CurrentlyOpenedFile = filePath;

            ConfigSettings.LastProject = filePath;
            registrySettings.InitialDirectory = Path.GetDirectoryName(filePath);

            CreateSnippetDocumentIfNone();
            EnableControls();

            searchForm.Activate();
        }

        private void EnableControls()
        {
            bool projectLoaded = this.application.Loaded;
            bool itemSelected = searchForm.SingleSnippetSelected;
            bool itemsSelected = searchForm.MultipleSnippetsSelected;

            searchForm.SearchEnabled = projectLoaded;

            mnuSnippetsViewModify.Enabled = projectLoaded && itemSelected;
            mnuSnippetsAdd.Enabled = projectLoaded;
            mnuResultsDeleteSelection.Enabled = projectLoaded && itemSelected;
            mnuFileOpenProjectFolder.Enabled = projectLoaded;

            mnuResultsAddKeywords.Enabled = projectLoaded && (itemSelected || itemsSelected);
            mnuResultsRemoveKeywords.Enabled = projectLoaded && (itemSelected || itemsSelected);

            mnuResultsCopyIdentifier.Enabled = projectLoaded && itemSelected;
            mnuResultsCopyKeywords.Enabled = projectLoaded && (itemSelected || itemsSelected);
        }

        private string WindowCaption()
        {
            if (!this.application.Loaded)
                return ConfigSettings.ApplicationTitle;
            else
                return ConfigSettings.ApplicationTitle + " - [" + this.application.GetContextFileTitle() + "]";
        }

        private void searchForm_OpenSnippet(object sender, OpenSnippetEventArgs e)
        {
            OpenSnippetDocument(e.Item);
            // Note dockPanel.Documents handles the management of your documents. It maintains a collection.
            // This does not include your docked windows, just your "document" windows. This is excellent because
            // you can use this existing collection property to maintain your code snippets.
        }

        private void OpenSnippetDocument(IKeywordIndexItem snippetIndex)
        {
            if (!SnippetIsOpen(snippetIndex))
            {
                CodeFile codeFile = application.OpenCodeSnippet(snippetIndex);
                SnippetForm snippetForm = new SnippetForm(codeFile, application.GetSyntaxes(),  application.GetSyntaxFile(codeFile.Syntax));
                snippetForm.Show(dockPanel, DockState.Document);
            }
            else
            {
                ActivateSnippet(snippetIndex);
            }
        }

        private void CreateSnippetDocumentIfNone()
        {
            if (!this.dockPanel.Contents.OfType<SnippetForm>().Any())
                CreateSnippetDocument();
        }

        private void CreateSnippetDocument()
        {
            CodeFile codeFile = application.CreateCodeSnippet();
            codeFile.Syntax = application.GetSyntaxFile(ConfigSettings.DefaultSyntax);

            SnippetForm snippetForm = new SnippetForm(codeFile, application.GetSyntaxes(), application.GetSyntaxFile(ConfigSettings.DefaultSyntax));
            snippetForm.EditMode = true;
            snippetForm.Show(dockPanel, DockState.Document);
        }

        private SnippetForm GetOpenDocument(string snippetId)
        {
            SnippetForm document = dockPanel.Documents
                .Where(doc => (doc as SnippetForm).SnippetId == snippetId)
                .OfType<SnippetForm>().SingleOrDefault();

            return document;
        }

        private void ActivateSnippet(IKeywordIndexItem snippetIndex)
        {
            SnippetForm document = GetOpenDocument(snippetIndex.Id);
            if (document != null)
                document.Activate();
        }

        private bool SnippetIsOpen(IKeywordIndexItem snippetIndex)
        {
            return dockPanel.Documents.Any(doc => (doc as SnippetForm).SnippetId == snippetIndex.Id);
        }

        private void ExecuteSearch(string delimitedKeywords)
        {
            IKeywordIndexItem[] IndexItems = this.application.FindIndeces(delimitedKeywords);
            searchForm.ReloadListview(IndexItems);
            this.indexCountLabel.Text = ItemCountCaption(IndexItems.Length);
        }

        private string ItemCountCaption(int foundItems)
        {
            if (!this.application.Loaded)
                return "No items loaded";
            else
                return string.Format("{0} of {1} available items found.", foundItems, this.application.GetIndexCount().ToString());
        }

        private void RecentProjectOpened(object sender, RecentProjectEventArgs e)
        {
            if (File.Exists(e.RecentFile.FullPath))
            {
                if (AnyUnsavedDocuments())
                {
                    DialogResult promptResult = PromptSaveChanges();

                    if (promptResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        SaveAllDocuments();
                        OpenProject(e.RecentFile.FullPath);
                    }
                    else if (promptResult == System.Windows.Forms.DialogResult.No)
                    {
                        OpenProject(e.RecentFile.FullPath);
                    }
                    // else, use has cancelled the request.
                }
                else
                {
                    OpenProject(e.RecentFile.FullPath);
                }
            }
            else
            {
                DialogResult result = Dialogs.RemoveRecentMenuItemDialogPrompt(this);
                if (result == System.Windows.Forms.DialogResult.Yes)
                    recentProjectMenu.Remove(e.RecentFile.FullPath);
            }
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            if (AnyUnsavedDocuments())
            {
                DialogResult promptResult = PromptSaveChanges();

                if (promptResult == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveAllDocuments();
                    PromptOpenProject();
                }
                else if (promptResult == System.Windows.Forms.DialogResult.No)
                {
                    PromptOpenProject();
                }
                // else, use has cancelled the request.
            }
            else
            {
                PromptOpenProject();
            }
        }

        private void mnuFileCreateNew_Click(object sender, EventArgs e)
        {
            if (AnyUnsavedDocuments())
            {
                DialogResult promptResult = PromptSaveChanges();

                if (promptResult == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveAllDocuments();
                    PromptCreateProject();
                }
                else if (promptResult == System.Windows.Forms.DialogResult.No)
                {
                    PromptCreateProject();
                }
                // else, use has cancelled the request.
            }
            else
            {
                PromptCreateProject();
            }

        }

        private void mnuWindowKeywordSearch_Click(object sender, EventArgs e)
        {
            searchForm.Activate();
        }

        private void mnuSnippetsAdd_Click(object sender, EventArgs e)
        {
            CreateSnippetDocument();
        }

        private void mnuSnippetsViewModify_Click(object sender, EventArgs e)
        {
            if (searchForm.SingleSnippetSelected && searchForm.SelectedSnippet != null)
            {
                OpenSnippetDocument(searchForm.SelectedSnippet);
            }
        }

        private void mnuResultsAddKeywords_Click(object sender, EventArgs e)
        {
            EnterKeywordsForm frm = new EnterKeywordsForm();
            DialogResult result = frm.ShowDialog(this);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string delimitedKeywordList = frm.Keywords;
                IKeywordIndexItem[] indexItems = searchForm.SelectedSnippets;

                application.AddKeywords(indexItems, delimitedKeywordList);

                foreach (IKeywordIndexItem indexItem in indexItems)
                {
                    SnippetForm snippetForm = GetOpenDocument(indexItem.Id);
                    if (snippetForm != null)
                    {
                        snippetForm.AddKeywords(delimitedKeywordList);
                    }
                }
            }
        }

        private void mnuResultsDeleteSelection_Click(object sender, EventArgs e)
        {
            // Here, you will do exactly the same thing as you did with addKeywordsMenuItem_Click
            // except you'll call application.RemoveKeywords() instead.
            // you overloaded the method in the AppFacade to remove keywords from a single snippet
            // but you can just as well remove from all of them at the same time.
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AnyUnsavedDocuments())
            {
                DialogResult result = PromptSaveChanges();

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (SaveAllDocuments())
                        ClearSnippetDocuments();
                    else
                        e.Cancel = true;
                }
                else if (result == System.Windows.Forms.DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private bool AnyUnsavedDocuments()
        {
            return this.dockPanel.Contents.OfType<SnippetForm>().Where(doc => doc.IsModified == true).Any();
        }

        private bool SaveAllDocuments()
        {
            IEnumerable<SnippetForm> documents = this.dockPanel.Contents.OfType<SnippetForm>().Where(doc => doc.IsModified == true);
            foreach (SnippetForm document in documents)
            {
                if (!document.SaveChanges())
                {
                    document.Activate();
                    return false;
                }
            }
            return true;
        }

        private DialogResult PromptSaveChanges()
        {
            SaveSnippetDialog dialog = new SaveSnippetDialog(UnsavedDocuments());
            DialogResult result = dialog.ShowDialog(this);

            return result;
        }

        private SnippetForm[] UnsavedDocuments()
        {
            return this.dockPanel.Contents.OfType<SnippetForm>().Where(doc => doc.IsModified == true).ToArray();
        }

        private void ClearSnippetDocuments()
        {
            var snippetDocs = this.dockPanel.Contents.OfType<SnippetForm>().ToList();

            while (snippetDocs.Count() > 0)
            {
                SnippetForm snippetDoc = snippetDocs.First();
                snippetDocs.Remove(snippetDoc);
                // important, otherwise snippet for will throw a messagebox.
                // we should have already been through the IsModified check process.
                snippetDoc.FlagSilentClose();
                snippetDoc.Close();
            }
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuFileOpenProjectFolder_Click(object sender, EventArgs e)
        {
            this.application.OpenContextFolder();
        }
    }
}
