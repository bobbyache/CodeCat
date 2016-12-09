using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using CygSoft.CodeCat.UI.WinForms.Controls;
using CygSoft.CodeCat.UI.WinForms.Documents;
using CygSoft.Qik.LanguageEngine.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class CodeGroupDocument : BaseDocument, IContentDocument
    {
        private ICodeGroupDocumentSet codeItemFile = null;
        private DocumentTabManager tabManager = null;

        #region Constructors

        public CodeGroupDocument(ICodeGroupDocumentSet codeItemFile, AppFacade application, bool isNew = false)
        {
            InitializeComponent();

            this.tabControlFile.ImageList = IconRepository.ImageList;
            base.application = application;
            this.codeItemFile = codeItemFile;
            this.codeItemFile.DocumentAdded += codeItemFile_DocumentAdded;
            this.codeItemFile.DocumentRemoved += codeItemFile_DocumentRemoved;
            this.codeItemFile.DocumentMovedLeft += codeItemFile_DocumentMovedLeft;
            this.codeItemFile.DocumentMovedRight += codeItemFile_DocumentMovedRight;
            base.persistableTarget = codeItemFile;
            this.Tag = codeItemFile.Id;
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
            codeItemFile.Save();
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
            this.Icon = IconRepository.CodeGroupIcon;
        }

        private void RegisterEvents()
        {            
            codeItemFile.AfterRevert += codeItemFile_ContentReverted;
            codeItemFile.BeforeSave += codeItemFile_BeforeContentSaved;
            codeItemFile.AfterSave += codeItemFile_ContentSaved;

            base.Deleting += CodeGroupDocumentDeleting;
            base.Saving += CodeGroupDocumentSaving;
            base.Reverting += CodeGroupDocumentReverting;
            base.HeaderFieldsVisibilityChanged += CodeGroupDocumentHeaderFieldsVisibilityChanged;
            base.ModifyStatusChanged += CodeGroupDocumentModifyStatusChanged;

            base.NewStatusChanged += CodeGroupDocumentNewStatusChanged;
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
            foreach (IDocument document in codeItemFile.Documents)
            {
                AddDocument(document, false);
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

        private void codeItemFile_ContentReverted(object sender, FileEventArgs e)
        {
            ControlGraphics.SuspendDrawing(this);
            ResetFields();
            RebuildTabs();
            ControlGraphics.ResumeDrawing(this);
        }

        private void codeItemFile_ContentSaved(object sender, FileEventArgs e)
        {
            ResetFields();
        }

        private void codeItemFile_BeforeContentSaved(object sender, FileEventArgs e)
        {
            codeItemFile.Title = this.txtTitle.Text.Trim();
            codeItemFile.CommaDelimitedKeywords = this.txtKeywords.Text.Trim();
            codeItemFile.Syntax = string.Empty;
        }

        private void codeItemFile_DocumentMovedRight(object sender, DocumentEventArgs e)
        {
            this.IsModified = true;
            ControlGraphics.SuspendDrawing(this);
            tabManager.OrderTabs(codeItemFile.Documents);
            tabManager.DisplayTab(e.Document.Id, true);
            ControlGraphics.ResumeDrawing(this);
        }

        private void codeItemFile_DocumentMovedLeft(object sender, DocumentEventArgs e)
        {
            this.IsModified = true;
            ControlGraphics.SuspendDrawing(this);
            tabManager.OrderTabs(codeItemFile.Documents);
            tabManager.DisplayTab(e.Document.Id, true);
            ControlGraphics.ResumeDrawing(this);
        }

        private void codeItemFile_DocumentRemoved(object sender, DocumentEventArgs e)
        {
            ControlGraphics.SuspendDrawing(this);
            tabManager.RemoveTab(e.Document.Id);
            tabManager.OrderTabs(codeItemFile.Documents);
            ControlGraphics.ResumeDrawing(this);
        }

        private void codeItemFile_DocumentAdded(object sender, DocumentEventArgs e)
        {
            ControlGraphics.SuspendDrawing(this);

            AddDocument(e.Document, true);

            tabManager.OrderTabs(codeItemFile.Documents);
            tabManager.DisplayTab(e.Document.Id, true);
            ControlGraphics.ResumeDrawing(this);
        }

        #endregion

        private void AddDocument(IDocument document, bool selected)
        {
            tabManager.AddTab(document,
                DocumentControlFactory.Create(document, this.codeItemFile, this.application, codeItemCtrl_Modified),
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
            ICodeDocument templateFile = codeItemFile.AddDocument(DocumentTypeEnum.CodeSnippet, ConfigSettings.DefaultSyntax, "txt") as ICodeDocument;
            this.IsModified = true;
        }

        private void btnAddHyperlinks_Click(object sender, EventArgs e)
        {
            IUrlGroupDocument urlFile = codeItemFile.AddDocument(DocumentTypeEnum.UrlGroup) as IUrlGroupDocument;
            this.IsModified = true;
        }

        private void btnAddPdfDocument_Click(object sender, EventArgs e)
        {
            IPdfDocument pdfDocument = codeItemFile.AddDocument(DocumentTypeEnum.PdfDocument) as IPdfDocument;
            this.IsModified = true;
        }

        private void btnImageSet_Click(object sender, EventArgs e)
        {
            IImageSetDocument imageSetDocument = codeItemFile.AddDocument(DocumentTypeEnum.ImageSet) as IImageSetDocument;
            this.IsModified = true;
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            IImageDocument imageDocument = codeItemFile.AddDocument(DocumentTypeEnum.ImageDocument, null, "png") as IImageDocument;
            this.IsModified = true;
        }

        private void btnRichText_Click(object sender, EventArgs e)
        {
            IRichTextDocument richTextDocument = codeItemFile.AddDocument(DocumentTypeEnum.RichTextDocument, null, "rtf") as IRichTextDocument;
            this.IsModified = true;
        }

        private void btnRemoveCodeItem_Click(object sender, EventArgs e)
        {
            if (!tabManager.HasTabs)
                return;

            DialogResult dialogResult = Dialogs.RemoveQikTemplateDialogPrompt(this);

            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string id = tabManager.SelectedTabId;
                codeItemFile.RemoveDocument(id);
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
            this.codeItemFile.MoveDocumentLeft(tabManager.SelectedTabId);
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            this.codeItemFile.MoveDocumentRight(tabManager.SelectedTabId);
        }

        #endregion

        #region Document Events

        private void CodeGroupDocumentReverting(object sender, EventArgs e)
        {
            codeItemFile.Revert();
        }

        private void CodeGroupDocumentHeaderFieldsVisibilityChanged(object sender, EventArgs e)
        {
            ControlGraphics.SuspendDrawing(this);
            this.chkEdit.Checked = base.HeaderFieldsVisible;
            this.toolstripKeywords.Visible = base.HeaderFieldsVisible;
            this.toolstripTitle.Visible = base.HeaderFieldsVisible;
            ControlGraphics.ResumeDrawing(this);
        }

        private void CodeGroupDocumentNewStatusChanged(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = !base.IsNew;
        }

        private void CodeGroupDocumentModifyStatusChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = base.IsModified;
            btnDiscardChange.Enabled = base.IsModified && !base.IsNew;
        }

        private void CodeGroupDocumentSaving(object sender, EventArgs e)
        {
            this.SaveChanges();
        }

        private void CodeGroupDocumentDeleting(object sender, EventArgs e)
        {
            base.persistableTarget.Delete();
        }

        #endregion

    }
}
