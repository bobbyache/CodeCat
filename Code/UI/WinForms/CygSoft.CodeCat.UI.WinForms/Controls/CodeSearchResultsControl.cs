using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Topics;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class CodeSearchResultsControl : UserControl
    {
        private ListViewSorter listViewSorter;
        private AppFacade application;

        public event EventHandler<SearchKeywordsModifiedEventArgs> KeywordsAdded;
        public event EventHandler<SearchKeywordsModifiedEventArgs> KeywordsRemoved;
        public event EventHandler<SearchDelimitedKeywordEventArgs> SearchExecuted;

        public event EventHandler<OpenSnippetEventArgs> OpenSnippet;
        public event EventHandler<SelectSnippetEventArgs> SelectSnippet;

        public AppFacade Application { set { this.application = value; } }
        public bool SingleSnippetSelected { get { return this.listView.SelectedItems.Count == 1; } }
        public bool MultipleSnippetsSelected { get { return this.listView.SelectedItems.Count > 1; } }
        
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

        public IKeywordIndexItem SelectedSnippet
        {
            get
            {
                if (SingleSnippetSelected)
                    return SelectedItem(listView);
                return null;
            }
        }

        public CodeSearchResultsControl()
        {
            InitializeComponent();
            listView.SmallImageList = IconRepository.ImageList;
            listViewSorter = new ListViewSorter(this.listView);
        }

        public void ExecuteSearch(string keywords)
        {
            IKeywordIndexItem[] indexItems = this.application.FindIndeces(keywords);
            ReloadListview(indexItems);
            SearchExecuted?.Invoke(this, new SearchDelimitedKeywordEventArgs(keywords, indexItems.Length));
        }

        private void ReloadListview(IKeywordIndexItem[] indexItems)
        {
            listView.Items.Clear();
            foreach (IKeywordIndexItem item in indexItems)
                CreateListviewItem(listView, item);

            listViewSorter.Sort(0);
        }

        private void CreateListviewItem(ListView listView, IKeywordIndexItem keywordIndexItem, bool select = false)
        {
            ListViewItem listItem = new ListViewItem();
            
            listItem.Name = keywordIndexItem.Id;
            listItem.Tag = keywordIndexItem;
            listItem.Text = keywordIndexItem.Title;
            listItem.ImageKey = GetImageKey(keywordIndexItem);
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, keywordIndexItem.DateCreated.ToShortDateString()));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, keywordIndexItem.DateModified.ToShortDateString()));
            listView.Items.Add(listItem);

            if (select)
            {
                listItem.Selected = true;
                listItem.Focused = true;
                listItem.EnsureVisible();
            }
        }

        private string GetImageKey(IKeywordIndexItem item)
        {
            string imageKey = null;

            if (item is ICodeKeywordIndexItem)
                imageKey = (item as ICodeKeywordIndexItem).Syntax;

            else if (item is IQikTemplateKeywordIndexItem)
                imageKey = IconRepository.TopicSections.QikGroup;

            else if (item is ITopicKeywordIndexItem)
                imageKey = IconRepository.TopicSections.CodeGroup;

            return imageKey;
        }

        private void OpenSelectedSnippet()
        {
            IKeywordIndexItem codeItem = SelectedItem(listView);

            if (codeItem != null)
                OpenSnippet?.Invoke(this, new OpenSnippetEventArgs(codeItem));
        }

        private IKeywordIndexItem SelectedItem(ListView listView)
        {
            if (listView.SelectedItems.Count == 1)
                return listView.SelectedItems[0].Tag as IKeywordIndexItem;
            
            return null;
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

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewSorter.Sort(e.Column);
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenSelectedSnippet();
        }

        private void menuContextViewSnippet_Click(object sender, EventArgs e)
        {
            OpenSelectedSnippet();
        }

        private void menuContextViewKeywords_Click(object sender, EventArgs e)
        {
            SelectKeywordsDialog frm = new SelectKeywordsDialog();
            frm.Text = "View Keywords";
            IKeywordIndexItem[] IndexItems = this.SelectedSnippets;
            frm.Keywords = application.AllKeywords(IndexItems);
            DialogResult result = frm.ShowDialog(this);
        }

        private void menuContextAddKeywords_Click(object sender, EventArgs e)
        {
            EnterKeywordsDialog frm = new EnterKeywordsDialog();
            DialogResult result = frm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                string delimitedKeywordList = frm.Keywords;
                IKeywordIndexItem[] indexItems = this.SelectedSnippets;
                application.AddKeywords(indexItems, delimitedKeywordList);
                KeywordsAdded?.Invoke(this, new SearchKeywordsModifiedEventArgs(delimitedKeywordList, indexItems));
            }
        }

        private void menuContextRemoveKeywords_Click(object sender, EventArgs e)
        {
            SelectKeywordsDialog frm = new SelectKeywordsDialog();
            frm.Text = "Remove Keywords";
            IKeywordIndexItem[] indexItems = this.SelectedSnippets;
            frm.Keywords = application.AllKeywords(indexItems);

            DialogResult result = frm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                string[] keywords = frm.Keywords;
                IKeywordIndexItem[] invalidItems;

                bool validated = application.RemoveKeywords(indexItems, keywords, out invalidItems);

                if (validated)
                {
                    string delimitedKeywordList = application.KeywordArrayToDelimitedText(keywords);
                    KeywordsRemoved?.Invoke(this, new SearchKeywordsModifiedEventArgs(delimitedKeywordList, indexItems));
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

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            IKeywordIndexItem codeItem = SelectedItem(listView);
            SelectSnippet?.Invoke(this, new SelectSnippetEventArgs(codeItem));
        }

        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                EnableContextMenuItems();
                if (listView.FocusedItem.Bounds.Contains(e.Location) == true)
                    contextMenu.Show(Cursor.Position);
            }
        }
    }
}
