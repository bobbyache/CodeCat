using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
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
    public partial class BaseDocument : DockContent
    {
        public event EventHandler DocumentDeleted;
        public event EventHandler<DocumentSavedFileEventArgs> DocumentSaved;

        protected event EventHandler HeaderFieldsVisibilityChanged;
        protected event EventHandler ModifyStatusChanged;
        protected event EventHandler NewStatusChanged;
        protected event EventHandler Saving;
        protected event EventHandler Deleting;
        protected event EventHandler Reverting;

        private bool flagForDelete = false;

        protected AppFacade application;
        protected IPersistableTarget persistableTarget;

        public string Id
        {
            get
            {
                if (this.persistableTarget != null)
                    return this.persistableTarget.Id;
                return null;
            }
        }

        public string Keywords
        {
            get
            {
                if (this.persistableTarget != null)
                    return this.persistableTarget.CommaDelimitedKeywords;
                return null;
            }
        }

        public Image IconImage
        {
            get { return this.Icon.ToBitmap(); }
        }

        protected bool isNew;
        public virtual bool IsNew
        {
            get { return this.isNew; }
            protected set
            {
                if (this.isNew != value)
                {
                    this.isNew = value;
                    if (this.NewStatusChanged != null)
                        this.NewStatusChanged(this, new EventArgs());
                }
            }
        }

        protected bool isModified;
        public virtual bool IsModified
        {
            get { return this.isModified; }
            protected set
            {
                if (this.isModified != value)
                {
                    this.isModified = value;
                    if (this.ModifyStatusChanged != null)
                        this.ModifyStatusChanged(this, new EventArgs());
                }
            }
        }

        private bool headerFieldsVisible;
        public bool HeaderFieldsVisible
        {
            get { return headerFieldsVisible; }
            set
            {
                this.headerFieldsVisible = value;
                if (HeaderFieldsVisibilityChanged != null)
                    HeaderFieldsVisibilityChanged(this, new EventArgs());
            }
        }

        public bool CloseWithoutPrompts { get; set; }

        public BaseDocument()
        {
            InitializeComponent();

            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.CloseButtonVisible = true;
            this.CloseButton = true;
        }

        public IKeywordIndexItem GetKeywordIndex()
        {
            if (this.persistableTarget != null)
                return this.persistableTarget.IndexItem;
            return null;
        }

        protected void Delete()
        {
            if (this.IsNew)
                return;

            DialogResult result = Dialogs.DeleteDocumentDialogPrompt(this);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                this.flagForDelete = true;

                if (Deleting != null)
                    Deleting(this, new EventArgs());

                if (DocumentDeleted != null)
                    DocumentDeleted(this, new EventArgs());

                this.Close();
            }
        }

        protected virtual bool ValidateChanges() { return false; }
        protected virtual void SaveFields() { }

        protected string AddKeywords(string keywords)
        {
            return this.application.AddKeywordsToDelimitedText(this.persistableTarget.CommaDelimitedKeywords, keywords);
        }

        protected string RemoveKeywords(string keywords)
        {
            return this.application.RemoveKeywordsFromDelimitedText(this.persistableTarget.CommaDelimitedKeywords, keywords);
        }

        protected void RevertChanges()
        {
            DialogResult result = Dialogs.RevertDocumentChangesDialogPrompt(this);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                if (Reverting != null)
                    Reverting(this, new EventArgs());

                this.IsModified = false;
            }
        }

        protected bool Save(IPersistableTarget target, IContentDocument contentDocument)
        {
            if (ValidateChanges())
            {
                try
                {
                    SaveFields();

                    this.IsModified = false;
                    this.IsNew = false;

                    if (DocumentSaved != null)
                        DocumentSaved(this, new DocumentSavedFileEventArgs(target, contentDocument));

                    return true;
                }
                catch (Exception ex)
                {
                    Dialogs.DocumentSaveErrorNotification(this, ex);
                }
            }
            return false;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // if this form cancels close, seems to stop the application from closing!
            // if forcing close (flagging for delete or closing from the main form)
            // want any dialog boxes popping up. 
            if (!this.CloseWithoutPrompts && !flagForDelete)
            {
                if (this.IsModified)
                {
                    if (e.CloseReason != CloseReason.MdiFormClosing && !this.CloseWithoutPrompts)
                    {
                        DialogResult result = Dialogs.SaveDocumentChangesDialogPrompt(this);

                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (this.ValidateChanges())
                            {
                                if (this.Saving != null)
                                    Saving(this, new EventArgs());
                            }
                            else
                                e.Cancel = true;
                        }
                        else if (result == System.Windows.Forms.DialogResult.No)
                        {
                            if (this.Reverting != null)
                                Reverting(this, new EventArgs());
                        }
                        else if (result == System.Windows.Forms.DialogResult.Cancel)
                            e.Cancel = true;
                    }
                }
            }
            base.OnFormClosing(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            this.persistableTarget.Close();
            base.OnFormClosed(e);
        }
    }
}
