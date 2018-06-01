using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.TopicSectionBase
{
    public interface IControlFunctionFactory
    {
        IListviewSorter CreateListviewSorter(ListView listview);
    }
}
