using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.TopicSections.SearchableSnippet;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.UI.WinForms.Dialogs;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class SearchableSnippetTopicSectionControl : BaseCodeTopicSectionControl
    {
        private ListViewSorter listViewSorter;

        private ToolStripButton btnEdit;
        private ToolStripButton btnAdd;
        private ToolStripButton btnDelete;

        public string SyntaxFile { get { return application.GetSyntaxFile(base.Syntax); } }

        private ISearchableSnippetTopicSection SearchableSnippetTopicSection
        {
            get { return base.topicSection as ISearchableSnippetTopicSection; }
        }

        public SearchableSnippetTopicSectionControl()
            : this(null, null, null, null)
        {

        }

        public SearchableSnippetTopicSectionControl(IAppFacade application, IImageResources imageResources, ITopicDocument topicDocument, ISearchableSnippetTopicSection topicSection)
            : base(application, imageResources, topicDocument, topicSection)
        {
            InitializeComponent();

            if (topicDocument == null)
                return;

            syntaxBox.ReadOnly = true;
            listView.SmallImageList = imageResources.ImageList;
            listViewSorter = new ListViewSorter(this.listView);
            listView.Sorting = SortOrder.Ascending;

            btnFind.Image = imageResources.GetImage(ImageKeys.FindSnippets);
            btnFind.Click += (s, e) => ReloadListview();

            btnDelete = Gui.ToolBar.CreateButton(HeaderToolstrip, "Delete", imageResources.GetImage(ImageKeys.DeleteSnippet), (s, e) => Delete());
            btnAdd = Gui.ToolBar.CreateButton(HeaderToolstrip, "Add", imageResources.GetImage(ImageKeys.AddSnippet), (s, e) => Add());
            btnEdit = Gui.ToolBar.CreateButton(HeaderToolstrip, "Edit", imageResources.GetImage(ImageKeys.EditSnippet), (s, e) => Edit());

            keywordsTextBox.CurrentTermCommitted += (s, e) => ReloadListview();
            keywordsTextBox.DropDownList = lstAutoComplete;

            ReloadListview();

            mnuEdit.Click += (s, e) => Edit();
            mnuDelete.Click += (s, e) => Delete();
            mnuNew.Click += (s, e) => Add();
            mnuCopyCode.Click += (s, e) => CopyCodeToClipboard();
            
            listView.MouseUp += listView_MouseUp;
            listView.ColumnClick += (s, e) => listViewSorter.Sort(e.Column);
            listView.SelectedIndexChanged += (s, e) => DisplaySourceCode();

            FontModified += Base_FontModified;
            SyntaxModified += Base_SyntaxModified;
            Reverted += Base_Reverted;
            ContentSaved += Base_ContentSaved;
            UnregisterFieldEvents += Base_UnregisterFieldEvents;
            RegisterFieldEvents += Base_RegisterFieldEvents;

            if (listView.Items.Count > 0)
                listView.Items[0].Selected = true;
        }

        private void CopyCodeToClipboard()
        {
            if (Gui.GroupedListView.SingleItemSelected<ISearchableSnippetKeywordIndexItem>(listView))
            {
                ISearchableSnippetKeywordIndexItem item = Gui.GroupedListView.SelectedItem<ISearchableSnippetKeywordIndexItem>(listView);
                if (item != null)
                    Clipboard.SetText(item.Text);
            }
        }

        private void DisplaySourceCode()
        {
            if (!Gui.GroupedListView.SingleItemSelected<ISearchableSnippetKeywordIndexItem>(listView))
            {
                syntaxDocument.SyntaxFile = null;
                syntaxBox.Document.Text = string.Empty;
            }
            else
            {
                ISearchableSnippetKeywordIndexItem item = Gui.GroupedListView.SelectedItem<ISearchableSnippetKeywordIndexItem>(listView);
                if (item != null)
                {
                    syntaxDocument.SyntaxFile = application.GetSyntaxFile(item.Syntax);
                    syntaxBox.Document.Text = item.Text;
                }
            }
        }

        private void Add()
        {
            if (!this.FileExists)
            {
                Gui.Dialogs.MustSaveGroupBeforeActionMessageBox(this);
                return;
            }

            ISearchableSnippetKeywordIndexItem newItem = SearchableSnippetTopicSection.NewSnippet(string.Empty);
            SearchableSnippetEditDialog dialog = new SearchableSnippetEditDialog(application, imageResources, newItem, 
                SearchableSnippetTopicSection.Categories);
            DialogResult result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                SearchableSnippetTopicSection.AddSnippet(dialog.CodeSnippet);
                ReloadListview();
                Modify();
                Gui.GroupedListView.Select(listView, dialog.CodeSnippet.Id);
            }
        }

        private void Edit()
        {
            if (Gui.GroupedListView.SingleItemSelected<ISearchableSnippetKeywordIndexItem>(listView))
            {
                ISearchableSnippetKeywordIndexItem selectedItem = Gui.GroupedListView.SelectedItem<ISearchableSnippetKeywordIndexItem>(listView);

                SearchableSnippetEditDialog dialog = new SearchableSnippetEditDialog(application, imageResources, selectedItem, 
                    SearchableSnippetTopicSection.Categories);
                DialogResult result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    SearchableSnippetTopicSection.UpdateSnippet(selectedItem);
                    ReloadListview();
                    Modify();
                    Gui.GroupedListView.Select(listView, dialog.CodeSnippet.Id);
                }
            }
        }

        private void Delete()
        {
            if (Gui.GroupedListView.ItemsSelected<ISearchableSnippetKeywordIndexItem>(listView))
            {
                DialogResult result = Gui.Dialogs.DeleteMultipleItemsMessageBox(this, "snippets");

                if (result == DialogResult.Yes)
                {
                    SearchableSnippetTopicSection.DeleteSnippets(Gui.GroupedListView.SelectedItems<ISearchableSnippetKeywordIndexItem>(listView));
                    ReloadListview();
                    Modify();
                }
            }
        }

        private void ReloadListview()
        {
            string[] categories = SearchableSnippetTopicSection.Categories;
            Gui.GroupedListView.LoadAllItems(this.listView, SearchableSnippetTopicSection.Find(keywordsTextBox.Text),
                categories, this.CreateListviewItem);

            keywordsTextBox.ResetList(SearchableSnippetTopicSection.Keywords);
            listViewSorter.Sort(0, listViewSorter.SortingOrder);
        }

        private ListViewItem CreateListviewItem(ListView listView, ISearchableSnippetKeywordIndexItem item, bool select)
        {
            ListViewItem listItem = new ListViewItem();

            listItem.ImageKey = item.Syntax;
            listItem.Name = item.Id;
            listItem.Tag = item;
            listItem.Text = item.Title;
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.Syntax));
            listView.Items.Add(listItem);

            return listItem;
        }

        private void listView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int cnt = listView.SelectedItems.Count;
                bool onItem = false;
                ISearchableSnippetKeywordIndexItem item = null;

                if (listView.FocusedItem != null)
                {
                    onItem = listView.FocusedItem.Bounds.Contains(e.Location);
                    item = listView.FocusedItem.Tag as ISearchableSnippetKeywordIndexItem;
                }

                mnuEdit.Enabled = cnt == 1 && onItem;
                mnuDelete.Enabled = cnt >= 1;
                mnuNew.Enabled = true;

                mnuCopy.Enabled = cnt == 1 && onItem;
                mnuPaste.Enabled = true;
                mnuCopyCode.Enabled = cnt == 1 && onItem;

                contextMenu.Show(Cursor.Position);
            }
        }

        private void Base_SyntaxModified(object sender, EventArgs e)
        {
            if (syntaxBox.Document.SyntaxFile != SyntaxFile)
                syntaxBox.Document.SyntaxFile = SyntaxFile;
        }

        private void Base_FontModified(object sender, EventArgs e)
        {
            if (syntaxBox.FontSize != FontSize)
                syntaxBox.FontSize = FontSize;
        }

        private void Base_RegisterFieldEvents(object sender, EventArgs e)
        {
            syntaxBox.TextChanged += SetModified;
        }

        private void Base_UnregisterFieldEvents(object sender, EventArgs e)
        {
            syntaxBox.TextChanged -= SetModified;
        }

        private void Base_Reverted(object sender, EventArgs e)
        {
            syntaxBox.Document.Text = string.Empty;
        }

        private void Base_ContentSaved(object sender, EventArgs e)
        {
            this.SearchableSnippetTopicSection.Text = string.Empty;
            this.SearchableSnippetTopicSection.Syntax = Syntax;
        }
    }
}
