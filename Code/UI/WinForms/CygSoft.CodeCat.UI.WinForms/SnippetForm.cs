using CygSoft.CodeCat.Domain;
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

        public Image IconImage
        {
            get { return this.Icon.ToBitmap(); }
        }

        public event EventHandler<DeleteCodeFileEventArgs> DeleteSnippetDocument;
        public event EventHandler<SaveCodeFileEventArgs> SaveSnippetDocument;

        public SnippetForm(CodeFile codeFile, AppFacade application, bool isNew = false)
        {
            InitializeComponent();

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
            RegisterEvents();

            // finally set the state of the document
            this.IsNew = isNew;
            this.IsModified = false;
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
            if (ValidateChanges())
            {
                try
                {
                    SaveValues();

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

            this.chkEdit.Click += (s, e) => { this.EditMode = chkEdit.Checked; };

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

            toolstripKeywords.Visible = false;
            toolstripTitle.Visible = false;
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

        private void SaveValues()
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

        private bool ValidateChanges()
        {
            if (string.IsNullOrWhiteSpace(this.txtTitle.Text))
            {
                Dialogs.MandatoryFieldRequired(this, "Title");
                this.EditMode = true;
                this.txtTitle.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(this.txtKeywords.Text))
            {
                Dialogs.MandatoryFieldRequired(this, "Keywords");
                this.EditMode = true;
                this.txtKeywords.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(this.cboSyntax.Text))
            {
                Dialogs.MandatoryFieldRequired(this, "Syntax");
                this.EditMode = true;
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // if this form cancels close, seems to stop the application from closing!
            // if forcing close (flagging for delete or closing from the main form)
            // want any dialog boxes popping up. 
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

        private void cboSyntax_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IsModified = true;
            SelectSyntax(cboSyntax.SelectedItem.ToString());
        }
    }
}
