using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
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
        private ICodeGroupDocumentGroup codeItemFile = null;
        private DocumentTabManager tabManager = null;

        #region Constructors

        public CodeGroupDocument(ICodeGroupDocumentGroup codeItemFile, AppFacade application, bool isNew = false)
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
            btnAddCodeItem.Image = Resources.GetImage(Constants.ImageKeys.AddTemplate);
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

            base.Deleting += QikCodeDocument_Deleting;
            base.Saving += QikCodeDocument_Saving;
            base.Reverting += QikCodeDocument_Reverting;
            base.HeaderFieldsVisibilityChanged += QikCodeDocument_HeaderFieldsVisibilityChanged;
            base.ModifyStatusChanged += QikCodeDocument_ModifyStatusChanged;

            base.NewStatusChanged += QikCodeDocument_NewStatusChanged;
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
            foreach (ICodeDocument document in codeItemFile.Documents)
            {
                tabManager.AddTab(document, NewCodeControl(document), true, false);
            }
        }

        private CodeItemCtrl NewCodeControl(IDocument document)
        {
            CodeItemCtrl templateControl = new CodeItemCtrl(this.application, this.codeItemFile, document as ICodeDocument);
            templateControl.Modified += codeItemCtrl_Modified;
            return templateControl;
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
            tabManager.AddTab(e.Document, NewCodeControl(e.Document), true, true);
            tabManager.OrderTabs(codeItemFile.Documents);
            tabManager.DisplayTab(e.Document.Id, true);
            ControlGraphics.ResumeDrawing(this);
        }

        #endregion

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

        private void btnAddCodeItem_Click(object sender, EventArgs e)
        {
            ICodeDocument templateFile = codeItemFile.AddDocument(ConfigSettings.DefaultSyntax);
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

        private void QikCodeDocument_Reverting(object sender, EventArgs e)
        {
            codeItemFile.Revert();
        }

        private void QikCodeDocument_HeaderFieldsVisibilityChanged(object sender, EventArgs e)
        {
            ControlGraphics.SuspendDrawing(this);
            this.chkEdit.Checked = base.HeaderFieldsVisible;
            this.toolstripKeywords.Visible = base.HeaderFieldsVisible;
            this.toolstripTitle.Visible = base.HeaderFieldsVisible;
            ControlGraphics.ResumeDrawing(this);
        }

        private void QikCodeDocument_NewStatusChanged(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = !base.IsNew;
        }

        private void QikCodeDocument_ModifyStatusChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = base.IsModified;
            btnDiscardChange.Enabled = base.IsModified && !base.IsNew;
        }

        private void QikCodeDocument_Saving(object sender, EventArgs e)
        {
            this.SaveChanges();
        }

        private void QikCodeDocument_Deleting(object sender, EventArgs e)
        {
            base.persistableTarget.Delete();
        }

        #endregion
    }
}
