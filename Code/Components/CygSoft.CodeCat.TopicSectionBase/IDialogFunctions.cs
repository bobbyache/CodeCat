using System;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.TopicSectionBase
{
    public interface IDialogFunctions
    {
        void ExceptionMessageBox(IWin32Window owner, Exception exception, string message);
        DialogResult MissingRequiredFieldMessageBox(IWin32Window owner, string fieldName);
        DialogResult OkInformationMessageBox(IWin32Window owner, string message);
        DialogResult YesNoQuestionMessageBox(IWin32Window owner, string message);
    }
}