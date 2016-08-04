using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using CygSoft.CodeCat.UI.WinForms.Controls;
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
        private QikFile qikFile;
        private AppFacade application;

        public QikCodeDocument(QikFile qikFile, AppFacade application, bool isNew = false)
        {
            InitializeComponent();

            this.application = application;
            this.qikFile = qikFile;
            this.Tag = qikFile.Id;

            BuildTabs();
            
            InitializeImages();

            EnableControls();
            ResetFields();

            // event registration after all properties are set...
            base.Deleting += QikCodeDocument_Deleting;
            base.Saving += QikCodeDocument_Saving;
            base.HeaderFieldsVisibilityChanged += QikCodeDocument_HeaderFieldsVisibilityChanged;
            base.ModifyStatusChanged += QikCodeDocument_ModifyStatusChanged;
            base.NewStatusChanged += QikCodeDocument_NewStatusChanged;
            RegisterEvents();

            // finally set the state of the document
            base.IsNew = isNew;
            base.IsModified = false;
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

        public bool SaveChanges()
        {
            return base.Save(this.qikFile, this);
        }

        private void QikCodeDocument_Deleting(object sender, EventArgs e)
        {
            this.qikFile.Delete();
        }

        private void SetModified(object sender, EventArgs e)
        {
            this.IsModified = true;
        }

        public string SnippetId
        {
            get
            {
                if (this.qikFile != null)
                    return this.qikFile.Id;
                return null;
            }
        }

        public IKeywordIndexItem KeywordIndex
        {
            get
            {
                if (this.qikFile != null)
                    return this.qikFile.IndexItem;
                return null;
            }
        }

        public string Keywords
        {
            get
            {
                if (this.qikFile != null)
                    return this.qikFile.CommaDelimitedKeywords;
                return null;
            }
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Delete();
        }

        public void FlagSilentClose()
        {
            base.CloseWithoutPrompts = true;
        }

        public void AddKeywords(string keywords, bool flagModified = true)
        {
            // in fact, it seems that "qikFile" has already been updated because we have a reference to it in memory, but
            // this is just a "defensive programming" approach.
            if (flagModified)
                txtKeywords.Text = this.application.AddKeywordsToDelimitedText(this.qikFile.CommaDelimitedKeywords, keywords);
            else
            {
                txtKeywords.TextChanged -= SetModified;
                txtKeywords.Text = this.application.AddKeywordsToDelimitedText(this.qikFile.CommaDelimitedKeywords, keywords);
                txtKeywords.TextChanged += SetModified;
            }
        }

        public void RemoveKeywords(string keywords, bool flagModified = true)
        {
            // in fact, it seems that "qikFile" has already been updated because we have a reference to it in memory, but
            // this is just a "defensive programming" approach.
            if (flagModified)
                txtKeywords.Text = this.application.RemoveKeywordsFromDelimitedText(this.qikFile.CommaDelimitedKeywords, keywords);

            else
            {
                txtKeywords.TextChanged -= SetModified;
                txtKeywords.Text = this.application.RemoveKeywordsFromDelimitedText(this.qikFile.CommaDelimitedKeywords, keywords);
                txtKeywords.TextChanged += SetModified;
            }
        }

        private void InitializeImages()
        {
            btnDelete.Image = Resources.GetImage(Constants.ImageKeys.DeleteSnippet);
            btnSave.Image = Resources.GetImage(Constants.ImageKeys.SaveSnippet);
            chkEdit.Image = Resources.GetImage(Constants.ImageKeys.EditSnippet);
            btnDiscardChange.Image = Resources.GetImage(Constants.ImageKeys.DiscardSnippetChanges);

            //this.Icon = IconRepository.GetIcon(this.qikFile.Syntax);
            this.Icon = null;
        }

        private void RegisterEvents()
        {

            this.chkEdit.Click += (s, e) => { base.HeaderFieldsVisible = chkEdit.Checked; };

            //this.qikFile.SnapshotTaken += (s, e) => { UpdateSnapshotsTab(); };
            //this.qikFile.SnapshotDeleted += (s, e) => { UpdateSnapshotsTab(); };

            txtTitle.TextChanged += SetModified;
            txtKeywords.TextChanged += SetModified;
            //syntaxBox.TextChanged += SetModified;
            btnDelete.Click += btnDelete_Click;
        }

        private void EnableControls()
        {
            //btnSave.Enabled = false;
            btnDiscardChange.Enabled = false;
            btnDelete.Enabled = !this.IsNew;
        }

        private void ResetFields()
        {
            this.Text = qikFile.Title;
            this.txtKeywords.Text = qikFile.CommaDelimitedKeywords;
            this.txtTitle.Text = qikFile.Title;
            //this.syntaxBox.Document.Text = qikFile.Text;
        }

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
            this.qikFile.Title = this.txtTitle.Text.Trim();
            this.qikFile.CommaDelimitedKeywords = this.txtKeywords.Text.Trim();
            this.qikFile.Syntax = string.Empty;
            //this.qikFile.Text = syntaxBox.Document.Text;

            foreach (TabPage tabPage in tabControlFile.TabPages)
            {
                QikTemplateCodeCtrl templateControl = tabPage.Controls[0] as QikTemplateCodeCtrl;
                this.qikFile.SetTemplateTitle(tabPage.Name, templateControl.Title);
                this.qikFile.SetTemplateText(tabPage.Name, templateControl.TemplateText);
            }

            this.qikFile.Save();
            this.Text = qikFile.Title;
            this.txtKeywords.Text = this.qikFile.CommaDelimitedKeywords;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            this.qikFile.Close();
            base.OnFormClosed(e);
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveChanges();
        }

        private void btnDiscardChange_Click(object sender, EventArgs e)
        {
            DialogResult result = Dialogs.DiscardSnippetChangesDialogPrompt(this);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                ResetFields();
                this.IsModified = false;
            }
        }

        private void btnAddTemplate_Click(object sender, EventArgs e)
        {
            string fileId = this.qikFile.AddTemplate();
            string title = this.qikFile.GetTemplateTitle(fileId);
            string code = this.qikFile.GetTemplateText(fileId);

            TabPage tabPage = NewTab(fileId, title, code);
            tabControlFile.TabPages.Add(tabPage);
            //tabControlFile.SelectedIndex = tabControlFile.tab
            //tabPage.Select();
            tabControlFile.SelectedTab = tabPage;
        }

        private void btnRemoveTemplate_Click(object sender, EventArgs e)
        {
            string fileTitle = tabControlFile.SelectedTab.Name;
            this.qikFile.RemoveTemplate(fileTitle);
            tabControlFile.TabPages.Remove(tabControlFile.SelectedTab);
        }

        private void BuildTabs()
        {
            tabControlFile.TabPages.Clear();

            foreach (string fileId in this.qikFile.Templates)
            {
                string title = this.qikFile.GetTemplateTitle(fileId);
                string code = this.qikFile.GetTemplateText(fileId);
                TabPage tabPage = NewTab(fileId, title, code);
                tabControlFile.TabPages.Add(tabPage);
            }
        }

        private TabPage NewTab(string id, string title, string code)
        {
            TabPage tabPage = new TabPage(title);
            tabPage.Name = id;
            QikTemplateCodeCtrl codeCtrl = new QikTemplateCodeCtrl();
            tabPage.Controls.Add(codeCtrl);
            codeCtrl.Dock = DockStyle.Fill;
            codeCtrl.Title = title;
            codeCtrl.TemplateText = code;

            return tabPage;
        }
    }
}
