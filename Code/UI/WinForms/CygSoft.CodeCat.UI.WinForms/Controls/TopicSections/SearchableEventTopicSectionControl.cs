using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.TopicSections.SearchableEventDiary;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.UI.Resources;
using CygSoft.CodeCat.UI.WinForms.Dialogs;
using CygSoft.CodeCat.UI.WinForms.TopicSectionBase;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class SearchableEventTopicSectionControl : BaseTopicSectionControl
    {
        private ToolStripButton btnEdit;
        private ToolStripButton btnAdd;
        private ToolStripButton btnDelete;

        public override int ImageKey { get { return iconRepository.Get(ImageResources.TopicSections.EventDiary).Index; } }
        public override Icon ImageIcon { get { return iconRepository.Get(ImageResources.TopicSections.EventDiary).Icon; } }
        public override Image IconImage { get { return iconRepository.Get(ImageResources.TopicSections.EventDiary).Image; } }

        private ISearchableEventTopicSection SearchableEventTopicSection
        {
            get { return base.topicSection as ISearchableEventTopicSection; }
        }

        public SearchableEventTopicSectionControl()
            : this(null, null, null, null, null)
        {

        }

        public SearchableEventTopicSectionControl(IAppFacade application, IImageResources imageResources, IIconRepository iconRepository, ITopicDocument topicDocument, ISearchableEventTopicSection topicSection)
            : base(application, imageResources, iconRepository, topicDocument, topicSection)
        {
            InitializeComponent();

            if (topicDocument == null)
                return;

            richTextBox.ReadOnly = true;

            listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView.SmallImageList = iconRepository.ImageList;
            listView.Sorting = SortOrder.Descending;

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

            listView.MouseUp += listView_MouseUp;
            listView.SelectedIndexChanged += (s, e) => DisplayRtf();

            Reverted += Base_Reverted;
            ContentSaved += Base_ContentSaved;
            UnregisterFieldEvents += Base_UnregisterFieldEvents;
            RegisterFieldEvents += Base_RegisterFieldEvents;

            if (listView.Items.Count > 0)
                listView.Items[0].Selected = true;
        }

        private void DisplayRtf()
        {
            if (!Gui.GroupedListView.SingleItemSelected<ISearchableEventKeywordIndexItem>(listView))
            {
                richTextBox.Text = string.Empty;
            }
            else
            {
                ISearchableEventKeywordIndexItem item = Gui.GroupedListView.SelectedItem<ISearchableEventKeywordIndexItem>(listView);
                if (item != null)
                {
                    richTextBox.Rtf = item.Text;
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

            ISearchableEventKeywordIndexItem newItem = SearchableEventTopicSection.NewEvent(string.Empty);
            SearchableEventEditDialog dialog = new SearchableEventEditDialog(application, newItem);
            DialogResult result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                SearchableEventTopicSection.AddEvent(dialog.DiaryEvent);
                ReloadListview();
                Modify();
                Gui.GroupedListView.Select(listView, dialog.DiaryEvent.Id);
            }
        }

        private void Edit()
        {
            if (Gui.GroupedListView.SingleItemSelected<ISearchableEventKeywordIndexItem>(listView))
            {
                ISearchableEventKeywordIndexItem selectedItem = Gui.GroupedListView.SelectedItem<ISearchableEventKeywordIndexItem>(listView);

                SearchableEventEditDialog dialog = new SearchableEventEditDialog(application, selectedItem);
                DialogResult result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    SearchableEventTopicSection.UpdateEvent(selectedItem);
                    ReloadListview();
                    Modify();
                    Gui.GroupedListView.Select(listView, dialog.DiaryEvent.Id);
                }
            }
        }

        private void Delete()
        {
            if (Gui.GroupedListView.ItemsSelected<ISearchableEventKeywordIndexItem>(listView))
            {
                DialogResult result = Gui.Dialogs.DeleteMultipleItemsMessageBox(this, "events");

                if (result == DialogResult.Yes)
                {
                    SearchableEventTopicSection.DeleteEvents(Gui.GroupedListView.SelectedItems<ISearchableEventKeywordIndexItem>(listView));
                    ReloadListview();
                    Modify();
                }
            }
        }

        private void ReloadListview()
        {
            string[] categories = SearchableEventTopicSection.Categories;
            Gui.GroupedListView.LoadAllItems(this.listView, SearchableEventTopicSection.Find(keywordsTextBox.Text),
                categories, this.CreateListviewItem);

            keywordsTextBox.ResetList(SearchableEventTopicSection.Keywords);
        }

        private ListViewItem CreateListviewItem(ListView listView, ISearchableEventKeywordIndexItem item, bool select)
        {
            ListViewItem listItem = new ListViewItem();

            listItem.ImageKey = null; //IconRepository.ImageList.Get; // item.Syntax;
            listItem.Name = item.Id;
            listItem.Tag = item;
            listItem.Text = item.DateCreated.ToString(); // item.Title;
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.Title));
            listView.Items.Add(listItem);

            return listItem;
        }

        private void listView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int cnt = listView.SelectedItems.Count;
                bool onItem = false;
                ISearchableEventKeywordIndexItem item = null;

                if (listView.FocusedItem != null)
                {
                    onItem = listView.FocusedItem.Bounds.Contains(e.Location);
                    item = listView.FocusedItem.Tag as ISearchableEventKeywordIndexItem;
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


        private void Base_RegisterFieldEvents(object sender, EventArgs e)
        {
            richTextBox.TextChanged += SetModified;
        }

        private void Base_UnregisterFieldEvents(object sender, EventArgs e)
        {
            richTextBox.TextChanged -= SetModified;
        }

        private void Base_Reverted(object sender, EventArgs e)
        {
            richTextBox.Text = string.Empty;
        }

        private void Base_ContentSaved(object sender, EventArgs e)
        {
            this.SearchableEventTopicSection.Text = string.Empty;
        }
    }
}
