using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Drawing;
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
                if (persistableTarget != null)
                    return persistableTarget.Id;
                return null;
            }
        }

        public string Keywords
        {
            get
            {
                if (persistableTarget != null)
                    return persistableTarget.CommaDelimitedKeywords;
                return null;
            }
        }

        public Image IconImage
        {
            get { return Icon.ToBitmap(); }
        }

        protected bool isNew;
        public virtual bool IsNew
        {
            get { return isNew; }
            protected set
            {
                if (isNew != value)
                {
                    isNew = value;
                    NewStatusChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        protected bool isModified;
        public virtual bool IsModified
        {
            get { return isModified; }
            protected set
            {
                if (isModified != value)
                {
                    isModified = value;
                    ModifyStatusChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        private bool headerFieldsVisible;
        public bool HeaderFieldsVisible
        {
            get { return headerFieldsVisible; }
            set
            {
                headerFieldsVisible = value;
                HeaderFieldsVisibilityChanged?.Invoke(this, new EventArgs());
            }
        }

        public bool CloseWithoutPrompts { get; set; }

        public BaseDocument()
        {
            InitializeComponent();

            DockAreas = DockAreas.Document;
            CloseButtonVisible = true;
            CloseButton = true;
        }

        public IKeywordIndexItem GetKeywordIndex()
        {
            if (persistableTarget != null)
                return persistableTarget.IndexItem;
            return null;
        }

        protected void Delete()
        {
            if (IsNew)
                return;

            DialogResult result = Dialogs.DeleteItemDialog(this, "document");

            if (result == DialogResult.Yes)
            {
                flagForDelete = true;
                Deleting?.Invoke(this, new EventArgs());
                DocumentDeleted?.Invoke(this, new EventArgs());

                Close();
            }
        }

        protected virtual bool ValidateChanges() { return false; }
        protected virtual void SaveFields() { }

        protected string AddKeywords(string keywords)
        {
            return application.AddKeywordsToDelimitedText(persistableTarget.CommaDelimitedKeywords, keywords);
        }

        protected string RemoveKeywords(string keywords)
        {
            return application.RemoveKeywordsFromDelimitedText(persistableTarget.CommaDelimitedKeywords, keywords);
        }

        protected void RevertChanges()
        {
            DialogResult result = Dialogs.RevertDocumentChangesDialogPrompt(this);
            if (result == DialogResult.Yes)
            {
                Reverting?.Invoke(this, new EventArgs());
                IsModified = false;
            }
        }

        protected bool Save(IPersistableTarget target, IContentDocument contentDocument)
        {
            if (ValidateChanges())
            {
                try
                {
                    SaveFields();

                    IsModified = false;
                    IsNew = false;

                    DocumentSaved?.Invoke(this, new DocumentSavedFileEventArgs(target, contentDocument));

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
            if (!CloseWithoutPrompts && !flagForDelete)
            {
                if (IsModified)
                {
                    if (e.CloseReason != CloseReason.MdiFormClosing && !CloseWithoutPrompts)
                    {
                        DialogResult result = Dialogs.SaveDocumentChangesDialogPrompt(this);

                        if (result == DialogResult.Yes)
                        {
                            if (ValidateChanges())
                                Saving?.Invoke(this, new EventArgs());
                            else
                                e.Cancel = true;
                        }
                        else if (result == DialogResult.No)
                        {
                            Reverting?.Invoke(this, new EventArgs());
                        }
                        else if (result == DialogResult.Cancel)
                            e.Cancel = true;
                    }
                }
            }
            base.OnFormClosing(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            persistableTarget.Close();
            base.OnFormClosed(e);
        }
    }
}
