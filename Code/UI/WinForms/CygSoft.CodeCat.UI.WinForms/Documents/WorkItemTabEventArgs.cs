using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Documents
{
    public class WorkItemTabEventArgs : EventArgs
    {
        public UserControl TabUserControl { get; private set; }
        public TabPage TabPage { get; private set; }
        public string ItemId { get; private set; }

        public WorkItemTabEventArgs(TabPage tabPage, UserControl tabControl)
        {
            TabUserControl = tabControl;
            TabPage = tabPage;
            ItemId = tabPage.Name;
        }
    }
}
