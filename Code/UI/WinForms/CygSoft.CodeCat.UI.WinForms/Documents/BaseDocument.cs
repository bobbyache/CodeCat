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
using WeifenLuo.WinFormsUI.Docking;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class BaseDocument : DockContent
    {
        public event EventHandler DocumentDeleted;
        public event EventHandler<DocumentSavedFileEventArgs> DocumentSaved;

        protected event EventHandler ModifyStatusChanged;
        protected event EventHandler NewStatusChanged;
        protected event EventHandler Saving;
        protected event EventHandler Deleting;

        private bool flagForDelete = false;

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

        public bool CloseWithoutPrompts { get; set; }

        public BaseDocument()
        {
            InitializeComponent();
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
                    Dialogs.SnippetSaveErrorNotification(this, ex);
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
                            if (this.Saving != null)
                                Saving(this, new EventArgs());
                        }
                        else if (result == System.Windows.Forms.DialogResult.Cancel)
                            e.Cancel = true;
                    }
                }
            }
            base.OnFormClosing(e);
        }
    }
}
