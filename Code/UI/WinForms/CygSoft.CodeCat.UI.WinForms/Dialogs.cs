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
        public static DialogResult SaveSnippetChangesDialogPrompt(IWin32Window owner)
        {
            return MessageBox.Show(owner, string.Format("{0}\n You currently have changes. Would you like to save these changes?", (owner as SnippetForm).Text), 
                string.Format("{0}: {1}", ConfigSettings.ApplicationTitle,(owner as SnippetForm).Text),
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult RemoveRecentMenuItemDialogPrompt(IWin32Window owner)
        {
            return MessageBox.Show(owner, "Project file does not exist, would you like to remove this from the recent menu?",
                ConfigSettings.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static void SnippetSaveErrorNotification(IWin32Window owner, Exception exception)
        {
            MessageBox.Show(owner, string.Format("An error occurred while trying to save the snippet:\n{0}", exception.Message), 
                ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ProjectFileLoadErrorNotification(IWin32Window owner, Exception exception)
        {
            MessageBox.Show(owner, string.Format("An error occurred while trying to open the project file. The target file is likely to be incompatible with this version:\n{0}", exception.Message),
                ConfigSettings.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult OpenIndexDialog(IWin32Window owner, out string filePath)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = "Index Files *.xml (*.xml)|*.xml";
            openDialog.DefaultExt = "*.xml";
            openDialog.Title = string.Format("Open Index");
            openDialog.AddExtension = true;
            openDialog.FilterIndex = 0;

            DialogResult result = openDialog.ShowDialog(owner);
            filePath = openDialog.FileName;

            return result;
        }

        public static DialogResult CreateIndexDialog(IWin32Window owner, out string filePath)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.Filter = "Index Files *.xml (*.xml)|*.xml";
            saveDialog.DefaultExt = "*.xml";
            saveDialog.Title = string.Format("Create Index");
            saveDialog.AddExtension = true;
            saveDialog.FilterIndex = 0;

            DialogResult result = saveDialog.ShowDialog(owner);
            filePath = saveDialog.FileName;

            return result;
        }
    }
}
