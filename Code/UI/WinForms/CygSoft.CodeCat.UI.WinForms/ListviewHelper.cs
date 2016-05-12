using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public class ListviewHelper
    {
        public static int SortingColumn { get; set; }

        public static void SortColumn(ListView listView, int columnIndex)
        {
            if (columnIndex != SortingColumn)
            {
                // Set the sort column to the new column.
                SortingColumn = columnIndex;
                // Set the sort order to ascending by default.
                listView.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (listView.Sorting == SortOrder.Ascending)
                    listView.Sorting = SortOrder.Descending;
                else
                    listView.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            //this.listView1.ListViewItemSorter = new ListViewItemComparer(e.Column,
            //                                     listView1.Sorting);
            listView.ListViewItemSorter = new ListViewItemComparer(columnIndex, listView.Sorting);
            listView.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
        }



        public static void CreateListviewItem(ListView listView, IKeywordIndexItem item, bool select = false)
        {
            ListViewItem listItem = new ListViewItem();
            listItem.Name = item.Id;
            listItem.Tag = item;
            listItem.Text = item.Title;
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateCreated.ToShortDateString()));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateModified.ToShortDateString()));
            listView.Items.Add(listItem);

            if (select)
            {
                listItem.Selected = true;
                listItem.Focused = true;
                listItem.EnsureVisible();
            }
        }

        public static void ReloadListview(ListView listView, IKeywordIndexItem[] indexItems)
        {
            listView.Items.Clear();
            foreach (IKeywordIndexItem item in indexItems)
                ListviewHelper.CreateListviewItem(listView, item);
        }

        public static void DeleteSelectedItem(ListView listView)
        {
            if (listView.SelectedItems.Count == 1)
            {
                listView.Items.Remove(listView.SelectedItems[0]);
            }
        }

        public static IKeywordIndexItem SelectedItem(ListView listView)
        {
            if (listView.SelectedItems.Count == 1)
            {
                return listView.SelectedItems[0].Tag as IKeywordIndexItem;
            }
            return null;
        }

        public static IKeywordIndexItem[] SelectedItems(ListView listView)
        {
            if (listView.SelectedItems.Count >= 0)
            {
                List<IKeywordIndexItem> items = new List<IKeywordIndexItem>();
                foreach (ListViewItem lvItem in listView.SelectedItems)
                    items.Add(lvItem.Tag as IKeywordIndexItem);
                
                return items.ToArray();
            }

            return new IKeywordIndexItem[0];
        }

        public static void UpdateListviewItem(ListView listView, IKeywordIndexItem codeItemIndex)
        {
            foreach (ListViewItem listItem in listView.Items)
            {
                IKeywordIndexItem codeTag = listItem.Tag as IKeywordIndexItem;
                if (codeTag.Id == codeItemIndex.Id)
                {
                    listItem.Text = codeItemIndex.Title;
                    listItem.SubItems[2].Text = codeItemIndex.DateCreated.ToShortDateString();
                    listItem.SubItems[3].Text = codeItemIndex.DateModified.ToShortDateString(); ;
                }
            }
        }

        public static void SelectItem(ListView listView, string id)
        {
            ListViewItem[] items = listView.Items.Find(id, false);
            if (items.Length == 1)
            {
                ListViewItem item = listView.Items[id];
                item.Selected = true;
                item.EnsureVisible();
            }
        }
    }
}
