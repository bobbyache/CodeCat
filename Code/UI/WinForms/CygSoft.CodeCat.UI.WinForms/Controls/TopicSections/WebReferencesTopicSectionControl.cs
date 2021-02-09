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
using CygSoft.CodeCat.UI.WinForms.UiHelpers;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class WebReferencesTopicSectionControl : BaseTopicSectionControl
    {
        private readonly ListViewSorter listViewSorter;
        public override int ImageKey { get { return IconRepository.Get(IconRepository.TopicSections.WebReferences).Index; } }
        public override Icon ImageIcon { get { return IconRepository.Get(IconRepository.TopicSections.WebReferences).Icon; } }
        public override Image IconImage { get { return IconRepository.Get(IconRepository.TopicSections.WebReferences).Image; } }
        private IWebReferencesTopicSection WebReferencesTopicSection => topicSection as IWebReferencesTopicSection;

        public WebReferencesTopicSectionControl() : this(null, null, null) { }

        public WebReferencesTopicSectionControl(AppFacade application, ITopicDocument topicDocument, IWebReferencesTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();

            Gui.ToolBar.CreateButton(HeaderToolstrip, "Delete", Constants.ImageKeys.DeleteSnippet, (s, e) => Delete());
            Gui.ToolBar.CreateButton(HeaderToolstrip, "Add", Constants.ImageKeys.AddSnippet, (s, e) => { Add(WebReferencesTopicSection.CreateWebReference()); });
            Gui.ToolBar.CreateButton(HeaderToolstrip, "Edit", Constants.ImageKeys.EditSnippet, (s, e) => Edit());

            listViewSorter = new ListViewSorter(listView);
            listView.Sorting = SortOrder.Ascending;

            listView.ColumnClick += (s, e) => listViewSorter.Sort(e.Column);
            listView.MouseUp += Listview_MouseUp;
            mnuCopy.Click += MnuCopy_Click;
            mnuPaste.Click += MnuPaste_Click;
            mnuCopyUrl.Click += MnuCopyUrl_Click;
            mnuNavigate.Click += (s, e) => NavigateToUrl();
            mnuDelete.Click += (s, e) => Delete();
            mnuEdit.Click += (s, e) => Edit();

            ReloadListview();
        }

        private void NavigateToUrl()
        {
            try
            {
                var webReference = Gui.GroupedListView.SelectedItem<IWebReference>(listView);
                if (webReference != null)
                    System.Diagnostics.Process.Start(webReference.Url);
            }
            catch (Exception ex)
            {
                Gui.Dialogs.WebPageErrorMessageBox(this, ex);
            }
        }

        private void Listview_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int cnt = listView.SelectedItems.Count;
                bool onItem = false;

                if (listView.FocusedItem != null)
                    onItem = listView.FocusedItem.Bounds.Contains(e.Location);

                mnuPaste.Enabled = Clipboard.ContainsText();
                mnuCopy.Enabled = cnt > 0 && onItem;
                mnuNavigate.Enabled = cnt == 1 && onItem;
                mnuEdit.Enabled = cnt == 1 && onItem;
                mnuDelete.Enabled = cnt >= 1;

                contextMenu.Show(Cursor.Position);
            }
        }

        private void MnuCopy_Click(object sender, EventArgs e)
        {
            var ids = listView.SelectedItems.Cast<ListViewItem>()
                .Select(lv => lv.Tag).Cast<IWebReference>()
                .Select(url => url.Id).ToArray();

            var copyXml = WebReferencesTopicSection.GetXml(ids);

            Clipboard.Clear();
            Clipboard.SetText(copyXml);
        }

        private void MnuPaste_Click(object sender, EventArgs e)
        {
            try
            {
                if (Clipboard.ContainsText())
                {
                    var text = Clipboard.GetText().Trim();

                    if (WebReferencesTopicSection.IsFullUrl(text))
                    {
                        var webReference = WebReferencesTopicSection.CreateWebReference(text, "", "");
                        Add(webReference);
                    }
                    else if (WebReferencesTopicSection.IsValidWebReferenceXml(text))
                        AddXml(text);
                    else
                    {
                        var firstLine = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0];
                        var webReference = WebReferencesTopicSection.CreateWebReference("", firstLine, "");
                        Add(webReference);
                    }
                }
            }
            catch (Exception ex)
            {
                Gui.Dialogs.PasteUrlErrorDialogMessageBox(this, ex);
            }
        }

        private void MnuCopyUrl_Click(object sender, EventArgs e)
        {
            try
            {
                var webReference = Gui.GroupedListView.SelectedItem<IWebReference>(listView);
                if (webReference != null)
                {
                    Clipboard.Clear();
                    Clipboard.SetText(webReference.Url);
                }
            }
            catch (Exception ex)
            {
                Gui.Dialogs.UrlCopyErrorMessageBox(this, ex);
            }
        }

        private void AddXml(string xml)
        {
            WebReferencesTopicSection.AddXml(xml);
            ReloadListview();
            Modify();
        }

        private void Add(IWebReference webReference)
        {
            var dialog = new UrlItemEditDialog(webReference, WebReferencesTopicSection.Categories);
            var result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                WebReferencesTopicSection.Add(webReference);
                ReloadListview();
                Modify();
            }
        }

        private void Edit()
        {
            if (listView.SelectedItems.Count == 1)
            {
                var webReference = Gui.GroupedListView.SelectedItem<IWebReference>(listView);

                var dialog = new UrlItemEditDialog(webReference, WebReferencesTopicSection.Categories);
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
                var result = Gui.Dialogs.DeleteMultipleItemsMessageBox(this, "hyperlinks");

                if (result == DialogResult.Yes)
                {
                    IEnumerable<IWebReference> webReferences = Gui.GroupedListView.SelectedItems<IWebReference>(listView);
                    WebReferencesTopicSection.Remove(webReferences);
                    ReloadListview();

                    Modify();
                }
            }
        }

        private ListViewItem CreateListviewItem(ListView listView, IWebReference webReference, bool select = false)
        {
            var listItem = new ListViewItem
            {
                Name = webReference.Id,
                Tag = webReference,
                ToolTipText = webReference.Url,
                Text = webReference.Title
            };

            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, webReference.HostName));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, webReference.Description));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, webReference.DateCreated.ToShortDateString()));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, webReference.DateModified.ToShortDateString()));

            listView.Items.Add(listItem);

            return listItem;
        }

        private void ReloadListview()
        {
            Gui.GroupedListView.LoadAllItems<IWebReference>(this.listView, WebReferencesTopicSection.WebReferences,
                this.WebReferencesTopicSection.Categories, this.CreateListviewItem);

            listViewSorter.Sort(0);
        }
    }
}
