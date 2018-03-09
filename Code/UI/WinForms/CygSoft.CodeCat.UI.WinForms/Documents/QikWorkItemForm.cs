using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.UI.WinForms.Controls;
using CygSoft.CodeCat.UI.WinForms.Documents;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class QikWorkItemForm : BaseWorkItemForm, IWorkItemForm
    {
        private ICompiler compiler = null;
        private IQikTemplateDocumentSet qikFile = null;
        private WorkItemTabManager tabManager = null;
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

        public QikWorkItemForm(IWorkItem workItem, AppFacade application, bool isNew = false)
        {
            InitializeComponent();

            if (!(workItem is IQikTemplateDocumentSet))
                throw new ArgumentException("Target is not the incorrect type.");

            this.qikFile = workItem as IQikTemplateDocumentSet;
            base.application = application;
            base.workItem = qikFile;

            tabControlFile.ImageList = IconRepository.ImageList;

            qikFile.TopicSectionAdded += qikFile_TopicSectionAdded;
            qikFile.TopicSectionRemoved += qikFile_TopicSectionRemoved;
            qikFile.TopicSectionMovedLeft += qikFile_TopicSectionMovedLeft;
            qikFile.TopicSectionMovedRight += qikFile_TopicSectionMovedRight;

            
            Tag = qikFile.Id;
            compiler = qikFile.Compiler;
            tabManager = new WorkItemTabManager(tabControlFile, btnMenu);
            tabManager.BeforeDeleteTab += tabManager_BeforeDeleteTab;
  
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

            inputPropertyGrid.Reset(compiler);
            Compile();
        }

        #endregion

        #region Public Methods

        public bool SaveChanges()
        {
            return base.Save(base.workItem, this);
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
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                Gui.Dialogs.MissingRequiredFieldMessageBox(this, "Title");
                base.HeaderFieldsVisible = true;
                txtTitle.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtKeywords.Text))
            {
                Gui.Dialogs.MissingRequiredFieldMessageBox(this, "Keywords");
                base.HeaderFieldsVisible = true;
                txtKeywords.Focus();
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
            IsModified = true;

        }
        private void InitializeImages()
        {
            btnDelete.Image = Gui.Resources.GetImage(Constants.ImageKeys.DeleteSnippet);
            btnSave.Image = Gui.Resources.GetImage(Constants.ImageKeys.SaveSnippet);
            chkEdit.Image = Gui.Resources.GetImage(Constants.ImageKeys.EditSnippet);
            btnDiscardChange.Image = Gui.Resources.GetImage(Constants.ImageKeys.DiscardSnippetChanges);
            btnAddTemplate.Image = Gui.Resources.GetImage(Constants.ImageKeys.AddTemplate);
            btnRemoveTemplate.Image = Gui.Resources.GetImage(Constants.ImageKeys.RemoveTemplate);
            btnShowProperties.Image = Gui.Resources.GetImage(Constants.ImageKeys.ShowProperties);
            btnCompile.Image = Gui.Resources.GetImage(Constants.ImageKeys.Compile);
            btnShowScript.Image = Gui.Resources.GetImage(Constants.ImageKeys.TemplateScript);
            btnMoveLeft.Image = Gui.Resources.GetImage(Constants.ImageKeys.MoveLeft);
            btnMoveRight.Image = Gui.Resources.GetImage(Constants.ImageKeys.MoveRight);
            btnMenu.Image = Gui.Resources.GetImage(Constants.ImageKeys.GroupMenu);
            Icon = IconRepository.QikGroupIcon;
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
            chkEdit.Click += (s, e) => { base.HeaderFieldsVisible = chkEdit.Checked; };
            
            txtTitle.TextChanged += SetModified;
            txtKeywords.TextChanged += SetModified;
            btnDelete.Click += btnDelete_Click;
            btnDiscardChange.Click += btnDiscardChange_Click;
        }

        private void InitializeControls()
        {
            btnSave.Enabled = false;
            btnDiscardChange.Enabled = false;
            btnDelete.Enabled = !IsNew;
        }

        private void ResetFields()
        {
            txtToolStripTitle.Text = base.workItem.Title;
            Text = base.workItem.Title;
            txtKeywords.Text = base.workItem.CommaDelimitedKeywords;
            txtTitle.Text = base.workItem.Title;

            base.IsModified = false;
        }

        private void RebuildTabs()
        {
            tabManager.Clear();
            foreach (ICodeTopicSection document in qikFile.TemplateSections)
            {
                tabManager.AddTab(document,
                    TopicSectionControlFactory.Create(document, qikFile, application, codeCtrl_Modified), true, false);
            }

            IQikScriptTopicSection qikScriptTopicSection = qikFile.ScriptSection as IQikScriptTopicSection;
            scriptControl = (QikScriptCtrl)TopicSectionControlFactory.Create(qikScriptTopicSection, qikFile, application, codeCtrl_Modified);
            tabManager.AddTab(qikScriptTopicSection, scriptControl, btnShowScript.Checked, false);
        }

        private void Compile()
        {
            try
            {
                if (!(string.IsNullOrEmpty(scriptControl.ScriptText)))
                    compiler.Compile(scriptControl.ScriptText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("A compilation error has occurred\n: {0}", ex.Message));
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
                QikTemplateCodeCtrl codeCtrl = e.TabUserControl as QikTemplateCodeCtrl;
                codeCtrl.Modified -= codeCtrl_Modified;
            }
        }

        #endregion

        #region QikFile Events

        private void qikFile_ContentReverted(object sender, FileEventArgs e)
        {
            Gui.Drawing.SuspendDrawing(this);
            ResetFields();
            RebuildTabs();
            Compile();
            Gui.Drawing.ResumeDrawing(this);
        }

        private void qikFile_ContentSaved(object sender, FileEventArgs e)
        {
            ResetFields();
        }

        private void qikFile_BeforeContentSaved(object sender, FileEventArgs e)
        {
            qikFile.Title = txtTitle.Text.Trim();
            qikFile.CommaDelimitedKeywords = txtKeywords.Text.Trim();
            qikFile.Syntax = string.Empty;
        }

        private void qikFile_TopicSectionMovedRight(object sender, TopicSectionEventArgs e)
        {
            IsModified = true;
            Gui.Drawing.SuspendDrawing(this);
            tabManager.OrderTabs(qikFile.TopicSections);
            tabManager.DisplayTab(scriptControl.Id, btnShowScript.Checked);
            tabManager.DisplayTab(e.TopicSection.Id, true);
            Gui.Drawing.ResumeDrawing(this);
        }

        private void qikFile_TopicSectionMovedLeft(object sender, TopicSectionEventArgs e)
        {
            IsModified = true;
            Gui.Drawing.SuspendDrawing(this);
            tabManager.OrderTabs(qikFile.TopicSections);
            tabManager.DisplayTab(scriptControl.Id, btnShowScript.Checked);
            tabManager.DisplayTab(e.TopicSection.Id, true);
            Gui.Drawing.ResumeDrawing(this);
        }

        private void qikFile_TopicSectionRemoved(object sender, TopicSectionEventArgs e)
        {
            Gui.Drawing.SuspendDrawing(this);
            tabManager.RemoveTab(e.TopicSection.Id);
            tabManager.OrderTabs(qikFile.TopicSections);
            tabManager.DisplayTab(scriptControl.Id, btnShowScript.Checked);
            Gui.Drawing.ResumeDrawing(this);
        }

        private void qikFile_TopicSectionAdded(object sender, TopicSectionEventArgs e)
        {
            Gui.Drawing.SuspendDrawing(this);
            tabManager.AddTab(e.TopicSection,
                TopicSectionControlFactory.Create(e.TopicSection, qikFile, application, codeCtrl_Modified),
                true, true);
            tabManager.OrderTabs(qikFile.TopicSections);
            tabManager.DisplayTab(scriptControl.Id, btnShowScript.Checked);
            tabManager.DisplayTab(e.TopicSection.Id, true);
            Gui.Drawing.ResumeDrawing(this);
        }

        #endregion

        #region Document Control Events

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void btnDiscardChange_Click(object sender, EventArgs e)
        {
            base.RevertChanges();
        }

        private void btnAddTemplate_Click(object sender, EventArgs e)
        {
            ICodeTopicSection templateFile = qikFile.AddTemplateSection(ConfigSettings.DefaultSyntax);
            IsModified = true;
        }

        private void btnRemoveTemplate_Click(object sender, EventArgs e)
        {
            if (!tabManager.HasTabs)
                return;

            if (tabManager.SelectedTabId == qikFile.ScriptSection.Id)
            {
                Gui.Dialogs.CannotRemoveTemplateScriptMessageBox(this);
                return;
            }

            DialogResult dialogResult = Gui.Dialogs.DeleteItemMessageBox(this, "template");

            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string id = tabManager.SelectedTabId;
                qikFile.RemoveTemplateSection(id);
                IsModified = true;
            }
        }

        private void codeCtrl_Modified(object sender, EventArgs e)
        {
            IsModified = true;
        }

        private void scriptCtrl_Modified(object sender, EventArgs e)
        {
            IsModified = true;
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
            qikFile.MoveTemplateSectionLeft(tabManager.SelectedTabId);
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            qikFile.MoveTemplateSectionRight(tabManager.SelectedTabId);
        }

        #endregion

        #region Document Events

        private void QikCodeDocument_Reverting(object sender, EventArgs e)
        {
            qikFile.Revert();
        }

        private void QikCodeDocument_HeaderFieldsVisibilityChanged(object sender, EventArgs e)
        {
            Gui.Drawing.SuspendDrawing(this);
            chkEdit.Checked = base.HeaderFieldsVisible;
            toolstripKeywords.Visible = base.HeaderFieldsVisible;
            toolstripTitle.Visible = base.HeaderFieldsVisible;
            Gui.Drawing.ResumeDrawing(this);
        }

        private void QikCodeDocument_NewStatusChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = !base.IsNew;
        }

        private void QikCodeDocument_ModifyStatusChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = base.IsModified;
            btnDiscardChange.Enabled = base.IsModified && !base.IsNew;
        }

        private void QikCodeDocument_Saving(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void QikCodeDocument_Deleting(object sender, EventArgs e)
        {
            base.workItem.Delete();
        }

        #endregion
    }
}
