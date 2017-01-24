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
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.DocumentManager.Infrastructure;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class UrlGroupControl : UserControl, IDocumentItemControl
    {
        private IUrlGroupDocument urlDocument;
        private ICodeGroupDocumentSet codeGroupDocumentSet;
        private AppFacade application;
        private ListViewSorter listViewSorter;

        public UrlGroupControl(AppFacade application, ICodeGroupDocumentSet codeGroupDocumentSet, IUrlGroupDocument urlDocument)
        {
            InitializeComponent();

            this.listViewSorter = new ListViewSorter(this.urlListview);
            urlListview.Sorting = SortOrder.Ascending;

            btnAdd.Image = Resources.GetImage(Constants.ImageKeys.AddSnippet);
            btnDelete.Image = Resources.GetImage(Constants.ImageKeys.DeleteSnippet);
            btnEdit.Image = Resources.GetImage(Constants.ImageKeys.EditSnippet);

            this.urlDocument = urlDocument;
            this.codeGroupDocumentSet = codeGroupDocumentSet;
            this.application = application;
            this.Id = urlDocument.Id;

            ReloadGroups();
            LoadListOfUrls();

            txtTitle.TextChanged += (s, e) => { SetModified(); };
            codeGroupDocumentSet.BeforeSave += codeGroupDocumentSet_BeforeContentSaved;
            codeGroupDocumentSet.AfterSave += codeGroupDocumentSet_ContentSaved;
            urlDocument.Paste += urlDocument_Paste;
            urlDocument.PasteConflict += urlDocument_PasteConflict;
            this.Disposed += (s, e) =>
            {
                urlDocument.Paste -= urlDocument_Paste;
                urlDocument.PasteConflict -= urlDocument_PasteConflict;
            };
        }

        

        private void urlDocument_PasteConflict(object sender, EventArgs e)
        {
            Dialogs.UrlsPasteConflictDetected(this);
        }

        private void urlDocument_Paste(object sender, EventArgs e)
        {
            ReloadGroups();
            ReloadListview(urlListview, urlDocument.Items);
            SetModified();
            Dialogs.UrlsPastedSuccessfully(this);
        }

        public event EventHandler Modified;

        public string Id { get; private set; }

        public string Title
        {
            get { return this.txtTitle.Text; }
        }

        public int ImageKey { get { return IconRepository.Get(IconRepository.Documents.HyperlinkSet).Index; } }
        public Icon ImageIcon { get { return IconRepository.Get(IconRepository.Documents.HyperlinkSet).Icon; } }
        public Image IconImage { get { return IconRepository.Get(IconRepository.Documents.HyperlinkSet).Image; } }

        public bool IsModified { get; private set; }

        public bool FileExists { get { return false; } }

        public void Revert()
        {
        }

        private void LoadListOfUrls()
        {
            txtTitle.Text = urlDocument.Title;
            ReloadListview(urlListview, urlDocument.Items);
            this.IsModified = false;
            SetChangeStatus();
        }


        private void ReloadListview(ListView listView, IUrlItem[] indexItems)
        {
            listView.Items.Clear();
            foreach (IUrlItem item in indexItems)
            {
                ListViewItem listItem = CreateListviewItem(listView, item);
                GroupItem(listItem, item);
                listView.Items.Add(listItem);
            }

            listViewSorter.Sort(0);
        }

        private ListViewItem CreateListviewItem(ListView listView, IUrlItem item, bool select = false)
        {
            ListViewItem listItem = new ListViewItem();

            listItem.Name = item.Id;
            listItem.Tag = item;
            listItem.ToolTipText = item.Url;
            listItem.Text = item.Title;
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.HostName));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.Description));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateCreated.ToShortDateString()));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateModified.ToShortDateString()));
            
            return listItem;
        }

        private void ReloadGroups()
        {
            this.urlListview.Groups.Clear();
            string[] categories = this.urlDocument.Categories;
            foreach (string category in categories)
            {
                ListViewGroup group = new ListViewGroup(category);
                group.HeaderAlignment = HorizontalAlignment.Left;
                this.urlListview.Groups.Add(group);
            }
            urlListview.ShowGroups = this.urlListview.Groups.Count > 1;
        }

        private void GroupItem(ListViewItem listItem, IUrlItem item)
        {
            bool groupExists = false;

            foreach (ListViewGroup group in this.urlListview.Groups)
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
                this.urlListview.Groups.Add(group);
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
            this.urlDocument.Title = txtTitle.Text;
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
            IUrlItem item = urlDocument.CreateNewUrl();
            UrlItemEditDialog dialog = new UrlItemEditDialog(item, urlDocument.Categories);
            DialogResult result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                urlDocument.Add(item);
                ReloadGroups();
                ReloadListview(urlListview, urlDocument.Items);
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
            if (urlListview.SelectedItems.Count == 1)
            {
                IUrlItem item = urlListview.SelectedItems[0].Tag as IUrlItem;
                UrlItemEditDialog dialog = new UrlItemEditDialog(item, urlDocument.Categories);
                DialogResult result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    ReloadGroups();
                    ReloadListview(urlListview, urlDocument.Items);
                    SetModified();
                }
            }
        }

        private void DeleteReferences()
        {
            if (urlListview.SelectedItems.Count >= 1)
            {
                DialogResult result = Dialogs.DeleteMultipleItemsDialog(this, "hyperlinks");

                if (result == DialogResult.Yes)
                {
                    //IUrlItem item = urlListview.SelectedItems[0].Tag as IUrlItem;

                    IEnumerable<IUrlItem> items = urlListview.SelectedItems.Cast<ListViewItem>()
                        .Select(lv => lv.Tag).Cast<IUrlItem>();

                    foreach (IUrlItem item in items)
                        urlDocument.Remove(item);

                    ReloadGroups();
                    ReloadListview(urlListview, urlDocument.Items);
                    SetModified();
                }
            }
        }

        private void NavigateToUrl()
        {
            try
            {
                IUrlItem item = SelectedItem(urlListview);
                if (item != null)
                    System.Diagnostics.Process.Start(item.Url);
            }
            catch (Exception ex)
            {
                Dialogs.WebPageErrorNotification(this, ex);
            }
        }

        private IUrlItem SelectedItem(ListView listView)
        {
            if (listView.SelectedItems.Count == 1)
            {
                return listView.SelectedItems[0].Tag as IUrlItem;
            }
            return null;
        }

        private void urlListview_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int cnt = urlListview.SelectedItems.Count;
                bool onItem = false;

                if (urlListview.FocusedItem != null)
                    onItem = urlListview.FocusedItem.Bounds.Contains(e.Location);

                mnuPaste.Enabled = Clipboard.ContainsText();
                mnuCopy.Enabled = cnt > 0 && onItem;
                mnuNavigate.Enabled = cnt == 1 && onItem;
                mnuEdit.Enabled = cnt == 1 && onItem;
                mnuDelete.Enabled = cnt >= 1;

                contextMenu.Show(Cursor.Position);
            }
        }

        private void mnuNavigate_Click(object sender, EventArgs e)
        {
            NavigateToUrl();
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

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            string[] ids = urlListview.SelectedItems.Cast<ListViewItem>()
                .Select(lv => lv.Tag).Cast<IUrlItem>()
                .Select(url => url.Id).ToArray();

            string copyXml = urlDocument.CopyXmlFor(ids);
            Clipboard.Clear();
            Clipboard.SetText(copyXml);
        }

        private void mnuPaste_Click(object sender, EventArgs e)
        {
            try
            {
                string xml = Clipboard.GetText();
                urlDocument.PasteXml(xml);
            }
            catch (Exception ex)
            {
                Dialogs.PasteUrlErrorDialogPrompt(this, ex);
            }
        }

        private void mnuCopyUrl_Click(object sender, EventArgs e)
        {
            try
            {
                IUrlItem item = SelectedItem(urlListview);
                if (item != null)
                {
                    Clipboard.Clear();
                    Clipboard.SetText(item.Url);
                }
            }
            catch (Exception ex)
            {
                Dialogs.UrlCopyErrorNotification(this, ex);
            }
        }
    }
}
