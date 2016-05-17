﻿using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Code;
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
    public partial class SnippetForm : DockContent
    {
        private TabPage snapshotsTab;
        private CodeFile codeFile;
        private bool flagSilentClose = false;
        private bool flagForDelete = false;
        private AppFacade application;

        public event EventHandler<DeleteCodeFileEventArgs> DeleteSnippetDocument;
        public event EventHandler<SaveCodeFileEventArgs> SaveSnippetDocument;

        //public event EventHandler MovePrevious;
        //public event EventHandler MoveNext;

        public SnippetForm(CodeFile codeFile, AppFacade application, bool isNew = false)
        {
            InitializeComponent();

            this.application = application;

            btnTakeSnapshot.Image = Resources.GetImage(Constants.ImageKeys.AddSnapshot);
            btnDeleteSnapshot.Image = Resources.GetImage(Constants.ImageKeys.DeleteSnapshot);
            btnDelete.Image = Resources.GetImage(Constants.ImageKeys.DeleteSnippet);
            btnSave.Image = Resources.GetImage(Constants.ImageKeys.SaveSnippet);
            chkEdit.Image = Resources.GetImage(Constants.ImageKeys.EditSnippet);
            btnDiscardChange.Image = Resources.GetImage(Constants.ImageKeys.DiscardSnippetChanges);

            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            tabControl.Alignment = TabAlignment.Left;
            this.snapshotsTab = this.tabPageSnapshots;

            this.IsNew = isNew;
            this.codeFile = codeFile;
            //this.syntaxBox.GotFocus += (s, e) => { Console.Write(this.IsActivated.ToString()); };

            cboSyntax.Items.Clear();
            cboSyntax.Items.AddRange(application.GetSyntaxes());

            btnTakeSnapshot.Enabled = !IsNew;
            btnDeleteSnapshot.Enabled = false;
            btnSave.Enabled = false;
            btnDiscardChange.Enabled = false;
            btnDelete.Enabled = !isNew;

            toolstripKeywords.Visible = false;
            toolstripTitle.Visible = false;

            this.Tag = codeFile.Id;

            ResetFields();

            cboFontSize.SelectedIndex = 4;

            snapshotListCtrl1.Attach(codeFile);
            UpdateSnapshotsTab();

            // -----------------------------------------------------------
            // these events MUST go after all properties are set...
            // -----------------------------------------------------------
            //syntaxDoc.Change += (s, e) => {
            //    this.IsModified = true;
            //} ;

            this.snapshotListCtrl1.SnapshotSelectionChanged += (s, e) =>
            {
                this.btnDeleteSnapshot.Enabled = (snapshotListCtrl1.SelectedSnapshot != null && tabControl.SelectedTab == snapshotsTab && !this.isNew);
            };

            this.tabControl.Selected += (s, e) =>
            {
                this.btnDeleteSnapshot.Enabled = (snapshotListCtrl1.SelectedSnapshot != null && tabControl.SelectedTab == snapshotsTab && !this.isNew);
            };

            chkEdit.Click += (s, e) =>
                {
                    toolstripTitle.Visible = chkEdit.Checked;
                    toolstripKeywords.Visible = chkEdit.Checked;
                };

            this.codeFile.SnapshotTaken += (s, e) => { UpdateSnapshotsTab(); };
            this.codeFile.SnapshotDeleted += (s, e) => { UpdateSnapshotsTab(); };

            txtTitle.TextChanged += SetModified;
            txtKeywords.TextChanged += SetModified;
            syntaxBox.TextChanged += SetModified;

            cboSyntax.SelectedIndexChanged += cboSyntax_SelectedIndexChanged;

            cboFontSize.SelectedIndexChanged += (s, e) =>
            {
                this.syntaxBox.FontSize = Convert.ToSingle(cboFontSize.SelectedItem);
                this.snapshotListCtrl1.EditorFontSize = this.syntaxBox.FontSize;
            };

            btnDelete.Click += btnDelete_Click;

            this.IsModified = false;

            this.CloseButtonVisible = true;
            this.CloseButton = true;
        }

        private void SetModified(object sender, EventArgs e)
        {
            this.IsModified = true;
        }

        private void cboSyntax_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IsModified = true;
            string syntax = cboSyntax.SelectedItem.ToString();
            SelectSyntax(syntax);
            string syntaxFile = application.GetSyntaxFile(syntax);
            this.syntaxBox.Document.SyntaxFile = syntaxFile;
            this.snapshotListCtrl1.SyntaxFile = syntaxFile;
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

        public string Keywords
        {
            get
            {
                if (this.codeFile != null)
                    return this.codeFile.CommaDelimitedKeywords;
                return null;
            }
        }

        private bool isModified;
        public bool IsModified 
        {
            get { return this.isModified; }
            private set
            {
                if (this.isModified != value)
                {
                    //btnSave.Enabled = hasChanges ? true : false;
                    lblEditStatus.Text = value ? "Edited" : "Saved";
                    lblEditStatus.ForeColor = value ? Color.DarkRed : Color.Black;
                    btnSave.Enabled = value;
                    btnDiscardChange.Enabled = value;
                    this.isModified = value;
                }
            }
        }

        private bool isNew;
        public bool IsNew 
        {
            get { return this.isNew; }
            private set
            {
                if (this.isNew != value)
                {
                    this.btnDelete.Enabled = !value;
                    this.isNew = value;
                }
            }
        }

        public bool EditMode
        {
            get { return this.chkEdit.Checked; }
            set
            {
                this.chkEdit.Checked = value;
                this.toolstripKeywords.Visible = value;
                this.toolstripTitle.Visible = value;
            }
        }
        
        public bool SaveChanges()
        {
            if (string.IsNullOrWhiteSpace(this.txtTitle.Text))
            {
                Dialogs.MandatoryFieldRequired(this, "Title");
                this.EditMode = true;
                this.txtTitle.Focus();
            }
            else if (string.IsNullOrWhiteSpace(this.txtKeywords.Text))
            {
                Dialogs.MandatoryFieldRequired(this, "Keywords");
                this.EditMode = true;
                this.txtKeywords.Focus();
            }
            else if (string.IsNullOrWhiteSpace(this.cboSyntax.Text))
            {
                Dialogs.MandatoryFieldRequired(this, "Syntax");
                this.EditMode = true;
                this.cboSyntax.Focus();
            }
            else
            {
                try
                {
                    this.codeFile.Title = this.txtTitle.Text.Trim();
                    this.codeFile.CommaDelimitedKeywords = this.txtKeywords.Text.Trim();
                    this.codeFile.Syntax = this.cboSyntax.Text.Trim();
                    this.codeFile.Text = syntaxBox.Document.Text;
                    this.codeFile.Save();
                    this.btnTakeSnapshot.Enabled = true;
                    this.Text = codeFile.Title;
                    this.txtKeywords.Text = this.codeFile.CommaDelimitedKeywords;
                    this.IsModified = false;
                    this.IsNew = false;

                    if (SaveSnippetDocument != null)
                        SaveSnippetDocument(this, new SaveCodeFileEventArgs(this.codeFile, this));

                    return true;
                }
                catch (Exception ex)
                {
                    Dialogs.SnippetSaveErrorNotification(this, ex);
                }
            }
            return false;
        }

        public void FlagSilentClose()
        {
            flagSilentClose = true;
        }

        public void Delete()
        {
            flagForDelete = true;
            this.Close();
        }

        public void DiscardChanges()
        {
            // discard all changes...
            throw new NotImplementedException();
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

        private void ResetFields()
        {
            this.Text = codeFile.Title;
            this.txtKeywords.Text = codeFile.CommaDelimitedKeywords;
            this.txtTitle.Text = codeFile.Title;
            SelectSyntax(codeFile.Syntax);

            this.syntaxBox.Document.Text = codeFile.Text;

            string syntaxFile = application.GetSyntaxFile(codeFile.Syntax);
            this.syntaxBox.Document.SyntaxFile =  syntaxFile;
            this.snapshotListCtrl1.SyntaxFile = syntaxFile;
        }

        private void SelectSyntax(string syntax)
        {
            string syn = syntax.ToUpper();

            foreach (object item in cboSyntax.Items)
            {
                if (item.ToString() == syn)
                    cboSyntax.SelectedItem = item;
            }
        }

        private void UpdateSnapshotsTab()
        {
            snapshotsTab.Text = string.Format("Snapshots ({0})", codeFile.Snapshots.Length);
            if (!codeFile.HasSnapshots)
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

        private void SnippetForm_Activated(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(string.Format("Activated: {1}, Document: {0}", this.Text, this.IsActivated.ToString()));
            // There's a bug here. It seems that if you activate a docked Document
            // from a floating Document the activated event doesn't fire ???
            // However this only happens when you click in the SyntaxBox control and 
            //doesn't happen if you click the docked window tab.
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // if one of these forms cancel, it also seems to stop the application
            // from closing!

            if (!flagSilentClose && !flagForDelete)
            {
                if (this.IsModified)
                {
                    if (e.CloseReason != CloseReason.MdiFormClosing && !flagSilentClose)
                    {
                        DialogResult result = Dialogs.SaveSnippetChangesDialogPrompt(this);

                        if (result == System.Windows.Forms.DialogResult.Yes)
                            this.SaveChanges();
                        else if (result == System.Windows.Forms.DialogResult.Cancel)
                            e.Cancel = true;
                    }
                }
            }
            base.OnFormClosing(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Isn't currently working, but you could apparently use this to "tab"
            // between different windows...
            // http://stackoverflow.com/questions/22873825/how-to-detect-shifttab-when-overriding-processcmdkey
            // when the time comes, research something like handling multiple keys pressed at the same time etc.

            //// combine any number of keys here
            //if (keyData == (Keys.ControlKey | Keys.Back))
            //{
            //    if (MovePrevious != null)
            //        MovePrevious(this, new EventArgs());
            //}
            //else if (keyData == (Keys.ControlKey | Keys.Next))
            //{
            //    if (MoveNext != null)
            //        MoveNext(this, new EventArgs());
            //}
            return base.ProcessCmdKey(ref msg, keyData);
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
            if (this.IsNew)
                return;

            DialogResult result = Dialogs.DeleteSnippetDialogPrompt(this);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                if (DeleteSnippetDocument != null)
                    DeleteSnippetDocument(this, new DeleteCodeFileEventArgs(this.codeFile, this));
            }
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
    }
}
