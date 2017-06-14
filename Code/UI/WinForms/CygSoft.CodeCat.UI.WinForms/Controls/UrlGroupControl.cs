using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Topics;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class UrlGroupControl : UserControl, ITopicSectionBaseControl
    {
        private IWebReferencesTopicSection topicSection;
        private ITopicDocument topicDocument;
        private AppFacade application;
        private ListViewSorter listViewSorter;

        public UrlGroupControl(AppFacade application, ITopicDocument topicDocument, IWebReferencesTopicSection topicSection)
        {
            InitializeComponent();

            listViewSorter = new ListViewSorter(urlListview);
            urlListview.Sorting = SortOrder.Ascending;

            btnAdd.Image = Resources.GetImage(Constants.ImageKeys.AddSnippet);
            btnDelete.Image = Resources.GetImage(Constants.ImageKeys.DeleteSnippet);
            btnEdit.Image = Resources.GetImage(Constants.ImageKeys.EditSnippet);

            this.topicSection = topicSection;
            this.topicDocument = topicDocument;
            this.application = application;
            Id = topicSection.Id;

            ReloadGroups();
            LoadListOfUrls();

            txtTitle.TextChanged += (s, e) => { SetModified(); };
            topicDocument.BeforeSave += codeGroupDocumentSet_BeforeContentSaved;
            topicDocument.AfterSave += codeGroupDocumentSet_ContentSaved;
            topicSection.Paste += urlDocument_Paste;
            topicSection.PasteConflict += urlDocument_PasteConflict;
            Disposed += (s, e) =>
            {
                topicSection.Paste -= urlDocument_Paste;
                topicSection.PasteConflict -= urlDocument_PasteConflict;
            };
        }

        

        private void urlDocument_PasteConflict(object sender, EventArgs e)
        {
            Dialogs.UrlsPasteConflictDetected(this);
        }

        private void urlDocument_Paste(object sender, EventArgs e)
        {
            ReloadGroups();
            ReloadListview(urlListview, topicSection.WebReferences);
            SetModified();
            Dialogs.UrlsPastedSuccessfully(this);
        }

        public event EventHandler Modified;

        public string Id { get; private set; }
        public string Title { get { return txtTitle.Text; } }
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
            txtTitle.Text = topicSection.Title;
            ReloadListview(urlListview, topicSection.WebReferences);
            IsModified = false;
            SetChangeStatus();
        }


        private void ReloadListview(ListView listView, IWebReference[] webReferences)
        {
            listView.Items.Clear();
            foreach (IWebReference webReference in webReferences)
            {
                ListViewItem listItem = CreateListviewItem(listView, webReference);
                GroupItem(listItem, webReference);
                listView.Items.Add(listItem);
            }

            listViewSorter.Sort(0);
        }

        private ListViewItem CreateListviewItem(ListView listView, IWebReference webReference, bool select = false)
        {
            ListViewItem listItem = new ListViewItem();

            listItem.Name = webReference.Id;
            listItem.Tag = webReference;
            listItem.ToolTipText = webReference.Url;
            listItem.Text = webReference.Title;
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, webReference.HostName));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, webReference.Description));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, webReference.DateCreated.ToShortDateString()));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, webReference.DateModified.ToShortDateString()));
            
            return listItem;
        }

        private void ReloadGroups()
        {
            urlListview.Groups.Clear();
            string[] categories = topicSection.Categories;
            foreach (string category in categories)
            {
                ListViewGroup group = new ListViewGroup(category);
                group.HeaderAlignment = HorizontalAlignment.Left;
                urlListview.Groups.Add(group);
            }
            urlListview.ShowGroups = urlListview.Groups.Count > 1;
        }

        private void GroupItem(ListViewItem listItem, IWebReference webReference)
        {
            bool groupExists = false;

            foreach (ListViewGroup group in urlListview.Groups)
            {
                if (group.Header == webReference.Category)
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
                ListViewGroup group = new ListViewGroup(webReference.Category);
                group.HeaderAlignment = HorizontalAlignment.Left;
                urlListview.Groups.Add(group);
                listItem.Group = group;
            }
        }

        private void codeGroupDocumentSet_ContentSaved(object sender, TopicEventArgs e)
        {
            IsModified = false;
            SetChangeStatus();
        }

        private void codeGroupDocumentSet_BeforeContentSaved(object sender, TopicEventArgs e)
        {
            topicSection.Title = txtTitle.Text;
        }

        private void SetChangeStatus()
        {
            lblEditStatus.Text = IsModified ? "Edited" : "Saved";
            lblEditStatus.ForeColor = IsModified ? Color.DarkRed : Color.Black;
        }

        private void SetModified()
        {
            IsModified = true;
            SetChangeStatus();
            Modified?.Invoke(this, new EventArgs());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            IWebReference webReference = topicSection.CreateWebReference();
            CreateReference(webReference);
        }

        private void CreateReference(IWebReference webReference)
        {
            
            UrlItemEditDialog dialog = new UrlItemEditDialog(webReference, topicSection.Categories);
            DialogResult result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                topicSection.Add(webReference);
                ReloadGroups();
                ReloadListview(urlListview, topicSection.WebReferences);
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
                IWebReference webReference = urlListview.SelectedItems[0].Tag as IWebReference;
                UrlItemEditDialog dialog = new UrlItemEditDialog(webReference, topicSection.Categories);
                DialogResult result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    ReloadGroups();
                    ReloadListview(urlListview, topicSection.WebReferences);
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

                    IEnumerable<IWebReference> webReferences = urlListview.SelectedItems.Cast<ListViewItem>()
                        .Select(lv => lv.Tag).Cast<IWebReference>();

                    foreach (IWebReference webReference in webReferences)
                        topicSection.Remove(webReference);

                    ReloadGroups();
                    ReloadListview(urlListview, topicSection.WebReferences);
                    SetModified();
                }
            }
        }

        private void NavigateToUrl()
        {
            try
            {
                IWebReference webReference = SelectedItem(urlListview);
                if (webReference != null)
                    System.Diagnostics.Process.Start(webReference.Url);
            }
            catch (Exception ex)
            {
                Dialogs.WebPageErrorNotification(this, ex);
            }
        }

        private IWebReference SelectedItem(ListView listView)
        {
            if (listView.SelectedItems.Count == 1)
            {
                return listView.SelectedItems[0].Tag as IWebReference;
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
                .Select(lv => lv.Tag).Cast<IWebReference>()
                .Select(url => url.Id).ToArray();

            string copyXml = topicSection.GetXml(ids);
            Clipboard.Clear();
            Clipboard.SetText(copyXml);
        }

        private void mnuPaste_Click(object sender, EventArgs e)
        {
            try
            {
                if (Clipboard.ContainsText())
                {
                    string text = Clipboard.GetText().Trim();
                    if (topicSection.IsFullUrl(text))
                    {
                        IWebReference webReference = topicSection.CreateWebReference(text, "", "");
                        CreateReference(webReference);
                    }
                    else if (topicSection.IsValidWebReferenceXml(text))
                        topicSection.AddXml(text);
                    else
                    {
                        string firstLine = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0];
                        IWebReference webReference = topicSection.CreateWebReference("", firstLine, "");
                        CreateReference(webReference);
                    }
                }
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
                IWebReference webReference = SelectedItem(urlListview);
                if (webReference != null)
                {
                    Clipboard.Clear();
                    Clipboard.SetText(webReference.Url);
                }
            }
            catch (Exception ex)
            {
                Dialogs.UrlCopyErrorNotification(this, ex);
            }
        }
    }
}
