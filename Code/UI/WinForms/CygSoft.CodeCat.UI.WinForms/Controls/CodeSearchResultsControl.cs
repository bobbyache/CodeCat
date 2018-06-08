using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Graphics;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class CodeSearchResultsControl : UserControl
    {
        private ListViewSorter listViewSorter;
        private IAppFacade application;
        private IImageResources imageResources;

        public event EventHandler<SearchKeywordsModifiedEventArgs> KeywordsAdded;
        public event EventHandler<SearchKeywordsModifiedEventArgs> KeywordsRemoved;
        public event EventHandler<SearchDelimitedKeywordEventArgs> SearchExecuted;

        public event EventHandler<TopicIndexEventArgs> OpenTopic;
        public event EventHandler<TopicIndexEventArgs> DeleteTopic;
        public event EventHandler<TopicIndexEventArgs> SelectTopic;

        public IAppFacade Application { set { this.application = value; } }
        public bool SingleTopicSelected { get { return this.listView.SelectedItems.Count == 1; } }
        public bool MultipleTopicsSelected { get { return this.listView.SelectedItems.Count > 1; } }

        public IImageResources ImageResources
        {
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Image Repository is a required constructor parameter and cannot be null");

                this.imageResources = value;
                this.listView.SmallImageList = imageResources.ImageList;
            }
        }
        
        public IKeywordIndexItem[] SelectedTopics
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

        public IKeywordIndexItem SelectedTopic
        {
            get
            {
                if (SingleTopicSelected)
                    return SelectedItem(listView);
                return null;
            }
        }

        public CodeSearchResultsControl()
        {
            InitializeComponent();
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
            listItem.ImageKey = GetKeywordIndexItemImage(keywordIndexItem).ImageKey;
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

        private IImageOutput GetKeywordIndexItemImage(IKeywordIndexItem item)
        {
            string imageKey = null;

            if (item is ICodeKeywordIndexItem)
                imageKey = (item as ICodeKeywordIndexItem).Syntax;

            else if (item is ITopicKeywordIndexItem)
                imageKey = Resources.ImageResources.TopicSections.CodeGroup;

            return imageResources.GetKeywordIndexItemImage(imageKey);
        }

        private void DeleteSelectedTopic()
        {
            IKeywordIndexItem codeItem = SelectedItem(listView);

            if (codeItem != null)
                DeleteTopic?.Invoke(this, new TopicIndexEventArgs(codeItem));

            if (codeItem != null)
                listView.Items.Remove(listView.SelectedItems[0]);
        }

        private void OpenSelectedTopic()
        {
            IKeywordIndexItem codeItem = SelectedItem(listView);

            if (codeItem != null)
                OpenTopic?.Invoke(this, new TopicIndexEventArgs(codeItem));
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

            ctxMenuCopyIdentifier.Enabled = singleSelection;
            ctxMenuCopyKeywords.Enabled = true;
            ctxMenuViewTopic.Enabled = singleSelection;
            ctxMenuAddKeywords.Enabled = true;
            ctxMenuRemoveKeywords.Enabled = true;
        }

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewSorter.Sort(e.Column);
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenSelectedTopic();
        }

        private void ctxMenuViewTopic_Click(object sender, EventArgs e)
        {
            OpenSelectedTopic();
        }

        private void ctxMenuDeleteTopic_Click(object sender, EventArgs e)
        {
            DeleteSelectedTopic();
        }

        private void ctxMenuViewKeywords_Click(object sender, EventArgs e)
        {
            SelectKeywordsDialog frm = new SelectKeywordsDialog();
            frm.Text = "View Keywords";
            IKeywordIndexItem[] IndexItems = this.SelectedTopics;
            frm.Keywords = application.AllKeywords(IndexItems);
            DialogResult result = frm.ShowDialog(this);
        }

        private void ctxMenuAddKeywords_Click(object sender, EventArgs e)
        {
            EnterKeywordsDialog frm = new EnterKeywordsDialog();
            DialogResult result = frm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                string delimitedKeywordList = frm.Keywords;
                IKeywordIndexItem[] indexItems = this.SelectedTopics;
                application.AddKeywords(indexItems, delimitedKeywordList);
                KeywordsAdded?.Invoke(this, new SearchKeywordsModifiedEventArgs(delimitedKeywordList, indexItems));
            }
        }

        private void ctxMenuRemoveKeywords_Click(object sender, EventArgs e)
        {
            SelectKeywordsDialog frm = new SelectKeywordsDialog();
            frm.Text = "Remove Keywords";
            IKeywordIndexItem[] indexItems = this.SelectedTopics;
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

        private void ctxMenuCopyKeywords_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(application.CopyAllKeywords(this.SelectedTopics));
        }

        private void ctxMenuCopyIdentifier_Click(object sender, EventArgs e)
        {
            if (this.SelectedTopic != null)
            {
                Clipboard.Clear();
                Clipboard.SetText(this.SelectedTopic.Id);
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            IKeywordIndexItem codeItem = SelectedItem(listView);
            SelectTopic?.Invoke(this, new TopicIndexEventArgs(codeItem));
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
