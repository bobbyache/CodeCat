using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Qik;
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
        private ICompiler compiler = null;
        private QikFile qikFile = null;
        private TabPage qikScriptTab = null;

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

        public QikCodeDocument(QikFile qikFile, AppFacade application, bool isNew = false)
        {
            InitializeComponent();

            this.tabControlFile.ImageList = IconRepository.ImageList;
            base.application = application;
            this.qikFile = qikFile;
            base.persistableTarget = qikFile;
            this.Tag = qikFile.Id;
            this.compiler = qikFile.Compiler;

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
            this.Icon = IconRepository.QikIcon;
        }

        private void RegisterEvents()
        {
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

        private void RebuildTabs()
        {
            // Completely removes all tabs
            ClearAllTabs();

            // Re-creates from source...
            foreach (ICodeDocument document in qikFile.Documents)
            {
                if (document is IQikScriptDocument)
                    DisplayScriptTab(document as IQikScriptDocument, btnShowScript.Checked);
                else
                    NewCodeTemplateTab(document);
            }
        }

        private void ClearAllTabs()
        {
            foreach (TabPage tabPage in tabControlFile.TabPages)
            {
                if (tabPage == this.qikScriptTab)
                {
                    QikScriptCtrl scriptCtrl = qikScriptTab.Controls[0] as QikScriptCtrl;
                    scriptCtrl.Modified -= scriptCtrl_Modified;
                }
                else
                {
                    QikTemplateCodeCtrl codeCtrl = tabPage.Controls[0] as QikTemplateCodeCtrl;
                    codeCtrl.Modified -= codeCtrl_Modified;
                }
            }
            tabControlFile.TabPages.Clear();
        }

        private TabPage NewCodeTemplateTab(ICodeDocument templateFile)
        {
            TabPage tabPage = new TabPage(templateFile.Title);
            tabPage.Name = templateFile.Id;
            tabPage.ImageIndex = IconRepository.ImageKeyFor(templateFile.Syntax);

            QikTemplateCodeCtrl codeCtrl = new QikTemplateCodeCtrl(application, qikFile, templateFile, tabPage);
            codeCtrl.Modified += codeCtrl_Modified;
            tabPage.Controls.Add(codeCtrl);
            codeCtrl.Dock = DockStyle.Fill;

            tabControlFile.TabPages.Add(tabPage);

            return tabPage;
        }

        private void DisplayScriptTab(IQikScriptDocument scriptDocument, bool visible)
        {
            if (qikScriptTab == null)
            {
                TabPage tabPage = new TabPage(scriptDocument.Title);
                tabPage.Name = scriptDocument.Id;
                tabPage.ImageIndex = IconRepository.ImageKeyFor(IconRepository.QikKey);
                this.qikScriptTab = tabPage;

                QikScriptCtrl scriptCtrl = new QikScriptCtrl(application, qikFile, tabPage);
                scriptCtrl.Modified += scriptCtrl_Modified;
                tabPage.Controls.Add(scriptCtrl);
                scriptCtrl.Dock = DockStyle.Fill;
            }

            if (visible)
            {
                tabControlFile.TabPages.Add(this.qikScriptTab);
                tabControlFile.SelectedTab = this.qikScriptTab;
            }
            else
            {
                if (tabControlFile.TabPages.Contains(this.qikScriptTab))
                    tabControlFile.TabPages.Remove(this.qikScriptTab);
            }
        }

        private void Compile()
        {
            try
            {
                QikScriptCtrl scriptCtrl = qikScriptTab.Controls[0] as QikScriptCtrl;

                if (!(string.IsNullOrEmpty(scriptCtrl.ScriptText)))
                    this.compiler.Compile(scriptCtrl.ScriptText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("A compilation error has occurred\n: {0}", ex.Message));
            }
        }

        #endregion

        #region QikFile Events

        private void qikFile_ContentReverted(object sender, EventArgs e)
        {
            ResetFields();
            RebuildTabs();
        }


        private void qikFile_ContentSaved(object sender, EventArgs e)
        {
            ResetFields();
        }

        private void qikFile_BeforeContentSaved(object sender, EventArgs e)
        {
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
            ICodeDocument templateFile = qikFile.AddTemplate(ConfigSettings.DefaultSyntax);

            TabPage tabPage = NewCodeTemplateTab(templateFile);

            // ensures that the script tab always remains at the end.
            DisplayScriptTab(qikFile.ScriptFile as IQikScriptDocument, false);
            DisplayScriptTab(qikFile.ScriptFile as IQikScriptDocument, true);
            tabControlFile.SelectedTab = tabPage;

            this.IsModified = true;
        }

        private void btnRemoveTemplate_Click(object sender, EventArgs e)
        {
            if (tabControlFile.TabPages.Count == 0)
                return;

            if (tabControlFile.SelectedTab.Name == qikFile.ScriptFile.Id)
            {
                Dialogs.CannotRemoveTemplateScriptNotification(this);
                return;
            }

            DialogResult dialogResult = Dialogs.RemoveQikTemplateDialogPrompt(this);

            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string id = tabControlFile.SelectedTab.Name;
                qikFile.RemoveTemplate(id);

                // hide the template, don't remove it, might need to revert back to it...
                TabPage tabPage = tabControlFile.SelectedTab;
                QikTemplateCodeCtrl codeCtrl = tabPage.Controls[0] as QikTemplateCodeCtrl;

                codeCtrl.Modified -= codeCtrl_Modified;
                tabControlFile.TabPages.Remove(tabPage);
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
            DisplayScriptTab(this.qikFile.ScriptFile as IQikScriptDocument, btnShowScript.Checked);
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
            Compile();
        }

        #endregion

        #region Document Events

        private void QikCodeDocument_Reverting(object sender, EventArgs e)
        {
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
    }
}
