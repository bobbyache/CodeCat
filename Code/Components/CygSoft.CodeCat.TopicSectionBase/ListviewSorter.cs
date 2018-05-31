using System;
using System.Collections;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.TopicSectionBase
{
    internal class ListviewSorter
    {
        private class ListViewItemComparer : IComparer
        {
            private int col;
            private SortOrder order;
            public ListViewItemComparer()
            {
                col = 0;
                order = SortOrder.Ascending;
            }
            public ListViewItemComparer(int column, SortOrder order)
            {
                col = column;
                this.order = order;
            }
            public int Compare(object x, object y)
            {
                int returnVal = -1;
                returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                                        ((ListViewItem)y).SubItems[col].Text);
                // Determine whether the sort order is descending.
                if (order == SortOrder.Descending)
                    // Invert the value returned by String.Compare.
                    returnVal = -(returnVal);
                return returnVal;
            }
        }

        private ListView listView;

        public int SortingColumn { get; set; }
        public SortOrder SortingOrder { get; private set; } = SortOrder.Ascending;

        public ListviewSorter(ListView listView)
        {
            this.listView = listView;
            this.SortingColumn = -1;
        }

        public void Sort(int columnIndex, SortOrder? sortingOrder = null)
        {
            if (this.SortingColumn != -1) // not the first time sorted.
            {
                if (columnIndex != SortingColumn)
                {
                    this.SortingColumn = columnIndex;
                    // Set the sort order to ascending by default.
                    listView.Sorting = SortOrder.Ascending;
                }
                else
                {
                    if (sortingOrder == null)
                    {
                        // Determine what the last sort order was and change it.
                        if (listView.Sorting == SortOrder.Ascending)
                            listView.Sorting = SortOrder.Descending;
                        else
                            listView.Sorting = SortOrder.Ascending;
                    }
                    else
                        listView.Sorting = sortingOrder.Value;
                }

                // Call the sort method to manually sort.
                listView.ListViewItemSorter = new ListViewItemComparer(this.SortingColumn, listView.Sorting);
                listView.Sort();
            }
            else
            {
                this.SortingColumn = columnIndex;
                listView.Sorting = SortOrder.Ascending;

                listView.ListViewItemSorter = new ListViewItemComparer(this.SortingColumn, listView.Sorting);
                listView.Sort();
            }

            this.SortingOrder = listView.Sorting;
        }
    }
}
