using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
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
        private readonly ListViewSorter listViewSorter;
        public override int ImageKey => IconRepository.Get(IconRepository.TopicSections.FileAttachments).Index;
        public override Icon ImageIcon => IconRepository.Get(IconRepository.TopicSections.FileAttachments).Icon;
        public override Image IconImage => IconRepository.Get(IconRepository.TopicSections.FileAttachments).Image;
        private IFileAttachmentsTopicSection FileAttachmentsTopicSection => topicSection as IFileAttachmentsTopicSection;
        public FileAttachmentsTopicSectionControl() : this(null, null, null) { }

        public FileAttachmentsTopicSectionControl(AppFacade application, ITopicDocument topicDocument, IFileAttachmentsTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();
            
            Gui.ToolBar.CreateButton(HeaderToolstrip, "Delete", Constants.ImageKeys.DeleteSnippet, (s, e) => Delete());
            Gui.ToolBar.CreateButton(HeaderToolstrip, "Add", Constants.ImageKeys.AddSnippet, (s, e) => Add());
            Gui.ToolBar.CreateButton(HeaderToolstrip, "Edit", Constants.ImageKeys.EditSnippet, (s, e) => Edit());

            listView.SmallImageList = IconRepository.ImageList;
            listViewSorter = new ListViewSorter(this.listView);
            listView.Sorting = SortOrder.Ascending;

            ReloadListview();

            listView.ColumnClick += (s, e) => listViewSorter.Sort(e.Column);
            listView.MouseUp += ListView_MouseUp;
            mnuEdit.Click += (s, e) => Edit();
            mnuDelete.Click += (s, e) => Delete();
            mnuSaveAs.Click += (s, e) => SaveAs();
            mnuOpen.Click += (s, e) => OpenFile();
        }

        private void OpenFile()
        {
            try
            {
                var item = Gui.GroupedListView.SelectedItem<IFileAttachment>(listView);
                if (item != null)
                    item.Open();
            }
            catch (Exception ex)
            {
                Gui.Dialogs.WebPageErrorMessageBox(this, ex);
            }
        }

        private void SaveAs()
        {
            var fileAttachment = Gui.GroupedListView.SelectedItem<IFileAttachment>(listView);

            if (fileAttachment != null)
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = string.Format("File *{0} (*{0})|*{0}", fileAttachment.FileExtension),
                    DefaultExt = "*{0}",
                    Title = string.Format("Save File"),
                    AddExtension = true,
                    FilterIndex = 0,
                    OverwritePrompt = true,
                    FileName = fileAttachment.FileName
                };

                var result = saveDialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    File.Copy(fileAttachment.FilePath, saveDialog.FileName, true);
                }
            }
        }

        private void ListView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int cnt = listView.SelectedItems.Count;
                if (cnt > 0)
                {
                    var onItem = false;
                    IFileAttachment item = null;

                    if (listView.FocusedItem != null)
                    {
                        onItem = listView.FocusedItem.Bounds.Contains(e.Location);
                        item = listView.FocusedItem.Tag as IFileAttachment;
                    }

                    var fileExists = item != null && item.FileExists;
                    mnuOpen.Enabled = cnt == 1 && onItem && item.AllowOpenOrExecute && fileExists;
                    mnuOpenWith.Enabled = false && onItem && item.AllowOpenOrExecute && fileExists;
                    mnuSaveAs.Enabled = cnt == 1 && onItem && fileExists;
                    mnuEdit.Enabled = cnt == 1 && onItem;
                    mnuDelete.Enabled = cnt >= 1;

                    contextMenu.Show(Cursor.Position);
                }
            }
        }

        private void Add()
        {
            var dialog = new FileGroupFileEditDialog(FileAttachmentsTopicSection);
            var result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                FileAttachmentsTopicSection.Add(dialog.EditedFile);
                ReloadListview();
                Modify();
            }
        }

        private void Edit()
        {
            if (listView.SelectedItems.Count == 1)
            {
                var fileAttachment = Gui.GroupedListView.SelectedItem<IFileAttachment>(listView);

                var dialog = new FileGroupFileEditDialog(fileAttachment, FileAttachmentsTopicSection);
                var result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    ReloadListview();
                    Modify();
                }
            }
        }

        private void Delete()
        {
            if (listView.SelectedItems.Count >= 1)
            {
                var result = Gui.Dialogs.DeleteMultipleItemsMessageBox(this, "files");

                if (result == DialogResult.Yes)
                {
                    IEnumerable<IFileAttachment> fileAttachments = Gui.GroupedListView.SelectedItems<IFileAttachment>(listView);
                    FileAttachmentsTopicSection.Remove(fileAttachments);
                    ReloadListview();

                    Modify();
                }
            }
        }

        private ListViewItem CreateListviewItem(ListView listView, IFileAttachment item, bool select = false)
        {
            var listItem = new ListViewItem
            {
                Name = item.Id,
                ImageKey = item.FileExtension,
                Tag = item,
                ToolTipText = item.FileName,
                Text = item.Title
            };
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.Description));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.FileName));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateCreated.ToShortDateString()));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateModified.ToShortDateString()));

            listView.Items.Add(listItem);

            return listItem;
        }

        private void ReloadListview()
        {
            IconRepository.AddFileExtensions(FileAttachmentsTopicSection.Items.Select(idx => idx.FileExtension));

            Gui.GroupedListView.LoadAllItems<IFileAttachment>(this.listView, FileAttachmentsTopicSection.Items,
                FileAttachmentsTopicSection.Categories, this.CreateListviewItem);

            listViewSorter.Sort(0);
        }
    }
}
