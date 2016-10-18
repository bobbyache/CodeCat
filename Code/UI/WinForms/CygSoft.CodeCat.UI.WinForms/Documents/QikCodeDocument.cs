using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
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
    public partial class QikCodeDocument : BaseDocument, IContentDocument
    {
        private ICompiler compiler = null;
        private IQikDocumentGroup qikFile = null;
        private DocumentTabManager tabManager = null;
        private QikScriptCtrl scriptControl;

        #region Public Properties

        public bool ShowScript
        {
            get { return btnShowScript.Checked; }
            set { btnShowScript.Checked = value; }
        }

        public bool ShowProperties
        {
            get { return btnShowProperties.Checked; }
            set { btnShowProperties.Checked = value; }
        }

        #endregion

        #region Constructors

        public QikCodeDocument(IQikDocumentGroup qikFile, AppFacade application, bool isNew = false)
        {
            InitializeComponent();

            this.tabControlFile.ImageList = IconRepository.ImageList;
            base.application = application;
            this.qikFile = qikFile;
            this.qikFile.DocumentAdded += qikFile_DocumentAdded;
            this.qikFile.DocumentRemoved += qikFile_DocumentRemoved;
            this.qikFile.DocumentMovedLeft += qikFile_DocumentMovedLeft;
            this.qikFile.DocumentMovedRight += qikFile_DocumentMovedRight;
            this.scriptControl = new QikScriptCtrl(application, qikFile);
            this.scriptControl.Modified += scriptCtrl_Modified;
            base.persistableTarget = qikFile;
            this.Tag = qikFile.Id;
            this.compiler = qikFile.Compiler;
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

            btnShowProperties.Checked = true;
            btnShowScript.Checked = base.IsNew;

            inputPropertyGrid.Reset(this.compiler);
            Compile();
        }

        #endregion

        #region Public Methods

        public bool SaveChanges()
        {
            return base.Save(base.persistableTarget, this);
        }

        public void AddKeywords(string keywords, bool flagModified = true)
        {
            // in fact, it seems that "qikFile" has already been updated because we have a reference to it in memory, but
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
            // in fact, it seems that "qikFile" has already been updated because we have a reference to it in memory, but
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
            qikFile.Save();
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
            btnAddTemplate.Image = Resources.GetImage(Constants.ImageKeys.AddTemplate);
            btnRemoveTemplate.Image = Resources.GetImage(Constants.ImageKeys.RemoveTemplate);
            btnShowProperties.Image = Resources.GetImage(Constants.ImageKeys.ShowProperties);
            btnCompile.Image = Resources.GetImage(Constants.ImageKeys.Compile);
            btnShowScript.Image = Resources.GetImage(Constants.ImageKeys.TemplateScript);
            btnMoveLeft.Image = Resources.GetImage(Constants.ImageKeys.MoveLeft);
            btnMoveRight.Image = Resources.GetImage(Constants.ImageKeys.MoveRight);
            btnMenu.Image = Resources.GetImage(Constants.ImageKeys.GroupMenu);
            this.Icon = IconRepository.QikIcon;
        }

        private void RegisterEvents()
        {
            qikFile.AfterRevert += qikFile_ContentReverted;
            qikFile.BeforeSave += qikFile_BeforeContentSaved;
            qikFile.AfterSave += qikFile_ContentSaved;

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
            foreach (ICodeDocument document in qikFile.TemplateFiles)
            {
                tabManager.AddTab(document, NewTemplateControl(document), true, false);
            }
            tabManager.AddTab(qikFile.ScriptFile, scriptControl, btnShowScript.Checked, false);
        }

        private QikTemplateCodeCtrl NewTemplateControl(IDocument document)
        {
            QikTemplateCodeCtrl templateControl = new QikTemplateCodeCtrl(this.application, this.qikFile, document as ICodeDocument);
            templateControl.Modified += codeCtrl_Modified;
            return templateControl;
        }

        private void Compile()
        {
            try
            {
                if (!(string.IsNullOrEmpty(scriptControl.ScriptText)))
                    this.compiler.Compile(scriptControl.ScriptText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("A compilation error has occurred\n: {0}", ex.Message));
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
                QikTemplateCodeCtrl codeCtrl = e.TabUserControl as QikTemplateCodeCtrl;
                codeCtrl.Modified -= codeCtrl_Modified;
            }
        }

        #endregion

        #region QikFile Events

        private void qikFile_ContentReverted(object sender, FileEventArgs e)
        {
            ControlGraphics.SuspendDrawing(this);
            ResetFields();
            RebuildTabs();
            ControlGraphics.ResumeDrawing(this);
        }

        private void qikFile_ContentSaved(object sender, FileEventArgs e)
        {
            ResetFields();
        }

        private void qikFile_BeforeContentSaved(object sender, FileEventArgs e)
        {
            qikFile.Title = this.txtTitle.Text.Trim();
            qikFile.CommaDelimitedKeywords = this.txtKeywords.Text.Trim();
            qikFile.Syntax = string.Empty;
        }

        private void qikFile_DocumentMovedRight(object sender, DocumentEventArgs e)
        {
            this.IsModified = true;
            ControlGraphics.SuspendDrawing(this);
            tabManager.OrderTabs(qikFile.Documents);
            tabManager.DisplayTab(scriptControl.Id, btnShowScript.Checked);
            tabManager.DisplayTab(e.Document.Id, true);
            ControlGraphics.ResumeDrawing(this);
        }

        private void qikFile_DocumentMovedLeft(object sender, DocumentEventArgs e)
        {
            this.IsModified = true;
            ControlGraphics.SuspendDrawing(this);
            tabManager.OrderTabs(qikFile.Documents);
            tabManager.DisplayTab(scriptControl.Id, btnShowScript.Checked);
            tabManager.DisplayTab(e.Document.Id, true);
            ControlGraphics.ResumeDrawing(this);
        }

        private void qikFile_DocumentRemoved(object sender, DocumentEventArgs e)
        {
            ControlGraphics.SuspendDrawing(this);
            tabManager.RemoveTab(e.Document.Id);
            tabManager.OrderTabs(qikFile.Documents);
            tabManager.DisplayTab(scriptControl.Id, btnShowScript.Checked);
            ControlGraphics.ResumeDrawing(this);
        }

        private void qikFile_DocumentAdded(object sender, DocumentEventArgs e)
        {
            ControlGraphics.SuspendDrawing(this);
            tabManager.AddTab(e.Document, NewTemplateControl(e.Document), true, true);
            tabManager.OrderTabs(qikFile.Documents);
            tabManager.DisplayTab(scriptControl.Id, btnShowScript.Checked);
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

        private void btnAddTemplate_Click(object sender, EventArgs e)
        {
            ICodeDocument templateFile = qikFile.AddTemplate(ConfigSettings.DefaultSyntax);
            this.IsModified = true;
        }

        private void btnRemoveTemplate_Click(object sender, EventArgs e)
        {
            if (!tabManager.HasTabs)
                return;

            if (tabManager.SelectedTabId == qikFile.ScriptFile.Id)
            {
                Dialogs.CannotRemoveTemplateScriptNotification(this);
                return;
            }

            DialogResult dialogResult = Dialogs.RemoveQikTemplateDialogPrompt(this);

            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string id = tabManager.SelectedTabId;
                qikFile.RemoveTemplate(id);
                this.IsModified = true;
            }
        }

        private void codeCtrl_Modified(object sender, EventArgs e)
        {
            this.IsModified = true;
        }

        private void scriptCtrl_Modified(object sender, EventArgs e)
        {
            this.IsModified = true;
        }

        private void btnShowProperties_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !btnShowProperties.Checked;
        }

        private void btnShowScript_CheckedChanged(object sender, EventArgs e)
        {
            tabManager.DisplayTab(scriptControl.Id, btnShowScript.Checked);
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
            Compile();
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            this.qikFile.MoveDocumentLeft(tabManager.SelectedTabId);
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            this.qikFile.MoveDocumentRight(tabManager.SelectedTabId);
        }

        #endregion

        #region Document Events

        private void QikCodeDocument_Reverting(object sender, EventArgs e)
        {
            qikFile.Revert();
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
