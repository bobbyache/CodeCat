using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Topics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class FileAttachmentsTopicSectionControl : BaseTopicSectionControl
    {
        private ListViewSorter listViewSorter;

        private ToolStripButton btnEdit;
        private ToolStripButton btnAdd;
        private ToolStripButton btnDelete;

        public override int ImageKey { get { return IconRepository.Get(IconRepository.Documents.FileSet).Index; } }
        public override Icon ImageIcon { get { return IconRepository.Get(IconRepository.Documents.FileSet).Icon; } }
        public override Image IconImage { get { return IconRepository.Get(IconRepository.Documents.FileSet).Image; } }

        public FileAttachmentsTopicSectionControl()
            : this(null, null, null)
        {

        }

        public FileAttachmentsTopicSectionControl(AppFacade application, ITopicDocument topicDocument, IFileAttachmentsTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();
            
            btnDelete = Ui.ToolBar.CreateButton(HeaderToolstrip, "Delete", Constants.ImageKeys.DeleteSnippet, (s, e) => Delete());
            btnAdd = Ui.ToolBar.CreateButton(HeaderToolstrip, "Add", Constants.ImageKeys.AddSnippet, (s, e) => Add());
            btnEdit = Ui.ToolBar.CreateButton(HeaderToolstrip, "Edit", Constants.ImageKeys.EditSnippet, (s, e) => Edit());

            listView.SmallImageList = IconRepository.ImageList;
            listViewSorter = new ListViewSorter(this.listView);
            listView.Sorting = SortOrder.Ascending;

            btnAdd.Image = Resources.GetImage(Constants.ImageKeys.AddSnippet);
            btnDelete.Image = Resources.GetImage(Constants.ImageKeys.DeleteSnippet);
            btnEdit.Image = Resources.GetImage(Constants.ImageKeys.EditSnippet);

            ReloadListview(listView, FileAttachmentsTopicSection().Items, false);

            listView.ColumnClick += (s, e) => listViewSorter.Sort(e.Column);
            listView.MouseUp += listView_MouseUp;
            mnuEdit.Click += (s, e) => Edit();
            mnuDelete.Click += (s, e) => Delete();
            mnuSaveAs.Click += (s, e) => SaveAs();
            mnuOpen.Click += (s, e) => OpenFile();
        }

        private void OpenFile()
        {
            try
            {
                IFileAttachment item = Ui.GroupedListView.SelectedItem<IFileAttachment>(listView);
                if (item != null)
                    item.Open();
            }
            catch (Exception ex)
            {
                Dialogs.WebPageErrorNotification(this, ex);
            }
        }

        private void SaveAs()
        {
            IFileAttachment fileAttachment = Ui.GroupedListView.SelectedItem<IFileAttachment>(listView);

            if (fileAttachment != null)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = string.Format("File *{0} (*{0})|*{0}", fileAttachment.FileExtension);
                saveDialog.DefaultExt = "*{0}";
                saveDialog.Title = string.Format("Save File");
                saveDialog.AddExtension = true;
                saveDialog.FilterIndex = 0;
                saveDialog.OverwritePrompt = true;
                saveDialog.FileName = fileAttachment.FileName;

                DialogResult result = saveDialog.ShowDialog(this);
                string filePath = saveDialog.FileName;

                if (result == DialogResult.OK)
                {
                    File.Copy(fileAttachment.FilePath, saveDialog.FileName, true);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void listView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int cnt = listView.SelectedItems.Count;
                bool onItem = false;
                IFileAttachment item = null;

                if (listView.FocusedItem != null)
                {
                    onItem = listView.FocusedItem.Bounds.Contains(e.Location);
                    item = listView.FocusedItem.Tag as IFileAttachment;
                }

                bool fileExists = item.FileExists;
                mnuOpen.Enabled = cnt == 1 && onItem && item.AllowOpenOrExecute && fileExists;
                mnuOpenWith.Enabled = false && onItem && item.AllowOpenOrExecute && fileExists;
                mnuSaveAs.Enabled = cnt == 1 && onItem && fileExists;
                mnuEdit.Enabled = cnt == 1 && onItem;
                mnuDelete.Enabled = cnt >= 1;

                contextMenu.Show(Cursor.Position);
            }
        }

        private void Add()
        {
            FileGroupFileEditDialog dialog = new FileGroupFileEditDialog(FileAttachmentsTopicSection());
            DialogResult result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                FileAttachmentsTopicSection().Add(dialog.EditedFile);
                ReloadListview(listView, FileAttachmentsTopicSection().Items, true);
                Modify();
            }
        }

        private void Edit()
        {
            if (listView.SelectedItems.Count == 1)
            {
                IFileAttachment fileAttachment = Ui.GroupedListView.SelectedItem<IFileAttachment>(listView);

                FileGroupFileEditDialog dialog = new FileGroupFileEditDialog(fileAttachment, FileAttachmentsTopicSection());
                DialogResult result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    ReloadListview(listView, FileAttachmentsTopicSection().Items, true);
                    Modify();
                }
            }
        }

        private void Delete()
        {
            if (listView.SelectedItems.Count >= 1)
            {
                DialogResult result = Dialogs.DeleteMultipleItemsDialog(this, "files");

                if (result == DialogResult.Yes)
                {
                    IEnumerable<IFileAttachment> fileAttachments = Ui.GroupedListView.SelectedItems<IFileAttachment>(listView);
                    FileAttachmentsTopicSection().Remove(fileAttachments);
                    Ui.GroupedListView.RemoveItems(listView);

                    Modify();
                }
            }
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

            listView.Items.Add(listItem);

            return listItem;
        }

        private void ReloadListview(ListView listView, IFileAttachment[] fileAttachments, bool reloadGroups = true)
        {
            if (reloadGroups)
                Ui.GroupedListView.ReloadGroups(this.listView, this.FileAttachmentsTopicSection().Categories);

            IconRepository.AddFileExtensions(fileAttachments.Select(idx => idx.FileExtension));
            Ui.GroupedListView.LoadAllItems<IFileAttachment>(this.listView, fileAttachments, this.CreateListviewItem);

            listViewSorter.Sort(0);
        }

        private IFileAttachmentsTopicSection FileAttachmentsTopicSection()
        {
            return topicSection as IFileAttachmentsTopicSection;
        }

    }
}
