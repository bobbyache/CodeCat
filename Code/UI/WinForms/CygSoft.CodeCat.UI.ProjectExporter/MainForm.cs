using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.Domain.Management;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.ProjectExporter
{
    public partial class MainForm : Form
    {
        private ProjManFacade projectManagement = null;
        private ListViewSorter listViewSorter;
        private int sortedColumn = 0;

        public MainForm()
        {
            InitializeComponent();

            this.listViewSorter = new ListViewSorter(this.listView);
            listView.Sorting = SortOrder.Ascending;
            EnableExecution();
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

        private void ReloadListview(IKeywordIndexItem[] indexItems)
        {
            listView.Items.Clear();
            foreach (IKeywordIndexItem item in indexItems)
                CreateListviewItem(listView, item);

            //listViewSorter.Sort(sortedColumn);
        }

        private void EnableExecution()
        {
            bool sourceExists = !string.IsNullOrEmpty(txtSourceProjectPath.Text.Trim()) && File.Exists(txtSourceProjectPath.Text.Trim());
            bool destExists = !string.IsNullOrEmpty(txtDestinationProjectPath.Text.Trim()) && File.Exists(txtDestinationProjectPath.Text.Trim());
            bool loaded = projectManagement != null && projectManagement.Loaded && sourceExists && destExists;
            bool okToExecute = loaded && listView.CheckedItems.Count > 0;

            btnOpen.Enabled = sourceExists && destExists;
            btnFind.Enabled = loaded;
            btnExecute.Enabled = loaded;
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
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, "Snippet"));
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateCreated.ToShortDateString()));
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.DateModified.ToShortDateString()));
                listView.Items.Add(listItem);
            }
            else if (item is IQikTemplateKeywordIndexItem)
            {
                IQikTemplateKeywordIndexItem codeItem = item as IQikTemplateKeywordIndexItem;
                listItem.Name = item.Id;
                listItem.Tag = item;
                listItem.Text = item.Title;
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, "Qik Template"));
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
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, "Code Group"));
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
            EnableExecution();
        }

        private void btnBrowseDestinationPath_Click(object sender, EventArgs e)
        {
            txtDestinationProjectPath.Text = GetProjectPath();
            EnableExecution();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Version version = GetVersion();

            projectManagement = new ProjManFacade();
            projectManagement.LoadProjects(txtSourceProjectPath.Text, txtDestinationProjectPath.Text, version);
            Find();
            EnableExecution();
        }

        private Version GetVersion()
        {
            Version version;
            if (Version.TryParse(txtVersion.Text.Trim(), out version))
                return version;
            return null;
        }

        private void Find()
        {
            bool hasPotentialDuplicates;
            IKeywordIndexItem[] potentialDuplicates;
            IKeywordIndexItem[] searchItems = projectManagement.FindIndeces(txtKeywords.Text, out hasPotentialDuplicates, out potentialDuplicates);

            ReloadListview(searchItems);
            DisplayDuplicates(potentialDuplicates);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Find();
        }

        private void DisplayDuplicates(IKeywordIndexItem[] potentialDuplicates)
        {
            Dictionary<string, IKeywordIndexItem> duplicateItemDictionary = new Dictionary<string, IKeywordIndexItem>();

            foreach (IKeywordIndexItem potentialDuplicate in potentialDuplicates)
            {
                duplicateItemDictionary.Add(potentialDuplicate.Id, potentialDuplicate);
            }

            foreach (ListViewItem item in listView.Items)
            {
                if (duplicateItemDictionary.ContainsKey(item.Name))
                    item.ForeColor = Color.Red;
                else
                    item.ForeColor = Color.Black;
            }
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

                        bool hasPotentialDuplicates;
                        IKeywordIndexItem[] potentialDuplicates;

                        IndexExportImportData[] exportData = projectManagement.GetExportData(keywordIndexes.ToArray(), out hasPotentialDuplicates, out potentialDuplicates);

                        if (!hasPotentialDuplicates)
                        {
                            try
                            {
                                projectManagement.ImportData(exportData, GetVersion());
                                MessageBox.Show(this, "Selection has been successfully exported.", "Project Exporter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(this, "Error occurred while trying to export", "Project Exporter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                            MessageBox.Show(this, "Selection contains possible duplicates. The importer cannot allow this operation.", "Project Exporter");

                        Find();
                    }
                    else
                        MessageBox.Show(this, "There is nothing to export. Please ensure the items you wish to export are checked.", "Project Exporter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            sortedColumn = e.Column;
            listViewSorter.Sort(sortedColumn);
        }

        private void txtSourceProjectPath_TextChanged(object sender, EventArgs e)
        {
            EnableExecution();
            if (listView.Items.Count > 0)
                listView.Items.Clear();
        }

        private void txtDestinationProjectPath_TextChanged(object sender, EventArgs e)
        {
            EnableExecution();
            if (listView.Items.Count > 0)
                listView.Items.Clear();
        }
    }
}
