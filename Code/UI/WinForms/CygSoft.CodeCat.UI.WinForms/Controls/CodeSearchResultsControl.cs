using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using CygSoft.CodeCat.Domain;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class CodeSearchResultsControl : UserControl
    {
        private ListViewSorter listViewSorter;
        private AppFacade application;

        public event EventHandler<SearchKeywordsModifiedEventArgs> KeywordsAdded;
        public event EventHandler<SearchKeywordsModifiedEventArgs> KeywordsRemoved;

        public event EventHandler<OpenSnippetEventArgs> OpenSnippet;
        public event EventHandler<SelectSnippetEventArgs> SelectSnippet;


        public AppFacade Application
        {
            set { this.application = value; }
        }

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
            this.listViewSorter = new ListViewSorter(this.listView);
        }

        public void ExecuteSearch(string keywords)
        {
            IKeywordIndexItem[] indexItems = this.application.FindIndeces(keywords);
            this.ReloadListview(indexItems);
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
            else if (item is IQikTemplateKeywordIndexItem)
            {
                IQikTemplateKeywordIndexItem codeItem = item as IQikTemplateKeywordIndexItem;
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
                listItem.ImageKey = IconRepository.CodeGroupKey;
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

        private void OpenSelectedSnippet()
        {
            IKeywordIndexItem codeItem = SelectedItem(listView);

            if (codeItem != null)
            {
                if (OpenSnippet != null)
                    OpenSnippet(this, new OpenSnippetEventArgs(codeItem));
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
            SelectKeywordsDialog frm = new SelectKeywordsDialog();
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

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            IKeywordIndexItem codeItem = SelectedItem(listView);

            if (SelectSnippet != null)
                SelectSnippet(this, new SelectSnippetEventArgs(codeItem));
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
