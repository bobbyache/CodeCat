using System;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.TopicSectionBase
{
    public class DialogFunctions : IDialogFunctions
    {
        private string applicationTitle;

        public DialogFunctions(string applicationTitle)
        {
            this.applicationTitle = applicationTitle;
        }

        public DialogResult MissingRequiredFieldMessageBox(IWin32Window owner, string fieldName)
        {
            string msg = string.Format("A valid value for {0} must be entered in order to continue.", fieldName);
            return OkInformationMessageBox(owner, msg);
        }

        public void ExceptionMessageBox(IWin32Window owner, Exception exception, string message)
        {
            MessageBox.Show(owner, string.Format("{0}\n{1}", message, exception.Message),
                applicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public DialogResult OkInformationMessageBox(IWin32Window owner, string message)
        {
            return MessageBox.Show(owner, message, applicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public DialogResult YesNoQuestionMessageBox(IWin32Window owner, string message)
        {
            return MessageBox.Show(owner, message, applicationTitle,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }
    }
}
