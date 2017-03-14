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
        private IFileAttachment fileAttachment;
        public IFileAttachment EditedFile { get { return fileAttachment; } }
        private IFileAttachmentsTopicSection fileAttachmentsTopicSection;

        public FileGroupFileEditDialog(IFileAttachment fileAttachment, IFileAttachmentsTopicSection fileAttachmentsTopicSection)
        {
            InitializeComponent();

            this.fileAttachment = fileAttachment;
            this.fileAttachmentsTopicSection = fileAttachmentsTopicSection;
            txtFilePath.Text = "";
            txtTitle.Text = fileAttachment.Title;
            txtFileName.Text = fileAttachment.FileTitle;
            lblExtension.Text = fileAttachment != null ? fileAttachment.FileExtension : "";
            txtDescription.Text = fileAttachment.Description;
            cboCategory.Items.AddRange(fileAttachmentsTopicSection.Categories);
            cboCategory.Sorted = true;
            cboCategory.SelectedItem = string.IsNullOrEmpty(fileAttachment.Category) ? "Unknown" : fileAttachment.Category;
            chkAllowOpenOrExecute.Checked = fileAttachment.AllowOpenOrExecute;
        }

        public FileGroupFileEditDialog(IFileAttachmentsTopicSection fileGroupDocument)
        {
            InitializeComponent();

            this.fileAttachmentsTopicSection = fileGroupDocument;
            txtFilePath.Text = "";
            txtTitle.Text = "";
            txtFileName.Text = "";
            lblExtension.Text = fileAttachment != null ? fileAttachment.FileExtension : "";
            txtDescription.Text = "";
            cboCategory.Items.AddRange(fileGroupDocument.Categories);
            cboCategory.Sorted = true;
            cboCategory.SelectedItem = "Unknown";
            chkAllowOpenOrExecute.Checked = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public string FilePath { get { return txtFilePath.Text.Trim(); } }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                if (fileAttachment == null)
                {
                    fileAttachment = fileAttachmentsTopicSection.CreateNewFile(txtFileName.Text.Trim() + lblExtension.Text, txtFilePath.Text.Trim());
                }

                if (!string.IsNullOrEmpty(txtFilePath.Text.Trim()))
                    fileAttachment.ImportFile(txtFilePath.Text.Trim());

                fileAttachment.Title = txtTitle.Text;
                fileAttachment.ChangeFileName(txtFileName.Text.Trim() + lblExtension.Text);
                fileAttachment.Description = txtDescription.Text;
                fileAttachment.DateModified = DateTime.Now;
                fileAttachment.Category = string.IsNullOrEmpty(cboCategory.Text) ? "Unknown" : cboCategory.Text.ToString();
                fileAttachment.AllowOpenOrExecute = chkAllowOpenOrExecute.Checked;
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidateFields()
        {
            if (txtTitle.Text.Trim() == "")
            {
                Dialogs.NoInputValueForMandatoryField(this, "Title");
                return false;
            }

            // In this case, no file name exists (no file has ever been captured for a source file)... hence no extension.
            // so ensure the user has actually selected a file.
            if (fileAttachment == null && (string.IsNullOrEmpty(txtFilePath.Text.Trim()) || !File.Exists(txtFilePath.Text.Trim())))
            {
                Dialogs.NoInputValueForMandatoryField(this, "File Path");
                return false;
            }

            if (!string.IsNullOrEmpty(txtFileName.Text.Trim()) && string.IsNullOrEmpty(lblExtension.Text))
            {
                Dialogs.NoInputValueForMandatoryField(this, "File Path");
                return false;
            }

            if (txtFileName.Text.Trim() == "")
            {
                
                Dialogs.NoInputValueForMandatoryField(this, "File Name");
                return false;
            }

            string id = fileAttachment == null ? "" : fileAttachment.Id;
            if (!fileAttachmentsTopicSection.ValidateFileName(txtFileName.Text.Trim() + lblExtension.Text, id))
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

        private void btnUpdateFileName_Click(object sender, EventArgs e)
        {
            string filePath = txtFilePath.Text.Trim();
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                
                txtFileName.Text = Path.GetFileNameWithoutExtension(filePath);
                lblExtension.Text = Path.GetExtension(filePath);
            }
        }
    }
}
