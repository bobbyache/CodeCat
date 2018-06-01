using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CygSoft.CodeCat.Infrastructure;

namespace CygSoft.CodeCat.UI.WinForms.TopicSectionBase
{
    public interface IListviewGrouper
    {
        void GroupItem(ListView listView, ListViewItem listItem, ICategorizedListItem categorizedItem);
        bool ItemsSelected<T>(ListView listView) where T : class;
        void LoadAllItems<T>(ListView listView, T[] items, string[] categories, Func<ListView, T, bool, ListViewItem> create, bool reloadGroups = true) where T : ICategorizedListItem;
        void ReloadGroups(ListView listView, string[] groups);
        void Select(ListView listView, string itemKey);
        T SelectedItem<T>(ListView listView) where T : class;
        IEnumerable<T> SelectedItems<T>(ListView listview) where T : class;
        bool SingleItemSelected<T>(ListView listView) where T : class;
    }
}