using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using CygSoft.CodeCat.UI.Resources;
using CygSoft.CodeCat.UI.WinForms.Controls;
using CygSoft.CodeCat.UI.WinForms.Documents;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class TopicWorkItemForm : BaseWorkItemForm, IWorkItemForm
    {
        private ITopicDocument topicDocument = null;
        private WorkItemTabManager tabManager = null;

        #region Constructors

        public TopicWorkItemForm(IFile workItem, IAppFacade application, IImageResources imageResources, bool isNew = false)
        {
            InitializeComponent();

            if (imageResources == null)
                throw new ArgumentNullException("Image Repository is a required constructor parameter and cannot be null");
            this.imageResources = imageResources;

            if (!(workItem is ITopicDocument))
                throw new ArgumentException("Target is not the incorrect type.");

            this.tabControlFile.ImageList = imageResources.ImageList;

            base.application = application;
            this.topicDocument = workItem as ITopicDocument;
            this.topicDocument.TopicSectionAdded += topicDocument_TopicSectionAdded;
            this.topicDocument.TopicSectionRemoved += topicDocument_TopicSectionRemoved;
            this.topicDocument.TopicSectionMovedLeft += topicDocument_TopicSectionMovedLeft;
            this.topicDocument.TopicSectionMovedRight += topicDocument_TopicSectionMovedRight;
            base.workItem = workItem;
            this.Tag = ((ITitledEntity)workItem).Id;
            this.tabManager = new WorkItemTabManager(this.tabControlFile, this.btnMenu, imageResources);
  
            RebuildTabs();
            InitializeImages();
            InitializeControls();
            ResetFields();
            // event registration after all properties are set...
            RegisterEvents();

            // finally set the state of the document
            base.IsNew = isNew;
        }

        #endregion

        #region Public Methods

        public bool SaveChanges()
        {
            return base.Save(base.workItem, this);
        }

        public void AddKeywords(string keywords, bool flagModified = true)
        {
            // in fact, it seems that "codeItemFile" has already been updated because we have a reference to it in memory, but
            // this is just a "defensive programming" approach.
            if (flagModified)
                txtKeywords.Text = base.AddKeywords(keywords);
            else
            {
                txtKeywords.TextChanged -= SetModified;
                txtKeywords.Text = base.AddKeywords(keywords);
                txtKeywords.TextChanged += SetModified;
            }
        }

        public void RemoveKeywords(string keywords, bool flagModified = true)
        {
            // in fact, it seems that "codeItemFile" has already been updated because we have a reference to it in memory, but
            // this is just a "defensive programming" approach.
            if (flagModified)
                txtKeywords.Text = base.RemoveKeywords(keywords);

            else
            {
                txtKeywords.TextChanged -= SetModified;
                txtKeywords.Text = base.RemoveKeywords(keywords);
                txtKeywords.TextChanged += SetModified;
            }
        }

        public void CreateTopicSection(TopicSectionType topicSectionType)
        {
            switch (topicSectionType)
            {
                case TopicSectionType.Code:
                    topicDocument.AddTopicSection(TopicSectionType.Code, "Code Snippet", ConfigSettings.DefaultSyntax, "txt");
                    break;

                case TopicSectionType.WebReferences:
                    topicDocument.AddTopicSection(TopicSectionType.WebReferences, "Web References");
                    break;

                default:
                    break;
            }

            this.IsModified = true;
        }

        #endregion

        #region Overrides

        protected override bool ValidateChanges()
        {
            if (string.IsNullOrWhiteSpace(this.txtTitle.Text))
            {
                Gui.Dialogs.MissingRequiredFieldMessageBox(this, "Title");
                base.HeaderFieldsVisible = true;
                this.txtTitle.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(this.txtKeywords.Text))
            {
                Gui.Dialogs.MissingRequiredFieldMessageBox(this, "Keywords");
                base.HeaderFieldsVisible = true;
                this.txtKeywords.Focus();
                return false;
            }
            else
                return true;
        }

        protected override void SaveFields()
        {
            topicDocument.Save();
        }

        #endregion

        #region Private Methods

        private void SetModified(object sender, EventArgs e)
        {
            this.IsModified = true;

        }
        private void InitializeImages()
        {
            btnDelete.Image = imageResources.GetImage(ImageKeys.DeleteSnippet);
            btnSave.Image = imageResources.GetImage(ImageKeys.SaveSnippet);
            chkEdit.Image = imageResources.GetImage(ImageKeys.EditSnippet);
            btnDiscardChange.Image = imageResources.GetImage(ImageKeys.DiscardSnippetChanges);

            btnAddItem.Image = imageResources.GetImage(ImageKeys.AddTemplate);
            btnRemoveCodeItem.Image = imageResources.GetImage(ImageKeys.RemoveTemplate);
            btnMoveLeft.Image = imageResources.GetImage(ImageKeys.MoveLeft);
            btnMoveRight.Image = imageResources.GetImage(ImageKeys.MoveRight);
            btnMenu.Image = imageResources.GetImage(ImageKeys.GroupMenu);

            btnAddHyperlinks.Image = imageResources.Get(ImageResources.TopicSections.WebReferences).Image;

            btnOpenFolder.Image = imageResources.GetImage(ImageKeys.Folder);

            this.Icon = imageResources.CodeGroupIcon;
        }

        private void RegisterEvents()
        {
            topicDocument.AfterRevert += topicDocument_ContentReverted;
            topicDocument.BeforeSave += topicDocument_BeforeContentSaved;
            topicDocument.AfterSave += topicDocument_ContentSaved;

            base.Deleting += TopicDocumentDeleting;
            base.Saving += TopicDocumentSaving;
            base.Reverting += TopicDocumentReverting;
            base.HeaderFieldsVisibilityChanged += TopicDocumentHeaderFieldsVisibilityChanged;
            base.ModifyStatusChanged += TopicDocumentModifyStatusChanged;

            base.NewStatusChanged += TopicDocumentNewStatusChanged;
            this.chkEdit.Click += (s, e) => { base.HeaderFieldsVisible = chkEdit.Checked; };

            txtTitle.TextChanged += SetModified;
            txtKeywords.TextChanged += SetModified;
            btnDelete.Click += btnDelete_Click;
            btnDiscardChange.Click += btnDiscardChange_Click;

            btnAddHyperlinks.Click += (s, e) => CreateTopicSection(TopicSectionType.WebReferences);

            btnOpenFolder.Click += (s, e) => FileSys.OpenFolder(topicDocument.Folder);
        }

        private void InitializeControls()
        {
            btnSave.Enabled = false;
            btnDiscardChange.Enabled = false;
            btnDelete.Enabled = !this.IsNew;
        }

        private void ResetFields()
        {
            this.txtToolStripTitle.Text = ((ITitledEntity)workItem).Title;
            this.Text = ((ITitledEntity)workItem).Title;
            this.txtKeywords.Text = ((IKeywordTarget)workItem).IndexItem.CommaDelimitedKeywords;
            this.txtTitle.Text = ((ITitledEntity)workItem).Title;

            base.IsModified = false;
        }

        private void RebuildTabs()
        {
            tabManager.Clear();
            foreach (ITopicSection topicSection in topicDocument.TopicSections)
            {
                AddTopicSection(topicSection, false);
            }
        }

        #endregion

        #region Work Item Events

        private void topicDocument_ContentReverted(object sender, FileEventArgs e)
        {
            Gui.Drawing.SuspendDrawing(this);
            ResetFields();
            RebuildTabs();
            Gui.Drawing.ResumeDrawing(this);
        }

        private void topicDocument_ContentSaved(object sender, FileEventArgs e)
        {
            ResetFields();
        }

        private void topicDocument_BeforeContentSaved(object sender, FileEventArgs e)
        {
            topicDocument.Title = this.txtTitle.Text.Trim();
            topicDocument.IndexItem.SetKeywords(this.txtKeywords.Text.Trim());
            topicDocument.Syntax = string.Empty;
        }

        private void topicDocument_TopicSectionMovedRight(object sender, TopicSectionEventArgs e)
        {
            this.IsModified = true;
            Gui.Drawing.SuspendDrawing(this);
            tabManager.OrderTabs(topicDocument.TopicSections);
            tabManager.DisplayTab(e.TopicSection.Id, true);
            Gui.Drawing.ResumeDrawing(this);
        }

        private void topicDocument_TopicSectionMovedLeft(object sender, TopicSectionEventArgs e)
        {
            this.IsModified = true;
            Gui.Drawing.SuspendDrawing(this);
            tabManager.OrderTabs(topicDocument.TopicSections);
            tabManager.DisplayTab(e.TopicSection.Id, true);
            Gui.Drawing.ResumeDrawing(this);
        }

        private void topicDocument_TopicSectionRemoved(object sender, TopicSectionEventArgs e)
        {
            Gui.Drawing.SuspendDrawing(this);
            tabManager.RemoveTab(e.TopicSection.Id);
            tabManager.OrderTabs(topicDocument.TopicSections);
            Gui.Drawing.ResumeDrawing(this);
        }

        private void topicDocument_TopicSectionAdded(object sender, TopicSectionEventArgs e)
        {
            Gui.Drawing.SuspendDrawing(this);

            AddTopicSection(e.TopicSection, true);

            tabManager.OrderTabs(topicDocument.TopicSections);
            tabManager.DisplayTab(e.TopicSection.Id, true);
            Gui.Drawing.ResumeDrawing(this);
        }

        #endregion

        private void AddTopicSection(ITopicSection topicSection, bool selected)
        {
            tabManager.AddTab(topicSection,
                TopicSectionControlFactory.Create(topicSection, imageResources, this.topicDocument, this.application, codeItemCtrl_Modified),
                true, selected);
        }

        #region Document Control Events

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveChanges();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Delete();
        }

        private void btnDiscardChange_Click(object sender, EventArgs e)
        {
            base.RevertChanges();
        }

        private void btnRemoveCodeItem_Click(object sender, EventArgs e)
        {
            if (!tabManager.HasTabs)
                return;

            DialogResult dialogResult = Gui.Dialogs.DeleteItemMessageBox(this, "template");

            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string id = tabManager.SelectedTabId;
                topicDocument.RemoveTopicSection(id);
                this.IsModified = true;
            }
        }

        private void codeItemCtrl_Modified(object sender, EventArgs e)
        {
            this.IsModified = true;
        }

        private void scriptCtrl_Modified(object sender, EventArgs e)
        {
            this.IsModified = true;
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            this.topicDocument.MoveTopicSectionLeft(tabManager.SelectedTabId);
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            this.topicDocument.MoveTopicSectionRight(tabManager.SelectedTabId);
        }

        #endregion

        #region Document Events

        private void TopicDocumentReverting(object sender, EventArgs e)
        {
            topicDocument.Revert();
        }

        private void TopicDocumentHeaderFieldsVisibilityChanged(object sender, EventArgs e)
        {
            Gui.Drawing.SuspendDrawing(this);
            this.chkEdit.Checked = base.HeaderFieldsVisible;
            this.toolstripKeywords.Visible = base.HeaderFieldsVisible;
            this.toolstripTitle.Visible = base.HeaderFieldsVisible;
            Gui.Drawing.ResumeDrawing(this);
        }

        private void TopicDocumentNewStatusChanged(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = !base.IsNew;
        }

        private void TopicDocumentModifyStatusChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = base.IsModified;
            btnDiscardChange.Enabled = base.IsModified && !base.IsNew;
        }

        private void TopicDocumentSaving(object sender, EventArgs e)
        {
            this.SaveChanges();
        }

        private void TopicDocumentDeleting(object sender, EventArgs e)
        {
            base.workItem.Delete();
        }


        #endregion
    }
}
