using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebReferencesTopicSectionPlugin
{
    public class GroupListviewFunctions
    {
        public void GroupItem(ListView listView, ListViewItem listItem, ICategorizedListItem categorizedItem)
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

        public void ReloadGroups(ListView listView, string[] groups)
        {
            listView.Groups.Clear();
            foreach (string group in groups)
            {
                ListViewGroup itemGroup = new ListViewGroup(group);
                itemGroup.Name = group;
                itemGroup.HeaderAlignment = HorizontalAlignment.Left;
                listView.Groups.Add(itemGroup);
            }
            listView.ShowGroups = listView.Groups.Count > 1;
        }

        public bool SingleItemSelected<T>(ListView listView) where T : class
        {
            if (listView.SelectedItems.Count != 1)
                return false;

            T selection = listView.SelectedItems[0].Tag as T;

            return selection != null ? true : false;
        }

        public bool ItemsSelected<T>(ListView listView) where T : class
        {
            return SelectedItems<T>(listView).Any();
        }

        public T SelectedItem<T>(ListView listView) where T : class
        {
            if (listView.SelectedItems.Count == 1)
                return listView.SelectedItems[0].Tag as T;
            return null;
        }

        public IEnumerable<T> SelectedItems<T>(ListView listview) where T : class
        {
            IEnumerable<T> items = listview.SelectedItems.Cast<ListViewItem>()
                .Select(lv => lv.Tag).Cast<T>();

            return items;
        }

        public void Select(ListView listView, string itemKey)
        {
            if (listView.Items.Count >= 1)
            {
                listView.SelectedItems.Clear();

                //var items = listView.Items.Find(itemKey, false);
                var item = listView.Items[itemKey];
                if (item != null)
                {
                    item.Selected = true;
                    item.Focused = true;
                    item.EnsureVisible();
                }
            }
        }

        public void LoadAllItems<T>(ListView listView, T[] items, string[] categories, Func<ListView, T, bool, ListViewItem> create, bool reloadGroups = true) where T : ICategorizedListItem
        {
            if (reloadGroups)
                ReloadGroups(listView, categories);

            listView.Items.Clear();

            foreach (T item in items)
            {
                ListViewItem listItem = create(listView, item, false);
                GroupItem(listView, listItem, item);
            }

            if (listView.Items.Count >= 1)
            {
                listView.SelectedItems.Clear();

                var lv = listView.Items[0];
                if (lv != null)
                {
                    lv.Selected = true;
                    lv.Focused = true;
                    lv.EnsureVisible();
                }
            }
        }
    }
}
