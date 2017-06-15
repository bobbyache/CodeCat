using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.UI.WinForms.Controls;
using CygSoft.CodeCat.UI.WinForms.Documents;
using System;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class TopicDocumentForm : BaseDocument, IContentDocument
    {
        private ITopicDocument topicDocument = null;
        private DocumentTabManager tabManager = null;

        #region Constructors

        public TopicDocumentForm(ITopicDocument topicDocument, AppFacade application, bool isNew = false)
        {
            InitializeComponent();

            this.tabControlFile.ImageList = IconRepository.ImageList;
            base.application = application;
            this.topicDocument = topicDocument;
            this.topicDocument.TopicSectionAdded += topicDocument_TopicSectionAdded;
            this.topicDocument.TopicSectionRemoved += topicDocument_TopicSectionRemoved;
            this.topicDocument.TopicSectionMovedLeft += topicDocument_TopicSectionMovedLeft;
            this.topicDocument.TopicSectionMovedRight += topicDocument_TopicSectionMovedRight;
            base.persistableTarget = topicDocument;
            this.Tag = topicDocument.Id;
            this.tabManager = new DocumentTabManager(this.tabControlFile, this.btnMenu);
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
            return base.Save(base.persistableTarget, this);
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

        #endregion

        #region Overrides

        protected override bool ValidateChanges()
        {
            if (string.IsNullOrWhiteSpace(this.txtTitle.Text))
            {
                Dialogs.MandatoryFieldRequired(this, "Title");
                base.HeaderFieldsVisible = true;
                this.txtTitle.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(this.txtKeywords.Text))
            {
                Dialogs.MandatoryFieldRequired(this, "Keywords");
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
            btnDelete.Image = Resources.GetImage(Constants.ImageKeys.DeleteSnippet);
            btnSave.Image = Resources.GetImage(Constants.ImageKeys.SaveSnippet);
            chkEdit.Image = Resources.GetImage(Constants.ImageKeys.EditSnippet);
            btnDiscardChange.Image = Resources.GetImage(Constants.ImageKeys.DiscardSnippetChanges);
            
            btnAddItem.Image = Resources.GetImage(Constants.ImageKeys.AddTemplate);
            btnRemoveCodeItem.Image = Resources.GetImage(Constants.ImageKeys.RemoveTemplate);
            btnMoveLeft.Image = Resources.GetImage(Constants.ImageKeys.MoveLeft);
            btnMoveRight.Image = Resources.GetImage(Constants.ImageKeys.MoveRight);
            btnMenu.Image = Resources.GetImage(Constants.ImageKeys.GroupMenu);

            btnAddPdfDocument.Image = IconRepository.Get(IconRepository.Documents.PDF).Image;
            btnAddImage.Image = IconRepository.Get(IconRepository.Documents.SingleImage).Image;
            btnAddHyperlinks.Image = IconRepository.Get(IconRepository.Documents.HyperlinkSet).Image;
            btnFileGroup.Image = IconRepository.Get(IconRepository.Documents.FileSet).Image;
            btnImageSet.Image = IconRepository.Get(IconRepository.Documents.ImageSet).Image;
            btnRichText.Image = IconRepository.Get(IconRepository.Documents.RTF).Image;
            btnAddCode.Image = IconRepository.Get(IconRepository.Documents.CodeFile).Image;

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
        }

        private void InitializeControls()
        {
            btnSave.Enabled = false;
            btnDiscardChange.Enabled = false;
            btnDelete.Enabled = !this.IsNew;
        }

        private void ResetFields()
        {
            this.txtToolStripTitle.Text = base.persistableTarget.Title;
            this.Text = base.persistableTarget.Title;
            this.txtKeywords.Text = base.persistableTarget.CommaDelimitedKeywords;
            this.txtTitle.Text = base.persistableTarget.Title;

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

        private void tabManager_BeforeDeleteTab(object sender, DocumentTabEventArgs e)
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

        #region QikFile Events

        private void topicDocument_ContentReverted(object sender, TopicEventArgs e)
        {
            ControlGraphics.SuspendDrawing(this);
            ResetFields();
            RebuildTabs();
            ControlGraphics.ResumeDrawing(this);
        }

        private void topicDocument_ContentSaved(object sender, TopicEventArgs e)
        {
            ResetFields();
        }

        private void topicDocument_BeforeContentSaved(object sender, TopicEventArgs e)
        {
            topicDocument.Title = this.txtTitle.Text.Trim();
            topicDocument.CommaDelimitedKeywords = this.txtKeywords.Text.Trim();
            topicDocument.Syntax = string.Empty;
        }

        private void topicDocument_TopicSectionMovedRight(object sender, TopicSectionEventArgs e)
        {
            this.IsModified = true;
            ControlGraphics.SuspendDrawing(this);
            tabManager.OrderTabs(topicDocument.TopicSections);
            tabManager.DisplayTab(e.TopicSection.Id, true);
            ControlGraphics.ResumeDrawing(this);
        }

        private void topicDocument_TopicSectionMovedLeft(object sender, TopicSectionEventArgs e)
        {
            this.IsModified = true;
            ControlGraphics.SuspendDrawing(this);
            tabManager.OrderTabs(topicDocument.TopicSections);
            tabManager.DisplayTab(e.TopicSection.Id, true);
            ControlGraphics.ResumeDrawing(this);
        }

        private void topicDocument_TopicSectionRemoved(object sender, TopicSectionEventArgs e)
        {
            ControlGraphics.SuspendDrawing(this);
            tabManager.RemoveTab(e.TopicSection.Id);
            tabManager.OrderTabs(topicDocument.TopicSections);
            ControlGraphics.ResumeDrawing(this);
        }

        private void topicDocument_TopicSectionAdded(object sender, TopicSectionEventArgs e)
        {
            ControlGraphics.SuspendDrawing(this);

            AddTopicSection(e.TopicSection, true);

            tabManager.OrderTabs(topicDocument.TopicSections);
            tabManager.DisplayTab(e.TopicSection.Id, true);
            ControlGraphics.ResumeDrawing(this);
        }

        #endregion

        private void AddTopicSection(ITopicSection topicSection, bool selected)
        {
            tabManager.AddTab(topicSection,
                DocumentControlFactory.Create(topicSection, this.topicDocument, this.application, codeItemCtrl_Modified),
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

        private void btnAddCode_Click(object sender, EventArgs e)
        {
            ICodeTopicSection templateFile = topicDocument.AddTopicSection(TopicSectionType.Code, ConfigSettings.DefaultSyntax, "txt") as ICodeTopicSection;
            this.IsModified = true;
        }

        private void btnAddVersionedCode_Click(object sender, EventArgs e)
        {
            IVersionedCodeTopicSection versionedCodeTopicSection = topicDocument.AddTopicSection(TopicSectionType.VersionedCode, ConfigSettings.DefaultSyntax, "txt") as IVersionedCodeTopicSection;
            this.IsModified = true;
        }

        private void btnAddHyperlinks_Click(object sender, EventArgs e)
        {
            IWebReferencesTopicSection webReferencesTopicSection = topicDocument.AddTopicSection(TopicSectionType.WebReferences) as IWebReferencesTopicSection;
            this.IsModified = true;
        }

        private void btnAddPdfDocument_Click(object sender, EventArgs e)
        {
            IPdfViewerTopicSection pdfViewerTopicSection = topicDocument.AddTopicSection(TopicSectionType.PdfViewer) as IPdfViewerTopicSection;
            this.IsModified = true;
        }

        private void btnImageSet_Click(object sender, EventArgs e)
        {
            IImagePagerTopicSection imagePagerTopicSection = topicDocument.AddTopicSection(TopicSectionType.ImagePager) as IImagePagerTopicSection;
            this.IsModified = true;
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            ISingleImageTopicSection singleImageTopicSection = topicDocument.AddTopicSection(TopicSectionType.SingleImage, null, "png") as ISingleImageTopicSection;
            this.IsModified = true;
        }

        private void btnRichText_Click(object sender, EventArgs e)
        {
            IRichTextEditorTopicSection richTextEditorTopicSection = topicDocument.AddTopicSection(TopicSectionType.RtfEditor, null, "rtf") as IRichTextEditorTopicSection;
            this.IsModified = true;
        }

        private void btnFileGroup_Click(object sender, EventArgs e)
        {
            IFileAttachmentsTopicSection fileAttachmentsTopicSection = topicDocument.AddTopicSection(TopicSectionType.FileAttachments, "File Attachments", null, null) as IFileAttachmentsTopicSection;
            this.IsModified = true;
        }

        private void btnRemoveCodeItem_Click(object sender, EventArgs e)
        {
            if (!tabManager.HasTabs)
                return;

            DialogResult dialogResult = Dialogs.DeleteItemDialog(this, "template");

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
            ControlGraphics.SuspendDrawing(this);
            this.chkEdit.Checked = base.HeaderFieldsVisible;
            this.toolstripKeywords.Visible = base.HeaderFieldsVisible;
            this.toolstripTitle.Visible = base.HeaderFieldsVisible;
            ControlGraphics.ResumeDrawing(this);
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
            base.persistableTarget.Delete();
        }

        #endregion


    }
}
