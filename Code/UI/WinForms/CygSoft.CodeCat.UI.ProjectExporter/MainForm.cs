using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.Domain.Management;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.ProjectExporter
{
    public partial class MainForm : Form
    {
        private ProjManFacade projectManagement = null;
        private ListViewSorter listViewSorter;

        public MainForm()
        {
            InitializeComponent();

            this.listViewSorter = new ListViewSorter(this.listView);
            listView.Sorting = SortOrder.Ascending;
        }

        private string GetProjectPath()
        {
            string filePath;
            DialogResult result = OpenProjectFileDialog(this, "codecat", out filePath);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                return filePath;
            }
            return string.Empty;
        }

        private DialogResult OpenProjectFileDialog(IWin32Window owner, string projectFileExt, out string filePath)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = CreateProjectFileFilter(projectFileExt);
            openDialog.DefaultExt = "*.codecat";
            openDialog.Title = string.Format("Open Code Cat Project File");
            openDialog.AddExtension = true;
            openDialog.FilterIndex = 0;

            DialogResult result = openDialog.ShowDialog(owner);
            filePath = openDialog.FileName;

            return result;
        }

        private void ExecuteSearch()
        {
            IKeywordIndexItem[] indexItems = this.projectManagement.FindIndeces(txtKeywords.Text);
            this.ReloadListview(indexItems);

            //if (this.SearchExecuted != null)
            //    SearchExecuted(this, new SearchDelimitedKeywordEventArgs(keywordsTextBox.Text, indexItems.Length));
        }

        private void ReloadListview(IKeywordIndexItem[] indexItems)
        {
            listView.Items.Clear();
            foreach (IKeywordIndexItem item in indexItems)
                CreateListviewItem(listView, item);

            listViewSorter.Sort(0);
        }

        private void CreateListviewItem(ListView listView, IKeywordIndexItem item, bool select = false)
        {
            ListViewItem listItem = new ListViewItem();

            if (item is ICodeKeywordIndexItem)
            {
                ICodeKeywordIndexItem codeItem = item as ICodeKeywordIndexItem;
                listItem.Name = item.Id;
                listItem.Tag = item;
                listItem.Text = item.Title;
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateCreated.ToShortDateString()));
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateModified.ToShortDateString()));
                listView.Items.Add(listItem);
            }
            else if (item is IQikKeywordIndexItem)
            {
                IQikKeywordIndexItem codeItem = item as IQikKeywordIndexItem;
                listItem.Name = item.Id;
                listItem.Tag = item;
                listItem.Text = item.Title;
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateCreated.ToShortDateString()));
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateModified.ToShortDateString()));
                listView.Items.Add(listItem);
            }
            else if (item is ICodeGroupKeywordIndexItem)
            {
                ICodeGroupKeywordIndexItem codeItem = item as ICodeGroupKeywordIndexItem;
                listItem.Name = item.Id;
                listItem.Tag = item;
                listItem.Text = item.Title;
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateCreated.ToShortDateString()));
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateModified.ToShortDateString()));
                listView.Items.Add(listItem);
            }

            if (select)
            {
                listItem.Selected = true;
                listItem.Focused = true;
                listItem.EnsureVisible();
            }

        }
        private string CreateProjectFileFilter(string projectFileExt)
        {
            return string.Format("Code Cat Project Files *.{0} (*.{0})|*.{0}", projectFileExt);
        }

        private void btnBrowseSourcePath_Click(object sender, EventArgs e)
        {
            txtSourceProjectPath.Text = GetProjectPath();
        }

        private void btnBrowseDestinationPath_Click(object sender, EventArgs e)
        {
            txtDestinationProjectPath.Text = GetProjectPath();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            projectManagement = new ProjManFacade();
            projectManagement.LoadProjects(txtSourceProjectPath.Text, txtDestinationProjectPath.Text, 5);
            ReloadListview(projectManagement.FindIndeces(txtKeywords.Text));
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            ReloadListview(projectManagement.FindIndeces(txtKeywords.Text));
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (projectManagement != null)
            {
                if (projectManagement.Loaded)
                {
                    if (listView.CheckedItems.Count > 0)
                    {
                        List<IKeywordIndexItem> keywordIndexes = new List<IKeywordIndexItem>();

                        foreach (ListViewItem item in listView.CheckedItems)
                        {
                            keywordIndexes.Add(item.Tag as IKeywordIndexItem);
                        }
                        IndexExportImportData[] exportData = projectManagement.GetExportData(keywordIndexes.ToArray());
                        projectManagement.ImportData(exportData);
                    }
                }
            }
        }
    }
}
