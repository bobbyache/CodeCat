using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.TopicSectionBase
{
    public interface IListviewSorter
    {
        int SortingColumn { get; set; }
        SortOrder SortingOrder { get; }

        void Sort(int columnIndex, SortOrder? sortingOrder = default(SortOrder?));
    }
}