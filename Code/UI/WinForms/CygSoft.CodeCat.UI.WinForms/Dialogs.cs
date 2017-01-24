using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public class Dialogs
    {
        public static void LoadLastProjectErrorNotification(IWin32Window owner, Exception exception)
        {
            MessageBox.Show(owner, string.Format("An error occured while attempting to load the last project.\n{0}", exception.Message),
                ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void MandatoryFieldRequired(IWin32Window owner, string fieldName)
        {
            MessageBox.Show(owner, string.Format( "{0} is a mandatory field and must be supplied. Please enter a valid value.", fieldName), 
                ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void TakeSnapshotInvalidInCurrentContext(IWin32Window owner)
        {
            MessageBox.Show(owner, "Taking a snapshot is invalid in this context. You must save or discard changes before you can take a snapshot of this snippet.", 
                ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult SaveSnippetDialogPrompt(IWin32Window owner)
        {
            return MessageBox.Show(owner, "Save this snippet?",
                ConfigSettings.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        public static DialogResult CannotRemoveTemplateScriptNotification(IWin32Window owner)
        {
            return MessageBox.Show(owner, "A template script cannot be removed.",
                ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult MustSaveGroupBeforeAction(IWin32Window owner)
        {
            return MessageBox.Show(owner, "The group must be saved before you can request this action.",
                ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult RevertDocumentChangesDialogPrompt(IWin32Window owner)
        {
            return MessageBox.Show(owner, "Sure you want to discard changes made to this document?",
                ConfigSettings.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        public static DialogResult ReplaceCurrentItemPrompt(IWin32Window owner)
        {
            return MessageBox.Show(owner, "Sure you want to permanently replace this item? Changes will be permanent.",
                ConfigSettings.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        public static void PasteUrlErrorDialogPrompt(IWin32Window owner, Exception exception)
        {
            MessageBox.Show(owner, string.Format("An error occurred while attempting to paste into this document:\n{0}", exception.Message),
            ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult DeleteItemDialog(IWin32Window owner, string itemName)
        {
            return MessageBox.Show(owner, string.Format("Sure you want to delete this {0}?", itemName),
                ConfigSettings.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        public static DialogResult DeleteMultipleItemsDialog(IWin32Window owner, string itemNames)
        {
            return MessageBox.Show(owner, string.Format("Sure you want to delete these {0}?", itemNames),
                ConfigSettings.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }


        public static DialogResult NoInputValueForMandatoryField(IWin32Window owner, string fieldName)
        {
            string msg = string.Format("A valid value for {0} must be entered in order to continue.", fieldName);

            return MessageBox.Show(owner, msg,
                ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult WillConflictDialogPrompt(IWin32Window owner, string fieldName)
        {
            string msg = string.Format("{0} will conflict with an existing {0}. You must rename this field before you can save this item.\n" +
                "Note, if you have recently deleted an item with the same file name, you must first save. Once saved you'll be able to add a new item with the specified file name.", fieldName);

            return MessageBox.Show(owner, msg,
                ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult SaveDocumentChangesDialogPrompt(IWin32Window owner)
        {
            return MessageBox.Show(owner, string.Format("{0}\n You have not saved this snippet. Would you like to save it first?", (owner as IContentDocument).Text),
                string.Format("{0}: {1}", ConfigSettings.ApplicationTitle, (owner as IContentDocument).Text),
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult RemoveRecentMenuItemDialogPrompt(IWin32Window owner)
        {
            return MessageBox.Show(owner, "Project file does not exist, would you like to remove this from the recent menu?",
                ConfigSettings.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static void WebPageErrorNotification(IWin32Window owner, Exception exception)
        {
            MessageBox.Show(owner, string.Format("An error occurred while trying to load the web page. \n{0}", exception.Message),
                ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void UrlCopyErrorNotification(IWin32Window owner, Exception exception)
        {
            MessageBox.Show(owner, string.Format("An error occurred while trying to copyt the URL. \n{0}", exception.Message),
                ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void DocumentSaveErrorNotification(IWin32Window owner, Exception exception)
        {
            MessageBox.Show(owner, string.Format("An error occurred while trying to save the document:\n{0}", exception.Message), 
                ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void RecordLastOpenedDocumentsErrorNotification(IWin32Window owner, Exception exception)
        {
            MessageBox.Show(owner, string.Format("An error occurred while trying to record the last opened documents:\n{0}", exception.Message),
                ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void LoadLastOpenedDocumentsErrorNotification(IWin32Window owner, Exception exception)
        {
            MessageBox.Show(owner, string.Format("An error occurred while trying to load the last opened documents:\n{0}", exception.Message),
                ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ProjectFileLoadErrorNotification(IWin32Window owner, Exception exception)
        {
            MessageBox.Show(owner, string.Format("An error occurred while trying to open the project file. The target file is likely to be incompatible with this version:\n{0}", exception.Message),
                ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult OpenProjectFileDialog(IWin32Window owner, string projectFileExt, out string filePath)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = CreateProjectFileFilter(projectFileExt);
            openDialog.DefaultExt = "*.codecat";
            openDialog.Title = string.Format("Open Code Cat Project File");
            openDialog.AddExtension = true;
            openDialog.FilterIndex = 0;

            DialogResult result = openDialog.ShowDialog(owner);
            filePath = openDialog.FileName;

            return result;
        }

        public static DialogResult CreateProjectFileDialog(IWin32Window owner, string projectFileExt, out string filePath)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.Filter = CreateProjectFileFilter(projectFileExt);
            saveDialog.DefaultExt = "*.codecat";
            saveDialog.Title = string.Format("Create Code Cat Project File");
            saveDialog.AddExtension = true;
            saveDialog.FilterIndex = 0;

            DialogResult result = saveDialog.ShowDialog(owner);
            filePath = saveDialog.FileName;

            return result;
        }

        private static string CreateProjectFileFilter(string projectFileExt)
        {
            return string.Format("Code Cat Project Files *.{0} (*.{0})|*.{0}", projectFileExt);
        }

        public static void UrlsPastedSuccessfully(IWin32Window owner)
        {
            MessageBox.Show(owner, "URL set has been pasted successfully.",
                ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void UrlsPasteConflictDetected(IWin32Window owner)
        {
            MessageBox.Show(owner, "URL set has paste has been aborted. Items with the same identifiers already exist within the document.",
                ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
