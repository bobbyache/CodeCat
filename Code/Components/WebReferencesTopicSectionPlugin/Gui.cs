using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WebReferencesTopicSectionPlugin
{
    public static partial class Gui
    {
        public static class Dialogs
        {
            public static string ApplicationTitle
            {
                get;set;
            }

            public static DialogResult MissingRequiredFieldMessageBox(IWin32Window owner, string fieldName)
            {
                string msg = string.Format("A valid value for {0} must be entered in order to continue.", fieldName);
                return OkInformationMessageBox(owner, msg);
            }

            public static void ExceptionMessageBox(IWin32Window owner, Exception exception, string message)
            {
                MessageBox.Show(owner, string.Format("{0}\n{1}", message, exception.Message),
                    ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            public static DialogResult OkInformationMessageBox(IWin32Window owner, string message)
            {
                return MessageBox.Show(owner, message, ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            public static DialogResult YesNoQuestionMessageBox(IWin32Window owner, string message)
            {
                return MessageBox.Show(owner, message, ApplicationTitle,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            }
        }

        public static class GroupedListView
        {
            public static void GroupItem(ListView listView, ListViewItem listItem, ICategorizedListItem categorizedItem)
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
                    itemGroup.Name = group;
                    itemGroup.HeaderAlignment = HorizontalAlignment.Left;
                    listView.Groups.Add(itemGroup);
                }
                listView.ShowGroups = listView.Groups.Count > 1;
            }

            public static bool SingleItemSelected<T>(ListView listView) where T : class
            {
                if (listView.SelectedItems.Count != 1)
                    return false;

                T selection = listView.SelectedItems[0].Tag as T;

                return selection != null ? true : false;
            }

            public static bool ItemsSelected<T>(ListView listView) where T : class
            {
                return SelectedItems<T>(listView).Any();
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

            public static void Select(ListView listView, string itemKey)
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

            //public static void RemoveItems(ListView listView)
            //{
            //    listView.Items.Cast<ListViewItem>().Where(l => l.Selected)
            //        .ToList().ForEach(l => listView.Items.RemoveByKey(l.Name));
            //}

            public static void LoadAllItems<T>(ListView listView, T[] items, string[] categories, Func<ListView, T, bool, ListViewItem> create, bool reloadGroups = true) where T : ICategorizedListItem
            {
                if (reloadGroups)
                    Gui.GroupedListView.ReloadGroups(listView, categories);

                listView.Items.Clear();

                foreach (T item in items)
                {
                    ListViewItem listItem = create(listView, item, false);
                    GroupedListView.GroupItem(listView, listItem, item);
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

        public static class ToolBar
        {
            public static ToolStripButton CreateButton(ToolStrip toolstrip, string buttonText, Image image, bool showTextAndImage = false)
            {
                ToolStripButton btn = CreateButton(buttonText, image, null, showTextAndImage);
                toolstrip.Items.Add(btn);
                return btn;
            }

            public static ToolStripButton CreateButton(ToolStrip toolstrip, string buttonText, Image image, System.EventHandler handler, bool showTextAndImage = false)
            {
                ToolStripButton btn = CreateButton(buttonText, image, handler, showTextAndImage);
                toolstrip.Items.Add(btn);
                return btn;
            }

            public static ToolStripButton CreateButton(string buttonText, Image image, System.EventHandler handler, bool showTextAndImage = false)
            {
                ToolStripButton btn = new ToolStripButton();

                btn.DisplayStyle = showTextAndImage ? ToolStripItemDisplayStyle.ImageAndText : ToolStripItemDisplayStyle.Image;
                btn.Image = image;
                btn.ImageTransparentColor = Color.Magenta;
                btn.Name = CreateControlName(buttonText, "btn");
                btn.Size = new Size(24, 24);
                btn.Text = buttonText;
                btn.Click += handler;

                return btn;
            }

            private static string CreateControlName(string caption, string prefix = "", string postfix = "")
            {
                string legalName = caption.Replace(" ", "");
                legalName = legalName.Trim();
                return prefix + legalName + postfix;
            }
        }
    }
}
