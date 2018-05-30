using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Files.Infrastructure;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using CygSoft.CodeCat.UI.Resources;
using CygSoft.CodeCat.UI.WinForms.Docked;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using CygX1.UI.WinForms.RecentFileMenu;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class MainForm : Form
    {
        private RecentProjectMenu recentProjectMenu;
        private RegistrySettings registrySettings;
        private IAppFacade application = null;
        private SearchForm searchForm;
        private TaskForm taskForm;
        private CategoryForm categoryForm;
        private PluginsForm pluginsForm;
        private IImageResources imageResources;
        private IconRepository iconRepository;

        // need this because we don't want to create a new document when
        // when all documents are closing because we're either creating
        // a new project or opening a new project.
        private bool projectClosing = false;

        public MainForm()
        {
            InitializeComponent();

            //dockPanel.SaveAsXml(
            //dockPanel.LoadFromXml(
            imageResources = new ImageResources();
            this.iconRepository = new IconRepository();

            this.registrySettings = new RegistrySettings(ConfigSettings.RegistryPath);
            this.application = new AppFacade(ConfigSettings.SyntaxFilePath);
            

            InitializeFileIcons();
            InitializeIconImages();

            dockPanel.ContentAdded += dockPanel_ContentAdded;
            dockPanel.ContentRemoved += dockPanel_ContentRemoved;

            InitializeMenuClickEvents();
            InitializeRecentProjectMenu();
            InitializeSearchForm();
            InitializeCategoryForm();
            InitializeTaskForm();

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
            imageResources.AddCategoryInfo();
            imageResources.AddDocuments();
            imageResources.AddSyntaxes(application.GetSyntaxFileInfo());
        }

        private void InitializeIconImages()
        {
            mnuFileOpen.Image = imageResources.GetImage(ImageKeys.OpenProject);
            mnuFileCreateNew.Image = imageResources.GetImage(ImageKeys.NewProject);
            mnuViewWorkItem.Image = imageResources.GetImage(ImageKeys.EditSnippet);
            mnuAddCodeItem.Image = imageResources.GetImage(ImageKeys.AddSnippet);
            mnuAddTopic.Image = imageResources.Get(ImageResources.TopicSections.CodeGroup).Image;
            mnuAddQikGenerator.Image = imageResources.Get(ImageResources.TopicSections.QikGroup).Image;
            mnuWindowKeywordSearch.Image = imageResources.GetImage(ImageKeys.FindSnippets);
            mnuCurrentTasks.Image = imageResources.GetImage(ImageKeys.EditText);
            mnuCategories.Image = imageResources.GetImage(ImageKeys.OpenCategory);
        }

        private void InitializeMenuClickEvents()
        {
            mnuFileOpen.Click += mnuFileOpen_Click;
            mnuWindowKeywordSearch.Click += mnuWindowKeywordSearch_Click;
            mnuCurrentTasks.Click += mnuCurrentTasks_Click;
            mnuAddCodeItem.Click += mnuAddCodeFile_Click;
            mnuAddQikGenerator.Click += mnuAddQikGenerator_Click;
            mnuAddTopic.Click += mnuAddTopic_Click;
            mnuViewWorkItem.Click += mnuWorkItemView_Click;
            mnuGenerators.Click += MnuGenerators_Click;
        }

        private void MnuGenerators_Click(object sender, EventArgs e)
        {
            DisplayPluginsWindow();
        }

        private void mnuCurrentTasks_Click(object sender, EventArgs e)
        {
            taskForm.Activate();
        }

        private void InitializeSearchForm()
        {
            searchForm = new SearchForm(this.application, imageResources, iconRepository);
            searchForm.OpenTopic += Control_OpenWorkItem;
            searchForm.DeleteTopic += Control_DeleteTopic;
            searchForm.SearchExecuted += (s, e) => { this.indexCountLabel.Text = ItemCountCaption(e.MatchedItemCount); };
            searchForm.SelectTopic += (s, e) => EnableControls();
            searchForm.KeywordsAdded += searchForm_KeywordsAdded;
            searchForm.KeywordsRemoved += searchForm_KeywordsRemoved;

            searchForm.Show(dockPanel, DockState.DockLeftAutoHide);
            //searchForm.CloseButton = false;
            //searchForm.CloseButtonVisible = false;
        }

        private void InitializeTaskForm()
        {
            taskForm = new TaskForm(this.application, this.imageResources);
            taskForm.Show(dockPanel, DockState.DockLeftAutoHide);
        }

        private void InitializeCategoryForm()
        {
            categoryForm = new CategoryForm(this.application, this.imageResources, this.iconRepository);
            categoryForm.OpenWorkItem += Control_OpenWorkItem;
            categoryForm.Show(dockPanel, DockState.DockLeftAutoHide);
        }

        private void DisplayPluginsWindow()
        {
            if (pluginsForm == null)
            {
                pluginsForm = new PluginsForm(this.application);
                pluginsForm.HideOnClose = true;
                pluginsForm.Show(dockPanel, DockState.DockBottomAutoHide);
            }
            else
            {
                pluginsForm.Show();
                pluginsForm.Activate();
                pluginsForm.Focus();
            }
        }

        private void searchForm_KeywordsRemoved(object sender, SearchKeywordsModifiedEventArgs e)
        {
            foreach (IKeywordIndexItem item in e.Items)
            {
                IWorkItemForm contentDocument = dockPanel.Documents
                    .Where(doc => (doc as IWorkItemForm).Id == item.Id)
                    .OfType<IWorkItemForm>().SingleOrDefault();

                if (contentDocument != null)
                    contentDocument.RemoveKeywords(e.Keywords, false);
            }
        }

        private void searchForm_KeywordsAdded(object sender, SearchKeywordsModifiedEventArgs e)
        {
            foreach (IKeywordIndexItem item in e.Items)
            {
                IWorkItemForm contentDocument = dockPanel.Documents
                    .Where(doc => (doc as IWorkItemForm).Id == item.Id)
                    .OfType<IWorkItemForm>().SingleOrDefault();

                if (contentDocument != null)
                    contentDocument.AddKeywords(e.Keywords, false);
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
            DialogResult result = Gui.Dialogs.OpenProjectFileDialog(this, application.ProjectFileExtension, out filePath);

            if (result == System.Windows.Forms.DialogResult.OK)
                OpenProject(filePath);
        }

        private void PromptCreateProject()
        {
            string filePath;
            DialogResult result = Gui.Dialogs.CreateProjectFileDialog(this, application.ProjectFileExtension, out filePath);

            if (result == System.Windows.Forms.DialogResult.OK)
                CreateProject(filePath);
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
                Gui.Dialogs.LoadLastProjectErrorMessageBox(this, exception);
            }
            return false;
        }

        private void OpenProject(string filePath)
        {
            try
            {
                // first lets record any open documents of an already opened project.
                if (this.application.Loaded)
                    RecordOpenWorkItemForms();

                // important: we want to ensure that we don't create a new document
                // because we're closing the project. Otherwise, when the project loads
                // it will find that there is a new document open and will not create
                // a new one... the old one from the old project will remain open and
                // the index item will never be created.
                this.projectClosing = true;
                ClearWorkItemForms();
                this.projectClosing = false;

                this.application.Open(filePath, ConfigSettings.ProjectFileVersion);
                this.Text = WindowCaption();

                recentProjectMenu.Notify(filePath);
                recentProjectMenu.CurrentlyOpenedFile = filePath;
                ConfigSettings.LastProject = filePath;
                registrySettings.InitialDirectory = Path.GetDirectoryName(filePath);

                CreateWorkItemFormIfNone();
                LoadLastOpenedDocuments();

                EnableControls();

                taskForm.LoadTasks();
                categoryForm.LoadCategories();

                searchForm.KeywordSearchText = string.Empty;
                searchForm.ExecuteSearch();
                searchForm.Activate();
            }
            catch (Exception exception)
            {
                Gui.Dialogs.ProjectFileLoadErrorMessageBox(this, exception);
            }
        }

        private void LoadLastOpenedDocuments()
        {
            try
            {
                IKeywordIndexItem[] indeces = application.GetLastOpenedIds();
                foreach (IKeywordIndexItem index in indeces)
                    OpenWorkItemForm(index);
            }
            catch (Exception exception)
            {
                Gui.Dialogs.LoadLastOpenedDocumentsErrorMessageBox(this, exception);
            }
        }

        private void RecordOpenWorkItemForms()
        {
            try
            {
                if (application.Loaded)
                {
                    IKeywordIndexItem[] keywordIndexItems = this.dockPanel.Contents.OfType<IWorkItemForm>()
                        .Where(doc => doc.IsNew == false)
                        .Select(doc => doc.GetKeywordIndex()).ToArray();

                    application.SetLastOpenedIds(keywordIndexItems);
                }
            }
            catch (Exception exception)
            {
                Gui.Dialogs.RecordLastOpenedDocumentsErrorMessageBox(this, exception);
            }
        }

        private void CreateProject(string filePath)
        {
            // first lets record any open documents of an already opened project.
            if (this.application.Loaded)
                RecordOpenWorkItemForms();
            // important: we want to ensure that we don't create a new document
            // because we're closing the project. Otherwise, when the project loads
            // it will find that there is a new document open and will not create
            // a new one... the old one from the old project will remain open and
            // the index item will never be created.
            this.projectClosing = true;
            ClearWorkItemForms();
            this.projectClosing = false;

            this.application.Create(filePath, ConfigSettings.ProjectFileVersion);

            OpenProject(filePath);
        }

        private void EnableControls()
        {
            bool projectLoaded = this.application.Loaded;
            bool itemSelected = searchForm.SingleWorkItemSelected;
            bool itemsSelected = searchForm.MultipleWorkItemsSelected;

            searchForm.SearchEnabled = projectLoaded;

            mnuViewWorkItem.Enabled = projectLoaded && itemSelected;
            mnuAddWorkItem.Enabled = projectLoaded;
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

        private void DeleteWorkItemForm(IKeywordIndexItem keywordIndexItem)
        {
            application.DeleteWorkItem(keywordIndexItem);

            if (WorkItemFormIsOpen(keywordIndexItem))
            {
                IWorkItemForm workItemForm = GetOpenWorkItemForm(keywordIndexItem.Id);
                // important, otherwise workItem for will throw a messagebox.
                // we should have already been through the IsModified check process.
                workItemForm.CloseWithoutPrompts = true;
                workItemForm.Close();
            }
        }

        private void OpenWorkItemForm(IKeywordIndexItem keywordIndexItem)
        {
            if (!WorkItemFormIsOpen(keywordIndexItem))
            {
                IWorkItemForm workItemForm = null;
                IFile workItem = application.OpenWorkItem(keywordIndexItem);

                if (keywordIndexItem is ICodeKeywordIndexItem)
                    workItemForm = new CodeWorkItemForm(workItem, application, iconRepository, imageResources);

                else if (keywordIndexItem is IQikTemplateKeywordIndexItem)
                    workItemForm = new QikWorkItemForm(workItem, application, iconRepository, imageResources);

                else if (keywordIndexItem is ITopicKeywordIndexItem)
                    workItemForm = new TopicWorkItemForm(workItem, application, iconRepository, imageResources);

                if (workItemForm == null)
                    throw new Exception("IContentDocument has not been defined and cannot be opened.");

                workItemForm.HeaderFieldsVisible = false;
                workItemForm.Deleted += workItemForm_DocumentDeleted;
                workItemForm.Saved += workItemForm_DocumentSaved;
                workItemForm.Show(dockPanel, DockState.Document);
            }
            else
            {
                ActivateWorkItemForm(keywordIndexItem);
            }
        }

        private void CreateWorkItemFormIfNone()
        {
            if (!this.dockPanel.Contents.OfType<IWorkItemForm>().Any())
                CreateWorkItem(WorkItemType.CodeFile);
        }

        private void CreateWorkItem(WorkItemType workItemType)
        {
            IWorkItemForm workItemForm = null;
            IFile workItem = application.CreateWorkItem(ConfigSettings.DefaultSyntax, workItemType);

            switch (workItemType)
            {
                case WorkItemType.CodeFile:
                    workItemForm = new CodeWorkItemForm(workItem, application, iconRepository, imageResources, true);
                    break;
                case WorkItemType.Topic:
                    workItemForm = new TopicWorkItemForm(workItem, application, iconRepository, imageResources, true);
                    break;
                case WorkItemType.QikGenerator:
                    workItemForm = new QikWorkItemForm(workItem, application, iconRepository, imageResources, true);
                    break;
            }

            workItemForm.Deleted += workItemForm_DocumentDeleted;
            workItemForm.Saved += workItemForm_DocumentSaved;
            workItemForm.HeaderFieldsVisible = true;
            workItemForm.Show(dockPanel, DockState.Document);
        }

        private IWorkItemForm GetOpenWorkItemForm(string workItemId)
        {
            IWorkItemForm contentDocument = dockPanel.Documents
                .Where(doc => (doc as IWorkItemForm).Id == workItemId)
                .OfType<IWorkItemForm>().SingleOrDefault();

            return contentDocument;
        }

        private void ActivateWorkItemForm(IKeywordIndexItem keywordIndexItem)
        {
            IWorkItemForm contentDocument = GetOpenWorkItemForm(keywordIndexItem.Id);
            if (contentDocument != null)
                contentDocument.Activate();
        }

        private bool WorkItemFormIsOpen(IKeywordIndexItem keywordIndexItem)
        {
            return dockPanel.Documents.Any(doc => (doc as IWorkItemForm).Id == keywordIndexItem.Id);
        }

        private string ItemCountCaption(int foundItems)
        {
            if (!this.application.Loaded)
                return "No items loaded";
            else
                return string.Format("{0} of {1} available items found.", foundItems, this.application.GetIndexCount().ToString());
        }

        private bool SaveAllWorkItemForms()
        {
            IEnumerable<IWorkItemForm> contentDocuments = this.dockPanel.Contents.OfType<IWorkItemForm>().Where(doc => doc.IsModified == true);
            foreach (IWorkItemForm contentDocument in contentDocuments)
            {
                if (!contentDocument.SaveChanges())
                {
                    contentDocument.Activate();
                    return false;
                }
            }
            return true;
        }

        private DialogResult PromptSaveChanges()
        {
            SaveWorkItemDialog dialog = new SaveWorkItemDialog(UnsavedWorkItemForms());
            DialogResult result = dialog.ShowDialog(this);

            return result;
        }

        private IWorkItemForm[] UnsavedWorkItemForms()
        {
            return this.dockPanel.Contents.OfType<IWorkItemForm>().Where(doc => doc.IsModified == true).ToArray();
        }

        private void ClearWorkItemForms()
        {
            var workItemForms = this.dockPanel.Contents.OfType<IWorkItemForm>().ToList();

            while (workItemForms.Count() > 0)
            {
                IWorkItemForm workItemForm = workItemForms.First();
                workItemForms.Remove(workItemForm);
                // important, otherwise workItem for will throw a messagebox.
                // we should have already been through the IsModified check process.
                workItemForm.CloseWithoutPrompts = true;
                workItemForm.Close();
            }
        }

        private bool AnyUnsavedDocuments()
        {
            return this.dockPanel.Contents.OfType<IWorkItemForm>().Where(doc => doc.IsModified == true).Any();
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
                        SaveAllWorkItemForms();
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
                DialogResult result = Gui.Dialogs.RemoveRecentMenuItemDialogMessageBox(this);
                if (result == System.Windows.Forms.DialogResult.Yes)
                    recentProjectMenu.Remove(e.RecentFile.FullPath);
            }
        }

        private void workItemForm_DocumentDeleted(object sender, EventArgs e)
        {
            searchForm.ExecuteSearch();
        }

        private void workItemForm_DocumentSaved(object sender, WorkItemSavedFileEventArgs e)
        {
            searchForm.ExecuteSearch(((ITitledEntity)e.WorkItem).Id);
            mnuDocuments.DropDownItems[((ITitledEntity)e.WorkItem).Id].Text = e.ContentDocument.Text;
            mnuDocuments.DropDownItems[((ITitledEntity)e.WorkItem).Id].Image = e.ContentDocument.IconImage;
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            if (AnyUnsavedDocuments())
            {
                DialogResult promptResult = PromptSaveChanges();

                if (promptResult == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveAllWorkItemForms();
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
                    SaveAllWorkItemForms();
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

        private void mnuAddCodeFile_Click(object sender, EventArgs e)
        {
            CreateWorkItem(WorkItemType.CodeFile);
        }

        private void mnuAddQikGenerator_Click(object sender, EventArgs e)
        {
            CreateWorkItem(WorkItemType.QikGenerator);
        }

        private void mnuAddTopic_Click(object sender, EventArgs e)
        {
            CreateWorkItem(WorkItemType.Topic);
        }

        private void mnuWorkItemView_Click(object sender, EventArgs e)
        {
            if (searchForm.SingleWorkItemSelected && searchForm.SelectedWorkItem != null)
            {
                OpenWorkItemForm(searchForm.SelectedWorkItem);
            }
        }

        private void mnuResultsAddKeywords_Click(object sender, EventArgs e)
        {
            EnterKeywordsDialog frm = new EnterKeywordsDialog();
            DialogResult result = frm.ShowDialog(this);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string delimitedKeywordList = frm.Keywords;
                IKeywordIndexItem[] indexItems = searchForm.SelectedWorkItems;

                application.AddKeywords(indexItems, delimitedKeywordList);

                foreach (IKeywordIndexItem indexItem in indexItems)
                {
                    IWorkItemForm workItemForm = GetOpenWorkItemForm(indexItem.Id);
                    if (workItemForm != null)
                    {
                        workItemForm.AddKeywords(delimitedKeywordList);
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
                Gui.Dialogs.WebPageErrorMessageBox(this, ex);
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
                    if (SaveAllWorkItemForms())
                        RecordOpenWorkItemForms();
                    else
                        e.Cancel = true;
                }
                else if (result == DialogResult.No)
                {
                    RecordOpenWorkItemForms();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                RecordOpenWorkItemForms();
            }
        }

        private void dockPanel_ContentRemoved(object sender, DockContentEventArgs e)
        {
            if (e.Content is IWorkItemForm)
            {
                IWorkItemForm workItemForm = e.Content as IWorkItemForm;
                ToolStripMenuItem menuItem = mnuDocuments.DropDownItems[workItemForm.Id] as ToolStripMenuItem;
                if (menuItem != null)
                {
                    menuItem.Click -= mnuDocumentWindow_Click;
                    mnuDocuments.DropDownItems.Remove(menuItem);
                    // don't create a new workItem document if the project is closing!
                    // this is important as it creates a bug that means that the keyword
                    // index item 
                    if (!projectClosing)
                        CreateWorkItemFormIfNone();
                }
            }

        }

        private void dockPanel_ContentAdded(object sender, DockContentEventArgs e)
        {
            if (e.Content is IWorkItemForm)
            {
                IWorkItemForm workItemForm = e.Content as IWorkItemForm;
                ToolStripMenuItem menuItem = new ToolStripMenuItem(workItemForm.Text, null, mnuDocumentWindow_Click);
                menuItem.Image = workItemForm.IconImage;
                menuItem.Name = workItemForm.Id;
                menuItem.Tag = workItemForm;
                mnuDocuments.DropDownItems.Add(menuItem);
            }
        }

        private void mnuDocumentWindow_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            IWorkItemForm workItemForm = menuItem.Tag as IWorkItemForm;
            workItemForm.Activate();
        }

        private void Control_OpenWorkItem(object sender, TopicIndexEventArgs e)
        {
            OpenWorkItemForm(e.Item);
            // Note dockPanel.Documents handles the management of your documents. It maintains a collection.
            // This does not include your docked windows, just your "document" windows. This is excellent because
            // you can use this existing collection property to maintain your code workItems.
        }

        private void Control_DeleteTopic(object sender, TopicIndexEventArgs e)
        {
            DeleteWorkItemForm(e.Item);
        }

        private void mnuCategories_Click(object sender, EventArgs e)
        {
            categoryForm.Activate();
        }
    }
}
