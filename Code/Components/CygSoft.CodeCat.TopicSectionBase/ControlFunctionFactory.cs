using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.TopicSectionBase
{
    public class ControlFunctionFactory : IControlFunctionFactory
    {
        public IListviewSorter CreateListviewSorter(ListView listview)
        {
            return new ListviewSorter(listview);
        }
    }
}
