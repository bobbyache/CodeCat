using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Files.Infrastructure;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class BaseWorkItemForm : DockContent
    {
        public event EventHandler Deleted;
        public event EventHandler<WorkItemSavedFileEventArgs> Saved;

        protected event EventHandler HeaderFieldsVisibilityChanged;
        protected event EventHandler ModifyStatusChanged;
        protected event EventHandler NewStatusChanged;
        protected event EventHandler Saving;
        protected event EventHandler Deleting;
        protected event EventHandler Reverting;

        private bool flagForDelete = false;

        protected AppFacade application;
        protected IFile workItem;

        public string Id
        {
            get
            {
                if (workItem != null)
                    return ((ITitledEntity)workItem).Id;
                return null;
            }
        }

        public string Keywords
        {
            get
            {
                if (workItem != null)
                    return ((IKeywordTarget)workItem).IndexItem.CommaDelimitedKeywords;
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

        public BaseWorkItemForm()
        {
            InitializeComponent();

            DockAreas = DockAreas.Document;
            CloseButtonVisible = true;
            CloseButton = true;
        }

        public IKeywordIndexItem GetKeywordIndex()
        {
            if (workItem != null)
                return ((IKeywordTarget)workItem).IndexItem;
            return null;
        }

        protected void Delete()
        {
            if (IsNew)
                return;

            DialogResult result = Gui.Dialogs.DeleteItemMessageBox(this, "document");

            if (result == DialogResult.Yes)
            {
                flagForDelete = true;
                Deleting?.Invoke(this, new EventArgs());
                Deleted?.Invoke(this, new EventArgs());

                Close();
            }
        }

        protected virtual bool ValidateChanges() { return false; }
        protected virtual void SaveFields() { }

        protected string AddKeywords(string keywords)
        {
            return application.AddKeywordsToDelimitedText(((IKeywordTarget)workItem).IndexItem.CommaDelimitedKeywords, keywords);
        }

        protected string RemoveKeywords(string keywords)
        {
            return application.RemoveKeywordsFromDelimitedText(((IKeywordTarget)workItem).IndexItem.CommaDelimitedKeywords, keywords);
        }

        protected void RevertChanges()
        {
            DialogResult result = Gui.Dialogs.RevertTopicChangesQuestionMessageBox(this);
            if (result == DialogResult.Yes)
            {
                Reverting?.Invoke(this, new EventArgs());
                IsModified = false;
            }
        }

        protected bool Save(IFile target, IWorkItemForm contentDocument)
        {
            if (ValidateChanges())
            {
                try
                {
                    SaveFields();

                    IsModified = false;
                    IsNew = false;

                    Saved?.Invoke(this, new WorkItemSavedFileEventArgs(target, contentDocument));

                    return true;
                }
                catch (Exception ex)
                {
                    Gui.Dialogs.DocumentSaveErrorMessageBox(this, ex);
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
                        DialogResult result = Gui.Dialogs.SaveDocumentChangesDialogMessageBox(this);

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
            workItem.Close();
            base.OnFormClosed(e);
        }
    }
}
