using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using CygSoft.CodeCat.Domain.TopicSections.SearchableSnippet;
using CygSoft.CodeCat.UI.WinForms.Dialogs;

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
            : this(null, null, null)
        {

        }

        public SearchableSnippetTopicSectionControl(AppFacade application, ITopicDocument topicDocument, ISearchableSnippetTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();

            if (topicDocument == null)
                return;

            syntaxBox.ReadOnly = true;
            listView.SmallImageList = IconRepository.ImageList;
            listViewSorter = new ListViewSorter(this.listView);
            listView.Sorting = SortOrder.Ascending;

            btnFind.Image = Gui.Resources.GetImage(Constants.ImageKeys.FindSnippets);

            btnDelete = Gui.ToolBar.CreateButton(HeaderToolstrip, "Delete", Constants.ImageKeys.DeleteSnippet, (s, e) => Delete());
            btnAdd = Gui.ToolBar.CreateButton(HeaderToolstrip, "Add", Constants.ImageKeys.AddSnippet, (s, e) => Add());
            btnEdit = Gui.ToolBar.CreateButton(HeaderToolstrip, "Edit", Constants.ImageKeys.EditSnippet, (s, e) => Edit());

            ReloadListview();

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
            ISearchableSnippetKeywordIndexItem newItem = SearchableSnippetTopicSection.NewSnippet(string.Empty);
            SearchableSnippetEditDialog dialog = new SearchableSnippetEditDialog(application, newItem, 
                SearchableSnippetTopicSection.Categories);
            DialogResult result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                SearchableSnippetTopicSection.AddSnippet(dialog.CodeSnippet);
                ReloadListview();
                Modify();
            }
        }

        private void Edit()
        {
            if (Gui.GroupedListView.SingleItemSelected<ISearchableSnippetKeywordIndexItem>(listView))
            {
                ISearchableSnippetKeywordIndexItem selectedItem = Gui.GroupedListView.SelectedItem<ISearchableSnippetKeywordIndexItem>(listView);

                SearchableSnippetEditDialog dialog = new SearchableSnippetEditDialog(application, selectedItem, 
                    SearchableSnippetTopicSection.Categories);
                DialogResult result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    ReloadListview();
                    Modify();
                }
            }
        }

        private void Delete()
        {
            if (Gui.GroupedListView.ItemsSelected< ISearchableSnippetKeywordIndexItem>(listView))
            {
                DialogResult result = Gui.Dialogs.DeleteMultipleItemsDialog(this, "snippets");

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
            Gui.GroupedListView.LoadAllItems(this.listView, SearchableSnippetTopicSection.SnippetList(),
                SearchableSnippetTopicSection.Categories, this.CreateListviewItem);

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

        private ToolStripButton CreateButton()
        {
            ToolStripButton btn = new ToolStripButton();
            btn.Alignment = ToolStripItemAlignment.Right;
            btn.Text = "Test";
            return btn;
        }
    }
}
