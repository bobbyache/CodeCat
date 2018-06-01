using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using CygSoft.CodeCat.UI.WinForms.TopicSectionBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WebReferencesTopicSectionPlugin
{
    public partial class WebReferencesTopicSectionControl : BaseTopicSectionControl
    {
        private IListviewSorter listViewSorter;
        private IToolBarFunctions toolbarFunctions;
        private IListviewGrouper listViewGrouper;
        private IDialogFunctions dialogFunctions;

        private ToolStripButton btnEdit;
        private ToolStripButton btnAdd;
        private ToolStripButton btnDelete;

        private IWebReferencesTopicSection WebReferencesTopicSection
        {
            get { return topicSection as IWebReferencesTopicSection; }
        }

        public WebReferencesTopicSectionControl()
            : this(null, null, null, null)
        {

        }

        public WebReferencesTopicSectionControl(IAppFacade application, IImageResources imageResources, ITopicDocument topicDocument, IWebReferencesTopicSection topicSection)
            : base(application, imageResources, topicDocument, topicSection)
        {
            InitializeComponent();

            toolbarFunctions = controlFunctionFactory.CreateToolBarFunctions();
            dialogFunctions = controlFunctionFactory.CreateDialogFunctions("Application Title");
            listViewGrouper = controlFunctionFactory.CreateListviewGrouper();

        btnDelete = toolbarFunctions.CreateButton(HeaderToolstrip, "Delete", imageResources.GetImage(ImageKeys.DeleteSnippet), (s, e) => Delete());
            btnAdd = toolbarFunctions.CreateButton(HeaderToolstrip, "Add", imageResources.GetImage(ImageKeys.AddSnippet), (s, e) => { Add(WebReferencesTopicSection.CreateWebReference()); });
            btnEdit = toolbarFunctions.CreateButton(HeaderToolstrip, "Edit", imageResources.GetImage(ImageKeys.EditSnippet), (s, e) => Edit());

            listViewSorter = controlFunctionFactory.CreateListviewSorter(listView);
            listView.Sorting = SortOrder.Ascending;

            listView.ColumnClick += (s, e) => listViewSorter.Sort(e.Column);
            listView.MouseUp += listview_MouseUp;
            mnuCopy.Click += mnuCopy_Click;
            mnuPaste.Click += mnuPaste_Click;
            mnuCopyUrl.Click += mnuCopyUrl_Click;
            mnuNavigate.Click += (s, e) => NavigateToUrl();
            mnuDelete.Click += (s, e) => Delete();
            mnuEdit.Click += (s, e) => Edit();

            ReloadListview();
        }

        private void NavigateToUrl()
        {
            try
            {
                IWebReference webReference = listViewGrouper.SelectedItem<IWebReference>(listView);
                if (webReference != null)
                    System.Diagnostics.Process.Start(webReference.Url);
            }
            catch (Exception ex)
            {
                dialogFunctions.ExceptionMessageBox(this.ParentForm, ex, "An error occurred while trying to load the web page.");
            }
        }

        private void listview_MouseUp(object sender, MouseEventArgs e)
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

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            string[] ids = listView.SelectedItems.Cast<ListViewItem>()
                .Select(lv => lv.Tag).Cast<IWebReference>()
                .Select(url => url.Id).ToArray();

            string copyXml = WebReferencesTopicSection.GetXml(ids);
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
                    if (WebReferencesTopicSection.IsFullUrl(text))
                    {
                        IWebReference webReference = WebReferencesTopicSection.CreateWebReference(text, "", "");
                        Add(webReference);
                    }
                    else if (WebReferencesTopicSection.IsValidWebReferenceXml(text))
                        AddXml(text);
                    else
                    {
                        string firstLine = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0];
                        IWebReference webReference = WebReferencesTopicSection.CreateWebReference("", firstLine, "");
                        Add(webReference);
                    }
                }
            }
            catch (Exception ex)
            {
                dialogFunctions.ExceptionMessageBox(this.ParentForm, ex, "An error occurred while attempting to paste into this document.");
            }
        }

        private void mnuCopyUrl_Click(object sender, EventArgs e)
        {
            try
            {
                IWebReference webReference = listViewGrouper.SelectedItem<IWebReference>(listView);
                if (webReference != null)
                {
                    Clipboard.Clear();
                    Clipboard.SetText(webReference.Url);
                }
            }
            catch (Exception ex)
            {
                dialogFunctions.ExceptionMessageBox(this.ParentForm, ex, "An error occurred while trying to copy the URL.");
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
            UrlItemEditDialog dialog = new UrlItemEditDialog(dialogFunctions, webReference, WebReferencesTopicSection.Categories);
            DialogResult result = dialog.ShowDialog(this);

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
                IWebReference webReference = listViewGrouper.SelectedItem<IWebReference>(listView);

                UrlItemEditDialog dialog = new UrlItemEditDialog(dialogFunctions, webReference, WebReferencesTopicSection.Categories);
                DialogResult result = dialog.ShowDialog(this);

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
                DialogResult result = dialogFunctions.YesNoQuestionMessageBox(this.ParentForm, 
                    $"Sure you want to delete these hyperlinks?");

                if (result == DialogResult.Yes)
                {
                    IEnumerable<IWebReference> webReferences = listViewGrouper.SelectedItems<IWebReference>(listView);
                    WebReferencesTopicSection.Remove(webReferences);
                    ReloadListview();

                    Modify();
                }
            }
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

            listView.Items.Add(listItem);

            return listItem;
        }

        private void ReloadListview()
        {
            listViewGrouper.LoadAllItems<IWebReference>(this.listView, WebReferencesTopicSection.WebReferences,
                this.WebReferencesTopicSection.Categories, this.CreateListviewItem);

            listViewSorter.Sort(0);
        }
    }
}
