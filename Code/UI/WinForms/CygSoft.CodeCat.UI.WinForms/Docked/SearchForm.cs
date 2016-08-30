using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using CygSoft.CodeCat.UI.WinForms.Controls;
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
        private ListViewSorter listViewSorter;


        public event EventHandler<SearchKeywordsModifiedEventArgs> KeywordsAdded;
        public event EventHandler<SearchKeywordsModifiedEventArgs> KeywordsRemoved;

        public event EventHandler<SearchDelimitedKeywordEventArgs> SearchExecuted;
        public event EventHandler<OpenSnippetEventArgs> OpenSnippet;
        public event EventHandler<SelectSnippetEventArgs> SelectSnippet;

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

            this.listViewSorter = new ListViewSorter(this.listView);
            
            listView.Sorting = SortOrder.Ascending;

            this.application = application;
            this.HideOnClose = true;
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight;

            listView.SmallImageList = IconRepository.ImageList;
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

            listViewSorter.Sort(0);
        }

        private void CreateListviewItem(ListView listView, IKeywordIndexItem item, bool select = false)
        {
            ListViewItem listItem = new ListViewItem();

            if (item is ICodeKeywordIndexItem)
            {
                ICodeKeywordIndexItem codeItem = item as ICodeKeywordIndexItem;
                listItem.Name = item.Id;
                listItem.Tag = item;
                listItem.ImageKey = codeItem.Syntax;
                listItem.Text = item.Title;
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateCreated.ToShortDateString()));
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateModified.ToShortDateString()));
                listView.Items.Add(listItem);
            }
            else if (item is IQikKeywordIndexItem)
            {
                IQikKeywordIndexItem codeItem = item as IQikKeywordIndexItem;
                listItem.Name = item.Id;
                listItem.Tag = item;
                listItem.ImageKey = IconRepository.QikKey;
                listItem.Text = item.Title;
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateCreated.ToShortDateString()));
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateModified.ToShortDateString()));
                listView.Items.Add(listItem);
            }
            else if (item is ICodeGroupKeywordIndexItem)
            {
                ICodeGroupKeywordIndexItem codeItem = item as ICodeGroupKeywordIndexItem;
                listItem.Name = item.Id;
                listItem.Tag = item;
                listItem.ImageKey = IconRepository.QikKey;
                listItem.Text = item.Title;
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateCreated.ToShortDateString()));
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateModified.ToShortDateString()));
                listView.Items.Add(listItem);
            }

            if (select)
            {
                listItem.Selected = true;
                listItem.Focused = true;
                listItem.EnsureVisible();
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
                listViewSorter.Sort(e.Column);
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
