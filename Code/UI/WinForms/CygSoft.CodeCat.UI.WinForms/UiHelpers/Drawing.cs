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
        public static class Drawing
        {
            #region Redraw Prevention Code

            // This code prevents the control partially loading by preventing the screen redraw until specified 
            // Needs to be move to the frameworks at some stage
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            private static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

            private const int WM_SETREDRAW = 11;

            public static void SuspendDrawing(Control currentControl)
            {
                SendMessage(currentControl.Handle, WM_SETREDRAW, false, 0);
            }

            public static void ResumeDrawing(Control currentControl)
            {
                if (currentControl.Handle != null)
                {
                    SendMessage(currentControl.Handle, WM_SETREDRAW, true, 0);
                    currentControl.Refresh();
                }
            }

            #endregion
        }
    }
}
