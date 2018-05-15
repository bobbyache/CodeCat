using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Files.Infrastructure;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
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

        public TopicWorkItemForm(IFile workItem, AppFacade application, bool isNew = false)
        {
            InitializeComponent();

            if (!(workItem is ITopicDocument))
                throw new ArgumentException("Target is not the incorrect type.");

            this.tabControlFile.ImageList = IconRepository.ImageList;
            base.application = application;
            this.topicDocument = workItem as ITopicDocument;
            this.topicDocument.TopicSectionAdded += topicDocument_TopicSectionAdded;
            this.topicDocument.TopicSectionRemoved += topicDocument_TopicSectionRemoved;
            this.topicDocument.TopicSectionMovedLeft += topicDocument_TopicSectionMovedLeft;
            this.topicDocument.TopicSectionMovedRight += topicDocument_TopicSectionMovedRight;
            base.workItem = workItem;
            this.Tag = ((ITitledEntity)workItem).Id;
            this.tabManager = new WorkItemTabManager(this.tabControlFile, this.btnMenu);
            this.tabManager.BeforeDeleteTab += tabManager_BeforeDeleteTab;
  
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

                case TopicSectionType.VersionedCode:
                    topicDocument.AddTopicSection(TopicSectionType.VersionedCode, "Versioned Code Snippet", ConfigSettings.DefaultSyntax, "txt");
                    break;

                case TopicSectionType.PdfViewer:
                    topicDocument.AddTopicSection(TopicSectionType.PdfViewer, "PDF Document");
                    break;

                case TopicSectionType.ImagePager:
                    topicDocument.AddTopicSection(TopicSectionType.ImagePager, "Image Pager");
                    break;

                case TopicSectionType.WebReferences:
                    topicDocument.AddTopicSection(TopicSectionType.WebReferences, "Web References");
                    break;

                case TopicSectionType.SearchableSnippet:
                    topicDocument.AddTopicSection(TopicSectionType.SearchableSnippet, "Searchable Snippets", ConfigSettings.DefaultSyntax, "xml");
                    break;

                case TopicSectionType.SearchableEvent:
                    topicDocument.AddTopicSection(TopicSectionType.SearchableEvent, "Event Diary", null, "xml");
                    break;

                case TopicSectionType.SingleImage:
                    topicDocument.AddTopicSection(TopicSectionType.SingleImage, "Single Image", null, "png");
                    break;

                case TopicSectionType.RtfEditor:
                    topicDocument.AddTopicSection(TopicSectionType.RtfEditor, "Notes", null, "rtf");
                    break;

                case TopicSectionType.FileAttachments:
                    topicDocument.AddTopicSection(TopicSectionType.FileAttachments, "File Attachments", null, null);
                    break;

                case TopicSectionType.CodeTemplate:
                    topicDocument.AddTopicSection(TopicSectionType.CodeTemplate, "Code Template", null);
                    break;

                case TopicSectionType.TaskList:
                    topicDocument.AddTopicSection(TopicSectionType.TaskList, "Task List");
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
            btnDelete.Image = Gui.Resources.GetImage(Constants.ImageKeys.DeleteSnippet);
            btnSave.Image = Gui.Resources.GetImage(Constants.ImageKeys.SaveSnippet);
            chkEdit.Image = Gui.Resources.GetImage(Constants.ImageKeys.EditSnippet);
            btnDiscardChange.Image = Gui.Resources.GetImage(Constants.ImageKeys.DiscardSnippetChanges);
            
            btnAddItem.Image = Gui.Resources.GetImage(Constants.ImageKeys.AddTemplate);
            btnRemoveCodeItem.Image = Gui.Resources.GetImage(Constants.ImageKeys.RemoveTemplate);
            btnMoveLeft.Image = Gui.Resources.GetImage(Constants.ImageKeys.MoveLeft);
            btnMoveRight.Image = Gui.Resources.GetImage(Constants.ImageKeys.MoveRight);
            btnMenu.Image = Gui.Resources.GetImage(Constants.ImageKeys.GroupMenu);

            btnAddPdfDocument.Image = IconRepository.Get(IconRepository.TopicSections.PDF).Image;
            btnAddImage.Image = IconRepository.Get(IconRepository.TopicSections.SingleImage).Image;
            btnAddHyperlinks.Image = IconRepository.Get(IconRepository.TopicSections.WebReferences).Image;
            btnFileGroup.Image = IconRepository.Get(IconRepository.TopicSections.FileAttachments).Image;
            btnImageSet.Image = IconRepository.Get(IconRepository.TopicSections.ImageSet).Image;
            btnRichText.Image = IconRepository.Get(IconRepository.TopicSections.RTF).Image;
            btnAddCode.Image = IconRepository.Get(IconRepository.TopicSections.CodeFile).Image;
            btnSearchableEventDiary.Image = IconRepository.Get(IconRepository.TopicSections.EventDiary).Image;
            btnOpenFolder.Image = Gui.Resources.GetImage(Constants.ImageKeys.Folder);

            this.Icon = IconRepository.CodeGroupIcon;
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

            btnAddCode.Click += (s, e) => CreateTopicSection(TopicSectionType.Code);
            btnAddVersionedCode.Click += (s, e) => CreateTopicSection(TopicSectionType.VersionedCode);
            btnAddHyperlinks.Click += (s, e) => CreateTopicSection(TopicSectionType.WebReferences);
            btnAddPdfDocument.Click += (s, e) => CreateTopicSection(TopicSectionType.PdfViewer);
            btnImageSet.Click += (s, e) => CreateTopicSection(TopicSectionType.ImagePager);
            btnSearchableSnippetList.Click += (s, e) => CreateTopicSection(TopicSectionType.SearchableSnippet);
            btnAddImage.Click += (s, e) => CreateTopicSection(TopicSectionType.SingleImage);
            btnRichText.Click += (s, e) => CreateTopicSection(TopicSectionType.RtfEditor);
            btnFileGroup.Click += (s, e) => CreateTopicSection(TopicSectionType.FileAttachments);
            btnSearchableEventDiary.Click += (s, e) => CreateTopicSection(TopicSectionType.SearchableEvent);
            btnCodeTemplate.Click += (s, e) => CreateTopicSection(TopicSectionType.CodeTemplate);
            btnTaskList.Click += (s, e) => CreateTopicSection(TopicSectionType.TaskList);

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
            this.txtKeywords.Text = ((IKeywordTarget)workItem).CommaDelimitedKeywords;
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

        #region Tab Manager Events

        private void tabManager_BeforeDeleteTab(object sender, WorkItemTabEventArgs e)
        {
            if (e.TabUserControl is QikScriptCtrl)
            {
                QikScriptCtrl scriptCtrl = e.TabUserControl as QikScriptCtrl;
                scriptCtrl.Modified -= scriptCtrl_Modified;
            }
            else if (e.TabUserControl is QikTemplateCodeCtrl)
            {
                QikTemplateCodeCtrl codeItemCtrl = e.TabUserControl as QikTemplateCodeCtrl;
                codeItemCtrl.Modified -= codeItemCtrl_Modified;
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
            topicDocument.CommaDelimitedKeywords = this.txtKeywords.Text.Trim();
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
                TopicSectionControlFactory.Create(topicSection, this.topicDocument, this.application, codeItemCtrl_Modified),
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
