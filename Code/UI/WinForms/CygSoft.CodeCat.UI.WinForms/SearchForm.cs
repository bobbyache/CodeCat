using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class SearchForm : DockContent
    {
        private AppFacade application;
        private ImageList imageList = new ImageList();

        public event EventHandler<SearchKeywordsModifiedEventArgs> KeywordsAdded;
        public event EventHandler<SearchKeywordsModifiedEventArgs> KeywordsRemoved;

        public event EventHandler<SearchDelimitedKeywordEventArgs> SearchExecuted;
        public event EventHandler<OpenSnippetEventArgs> OpenSnippet;
        public event EventHandler<SelectSnippetEventArgs> SelectSnippet;

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

        public int SortingColumn { get; set; }

        private bool searchEnabled;
        public bool SearchEnabled
        {
            get { return this.searchEnabled; }
            set
            {
                this.searchEnabled = value;
                this.btnFind.Enabled = value;
            }

        }

        public string KeywordSearchText
        {
            get { return this.keywordsTextBox.Text; }
            set { this.keywordsTextBox.Text = value; }
        }

        public bool SingleSnippetSelected { get { return this.listView.SelectedItems.Count == 1; } }
        public bool MultipleSnippetsSelected { get { return this.listView.SelectedItems.Count > 1; } }

        public IKeywordIndexItem SelectedSnippet
        {
            get
            {
                if (SingleSnippetSelected)
                    return SelectedItem(listView);
                return null;
            }
        }

        public IKeywordIndexItem[] SelectedSnippets
        {
            get
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
        }

        public SearchForm(AppFacade application)
        {
            InitializeComponent();

            btnFind.Image = Resources.GetImage(Constants.ImageKeys.FindSnippets);

            this.application = application;
            this.HideOnClose = true;
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight;

            PopulateSystemIconsImageList();

            listView.SmallImageList = imageList;
            listView.ColumnClick += listView_ColumnClick;
            listView.SelectedIndexChanged += listView_SelectedIndexChanged;
            listView.MouseClick += listView_MouseClick;
            listView.MouseDoubleClick += listView_MouseDoubleClick;
            btnFind.Click += (s, e) => ExecuteSearch();
            keywordsTextBox.TextChanged += (s, e) => ExecuteSearch();
        }

        public void ExecuteSearch()
        {
            if (this.searchEnabled)
            {
                IKeywordIndexItem[] indexItems = this.application.FindIndeces(keywordsTextBox.Text);
                this.ReloadListview(indexItems);

                if (this.SearchExecuted != null)
                    SearchExecuted(this, new SearchDelimitedKeywordEventArgs(keywordsTextBox.Text, indexItems.Length));
            }
        }

        public void ExecuteSearch(string selectedId)
        {
            ExecuteSearch();
        }

        private void ReloadListview(IKeywordIndexItem[] indexItems)
        {

            listView.Items.Clear();
            foreach (IKeywordIndexItem item in indexItems)
                CreateListviewItem(listView, item);
        }

        private void CreateListviewItem(ListView listView, IKeywordIndexItem item, bool select = false)
        {
            IKeywordIndexItem codeItem = item as IKeywordIndexItem;
            ListViewItem listItem = new ListViewItem();
            listItem.Name = item.Id;
            listItem.Tag = item;
            listItem.ImageKey = codeItem.Syntax;
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

        private void PopulateSystemIconsImageList()
        {
            foreach (SyntaxFile syntaxFile in application.GetSyntaxFileInfo())
            {
                Icon icon = IconRepository.GetIcon(syntaxFile.Syntax);

                if (!imageList.Images.ContainsKey(syntaxFile.Syntax))
                {
                    imageList.Images.Add(syntaxFile.Syntax, icon);
                }
            }
        }

        private void EnableContextMenuItems()
        {
            bool singleSelection = listView.SelectedItems.Count == 1;

            menuContextCopyIdentifier.Enabled = singleSelection;
            menuContextCopyKeywords.Enabled = true;
            menuContextViewSnippet.Enabled = singleSelection;
            menuContextAddKeywords.Enabled = true;
            menuContextRemoveKeywords.Enabled = true;
        }

        private void SortColumn(int columnIndex)
        {
            if (columnIndex != SortingColumn)
            {
                // Set the sort column to the new column.
                this.SortingColumn = columnIndex;
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

        private void OpenSelectedSnippet()
        {
            if (this.searchEnabled)
            {
                IKeywordIndexItem codeItem = SelectedItem(listView);

                if (codeItem != null)
                {
                    if (OpenSnippet != null)
                        OpenSnippet(this, new OpenSnippetEventArgs(codeItem));
                }
            }
        }

        private IKeywordIndexItem SelectedItem(ListView listView)
        {
            if (listView.SelectedItems.Count == 1)
            {
                return listView.SelectedItems[0].Tag as IKeywordIndexItem;
            }
            return null;
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenSelectedSnippet();
        }

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.searchEnabled)
            {
                this.SortColumn(e.Column);
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.searchEnabled)
            {
                IKeywordIndexItem codeItem = SelectedItem(listView);

                if (SelectSnippet != null)
                    SelectSnippet(this, new SelectSnippetEventArgs(codeItem));
            }
        }

        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.searchEnabled)
            {
                if (e.Button == MouseButtons.Right)
                {
                    EnableContextMenuItems();
                    if (listView.FocusedItem.Bounds.Contains(e.Location) == true)
                        contextMenu.Show(Cursor.Position);
                }
            }
        }

        private void menuContextViewSnippet_Click(object sender, EventArgs e)
        {
            if (this.searchEnabled)
            {
                OpenSelectedSnippet();
            }
        }

        private void menuContextViewKeywords_Click(object sender, EventArgs e)
        {
            SelectKeywordsForm frm = new SelectKeywordsForm();
            frm.Text = "View Keywords";
            IKeywordIndexItem[] IndexItems = this.SelectedSnippets;
            frm.Keywords = application.AllKeywords(IndexItems);
            DialogResult result = frm.ShowDialog(this);
        }

        private void menuContextAddKeywords_Click(object sender, EventArgs e)
        {
            EnterKeywordsForm frm = new EnterKeywordsForm();
            DialogResult result = frm.ShowDialog(this);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string delimitedKeywordList = frm.Keywords;
                IKeywordIndexItem[] indexItems = this.SelectedSnippets;
                application.AddKeywords(indexItems, delimitedKeywordList);

                if (KeywordsAdded != null)
                    KeywordsAdded(this, new SearchKeywordsModifiedEventArgs(delimitedKeywordList, indexItems));
            }
        }

        private void menuContextRemoveKeywords_Click(object sender, EventArgs e)
        {
            SelectKeywordsForm frm = new SelectKeywordsForm();
            frm.Text = "Remove Keywords";
            IKeywordIndexItem[] indexItems = this.SelectedSnippets;
            frm.Keywords = application.AllKeywords(indexItems);
            
            DialogResult result = frm.ShowDialog(this);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string[] keywords = frm.Keywords;
                IKeywordIndexItem[] invalidItems;

                bool validated = application.RemoveKeywords(indexItems, keywords, out invalidItems);

                if (validated)
                {
                    string delimitedKeywordList = application.KeywordArrayToDelimitedText(keywords);

                    if (KeywordsRemoved != null)
                        KeywordsRemoved(this, new SearchKeywordsModifiedEventArgs(delimitedKeywordList, indexItems));
                }
                else
                {
                    InvalidRemoveDialog dialog = new InvalidRemoveDialog(invalidItems);
                    dialog.ShowDialog(this);
                }
            }
        }

        private void menuContextCopyKeywords_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(application.CopyAllKeywords(this.SelectedSnippets));
        }

        private void menuContextCopyIdentifier_Click(object sender, EventArgs e)
        {
            if (this.SelectedSnippet != null)
            {
                Clipboard.Clear();
                Clipboard.SetText(this.SelectedSnippet.Id);
            }
        }
    }
}
