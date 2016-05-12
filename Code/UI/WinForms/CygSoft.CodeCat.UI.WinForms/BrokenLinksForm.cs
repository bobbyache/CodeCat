using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class BrokenLinksForm : Form
    {
        private AppFacade application;
        private ItemFormController<SnippetForm> formController;

        public BrokenLinksForm(AppFacade application, ItemFormController<SnippetForm> formController)
        {
            InitializeComponent();
            this.application = application;
            this.formController = formController;
        }

        
        private void GetUnindexedFiles()
        {
            unIndexedlistView.Items.Clear();
            foreach (string file in application.FindOrphanedFiles())
            {
                CreateUnindexedListviewItem(file);
            }
        }

        private void GetMissingFiles()
        {
            missingFileListView.Items.Clear();
            foreach (IKeywordIndexItem indexItem in application.FindMissingFiles())
            {
                CreateMissingListviewItem(indexItem);
            }
        }

        private void CreateMissingListviewItem(IKeywordIndexItem indexItem)
        {
            ListViewItem listItem = new ListViewItem();
            listItem.Tag = indexItem;
            listItem.Text = indexItem.FileTitle;
            missingFileListView.Items.Add(listItem);
        }

        private void CreateUnindexedListviewItem(string fileTitle)
        {
            ListViewItem listItem = new ListViewItem();
            listItem.Tag = Path.GetFileNameWithoutExtension(fileTitle);
            listItem.Text = fileTitle;
            unIndexedlistView.Items.Add(listItem);

        }

        private void unIndexedlistView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (unIndexedlistView.SelectedItems.Count == 1)
            {
                OpenCodeFile();
            }
        }

        private void refreshInfoButton_Click(object sender, EventArgs e)
        {
            GetUnindexedFiles();
            //GetMissingFiles();
        }

        private void missingFileListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BrokenLinksForm_Activated(object sender, EventArgs e)
        {
            GetUnindexedFiles();
            //GetMissingFiles();
        }

        private void unIndexedlistView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (unIndexedlistView.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    unindexedContextMenu.Show(Cursor.Position);
                }
            } 
        }

        private void mnuOpenSnippet_Click(object sender, EventArgs e)
        {
            OpenCodeFile();
        }

        private void mnuExploreTo_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = (string)unIndexedlistView.SelectedItems[0].Text;
                string args = string.Format("/e, /select, \"{0}\"", filePath);
                //string args = string.Format("/select, \"{0}\"", filePath);
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
                info.FileName = "explorer";
                info.Arguments = args;
                System.Diagnostics.Process.Start(info);
            }
            catch (Exception)
            { }
        }



        private void OpenCodeFile()
        {
            string file = (string)unIndexedlistView.SelectedItems[0].Tag;
            CodeFile codeFile = application.CreateCodeSnippetFromOrphan(file);
            formController.OpenCodeWindow(codeFile, application);
        }
    }
}
