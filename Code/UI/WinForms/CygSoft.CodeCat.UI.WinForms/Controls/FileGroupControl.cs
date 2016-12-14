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
        private IFileGroupDocument fileDocument;
        private ICodeGroupDocumentSet codeGroupDocumentSet;
        private AppFacade application;
        private ListViewSorter listViewSorter;

        public FileGroupControl(AppFacade application, ICodeGroupDocumentSet codeGroupDocumentSet, IFileGroupDocument fileDocument)
        {
            InitializeComponent();

            this.listViewSorter = new ListViewSorter(this.fileListview);
            fileListview.Sorting = SortOrder.Ascending;

            btnAdd.Image = Resources.GetImage(Constants.ImageKeys.AddSnippet);
            btnDelete.Image = Resources.GetImage(Constants.ImageKeys.DeleteSnippet);
            btnEdit.Image = Resources.GetImage(Constants.ImageKeys.EditSnippet);

            this.fileDocument = fileDocument;
            this.codeGroupDocumentSet = codeGroupDocumentSet;
            this.application = application;
            this.Id = fileDocument.Id;

            ReloadGroups();
            LoadListOfUrls();

            txtTitle.TextChanged += (s, e) => { SetModified(); };
            codeGroupDocumentSet.BeforeSave += codeGroupDocumentSet_BeforeContentSaved;
            codeGroupDocumentSet.AfterSave += codeGroupDocumentSet_ContentSaved;

            // unwire events...
            this.Disposed += (s, e) =>
            {
            };

        }

        public event EventHandler Modified;

        public string Id { get; private set; }

        public string Title
        {
            get { return this.txtTitle.Text; }
        }

        public int ImageKey { get { return IconRepository.ImageKeyFor("HTML"); } }

        public Icon ImageIcon { get { return IconRepository.GetIcon("HTML"); } }

        public Image IconImage { get { return IconRepository.GetImage("HTML"); } }

        public bool IsModified { get; private set; }

        public bool FileExists { get { return false; } }

        public void Revert()
        {
        }

        private void LoadListOfUrls()
        {
            txtTitle.Text = fileDocument.Title;
            ReloadListview(fileListview, fileDocument.Items);
            this.IsModified = false;
            SetChangeStatus();
        }


        private void ReloadListview(ListView listView, IFileGroupFile[] indexItems)
        {
            listView.Items.Clear();
            listView.SmallImageList = IconRepository.NewFileImageList(indexItems.Select(idx => idx.FileExtension).ToArray());

            foreach (IFileGroupFile item in indexItems)
            {
                ListViewItem listItem = CreateListviewItem(listView, item);
                GroupItem(listItem, item);
                listView.Items.Add(listItem);
            }

            listViewSorter.Sort(0);
        }

        private ListViewItem CreateListviewItem(ListView listView, IFileGroupFile item, bool select = false)
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
            string[] categories = this.fileDocument.Categories;
            foreach (string category in categories)
            {
                ListViewGroup group = new ListViewGroup(category);
                group.HeaderAlignment = HorizontalAlignment.Left;
                this.fileListview.Groups.Add(group);
            }
            fileListview.ShowGroups = this.fileListview.Groups.Count > 1;
        }

        private void GroupItem(ListViewItem listItem, IFileGroupFile item)
        {
            bool groupExists = false;

            foreach (ListViewGroup group in this.fileListview.Groups)
            {
                if (group.Header == item.Category)
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
                ListViewGroup group = new ListViewGroup(item.Category);
                group.HeaderAlignment = HorizontalAlignment.Left;
                this.fileListview.Groups.Add(group);
                listItem.Group = group;
            }
        }

        private void codeGroupDocumentSet_ContentSaved(object sender, FileEventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void codeGroupDocumentSet_BeforeContentSaved(object sender, FileEventArgs e)
        {
            this.fileDocument.Title = txtTitle.Text;
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

            if (this.Modified != null)
                this.Modified(this, new EventArgs());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FileGroupFileEditDialog dialog = new FileGroupFileEditDialog(fileDocument);
            DialogResult result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                fileDocument.Add(dialog.EditedFile);
                ReloadGroups();
                ReloadListview(fileListview, fileDocument.Items);
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
                IFileGroupFile item = fileListview.SelectedItems[0].Tag as IFileGroupFile;
                FileGroupFileEditDialog dialog = new FileGroupFileEditDialog(item, fileDocument);
                DialogResult result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    ReloadGroups();
                    ReloadListview(fileListview, fileDocument.Items);
                    SetModified();
                }
            }
        }

        private void DeleteReferences()
        {
            if (fileListview.SelectedItems.Count >= 1)
            {
                DialogResult result = Dialogs.DeleteUrlDialogPrompt(this);

                if (result == DialogResult.Yes)
                {
                    //IFileGroupFile item = urlListview.SelectedItems[0].Tag as IFileGroupFile;

                    IEnumerable<IFileGroupFile> items = fileListview.SelectedItems.Cast<ListViewItem>()
                        .Select(lv => lv.Tag).Cast<IFileGroupFile>();

                    foreach (IFileGroupFile item in items)
                        fileDocument.Remove(item);

                    ReloadGroups();
                    ReloadListview(fileListview, fileDocument.Items);
                    SetModified();
                }
            }
        }

        //private void NavigateToUrl()
        //{
        //    try
        //    {
        //        IFileGroupFile item = SelectedItem(urlListview);
        //        if (item != null)
        //            System.Diagnostics.Process.Start(item.Url);
        //    }
        //    catch (Exception ex)
        //    {
        //        Dialogs.WebPageErrorNotification(this, ex);
        //    }
        //}

        private IFileGroupFile SelectedItem(ListView listView)
        {
            if (listView.SelectedItems.Count == 1)
            {
                return listView.SelectedItems[0].Tag as IFileGroupFile;
            }
            return null;
        }

        private void urlListview_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int cnt = fileListview.SelectedItems.Count;
                bool onItem = false;

                if (fileListview.FocusedItem != null)
                    onItem = fileListview.FocusedItem.Bounds.Contains(e.Location);

                mnuOpenWith.Enabled = false;
                mnuSaveAs.Enabled = cnt == 1 && onItem;
                //mnuPaste.Enabled = Clipboard.ContainsText();
                //mnuCopy.Enabled = cnt > 0 && onItem;
                mnuOpen.Enabled = cnt == 1 && onItem;
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
                IFileGroupFile item = SelectedItem(fileListview);
                if (item != null)
                    System.Diagnostics.Process.Start(item.FilePath);
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

        private void mnuOpenWith_Click(object sender, EventArgs e)
        {
            //IFileGroupFile item = SelectedItem(fileListview);
            //if (item != null)
            //{
            //    OpenWithDialog dialog = new OpenWithDialog();
            //    dialog.Open(item.FilePath);
            //}
        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            IFileGroupFile item = SelectedItem(fileListview);
            if (item != null)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = string.Format("File *{0} (*{0})|*{0}", item.FileExtension);
                saveDialog.DefaultExt = "*{0}";
                saveDialog.Title = string.Format("Save File");
                saveDialog.AddExtension = true;
                saveDialog.FilterIndex = 0;
                saveDialog.OverwritePrompt = true;

                DialogResult result = saveDialog.ShowDialog(this);
                string filePath = saveDialog.FileName;

                if (result == DialogResult.OK)
                {
                    File.Copy(item.FilePath, saveDialog.FileName, true);
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
