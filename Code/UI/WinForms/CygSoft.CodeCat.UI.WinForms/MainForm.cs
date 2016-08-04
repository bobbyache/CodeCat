﻿using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using CygSoft.CodeCat.UI.WinForms;
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
using WeifenLuo.WinFormsUI.Docking;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class MainForm : Form
    {
        private RecentProjectMenu recentProjectMenu;
        private RegistrySettings registrySettings;
        private AppFacade application = null;
        private SearchForm searchForm;

        // need this because we don't want to create a new document when
        // when all documents are closing because we're either creating
        // a new project or opening a new project.
        private bool projectClosing = false;

        public MainForm()
        {
            InitializeComponent();

            //dockPanel.SaveAsXml(
            //dockPanel.LoadFromXml(
            this.registrySettings = new RegistrySettings(ConfigSettings.RegistryPath);
            this.application = new AppFacade(ConfigSettings.SyntaxFilePath);

            InitializeFileIcons();
            InitializeIconImages();

            dockPanel.ContentAdded += dockPanel_ContentAdded;
            dockPanel.ContentRemoved += dockPanel_ContentRemoved;

            InitializeMenuClickEvents();
            InitializeRecentProjectMenu();
            InitializeSearchForm();

            if (!LoadLastProject())
            {
                this.Text = WindowCaption();
                EnableControls();
            }

            EnableControls();

            searchForm.Activate();
        }

        private void InitializeFileIcons()
        {
            IconRepository.Load(application.GetSyntaxFileInfo());

        }

        private void InitializeIconImages()
        {
            Resources.Namespace = "CygSoft.CodeCat.UI.WinForms.UiResource";
            Resources.ExecutingAssembly = Assembly.GetExecutingAssembly();

            mnuFileOpen.Image = Resources.GetImage(Constants.ImageKeys.OpenProject);
            mnuFileCreateNew.Image = Resources.GetImage(Constants.ImageKeys.NewProject);
            mnuSnippetsViewModify.Image = Resources.GetImage(Constants.ImageKeys.EditSnippet);
            mnuSnippetsAdd.Image = Resources.GetImage(Constants.ImageKeys.AddSnippet);
        }

        private void InitializeMenuClickEvents()
        {
            mnuFileOpen.Click += mnuFileOpen_Click;
            mnuWindowKeywordSearch.Click += mnuWindowKeywordSearch_Click;
            mnuSnippetsAdd.Click += mnuSnippetsAdd_Click;
            mnuSnippetsViewModify.Click += mnuSnippetsViewModify_Click;
        }

        private void InitializeSearchForm()
        {
            searchForm = new SearchForm(this.application);
            searchForm.OpenSnippet += searchForm_OpenSnippet;
            searchForm.SearchExecuted += (s, e) => { this.indexCountLabel.Text = ItemCountCaption(e.MatchedItemCount); };
            searchForm.SelectSnippet += (s, e) => EnableControls();
            searchForm.KeywordsAdded += searchForm_KeywordsAdded;
            searchForm.KeywordsRemoved += searchForm_KeywordsRemoved;

            searchForm.Show(dockPanel, DockState.DockLeft);
            //searchForm.CloseButton = false;
            //searchForm.CloseButtonVisible = false;
        }

        private void searchForm_KeywordsRemoved(object sender, SearchKeywordsModifiedEventArgs e)
        {
            foreach (IKeywordIndexItem item in e.Items)
            {
                IContentDocument document = dockPanel.Documents
                    .Where(doc => (doc as IContentDocument).Id == item.Id)
                    .OfType<IContentDocument>().SingleOrDefault();

                if (document != null)
                    document.RemoveKeywords(e.Keywords, false);
            }
        }

        private void searchForm_KeywordsAdded(object sender, SearchKeywordsModifiedEventArgs e)
        {
            foreach (IKeywordIndexItem item in e.Items)
            {
                IContentDocument document = dockPanel.Documents
                    .Where(doc => (doc as IContentDocument).Id == item.Id)
                    .OfType<IContentDocument>().SingleOrDefault();

                if (document != null)
                    document.AddKeywords(e.Keywords, false);
            }
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
            DialogResult result = Dialogs.OpenProjectFileDialog(this, application.ProjectFileExtension, out filePath);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                OpenProject(filePath);
            }
        }

        private void PromptCreateProject()
        {
            string filePath;
            DialogResult result = Dialogs.CreateProjectFileDialog(this, application.ProjectFileExtension, out filePath);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                CreateProject(filePath);
            }
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
                Dialogs.LoadLastProjectErrorNotification(this, exception);
            }
            return false;
        }

        private void OpenProject(string filePath)
        {
            try
            {
                // first lets record any open documents of an already opened project.
                if (this.application.Loaded)
                    RecordOpenDocuments();

                // important: we want to ensure that we don't create a new document
                // because we're closing the project. Otherwise, when the project loads
                // it will find that there is a new document open and will not create
                // a new one... the old one from the old project will remain open and
                // the index item will never be created.
                this.projectClosing = true;
                ClearSnippetDocuments();
                this.projectClosing = false;

                this.application.Open(filePath, ConfigSettings.CodeLibraryIndexFileVersion);
                this.Text = WindowCaption();

                recentProjectMenu.Notify(filePath);
                recentProjectMenu.CurrentlyOpenedFile = filePath;
                ConfigSettings.LastProject = filePath;
                registrySettings.InitialDirectory = Path.GetDirectoryName(filePath);

                CreateSnippetDocumentIfNone();
                LoadLastOpenedDocuments();

                EnableControls();

                searchForm.KeywordSearchText = string.Empty;
                searchForm.ExecuteSearch();
                searchForm.Activate();
            }
            catch (Exception exception)
            {
                Dialogs.ProjectFileLoadErrorNotification(this, exception);
            }
        }

        private void LoadLastOpenedDocuments()
        {
            try
            {
                IKeywordIndexItem[] indeces = application.GetLastOpenedIds();
                foreach (IKeywordIndexItem index in indeces)
                {
                    OpenSnippetDocument(index);
                }
            }
            catch (Exception exception)
            {
                Dialogs.LoadLastOpenedDocumentsErrorNotification(this, exception);
            }
        }

        private void RecordOpenDocuments()
        {
            try
            {
                if (application.Loaded)
                {
                    IKeywordIndexItem[] keywordIndexItems = this.dockPanel.Contents.OfType<IContentDocument>()
                        .Where(doc => doc.IsNew == false)
                        .Select(doc => doc.KeywordIndex).ToArray();

                    application.SetLastOpenedIds(keywordIndexItems);
                }
            }
            catch (Exception exception)
            {
                Dialogs.RecordLastOpenedDocumentsErrorNotification(this, exception);
            }
        }

        private void CreateProject(string filePath)
        {
            // first lets record any open documents of an already opened project.
            if (this.application.Loaded)
                RecordOpenDocuments();
            // important: we want to ensure that we don't create a new document
            // because we're closing the project. Otherwise, when the project loads
            // it will find that there is a new document open and will not create
            // a new one... the old one from the old project will remain open and
            // the index item will never be created.
            this.projectClosing = true;
            ClearSnippetDocuments();
            this.projectClosing = false;

            this.application.Create(filePath, ConfigSettings.CodeLibraryIndexFileVersion);
            this.Text = WindowCaption();
            searchForm.KeywordSearchText = string.Empty;
            searchForm.ExecuteSearch();
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
            mnuFileOpenProjectFolder.Enabled = projectLoaded;
            mnuDocuments.Enabled = projectLoaded;
        }

        private string WindowCaption()
        {
            if (!this.application.Loaded)
                return ConfigSettings.ApplicationTitle;
            else
                return ConfigSettings.ApplicationTitle + " - [" + this.application.ProjectFileTitle + "]";
        }

        private void OpenSnippetDocument(IKeywordIndexItem snippetIndex)
        {
            if (!SnippetIsOpen(snippetIndex))
            {
                if (snippetIndex is ICodeKeywordIndexItem)
                {
                    CodeFile codeFile = application.OpenCodeFileTarget(snippetIndex);
                    IContentDocument snippetForm = new SnippetDocument(codeFile, application);
                    snippetForm.HeaderFieldsVisible = false;
                    snippetForm.DocumentDeleted += snippetForm_DocumentDeleted;
                    snippetForm.DocumentSaved += snippetForm_DocumentSaved;
                    snippetForm.Show(dockPanel, DockState.Document);
                }
                else if (snippetIndex is IQikKeywordIndexItem)
                {
                    QikFile qikFile = application.OpenQikSnippet(snippetIndex);
                    IContentDocument snippetForm = new QikCodeDocument(qikFile, application);
                    snippetForm.HeaderFieldsVisible = false;
                    snippetForm.DocumentDeleted += snippetForm_DocumentDeleted;
                    snippetForm.DocumentSaved += snippetForm_DocumentSaved;
                    snippetForm.Show(dockPanel, DockState.Document);
                }
            }
            else
            {
                ActivateSnippet(snippetIndex);
            }
        }

        private void CreateSnippetDocumentIfNone()
        {
            if (!this.dockPanel.Contents.OfType<IContentDocument>().Any())
                CreateSnippetDocument();
        }

        private void CreateSnippetDocument()
        {
            CodeFile codeFile = application.CreateCodeSnippet(ConfigSettings.DefaultSyntax);
            IContentDocument snippetForm = new SnippetDocument(codeFile, application, true);

            snippetForm.DocumentDeleted += snippetForm_DocumentDeleted;
            snippetForm.DocumentSaved += snippetForm_DocumentSaved;
            snippetForm.HeaderFieldsVisible = true;
            snippetForm.Show(dockPanel, DockState.Document);
        }

        private void CreateQikTemplateDocument()
        {
            QikFile qikFile = application.CreateQikTemplate(ConfigSettings.DefaultSyntax);
            IContentDocument snippetForm = new QikCodeDocument(qikFile, application, true);

            snippetForm.DocumentDeleted += snippetForm_DocumentDeleted;
            snippetForm.DocumentSaved += snippetForm_DocumentSaved;
            snippetForm.HeaderFieldsVisible = true;
            snippetForm.Show(dockPanel, DockState.Document);
        }

        private IContentDocument GetOpenDocument(string snippetId)
        {
            IContentDocument document = dockPanel.Documents
                .Where(doc => (doc as IContentDocument).Id == snippetId)
                .OfType<IContentDocument>().SingleOrDefault();

            return document;
        }

        private void ActivateSnippet(IKeywordIndexItem snippetIndex)
        {
            IContentDocument document = GetOpenDocument(snippetIndex.Id);
            if (document != null)
                document.Activate();
        }

        private bool SnippetIsOpen(IKeywordIndexItem snippetIndex)
        {
            return dockPanel.Documents.Any(doc => (doc as IContentDocument).Id == snippetIndex.Id);
        }

        private string ItemCountCaption(int foundItems)
        {
            if (!this.application.Loaded)
                return "No items loaded";
            else
                return string.Format("{0} of {1} available items found.", foundItems, this.application.GetIndexCount().ToString());
        }

        private bool SaveAllDocuments()
        {
            IEnumerable<IContentDocument> documents = this.dockPanel.Contents.OfType<IContentDocument>().Where(doc => doc.IsModified == true);
            foreach (IContentDocument document in documents)
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

        private IContentDocument[] UnsavedDocuments()
        {
            return this.dockPanel.Contents.OfType<IContentDocument>().Where(doc => doc.IsModified == true).ToArray();
        }

        private void ClearSnippetDocuments()
        {
            var snippetDocs = this.dockPanel.Contents.OfType<IContentDocument>().ToList();

            while (snippetDocs.Count() > 0)
            {
                IContentDocument snippetDoc = snippetDocs.First();
                snippetDocs.Remove(snippetDoc);
                // important, otherwise snippet for will throw a messagebox.
                // we should have already been through the IsModified check process.
                snippetDoc.CloseWithoutPrompts = true;
                snippetDoc.Close();
            }
        }

        private bool AnyUnsavedDocuments()
        {
            return this.dockPanel.Contents.OfType<IContentDocument>().Where(doc => doc.IsModified == true).Any();
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

        private void snippetForm_DocumentDeleted(object sender, EventArgs e)
        {
            searchForm.ExecuteSearch();
        }

        private void snippetForm_DocumentSaved(object sender, DocumentSavedFileEventArgs e)
        {
            searchForm.ExecuteSearch(e.Item.Id);
            mnuDocuments.DropDownItems[e.Item.Id].Text = e.Document.Text;
            mnuDocuments.DropDownItems[e.Item.Id].Image = e.Document.IconImage;
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
                // else, user has cancelled the request.
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

        private void mnuAddQikTemplate_Click(object sender, EventArgs e)
        {
            CreateQikTemplateDocument();
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
                    IContentDocument snippetForm = GetOpenDocument(indexItem.Id);
                    if (snippetForm != null)
                    {
                        snippetForm.AddKeywords(delimitedKeywordList);
                    }
                }
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

        private void mnuHelpHelpTopics_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(ConfigSettings.HelpPageUrl);
            }
            catch (Exception ex)
            {
                Dialogs.HelpPageErrorNotification(this, ex);
            }
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            AboutBoxDialog dialog = new AboutBoxDialog();
            dialog.ShowDialog(this);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AnyUnsavedDocuments())
            {
                DialogResult result = PromptSaveChanges();

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (SaveAllDocuments())
                    {
                        RecordOpenDocuments();
                        //ClearSnippetDocuments();
                    }
                    else
                        e.Cancel = true;
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    RecordOpenDocuments();
                }
                else if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                RecordOpenDocuments();
            }
        }

        private void dockPanel_ContentRemoved(object sender, DockContentEventArgs e)
        {
            if (e.Content is IContentDocument)
            {
                IContentDocument snippetForm = e.Content as IContentDocument;
                ToolStripMenuItem menuItem = mnuDocuments.DropDownItems[snippetForm.Id] as ToolStripMenuItem;
                if (menuItem != null)
                {
                    menuItem.Click -= mnuDocumentWindow_Click;
                    mnuDocuments.DropDownItems.Remove(menuItem);
                    // don't create a new snippet document if the project is closing!
                    // this is important as it creates a bug that means that the keyword
                    // index item 
                    if (!projectClosing)
                        CreateSnippetDocumentIfNone();
                }
            }

        }

        private void dockPanel_ContentAdded(object sender, DockContentEventArgs e)
        {
            if (e.Content is IContentDocument)
            {
                IContentDocument snippetForm = e.Content as IContentDocument;
                ToolStripMenuItem menuItem = new ToolStripMenuItem(snippetForm.Text, null, mnuDocumentWindow_Click);
                menuItem.Image = snippetForm.IconImage;
                menuItem.Name = snippetForm.Id;
                menuItem.Tag = snippetForm;
                mnuDocuments.DropDownItems.Add(menuItem);
            }
        }

        private void mnuDocumentWindow_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            string snippetId = menuItem.Name;
            IContentDocument snippetForm = menuItem.Tag as IContentDocument;
            snippetForm.Activate();
        }

        private void searchForm_OpenSnippet(object sender, OpenSnippetEventArgs e)
        {
            OpenSnippetDocument(e.Item);
            // Note dockPanel.Documents handles the management of your documents. It maintains a collection.
            // This does not include your docked windows, just your "document" windows. This is excellent because
            // you can use this existing collection property to maintain your code snippets.
        }


    }
}
