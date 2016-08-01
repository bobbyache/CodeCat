using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Qik;
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
    public partial class QikCodeDocument : DockContent, IContentDocument
    {
        private QikFile qikFile;
        private bool flagSilentClose = false;
        private bool flagForDelete = false;
        private AppFacade application;

        public Image IconImage
        {
            get { return this.Icon.ToBitmap(); }
        }

        public event EventHandler DocumentDeleted;
        public event EventHandler<DocumentSavedFileEventArgs> DocumentSaved;

        public QikCodeDocument(QikFile qikFile, AppFacade application, bool isNew = false)
        {
            InitializeComponent();
            
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.CloseButtonVisible = true;
            this.CloseButton = true;

            this.application = application;
            this.qikFile = qikFile;
            this.Tag = qikFile.Id;

            SetDefaultFont();
            
            InitializeImages();
            InitializeSyntaxList();

            EnableControls();
            ResetFields();

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

        private bool isModified;
        public bool IsModified 
        {
            get { return this.isModified; }
            private set
            {
                if (this.isModified != value)
                {
                    if (this.isNew)
                        lblEditStatus.Text = value ? "Edited" : "No Changes";
                    else
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
        
        public bool ShowIndexEditControls
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

                    if (DocumentSaved != null)
                        DocumentSaved(this, new DocumentSavedFileEventArgs(this.qikFile, this));

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

            this.Icon = IconRepository.GetIcon(this.qikFile.Syntax);
            lblEditStatus.Image = this.IconImage;
        }

        private void RegisterEvents()
        {

            this.chkEdit.Click += (s, e) => { this.ShowIndexEditControls = chkEdit.Checked; };

            cboFontSize.SelectedIndexChanged += (s, e) =>
            {
                //this.syntaxBox.FontSize = Convert.ToSingle(cboFontSize.SelectedItem);
                //this.snapshotListCtrl1.EditorFontSize = this.syntaxBox.FontSize;
            };

            //this.qikFile.SnapshotTaken += (s, e) => { UpdateSnapshotsTab(); };
            //this.qikFile.SnapshotDeleted += (s, e) => { UpdateSnapshotsTab(); };

            txtTitle.TextChanged += SetModified;
            txtKeywords.TextChanged += SetModified;
            //syntaxBox.TextChanged += SetModified;
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
            btnSave.Enabled = false;
            btnDiscardChange.Enabled = false;
            btnDelete.Enabled = !this.IsNew;
        }

        private void ResetFields()
        {
            this.Text = qikFile.Title;
            this.txtIdentifier.Text = qikFile.Id;
            this.txtKeywords.Text = qikFile.CommaDelimitedKeywords;
            this.txtTitle.Text = qikFile.Title;
            //this.syntaxBox.Document.Text = qikFile.Text;

            SelectSyntax(qikFile.Syntax);
        }

        private void SaveValues()
        {
            this.qikFile.Title = this.txtTitle.Text.Trim();
            this.qikFile.CommaDelimitedKeywords = this.txtKeywords.Text.Trim();
            this.qikFile.Syntax = this.cboSyntax.Text.Trim();
            //this.qikFile.Text = syntaxBox.Document.Text;
            this.qikFile.Save();
            this.Text = qikFile.Title;
            this.txtKeywords.Text = this.qikFile.CommaDelimitedKeywords;
        }

        private bool ValidateChanges()
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
            //this.syntaxBox.Document.SyntaxFile = syntaxFile;
            //this.snapshotListCtrl1.SyntaxFile = syntaxFile;

            this.Icon = IconRepository.GetIcon(syntax);
            this.lblEditStatus.Image = IconRepository.GetIcon(syntax).ToBitmap();
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            this.qikFile.Close();
            base.OnFormClosed(e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.IsNew)
                return;

            DialogResult result = Dialogs.DeleteSnippetDialogPrompt(this);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                string snippetId = this.SnippetId;
                this.flagForDelete = true;
                this.qikFile.Delete();

                if (DocumentDeleted != null)
                    DocumentDeleted(this, new EventArgs());

                this.Close();
            }
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
