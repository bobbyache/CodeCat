using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.DocumentManager.Infrastructure;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class FileAttachmentsTopicSectionControl : BaseTopicSectionControl
    {
        private ListViewSorter listViewSorter;

        private ToolStripButton btnEdit;
        private ToolStripButton btnAdd;
        private ToolStripButton btnDelete;


        public FileAttachmentsTopicSectionControl()
            : this(null, null, null)
        {

        }

        public FileAttachmentsTopicSectionControl(AppFacade application, ITopicDocument topicDocument, IFileAttachmentsTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();

            btnDelete = new ToolStripButton();
            btnEdit = new ToolStripButton();
            btnAdd = new ToolStripButton();

            CreateToolbarButton(btnAdd, Constants.ImageKeys.AddSnippet, "btnAdd", "Add", new System.EventHandler(this.btnAdd_Click));
            CreateToolbarButton(btnEdit, Constants.ImageKeys.EditSnippet, "btnEdit", "Edit", new System.EventHandler(this.btnEdit_Click));
            CreateToolbarButton(btnDelete, Constants.ImageKeys.DeleteSnippet, "btnDelete", "Delete", new System.EventHandler(this.btnDelete_Click));

            fileListview.SmallImageList = IconRepository.ImageList;
            listViewSorter = new ListViewSorter(this.fileListview);
            fileListview.Sorting = SortOrder.Ascending;

            btnAdd.Image = Resources.GetImage(Constants.ImageKeys.AddSnippet);
            btnDelete.Image = Resources.GetImage(Constants.ImageKeys.DeleteSnippet);
            btnEdit.Image = Resources.GetImage(Constants.ImageKeys.EditSnippet);

            ReloadListview(fileListview, FileAttachmentsTopicSection().Items);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void Add()
        {
            FileGroupFileEditDialog dialog = new FileGroupFileEditDialog(FileAttachmentsTopicSection());
            DialogResult result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                FileAttachmentsTopicSection().Add(dialog.EditedFile);
                ReloadGroups();
                ReloadListview(fileListview, FileAttachmentsTopicSection().Items);
                Modify();
            }
        }

        private void Edit()
        {
            if (fileListview.SelectedItems.Count == 1)
            {
                // TODO: Instead of pulling the file object out of the ListViewItemTag we should be getting it from the FileGroup object. 
                // In fact, need to run a search on .Tag and see where you can improve this design.
                IFileAttachment fileAttachment = fileListview.SelectedItems[0].Tag as IFileAttachment;
                FileGroupFileEditDialog dialog = new FileGroupFileEditDialog(fileAttachment, FileAttachmentsTopicSection());
                DialogResult result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    ReloadGroups();
                    ReloadListview(fileListview, FileAttachmentsTopicSection().Items);
                    Modify();
                }
            }
        }

        private void Delete()
        {
            if (fileListview.SelectedItems.Count >= 1)
            {
                DialogResult result = Dialogs.DeleteMultipleItemsDialog(this, "files");

                if (result == DialogResult.Yes)
                {
                    IEnumerable<IFileAttachment> fileAttachments = fileListview.SelectedItems.Cast<ListViewItem>()
                        .Select(lv => lv.Tag).Cast<IFileAttachment>();

                    foreach (IFileAttachment fileAttachment in fileAttachments)
                        FileAttachmentsTopicSection().Remove(fileAttachment);

                    ReloadGroups();
                    ReloadListview(fileListview, FileAttachmentsTopicSection().Items);
                    Modify();
                }
            }
        }

        private void CreateToolbarButton(ToolStripButton btn, string imageKey, string buttonName, string buttonText, System.EventHandler handler)
        {
            btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn.Image = Resources.GetImage(imageKey); ;
            btn.ImageTransparentColor = Color.Magenta;
            btn.Name = buttonName;
            btn.Size = new Size(24, 24);
            btn.Text = buttonText;
            btn.Click += handler;
        }

        private ListViewItem CreateListviewItem(ListView listView, IFileAttachment item, bool select = false)
        {
            ListViewItem listItem = new ListViewItem();

            listItem.Name = item.Id;
            listItem.ImageKey = item.FileExtension;
            listItem.Tag = item;
            listItem.ToolTipText = item.FileName;
            listItem.Text = item.Title;
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.Description));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.FileName));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateCreated.ToShortDateString()));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateModified.ToShortDateString()));

            return listItem;
        }

        private void ReloadListview(ListView listView, IFileAttachment[] fileAttachments)
        {
            listView.Items.Clear();
            IconRepository.AddFileExtensions(fileAttachments.Select(idx => idx.FileExtension));

            foreach (IFileAttachment fileAttachment in fileAttachments)
            {
                ListViewItem listItem = CreateListviewItem(listView, fileAttachment);
                GroupItem(listItem, fileAttachment);
                listView.Items.Add(listItem);
            }

            listViewSorter.Sort(0);
        }

        private void GroupItem(ListViewItem listItem, IFileAttachment fileAttachment)
        {
            bool groupExists = false;

            foreach (ListViewGroup group in this.fileListview.Groups)
            {
                if (group.Header == fileAttachment.Category)
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
                ListViewGroup group = new ListViewGroup(fileAttachment.Category);
                group.HeaderAlignment = HorizontalAlignment.Left;
                this.fileListview.Groups.Add(group);
                listItem.Group = group;
            }
        }

        private void ReloadGroups()
        {
            this.fileListview.Groups.Clear();
            string[] categories = FileAttachmentsTopicSection().Categories;

            foreach (string category in categories)
            {
                ListViewGroup group = new ListViewGroup(category);
                group.HeaderAlignment = HorizontalAlignment.Left;
                this.fileListview.Groups.Add(group);
            }

            fileListview.ShowGroups = this.fileListview.Groups.Count > 1;
        }

        private IFileAttachmentsTopicSection FileAttachmentsTopicSection()
        {
            return topicSection as IFileAttachmentsTopicSection;
        }

    }
}
