using CygSoft.CodeCat.DocumentManager.Infrastructure;
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
    public partial class FileGroupFileEditDialog : Form
    {
        private IFileGroupFile fileGroupFile;
        public IFileGroupFile EditedFile { get { return fileGroupFile; } }
        private IFileGroupDocument fileGroupDocument;

        public FileGroupFileEditDialog(IFileGroupFile fileGroupFile, IFileGroupDocument fileGroupDocument)
        {
            InitializeComponent();

            this.fileGroupFile = fileGroupFile;
            this.fileGroupDocument = fileGroupDocument;
            this.txtFilePath.Text = "";
            this.txtTitle.Text = fileGroupFile.Title;
            this.txtFileName.Text = fileGroupFile.FileTitle;
            this.lblExtension.Text = fileGroupFile != null ? fileGroupFile.FileExtension : "";
            this.txtDescription.Text = fileGroupFile.Description;
            this.cboCategory.Items.AddRange(fileGroupDocument.Categories);
            this.cboCategory.Sorted = true;
            this.cboCategory.SelectedItem = string.IsNullOrEmpty(fileGroupFile.Category) ? "Unknown" : fileGroupFile.Category;
        }

        public FileGroupFileEditDialog(IFileGroupDocument fileGroupDocument)
        {
            InitializeComponent();

            this.fileGroupDocument = fileGroupDocument;
            this.txtFilePath.Text = "";
            this.txtTitle.Text = "";
            this.txtFileName.Text = "";
            this.lblExtension.Text = fileGroupFile != null ? fileGroupFile.FileExtension : "";
            this.txtDescription.Text = "";
            this.cboCategory.Items.AddRange(fileGroupDocument.Categories);
            this.cboCategory.Sorted = true;
            this.cboCategory.SelectedItem = "Unknown";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        public string FilePath { get { return txtFilePath.Text.Trim(); } }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                if (this.fileGroupFile == null)
                {
                    this.fileGroupFile = fileGroupDocument.CreateNewFile(txtFileName.Text.Trim() + lblExtension.Text, txtFilePath.Text.Trim());
                }

                if (!string.IsNullOrEmpty(this.txtFilePath.Text.Trim()))
                    fileGroupFile.ImportFile(this.txtFilePath.Text.Trim());

                fileGroupFile.Title = txtTitle.Text;
                fileGroupFile.ChangeFileName(txtFileName.Text.Trim() + lblExtension.Text);
                //fileItem.FileName = Path.GetFileName(txtFile.Text.Trim());
                fileGroupFile.Description = txtDescription.Text;
                fileGroupFile.DateModified = DateTime.Now;
                fileGroupFile.Category = string.IsNullOrEmpty(cboCategory.Text) ? "Unknown" : cboCategory.Text.ToString();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private bool ValidateFields()
        {
            if (txtTitle.Text.Trim() == "")
            {
                Dialogs.NoInputValueForMandatoryField(this, "Title");
                return false;
            }

            //if (this.fileGroupFile == null)
            //{
            //    if (file
            //}
            //if (fileGroupDocument.ValidateFileName(

            //if (txtFilePath.Text.Trim() == "" || !File.Exists(txtFilePath.Text.Trim()))
            //{
            //    Dialogs.NoInputValueForMandatoryField(this, "File Path");
            //    return false;
            //}
            if (txtFileName.Text.Trim() == "")
            {
                
                Dialogs.NoInputValueForMandatoryField(this, "File Name");
                return false;
            }

            string id = fileGroupFile == null ? "" : fileGroupFile.Id;
            if (!fileGroupDocument.ValidateFileName(txtFileName.Text.Trim() + lblExtension.Text, id))
            {
                Dialogs.WillConflictDialogPrompt(this, "File Name");
                return false;
            }

            if (txtDescription.Text.Trim() == "")
            {
                Dialogs.NoInputValueForMandatoryField(this, "Description");
                return false;
            }
            return true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "All Files *.* (*.*)|*.*";
            openDialog.Title = string.Format("Open File");
            openDialog.AddExtension = true;
            openDialog.FilterIndex = 0;
            openDialog.CheckPathExists = true;

            DialogResult result = openDialog.ShowDialog(this);
            string filePath = openDialog.FileName;

            if (result == DialogResult.OK)
            {
                txtFilePath.Text = filePath;
                string fileName = Path.GetFileName(txtFilePath.Text);
                string fileTitle = Path.GetFileNameWithoutExtension(fileName);

                if (string.IsNullOrEmpty(txtDescription.Text))
                    txtDescription.Text = fileTitle;
                if (string.IsNullOrEmpty(txtTitle.Text))
                    txtTitle.Text = fileTitle;
                if (string.IsNullOrEmpty(txtFileName.Text))
                    txtFileName.Text = fileTitle;
                lblExtension.Text = Path.GetExtension(fileName);
            }
        }
    }
}
