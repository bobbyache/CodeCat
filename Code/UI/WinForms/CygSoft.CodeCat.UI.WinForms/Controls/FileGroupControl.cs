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
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.Domain;
using System.Diagnostics;
using System.IO;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class FileGroupControl : UserControl, IDocumentItemControl
    {
        private IFileAttachmentsTopicSection fileAttachmentsTopicSection;
        private ICodeGroupDocumentSet codeGroupDocumentSet;
        private AppFacade application;
        private ListViewSorter listViewSorter;

        public FileGroupControl(AppFacade application, ICodeGroupDocumentSet codeGroupDocumentSet, IFileAttachmentsTopicSection fileAttachmentsTopicSection)
        {
            InitializeComponent();

            fileListview.SmallImageList = IconRepository.ImageList;
            this.listViewSorter = new ListViewSorter(this.fileListview);
            fileListview.Sorting = SortOrder.Ascending;

            btnAdd.Image = Resources.GetImage(Constants.ImageKeys.AddSnippet);
            btnDelete.Image = Resources.GetImage(Constants.ImageKeys.DeleteSnippet);
            btnEdit.Image = Resources.GetImage(Constants.ImageKeys.EditSnippet);

            this.fileAttachmentsTopicSection = fileAttachmentsTopicSection;
            this.codeGroupDocumentSet = codeGroupDocumentSet;
            this.application = application;
            this.Id = fileAttachmentsTopicSection.Id;

            ReloadGroups();
            LoadListOfUrls();

            txtTitle.TextChanged += (s, e) => { SetModified(); };
            codeGroupDocumentSet.BeforeSave += codeGroupDocumentSet_BeforeContentSaved;
            codeGroupDocumentSet.AfterSave += codeGroupDocumentSet_ContentSaved;
        }

        public event EventHandler Modified;

        public string Id { get; private set; }
        public string Title { get { return this.txtTitle.Text; } }
        public int ImageKey { get { return IconRepository.Get(IconRepository.Documents.FileSet).Index; } }
        public Icon ImageIcon { get { return IconRepository.Get(IconRepository.Documents.FileSet).Icon; } }
        public Image IconImage { get { return IconRepository.Get(IconRepository.Documents.FileSet).Image; } }

        public bool IsModified { get; private set; }

        public bool FileExists { get { return false; } }

        public void Revert()
        {
        }

        private void LoadListOfUrls()
        {
            txtTitle.Text = fileAttachmentsTopicSection.Title;
            ReloadListview(fileListview, fileAttachmentsTopicSection.Items);
            this.IsModified = false;
            SetChangeStatus();
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

        private void ReloadGroups()
        {
            this.fileListview.Groups.Clear();
            string[] categories = this.fileAttachmentsTopicSection.Categories;

            foreach (string category in categories)
            {
                ListViewGroup group = new ListViewGroup(category);
                group.HeaderAlignment = HorizontalAlignment.Left;
                this.fileListview.Groups.Add(group);
            }

            fileListview.ShowGroups = this.fileListview.Groups.Count > 1;
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

        private void codeGroupDocumentSet_ContentSaved(object sender, DocumentIndexEventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void codeGroupDocumentSet_BeforeContentSaved(object sender, DocumentIndexEventArgs e)
        {
            this.fileAttachmentsTopicSection.Title = txtTitle.Text;
        }

        private void SetChangeStatus()
        {
            lblEditStatus.Text = this.IsModified ? "Edited" : "Saved";
            lblEditStatus.ForeColor = this.IsModified ? Color.DarkRed : Color.Black;
        }

        private void SetModified()
        {
            this.IsModified = true;
            SetChangeStatus();
            Modified?.Invoke(this, new EventArgs());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FileGroupFileEditDialog dialog = new FileGroupFileEditDialog(fileAttachmentsTopicSection);
            DialogResult result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                fileAttachmentsTopicSection.Add(dialog.EditedFile);
                ReloadGroups();
                ReloadListview(fileListview, fileAttachmentsTopicSection.Items);
                SetModified();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditReference();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteReferences();
        }

        private void EditReference()
        {
            if (fileListview.SelectedItems.Count == 1)
            {
                // TODO: Instead of pulling the file object out of the ListViewItemTag we should be getting it from the FileGroup object. In fact, need to run a search on .Tag and see where you can improve this design.
                IFileAttachment fileAttachment = fileListview.SelectedItems[0].Tag as IFileAttachment;
                FileGroupFileEditDialog dialog = new FileGroupFileEditDialog(fileAttachment, fileAttachmentsTopicSection);
                DialogResult result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    ReloadGroups();
                    ReloadListview(fileListview, fileAttachmentsTopicSection.Items);
                    SetModified();
                }
            }
        }

        private void DeleteReferences()
        {
            if (fileListview.SelectedItems.Count >= 1)
            {
                DialogResult result = Dialogs.DeleteMultipleItemsDialog(this, "files");

                if (result == DialogResult.Yes)
                {
                    IEnumerable<IFileAttachment> fileAttachments = fileListview.SelectedItems.Cast<ListViewItem>()
                        .Select(lv => lv.Tag).Cast<IFileAttachment>();

                    foreach (IFileAttachment fileAttachment in fileAttachments)
                        fileAttachmentsTopicSection.Remove(fileAttachment);

                    ReloadGroups();
                    ReloadListview(fileListview, fileAttachmentsTopicSection.Items);
                    SetModified();
                }
            }
        }

        private IFileAttachment SelectedFileAttachment(ListView listView)
        {
            if (listView.SelectedItems.Count == 1)
                return listView.SelectedItems[0].Tag as IFileAttachment;
            
            return null;
        }

        private void urlListview_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int cnt = fileListview.SelectedItems.Count;
                bool onItem = false;
                IFileAttachment item = null;

                if (fileListview.FocusedItem != null)
                {
                    onItem = fileListview.FocusedItem.Bounds.Contains(e.Location);
                    item = fileListview.FocusedItem.Tag as IFileAttachment;
                }

                mnuOpen.Enabled = cnt == 1 && onItem && item.AllowOpenOrExecute;
                mnuOpenWith.Enabled = false && onItem && item.AllowOpenOrExecute;
                mnuSaveAs.Enabled = cnt == 1 && onItem;
                mnuEdit.Enabled = cnt == 1 && onItem;
                mnuDelete.Enabled = cnt >= 1;

                contextMenu.Show(Cursor.Position);
            }
        }

        private void mnuNavigate_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenFile()
        {
            try
            {
                IFileAttachment item = SelectedFileAttachment(fileListview);
                if (item != null)
                    item.Open();
            }
            catch (Exception ex)
            {
                Dialogs.WebPageErrorNotification(this, ex);
            }
        }

        private void mnuEdit_Click(object sender, EventArgs e)
        {
            EditReference();
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            DeleteReferences();
        }

        private void urlListview_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewSorter.Sort(e.Column);
        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            IFileAttachment fileAttachment = SelectedFileAttachment(fileListview);
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

        private void mnuEdit_Click_1(object sender, EventArgs e)
        {
            EditReference();
        }

        private void mnuDelete_Click_1(object sender, EventArgs e)
        {
            DeleteReferences();
        }
    }
}
