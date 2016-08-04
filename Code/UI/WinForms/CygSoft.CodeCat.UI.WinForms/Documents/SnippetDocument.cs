﻿using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
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
    public partial class SnippetDocument : BaseDocument, IContentDocument
    {
        private TabPage snapshotsTab;
        private CodeFile codeFile;
        private AppFacade application;

        public SnippetDocument(CodeFile codeFile, AppFacade application, bool isNew = false)
        {
            InitializeComponent();
            this.syntaxBox.AllowBreakPoints = false;
            
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            tabControl.Alignment = TabAlignment.Left;
            this.snapshotsTab = this.tabPageSnapshots;
            this.CloseButtonVisible = true;
            this.CloseButton = true;

            this.application = application;
            this.codeFile = codeFile;
            this.Tag = codeFile.Id;

            SetDefaultFont();
            
            InitializeImages();
            InitializeSyntaxList();

            EnableControls();
            ResetFields();
            UpdateSnapshotsTab();

            // event registration after all properties are set...
            // event registration after all properties are set...
            base.Deleting += SnippetDocument_Deleting;
            base.Saving += SnippetDocument_Saving;
            base.HeaderFieldsVisibilityChanged += SnippetDocument_HeaderFieldsVisibilityChanged;
            base.ModifyStatusChanged += SnippetDocument_ModifyStatusChanged;
            base.NewStatusChanged += SnippetDocument_NewStatusChanged;

            RegisterEvents();

            // finally set the state of the document
            this.IsNew = isNew;
            this.IsModified = false;
        }

        private void SnippetDocument_NewStatusChanged(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = !base.IsNew;
        }

        private void SnippetDocument_ModifyStatusChanged(object sender, EventArgs e)
        {
            if (base.IsNew)
                lblEditStatus.Text = base.IsModified ? "Edited" : "No Changes";
            else
                lblEditStatus.Text = base.IsModified ? "Edited" : "Saved";

            lblEditStatus.ForeColor = base.IsModified ? Color.DarkRed : Color.Black;
            btnSave.Enabled = base.IsModified;
            btnDiscardChange.Enabled = base.IsModified;
        }

        private void SnippetDocument_Saving(object sender, EventArgs e)
        {
            this.SaveChanges();
        }

        private void SnippetDocument_Deleting(object sender, EventArgs e)
        {
            this.codeFile.Delete();
        }

        public bool SaveChanges()
        {
            return base.Save(this.codeFile, this);
        }

        private void SetModified(object sender, EventArgs e)
        {
            this.IsModified = true;
        }

        public string SnippetId
        {
            get
            {
                if (this.codeFile != null)
                    return this.codeFile.Id;
                return null;
            }
        }

        public IKeywordIndexItem KeywordIndex
        {
            get
            {
                if (this.codeFile != null)
                    return this.codeFile.IndexItem;
                return null;
            }
        }

        public string Keywords
        {
            get
            {
                if (this.codeFile != null)
                    return this.codeFile.CommaDelimitedKeywords;
                return null;
            }
        }

        public bool ShowIndexEditControls
        {
            get { return base.HeaderFieldsVisible; }
            set { base.HeaderFieldsVisible = value; }
        }

        private void SnippetDocument_HeaderFieldsVisibilityChanged(object sender, EventArgs e)
        {
            this.chkEdit.Checked = base.HeaderFieldsVisible;
            this.toolstripKeywords.Visible = base.HeaderFieldsVisible;
            this.toolstripTitle.Visible = base.HeaderFieldsVisible;
        }

        public void FlagSilentClose()
        {
            base.CloseWithoutPrompts = true;
        }

        public void AddKeywords(string keywords, bool flagModified = true)
        {
            // in fact, it seems that "codeFile" has already been updated because we have a reference to it in memory, but
            // this is just a "defensive programming" approach.
            if (flagModified)
                txtKeywords.Text = this.application.AddKeywordsToDelimitedText(this.codeFile.CommaDelimitedKeywords, keywords);
            else
            {
                txtKeywords.TextChanged -= SetModified;
                txtKeywords.Text = this.application.AddKeywordsToDelimitedText(this.codeFile.CommaDelimitedKeywords, keywords);
                txtKeywords.TextChanged += SetModified;
            }
        }

        public void RemoveKeywords(string keywords, bool flagModified = true)
        {
            // in fact, it seems that "codeFile" has already been updated because we have a reference to it in memory, but
            // this is just a "defensive programming" approach.
            if (flagModified)
                txtKeywords.Text = this.application.RemoveKeywordsFromDelimitedText(this.codeFile.CommaDelimitedKeywords, keywords);

            else
            {
                txtKeywords.TextChanged -= SetModified;
                txtKeywords.Text = this.application.RemoveKeywordsFromDelimitedText(this.codeFile.CommaDelimitedKeywords, keywords);
                txtKeywords.TextChanged += SetModified;
            }
        }

        private void InitializeImages()
        {
            btnTakeSnapshot.Image = Resources.GetImage(Constants.ImageKeys.AddSnapshot);
            btnDeleteSnapshot.Image = Resources.GetImage(Constants.ImageKeys.DeleteSnapshot);
            btnDelete.Image = Resources.GetImage(Constants.ImageKeys.DeleteSnippet);
            btnSave.Image = Resources.GetImage(Constants.ImageKeys.SaveSnippet);
            chkEdit.Image = Resources.GetImage(Constants.ImageKeys.EditSnippet);
            btnDiscardChange.Image = Resources.GetImage(Constants.ImageKeys.DiscardSnippetChanges);

            this.Icon = IconRepository.GetIcon(this.codeFile.Syntax);
            lblEditStatus.Image = this.IconImage;
        }

        private void RegisterEvents()
        {
            this.snapshotListCtrl1.SnapshotSelectionChanged += (s, e) =>
            {
                this.btnDeleteSnapshot.Enabled = (snapshotListCtrl1.SelectedSnapshot != null && tabControl.SelectedTab == snapshotsTab && !this.isNew);
            };

            this.tabControl.Selected += (s, e) =>
            {
                this.btnDeleteSnapshot.Enabled = (snapshotListCtrl1.SelectedSnapshot != null && tabControl.SelectedTab == snapshotsTab && !this.isNew);
            };

            this.chkEdit.Click += (s, e) => { this.ShowIndexEditControls = chkEdit.Checked; };

            cboFontSize.SelectedIndexChanged += (s, e) =>
            {
                this.syntaxBox.FontSize = Convert.ToSingle(cboFontSize.SelectedItem);
                this.snapshotListCtrl1.EditorFontSize = this.syntaxBox.FontSize;
            };

            this.codeFile.SnapshotTaken += (s, e) => { UpdateSnapshotsTab(); };
            this.codeFile.SnapshotDeleted += (s, e) => { UpdateSnapshotsTab(); };

            txtTitle.TextChanged += SetModified;
            txtKeywords.TextChanged += SetModified;
            syntaxBox.TextChanged += SetModified;
            btnDelete.Click += btnDelete_Click;
            cboSyntax.SelectedIndexChanged += cboSyntax_SelectedIndexChanged;
        }

        private void InitializeSyntaxList()
        {
            cboSyntax.Items.Clear();
            cboSyntax.Items.AddRange(application.GetSyntaxes());
        }

        private void EnableControls()
        {
            btnTakeSnapshot.Enabled = !IsNew;
            btnDeleteSnapshot.Enabled = false;
            btnSave.Enabled = false;
            btnDiscardChange.Enabled = false;
            btnDelete.Enabled = !this.IsNew;
        }

        private void ResetFields()
        {
            this.Text = codeFile.Title;
            this.txtIdentifier.Text = codeFile.Id;
            this.txtKeywords.Text = codeFile.CommaDelimitedKeywords;
            this.txtTitle.Text = codeFile.Title;
            this.syntaxBox.Document.Text = codeFile.Text;

            SelectSyntax(codeFile.Syntax);
        }

        protected override void SaveFields()
        {
            this.codeFile.Title = this.txtTitle.Text.Trim();
            this.codeFile.CommaDelimitedKeywords = this.txtKeywords.Text.Trim();
            this.codeFile.Syntax = this.cboSyntax.Text.Trim();
            this.codeFile.Text = syntaxBox.Document.Text;
            this.codeFile.Save();
            this.btnTakeSnapshot.Enabled = true;
            this.Text = codeFile.Title;
            this.txtKeywords.Text = this.codeFile.CommaDelimitedKeywords;
        }

        protected override bool ValidateChanges()
        {
            if (string.IsNullOrWhiteSpace(this.txtTitle.Text))
            {
                Dialogs.MandatoryFieldRequired(this, "Title");
                this.ShowIndexEditControls = true;
                this.txtTitle.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(this.txtKeywords.Text))
            {
                Dialogs.MandatoryFieldRequired(this, "Keywords");
                this.ShowIndexEditControls = true;
                this.txtKeywords.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(this.cboSyntax.Text))
            {
                Dialogs.MandatoryFieldRequired(this, "Syntax");
                this.ShowIndexEditControls = true;
                this.cboSyntax.Focus();
                return false;
            }
            else
                return true;
        }

        private void SetDefaultFont()
        {
            int index = cboFontSize.FindStringExact(ConfigSettings.DefaultFontSize.ToString());
            if (index >= 0)
                cboFontSize.SelectedIndex = index;
            else
                cboFontSize.SelectedIndex = 4;
        }

        private void SelectSyntax(string syntax)
        {
            // ensures that all controls are up to date with the new syntax.
            string syn = syntax.ToUpper();

            foreach (object item in cboSyntax.Items)
            {
                if (item.ToString() == syn)
                    cboSyntax.SelectedItem = item;
            }

            string syntaxFile = application.GetSyntaxFile(syntax);
            this.syntaxBox.Document.SyntaxFile = syntaxFile;
            this.snapshotListCtrl1.SyntaxFile = syntaxFile;

            this.Icon = IconRepository.GetIcon(syntax);
            this.lblEditStatus.Image = IconRepository.GetIcon(syntax).ToBitmap();
        }

        private void UpdateSnapshotsTab()
        {
            if (!snapshotListCtrl1.Attached)
                snapshotListCtrl1.Attach(this.codeFile);

            snapshotsTab.Text = string.Format("Snapshots ({0})", this.codeFile.Snapshots.Length);

            if (!this.codeFile.HasSnapshots)
            {
                if (tabControl.TabPages.Contains(snapshotsTab))
                    tabControl.TabPages.Remove(snapshotsTab);
            }
            else
            {
                if (!tabControl.TabPages.Contains(snapshotsTab))
                    tabControl.TabPages.Add(snapshotsTab);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            this.codeFile.Close();
            base.OnFormClosed(e);
        }

        private void btnTakeSnapshot_Click(object sender, EventArgs e)
        {
            // Important: that changes are saved. because the snapshot will immediately save the file
            // in order to keep integrity intact.
            // because we're only taking a snapshot, the Date Modified of the code index does not
            // necessarily have to change.
            if (this.IsModified)
            {
                Dialogs.TakeSnapshotInvalidInCurrentContext(this);
                return;
            }

            // ok to continue...
            SnapshotDescForm frm = new SnapshotDescForm();
            DialogResult result = frm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                this.codeFile.TakeSnapshot(frm.Description);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            base.Delete();
        }

        private void btnDeleteSnapshot_Click(object sender, EventArgs e)
        {
            this.snapshotListCtrl1.DeleteSnapshot();
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

        private void cboSyntax_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IsModified = true;
            SelectSyntax(cboSyntax.SelectedItem.ToString());
        }
    }
}
