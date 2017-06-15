using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.UiHelpers
{
    public static partial class Gui
    {
        public static class GroupedListView
        {
            public static void GroupItem(ListView listView, ListViewItem listItem, ICategorizedItem categorizedItem)
            {
                bool groupExists = false;

                foreach (ListViewGroup group in listView.Groups)
                {
                    if (group.Header == categorizedItem.Category)
                    {
                        // Add item to the group.
                        // Alternative is: group.Items.Add(item);
                        listItem.Group = group;
                        groupExists = true;
                        break;
                    }
                }

                if (!groupExists)
                {
                    ListViewGroup group = new ListViewGroup(categorizedItem.Category);
                    group.HeaderAlignment = HorizontalAlignment.Left;
                    listView.Groups.Add(group);
                    listItem.Group = group;
                }
            }

            public static void ReloadGroups(ListView listView, string[] groups)
            {
                listView.Groups.Clear();
                foreach (string group in groups)
                {
                    ListViewGroup itemGroup = new ListViewGroup(group);
                    itemGroup.HeaderAlignment = HorizontalAlignment.Left;
                    listView.Groups.Add(itemGroup);
                }
                listView.ShowGroups = listView.Groups.Count > 1;
            }

            public static T SelectedItem<T>(ListView listView) where T : class
            {
                if (listView.SelectedItems.Count == 1)
                    return listView.SelectedItems[0].Tag as T;
                return null;
            }

            public static IEnumerable<T> SelectedItems<T>(ListView listview) where T : class
            {
                IEnumerable<T> items = listview.SelectedItems.Cast<ListViewItem>()
                    .Select(lv => lv.Tag).Cast<T>();

                return items;
            }

            public static void RemoveItems(ListView listView)
            {
                listView.Items.Cast<ListViewItem>().Where(l => l.Selected)
                    .ToList().ForEach(l => listView.Items.RemoveByKey(l.Name));
            }

            public static void LoadAllItems<T>(ListView listView, T[] items, Func<ListView, T, bool, ListViewItem> create) where T : ICategorizedItem
            {
                listView.Items.Clear();

                foreach (T item in items)
                {
                    ListViewItem listItem = create(listView, item, false);
                    GroupedListView.GroupItem(listView, listItem, item);
                }
            }
        }
    }
}
