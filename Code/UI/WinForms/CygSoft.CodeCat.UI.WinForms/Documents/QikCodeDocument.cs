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
        private AppFacade application;

        public QikCodeDocument(QikFile qikFile, AppFacade application, bool isNew = false)
        {
            InitializeComponent();

            this.application = application;
            base.persistableTarget = qikFile;
            this.Tag = qikFile.Id;

            BuildTabs();
            
            InitializeImages();

            EnableControls();
            ResetFields();

            // event registration after all properties are set...
            base.Deleting += QikCodeDocument_Deleting;
            base.Saving += QikCodeDocument_Saving;
            base.Reverting += QikCodeDocument_Reverting;
            base.HeaderFieldsVisibilityChanged += QikCodeDocument_HeaderFieldsVisibilityChanged;
            base.ModifyStatusChanged += QikCodeDocument_ModifyStatusChanged;
            base.NewStatusChanged += QikCodeDocument_NewStatusChanged;
            RegisterEvents();

            // finally set the state of the document
            base.IsNew = isNew;
            base.IsModified = false;
        }

        private void QikCodeDocument_Reverting(object sender, EventArgs e)
        {
            ResetFields();
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
            return base.Save(base.persistableTarget, this);
        }

        private void QikCodeDocument_Deleting(object sender, EventArgs e)
        {
            base.persistableTarget.Delete();
        }

        private void SetModified(object sender, EventArgs e)
        {
            this.IsModified = true;
        }

        public string Keywords
        {
            get
            {
                if (base.persistableTarget != null)
                    return base.persistableTarget.CommaDelimitedKeywords;
                return null;
            }
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Delete();
        }

        public void AddKeywords(string keywords, bool flagModified = true)
        {
            // in fact, it seems that "qikFile" has already been updated because we have a reference to it in memory, but
            // this is just a "defensive programming" approach.
            if (flagModified)
                txtKeywords.Text = this.application.AddKeywordsToDelimitedText(base.persistableTarget.CommaDelimitedKeywords, keywords);
            else
            {
                txtKeywords.TextChanged -= SetModified;
                txtKeywords.Text = this.application.AddKeywordsToDelimitedText(base.persistableTarget.CommaDelimitedKeywords, keywords);
                txtKeywords.TextChanged += SetModified;
            }
        }

        public void RemoveKeywords(string keywords, bool flagModified = true)
        {
            // in fact, it seems that "qikFile" has already been updated because we have a reference to it in memory, but
            // this is just a "defensive programming" approach.
            if (flagModified)
                txtKeywords.Text = this.application.RemoveKeywordsFromDelimitedText(base.persistableTarget.CommaDelimitedKeywords, keywords);

            else
            {
                txtKeywords.TextChanged -= SetModified;
                txtKeywords.Text = this.application.RemoveKeywordsFromDelimitedText(base.persistableTarget.CommaDelimitedKeywords, keywords);
                txtKeywords.TextChanged += SetModified;
            }
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

            this.chkEdit.Click += (s, e) => { base.HeaderFieldsVisible = chkEdit.Checked; };

            //base.persistableTarget.SnapshotTaken += (s, e) => { UpdateSnapshotsTab(); };
            //base.persistableTarget.SnapshotDeleted += (s, e) => { UpdateSnapshotsTab(); };

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
            this.Text = base.persistableTarget.Title;
            this.txtKeywords.Text = base.persistableTarget.CommaDelimitedKeywords;
            this.txtTitle.Text = base.persistableTarget.Title;
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
            QikFile qikFile = base.persistableTarget as QikFile;

            qikFile.Title = this.txtTitle.Text.Trim();
            qikFile.CommaDelimitedKeywords = this.txtKeywords.Text.Trim();
            qikFile.Syntax = string.Empty;
            //qikFile.Text = syntaxBox.Document.Text;

            foreach (TabPage tabPage in tabControlFile.TabPages)
            {
                QikTemplateCodeCtrl templateControl = tabPage.Controls[0] as QikTemplateCodeCtrl;
                qikFile.SetTemplateTitle(tabPage.Name, templateControl.Title);
                qikFile.SetTemplateText(tabPage.Name, templateControl.TemplateText);
            }

            qikFile.Save();
            this.Text = qikFile.Title;
            this.txtKeywords.Text = qikFile.CommaDelimitedKeywords;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.persistableTarget.Close();
            base.OnFormClosed(e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveChanges();
        }

        private void btnDiscardChange_Click(object sender, EventArgs e)
        {
            base.RevertChanges();
        }

        private void btnAddTemplate_Click(object sender, EventArgs e)
        {
            QikFile qikFile = base.persistableTarget as QikFile;

            string fileId = qikFile.AddTemplate();
            string title = qikFile.GetTemplateTitle(fileId);
            string code = qikFile.GetTemplateText(fileId);

            TabPage tabPage = NewTab(fileId, title, code);
            tabControlFile.TabPages.Add(tabPage);
            //tabControlFile.SelectedIndex = tabControlFile.tab
            //tabPage.Select();
            tabControlFile.SelectedTab = tabPage;
        }

        private void btnRemoveTemplate_Click(object sender, EventArgs e)
        {
            QikFile qikFile = base.persistableTarget as QikFile;

            string fileTitle = tabControlFile.SelectedTab.Name;
            qikFile.RemoveTemplate(fileTitle);
            tabControlFile.TabPages.Remove(tabControlFile.SelectedTab);
        }

        private void BuildTabs()
        {
            QikFile qikFile = base.persistableTarget as QikFile;
            tabControlFile.TabPages.Clear();

            foreach (string fileId in qikFile.Templates)
            {
                string title = qikFile.GetTemplateTitle(fileId);
                string code = qikFile.GetTemplateText(fileId);
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
