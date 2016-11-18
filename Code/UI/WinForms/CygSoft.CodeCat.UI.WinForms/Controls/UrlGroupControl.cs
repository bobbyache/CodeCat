﻿using System;
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
        private ICodeGroupDocumentGroup codeGroupFile;
        private AppFacade application;
        private ListViewSorter listViewSorter;

        public UrlGroupControl(AppFacade application, ICodeGroupDocumentGroup codeGroupFile, IUrlGroupDocument urlDocument)
        {
            InitializeComponent();

            this.listViewSorter = new ListViewSorter(this.urlListview);
            urlListview.Sorting = SortOrder.Ascending;

            btnAdd.Image = Resources.GetImage(Constants.ImageKeys.AddSnippet);
            btnDelete.Image = Resources.GetImage(Constants.ImageKeys.DeleteSnippet);
            btnEdit.Image = Resources.GetImage(Constants.ImageKeys.EditSnippet);

            this.urlDocument = urlDocument;
            this.codeGroupFile = codeGroupFile;
            this.application = application;
            this.Id = urlDocument.Id;

            LoadListOfUrls();
            RegisterDataFieldEvents();
            RegisterFileEvents();
        }

        public event EventHandler Modified;

        public string Id { get; private set; }

        public string Title
        {
            get { return this.txtTitle.Text; }
        }

        public int ImageKey { get { return 0; } }

        public Icon ImageIcon { get { return IconRepository.GetIcon("TEXT"); } }

        public Image IconImage { get { return IconRepository.GetImage("TEXT"); } }

        public bool IsModified { get; private set; }

        public bool FileExists { get { return false; } }

        public void Revert()
        {
        }

        private void RegisterDataFieldEvents()
        {
            txtTitle.TextChanged += (s, e) => { SetModified(); };
        }

        private void RegisterFileEvents()
        {
            codeGroupFile.BeforeSave += codeGroupFile_BeforeContentSaved;
            codeGroupFile.AfterSave += codeGroupFile_ContentSaved;
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
                CreateListviewItem(listView, item);

            listViewSorter.Sort(0);
        }

        private void CreateListviewItem(ListView listView, IUrlItem item, bool select = false)
        {
            ListViewItem listItem = new ListViewItem();

            listItem.Name = item.Id;
            listItem.Tag = item;
            listItem.ForeColor = Color.DarkBlue;
            //listItem.ImageKey = IconRepository.IMG;
            listItem.Text = item.Title;
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.Description));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateCreated.ToShortDateString()));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateModified.ToShortDateString()));
            listView.Items.Add(listItem);
        }

        private void codeGroupFile_ContentSaved(object sender, FileEventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void codeGroupFile_BeforeContentSaved(object sender, FileEventArgs e)
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
            IUrlItem item = application.NewUrl();
            UrlItemEditDialog dialog = new UrlItemEditDialog(item);
            DialogResult result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                urlDocument.Add(item);
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
            DeleteReference();
        }

        private void EditReference()
        {
            if (urlListview.SelectedItems.Count == 1)
            {
                IUrlItem item = urlListview.SelectedItems[0].Tag as IUrlItem;
                UrlItemEditDialog dialog = new UrlItemEditDialog(item);
                DialogResult result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    ReloadListview(urlListview, urlDocument.Items);
                    SetModified();
                }
            }
        }

        private void DeleteReference()
        {
            if (urlListview.SelectedItems.Count == 1)
            {
                DialogResult result = Dialogs.DeleteUrlDialogPrompt(this);

                if (result == DialogResult.Yes)
                {
                    IUrlItem item = urlListview.SelectedItems[0].Tag as IUrlItem;
                    urlDocument.Remove(item);
                    ReloadListview(urlListview, urlDocument.Items);
                    SetModified();
                }
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

        private void urlListview_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (urlListview.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenu.Show(Cursor.Position);
                }
            } 
        }

        private void mnuNavigate_Click(object sender, EventArgs e)
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

        private void mnuEdit_Click(object sender, EventArgs e)
        {
            EditReference();
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            DeleteReference();
        }

        private void urlListview_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewSorter.Sort(e.Column);
        }
    }
}
