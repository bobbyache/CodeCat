using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class SnippetForm : Form, IItemForm
    {
        public string Id { get { return this.codeFile.Id; } }
        public string SnippetTitle { get { return this.codeFile.Title; } }

        private bool hasChanges = false;
        public bool HasChanges 
        {
            get { return this.hasChanges; }
            private set
            {
                Edited(value);
                this.hasChanges = value;
            }
        }

        private AppFacade application;
        private CodeFile codeFile;
        private TabPage snapshotsTab;

        public SnippetForm()
        {
            InitializeComponent();
            btnTakeSnapshot.Image = Resources.GetImage(Constants.ImageKeys.SnippetSnapshot);
            this.snapshotsTab = this.tabPageSnapshots;
            this.HasChanges = false;
            this.KeyPreview = true;
            this.KeyDown += SnippetForm_KeyDown;
            this.cboFontSize.SelectedIndexChanged += cboFontSize_SelectedIndexChanged;
            this.btnSave.Enabled = false;
            cboFontSize.SelectedIndex = 4;
        }

        public void Initialize(IPersistableFile persistableFile, AppFacade application)
        {

            this.codeFile = (CodeFile)persistableFile;
            this.application = application;
            this.Text = "Snippet: " + codeFile.Title;
            this.txtSnippetId.Text = codeFile.Id;
            this.syntaxBox.Document.Text = codeFile.Text;

            SetLanguages();

            this.keywordsTextBox.Text = codeFile.CommaDelimitedKeywords;
            this.titleTextBox.Text = codeFile.Title;
            this.snapshotListCtrl.Attach(codeFile);

            UpdateSnapshotsTab();

            SelectLanguage(codeFile.Syntax);
            SetSyntax(codeFile.Syntax);

            WireEvents();
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
        

        private void SelectLanguage(string language)
        {
            foreach (object item in languageComboBox.Items)
            {
                if (item.ToString() == language)
                    languageComboBox.SelectedItem = item;
            }
        }

        private void SetLanguages()
        {
            languageComboBox.Items.Clear();
            languageComboBox.Items.AddRange(application.GetLanguages());
        }

        private void SnippetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.HasChanges)
            {
                DialogResult result = Dialogs.SaveSnippetChangesDialogPrompt(this);

                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                else if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (!SaveSnippet())
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            UnwireEvents();
        }

        private void WireEvents()
        {
            this.languageComboBox.SelectedIndexChanged += languageComboBox_SelectedIndexChanged;
            this.languageComboBox.SelectionChangeCommitted += languageComboBox_SelectionChangeCommitted;
            this.titleTextBox.TextChanged += titleTextBox_TextChanged;
            this.syntaxDocument1.Change += syntaxDocument1_Change;
            this.keywordsTextBox.TextChanged += this.keywordsTextBox_TextChanged;
            this.codeFile.SnapshotTaken += (s, e) => { UpdateSnapshotsTab(); };
            this.codeFile.SnapshotDeleted += (s, e) => { UpdateSnapshotsTab(); };
            //this.syntaxDocument1.ModifiedChanged += syntaxDocument1_ModifiedChanged;
        }

        private void UnwireEvents()
        {
            this.languageComboBox.SelectedIndexChanged -= languageComboBox_SelectedIndexChanged;
            this.languageComboBox.SelectionChangeCommitted -= languageComboBox_SelectionChangeCommitted;
            this.titleTextBox.TextChanged -= titleTextBox_TextChanged;
            this.syntaxDocument1.Change -= syntaxDocument1_Change;
            this.keywordsTextBox.TextChanged -= this.keywordsTextBox_TextChanged;
            this.codeFile.SnapshotTaken -= (s, e) => { UpdateSnapshotsTab(); };
            this.codeFile.SnapshotDeleted -= (s, e) => { UpdateSnapshotsTab(); };
            //this.syntaxDocument1.ModifiedChanged -= syntaxDocument1_ModifiedChanged;
        }

        private bool SaveSnippet()
        {
            if (string.IsNullOrWhiteSpace(this.titleTextBox.Text))
            {
                MessageBox.Show("Invalid data.");
            }
            else if (string.IsNullOrWhiteSpace(this.keywordsTextBox.Text))
            {
                MessageBox.Show("Invalid data.");
            }
            else if (string.IsNullOrWhiteSpace(this.languageComboBox.Text))
            {
                MessageBox.Show("Invalid data.");
            }
            else
            {
                try
                {
                    this.codeFile.Title = this.titleTextBox.Text.Trim();
                    this.codeFile.Syntax = this.languageComboBox.Text.Trim();
                    this.codeFile.CommaDelimitedKeywords = this.keywordsTextBox.Text.Trim();
                    this.codeFile.Text = syntaxBox.Document.Text;
                    this.codeFile.Save();
                    this.keywordsTextBox.Text = this.codeFile.CommaDelimitedKeywords;
                    this.HasChanges = false;
                    return true;
                }
                catch (Exception ex)
                {
                    Dialogs.SnippetSaveErrorNotification(this, ex);
                }
            }
            return false;
        }

        private void SetSyntax(string language)
        {
            string filePath = application.GetSyntaxFile(language);

            this.syntaxBox.Document.SyntaxFile = filePath;
            this.snapshotListCtrl.EditorSyntaxFile = filePath;
        }

        private void titleTextBox_TextChanged(object sender, EventArgs e)
        {
            this.HasChanges = true;
            this.Text = "Snippet: " + titleTextBox.Text;
        }

        private void languageComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.HasChanges = true;
            SetSyntax(languageComboBox.Text);
        }

        private void languageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.HasChanges = true;
            
        }

        private void syntaxDocument1_Change(object sender, EventArgs e)
        {
            this.HasChanges = true;
        }

        private void keywordsTextBox_TextChanged(object sender, EventArgs e)
        {
            this.HasChanges = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSnippet();
        }

        private void SnippetForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control)
            {
                SaveSnippet();
                e.SuppressKeyPress = true;
            }
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.syntaxBox.FontSize = Convert.ToSingle(cboFontSize.SelectedItem);
            this.snapshotListCtrl.EditorFontSize = this.syntaxBox.FontSize;
        }

        private void Edited(bool hasChanges)
        {
            if (this.hasChanges != hasChanges)
            {
                btnSave.Enabled = hasChanges ? true : false;
                currentStateLabel.Text = hasChanges ? "Edited" : "Saved";
                currentStateLabel.ForeColor = hasChanges ? Color.DarkRed : Color.Black;
            }
        }

        private void btnTakeSnapshot_Click(object sender, EventArgs e)
        {
            // Important: that changes are saved. because the snapshot will immediately save the file
            // in order to keep integrity intact.
            // because we're only taking a snapshot, the Date Modified of the code index does not
            // necessarily have to change.
            if (this.HasChanges)
            {
                MessageBox.Show("Must save or discard changes before you can make a snapshot.");
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
    }
}
