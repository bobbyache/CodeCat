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

                searchForm.KeywordSearchText = string.Empty;
                ExecuteSearch(searchForm.KeywordSearchText);
                recentProjectMenu.Notify(filePath);
                recentProjectMenu.CurrentlyOpenedFile = filePath;
                ConfigSettings.LastProject = filePath;
                registrySettings.InitialDirectory = Path.GetDirectoryName(filePath);

                CreateNewSnippet();
                EnableControls();

                searchForm.Activate();
            }
            catch (Exception exception)
            {
                Dialogs.ProjectFileLoadErrorNotification(this, exception);
            }
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
            OpenSnippet(e.Item);
            // Note dockPanel.Documents handles the management of your documents. It maintains a collection.
            // This does not include your docked windows, just your "document" windows. This is excellent because
            // you can use this existing collection property to maintain your code snippets.
        }

        private void OpenSnippet(IKeywordIndexItem snippetIndex)
        {
            if (!SnippetIsOpen(snippetIndex))
            {
                CodeFile codeFile = application.OpenCodeSnippet(snippetIndex);
                SnippetForm snippetForm = new SnippetForm(codeFile, application.GetSyntaxFile(codeFile.Syntax));
                snippetForm.Text = snippetIndex.Title;
                snippetForm.Tag = snippetIndex.Id;

                snippetForm.Show(dockPanel, DockState.Document);
            }
            else
            {
                ActivateSnippet(snippetIndex);
            }
        }

        private void CreateNewSnippet()
        {
            CodeFile codeFile = application.CreateCodeSnippet();
            codeFile.Title = "New Code Snippet";
            codeFile.Syntax = "JavaScript";

            SnippetForm snippetForm = new SnippetForm(codeFile, application.GetSyntaxFile("JavaScript"));
            snippetForm.Text = codeFile.Title;
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

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            string filePath;
            DialogResult result = Dialogs.OpenIndexDialog(this, out filePath);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                OpenProject(filePath);
            }
        }

        private void mnuWindowKeywordSearch_Click(object sender, EventArgs e)
        {
            searchForm.Activate();
        }

        private void mnuSnippetsAdd_Click(object sender, EventArgs e)
        {
            CreateNewSnippet();
        }

        private void mnuSnippetsViewModify_Click(object sender, EventArgs e)
        {
            if (searchForm.SingleSnippetSelected && searchForm.SelectedSnippet != null)
            {
                OpenSnippet(searchForm.SelectedSnippet);
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
    }
}
