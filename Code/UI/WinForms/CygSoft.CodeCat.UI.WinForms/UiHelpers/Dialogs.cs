using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.UiHelpers
{
    public static partial class Gui
    {
        public static class Dialogs
        {
            /*************************************************************************************************
             * Exception Messages
             ********************************************************************************************** */
            public static void LoadLastProjectErrorMessageBox(IWin32Window owner, Exception exception)
            {
                ExceptionMessageBox(owner, exception, "An error occured while attempting to load the last project.");
            }

            public static void PasteUrlErrorDialogMessageBox(IWin32Window owner, Exception exception)
            {
                ExceptionMessageBox(owner, exception, "An error occurred while attempting to paste into this document:");
            }

            public static void WebPageErrorMessageBox(IWin32Window owner, Exception exception)
            {
                ExceptionMessageBox(owner, exception, "An error occurred while trying to load the web page.");
            }

            public static void PluginFolderNotFoundMessageBox(IWin32Window owner, Exception exception)
            {
                ExceptionMessageBox(owner, exception, "The plugins folder path could not be found.");
            }

            public static void UrlCopyErrorMessageBox(IWin32Window owner, Exception exception)
            {
                ExceptionMessageBox(owner, exception, "An error occurred while trying to copyt the URL.");
            }

            public static void DocumentSaveErrorMessageBox(IWin32Window owner, Exception exception)
            {
                ExceptionMessageBox(owner, exception, "An error occurred while trying to save the document:");
            }

            public static void RecordLastOpenedDocumentsErrorMessageBox(IWin32Window owner, Exception exception)
            {
                ExceptionMessageBox(owner, exception, "An error occurred while trying to record the last opened documents:");
            }

            public static void LoadLastOpenedDocumentsErrorMessageBox(IWin32Window owner, Exception exception)
            {
                ExceptionMessageBox(owner, exception, "An error occurred while trying to load the last opened documents:");
            }

            public static void ProjectFileLoadErrorMessageBox(IWin32Window owner, Exception exception)
            {
                ExceptionMessageBox(owner, exception, "An error occurred while trying to open the project file. The target file is likely to be incompatible with this version:");
            }

            /*************************************************************************************************
             * Ok Messages
             ********************************************************************************************** */
            public static DialogResult MissingRequiredFieldMessageBox(IWin32Window owner, string fieldName)
            {
                string msg = string.Format("A valid value for {0} must be entered in order to continue.", fieldName);
                return OkInformationMessageBox(owner, msg);
            }

            public static DialogResult ConflictingFilenameMessageBox(IWin32Window owner)
            {
                string msg = "File name will conflict with an existing File name. You must rename this field before you can save this item.\n" +
                    "Note, if you have recently deleted an item with the same file name, you must first save. Once saved you'll be able to add a new item with the specified file name.";
                return OkInformationMessageBox(owner, msg);
            }

            public static DialogResult InvalidSnapshotRequestMessageBox(IWin32Window owner)
            {
                return OkInformationMessageBox(owner, "Taking a snapshot is invalid in this context. You must save or discard changes before you can take a snapshot of this snippet.");
            }

            public static DialogResult CannotRemoveTemplateScriptMessageBox(IWin32Window owner)
            {
                return OkInformationMessageBox(owner, "A template script cannot be removed.");
            }

            public static DialogResult MustSaveGroupBeforeActionMessageBox(IWin32Window owner)
            {
                return OkInformationMessageBox(owner, "The group must be saved before you can request this action.");
            }
            public static DialogResult CannotLoadImageEditorMessageBox(IWin32Window owner)
            {
                return OkInformationMessageBox(owner, "The image editor cannot be loaded. Either the editor executeable doesn't exist or the file does not exist.");
            }

            /*************************************************************************************************
             * Yes/No Questions
             ********************************************************************************************** */

            public static DialogResult RevertTopicChangesQuestionMessageBox(IWin32Window owner)
            {
                return YesNoQuestionMessageBox(owner, "Sure you want to discard changes made to this topic?");
            }

            public static DialogResult ReplaceCurrentItemQuestionMessageBox(IWin32Window owner)
            {
                return YesNoQuestionMessageBox(owner, "Sure you want to permanently replace this item? Changes will be permanent.");
            }

            public static DialogResult DeleteItemMessageBox(IWin32Window owner, string itemName)
            {
                return YesNoQuestionMessageBox(owner, string.Format("Sure you want to delete this {0}?", itemName));
            }

            public static DialogResult DeleteMultipleItemsMessageBox(IWin32Window owner, string itemNames)
            {
                return YesNoQuestionMessageBox(owner, string.Format("Sure you want to delete these {0}?", itemNames));
            }

            public static DialogResult RemoveRecentMenuItemDialogMessageBox(IWin32Window owner)
            {
                return YesNoQuestionMessageBox(owner, "Project file does not exist, would you like to remove this from the recent menu?");
            }

            /*************************************************************************************************
             * Yes/No/Cancel
             ********************************************************************************************** */

            public static DialogResult SaveDocumentChangesDialogMessageBox(IWin32Window owner)
            {
                return MessageBox.Show(owner, string.Format("{0}\n You have not saved this snippet. Would you like to save it first?", (owner as IWorkItemForm).Text),
                    string.Format("{0}: {1}", ConfigSettings.ApplicationTitle, (owner as IWorkItemForm).Text),
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }

            /*************************************************************************************************
             * File Dialogs
             ********************************************************************************************** */

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

            private static void ExceptionMessageBox(IWin32Window owner, Exception exception, string message)
            {
                MessageBox.Show(owner, string.Format("{0}\n{1}", message, exception.Message),
                    ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            private static DialogResult OkInformationMessageBox(IWin32Window owner, string message)
            {
                return MessageBox.Show(owner, message, ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            private static DialogResult YesNoQuestionMessageBox(IWin32Window owner, string message)
            {
                return MessageBox.Show(owner, message, ConfigSettings.ApplicationTitle,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            }
        }
    }
}
