using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Infrastructure.Qik;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using CygSoft.CodeCat.UI.WinForms.Controls;
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
        ICompiler compiler = null;

        #region Constructors

        public QikCodeDocument(QikFile qikFile, AppFacade application, bool isNew = false)
        {
            InitializeComponent();

            btnShowProperties.Checked = false;
            this.tabControlFile.ImageList = IconRepository.ImageList;
            
            base.application = application;
            base.persistableTarget = qikFile;
            this.Tag = qikFile.Id;
            this.compiler = qikFile.Compiler;

            BuildTabs();
            InitializeImages();
            InitializeControls();
            ResetFields();

            // event registration after all properties are set...
            RegisterEvents();

            // finally set the state of the document
            base.IsNew = isNew;

            QikScriptCtrl scriptCtrl = tabControlFile.TabPages["script"].Controls[0] as QikScriptCtrl;
            inputPropertyGrid.Reset(this.compiler);

            this.compiler.Compile(scriptCtrl.ScriptText);
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
            QikFile qikFile = base.persistableTarget as QikFile;
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
            //this.Icon = IconRepository.GetIcon(base.persistableTarget.Syntax);
            this.Icon = null;
        }

        private void RegisterEvents()
        {
            QikFile qikFile = base.persistableTarget as QikFile;
            qikFile.ContentReverted += qikFile_ContentReverted;
            qikFile.BeforeContentSaved += qikFile_BeforeContentSaved;
            qikFile.ContentSaved += qikFile_ContentSaved;

            base.Deleting += QikCodeDocument_Deleting;
            base.Saving += QikCodeDocument_Saving;
            base.Reverting += QikCodeDocument_Reverting;
            base.HeaderFieldsVisibilityChanged += QikCodeDocument_HeaderFieldsVisibilityChanged;
            base.ModifyStatusChanged += QikCodeDocument_ModifyStatusChanged;

            base.NewStatusChanged += QikCodeDocument_NewStatusChanged;
            this.chkEdit.Click += (s, e) => { base.HeaderFieldsVisible = chkEdit.Checked; };

            txtTitle.TextChanged += SetModified;
            txtKeywords.TextChanged += SetModified;
            //syntaxBox.TextChanged += SetModified;
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
            this.Text = base.persistableTarget.Title;
            this.txtKeywords.Text = base.persistableTarget.CommaDelimitedKeywords;
            this.txtTitle.Text = base.persistableTarget.Title;

            base.IsModified = false;
        }

        private void BuildTabs()
        {
            QikFile qikFile = base.persistableTarget as QikFile;
            tabControlFile.TabPages.Clear();

            NewScriptTab();

            foreach (ITemplateFile templateFile in qikFile.Templates)
            {
                NewCodeTemplateTab(templateFile);
            }
        }

        private TabPage NewCodeTemplateTab(ITemplateFile templateFile)
        {
            QikFile qikFile = base.persistableTarget as QikFile;
            TabPage tabPage = new TabPage(templateFile.Title);
            tabPage.Name = templateFile.FileName;
            tabPage.ImageIndex = IconRepository.ImageKeyFor(templateFile.Syntax);

            QikTemplateCodeCtrl codeCtrl = new QikTemplateCodeCtrl(application, qikFile, templateFile, tabPage);
            codeCtrl.Modified += codeCtrl_Modified;
            tabPage.Controls.Add(codeCtrl);
            codeCtrl.Dock = DockStyle.Fill;

            tabControlFile.TabPages.Add(tabPage);

            return tabPage;
        }

        private TabPage NewScriptTab()
        {
            
            QikFile qikFile = base.persistableTarget as QikFile;
            TabPage tabPage = new TabPage("Qik Script");
            tabPage.Name = "script";
            QikScriptCtrl scriptCtrl = new QikScriptCtrl(application, qikFile, tabPage);
            scriptCtrl.Modified += scriptCtrl_Modified;
            tabPage.Controls.Add(scriptCtrl);
            scriptCtrl.Dock = DockStyle.Fill;

            tabControlFile.TabPages.Add(tabPage);

            return tabPage;
        }

        #endregion

        #region QikFile Events

        private void qikFile_ContentReverted(object sender, EventArgs e)
        {
            ResetFields();
            BuildTabs();
        }


        private void qikFile_ContentSaved(object sender, EventArgs e)
        {
            ResetFields();
        }

        private void qikFile_BeforeContentSaved(object sender, EventArgs e)
        {
            QikFile qikFile = base.persistableTarget as QikFile;

            qikFile.Title = this.txtTitle.Text.Trim();
            qikFile.CommaDelimitedKeywords = this.txtKeywords.Text.Trim();
            qikFile.Syntax = string.Empty;
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
            QikFile qikFile = base.persistableTarget as QikFile;

            ITemplateFile templateFile = qikFile.AddTemplate();

            TabPage tabPage = NewCodeTemplateTab(templateFile);
            tabControlFile.SelectedTab = tabPage;
            this.IsModified = true;
        }

        private void btnRemoveTemplate_Click(object sender, EventArgs e)
        {
            if (tabControlFile.TabPages.Count == 0)
                return;

            if (tabControlFile.SelectedTab.Name == "script")
                return;

            QikFile qikFile = base.persistableTarget as QikFile;

            string fileTitle = tabControlFile.SelectedTab.Name;
            qikFile.RemoveTemplate(fileTitle);

            // hide the template, don't remove it, might need to revert back to it...
            TabPage tabPage = tabControlFile.SelectedTab;
            QikTemplateCodeCtrl codeCtrl = tabPage.Controls[0] as QikTemplateCodeCtrl;

            codeCtrl.Modified -= codeCtrl_Modified;
            tabControlFile.TabPages.Remove(tabPage);
            this.IsModified = true;
        }

        private void codeCtrl_Modified(object sender, EventArgs e)
        {
            this.IsModified = true;
        }

        private void scriptCtrl_Modified(object sender, EventArgs e)
        {
            this.IsModified = true;
        }

        #endregion

        #region Document Events

        private void QikCodeDocument_Reverting(object sender, EventArgs e)
        {
            QikFile qikFile = base.persistableTarget as QikFile;
            qikFile.Revert();
        }

        private void QikCodeDocument_HeaderFieldsVisibilityChanged(object sender, EventArgs e)
        {
            this.chkEdit.Checked = base.HeaderFieldsVisible;
            this.toolstripKeywords.Visible = base.HeaderFieldsVisible;
            this.toolstripTitle.Visible = base.HeaderFieldsVisible;
        }

        private void QikCodeDocument_NewStatusChanged(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = !base.IsNew;
        }

        private void QikCodeDocument_ModifyStatusChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = base.IsModified;
            btnDiscardChange.Enabled = base.IsModified;
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

        private void btnShowProperties_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !btnShowProperties.Checked;
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
            QikFile qikFile = base.persistableTarget as QikFile;
            QikScriptCtrl scriptCtrl = tabControlFile.TabPages["script"].Controls[0] as QikScriptCtrl;

            this.compiler.Compile(scriptCtrl.ScriptText);
        }
    }
}
