﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using System.IO;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    // TODO: Rename this to PdfDocumentCtrl. Also consider moving all items controls into their own directory?
    public partial class ImageControl :  UserControl, IDocumentItemControl
    {
        private IImageDocument imageDocument;
        private ICodeGroupDocumentGroup codeGroupFile;

        public ImageControl(AppFacade application, ICodeGroupDocumentGroup codeGroupFile, IImageDocument imageDocument)
        {
            InitializeComponent();

            this.imageDocument = imageDocument;
            this.codeGroupFile = codeGroupFile;

            this.btnImport.Image = Resources.GetImage(Constants.ImageKeys.OpenProject);
            this.Id = imageDocument.Id;

            ResetFieldValues();
            RegisterDataFieldEvents();
            RegisterFileEvents();
            LoadIfExists();
        }

        private void LoadIfExists()
        {
            if (this.imageDocument.Exists)
            {
                pictureBox.Image = LoadBitmap(this.imageDocument.FilePath);
                CenterPictureBox(pictureBox);
            }
        }

        private Bitmap LoadBitmap(string path)
        {
            //Open file in read only mode
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            //Get a binary reader for the file stream
            using (BinaryReader reader = new BinaryReader(stream))
            {
                //copy the content of the file into a memory stream
                var memoryStream = new MemoryStream(reader.ReadBytes((int)stream.Length));
                //make a new Bitmap object the owner of the MemoryStream
                return new Bitmap(memoryStream);
            }
        }

        public event EventHandler Modified;

        public string Id { get; private set; }

        public string Title
        {
            get { return txtTitle.Text; }
        }

        public int ImageKey { get { return IconRepository.ImageKeyFor(IconRepository.PDF); } }
        public Icon ImageIcon { get { return IconRepository.GetIcon(IconRepository.PDF); } }
        public Image IconImage { get { return IconRepository.GetImage(IconRepository.PDF); } }

        public bool IsModified { get; private set; }

        public bool FileExists { get { return false; } }

        public void Revert()
        {
        }

        private void RegisterFileEvents()
        {
            codeGroupFile.BeforeSave += codeGroupFile_BeforeContentSaved;
            codeGroupFile.AfterSave += codeGroupFile_ContentSaved;
        }

        private void ResetFieldValues()
        {
            txtTitle.Text = imageDocument.Title;
            this.IsModified = false;
        }

        private void RegisterDataFieldEvents()
        {
            txtTitle.TextChanged += SetModified;
            this.Modified += CodeItemCtrl_Modified;
        }

        private void codeGroupFile_ContentSaved(object sender, FileEventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void codeGroupFile_BeforeContentSaved(object sender, FileEventArgs e)
        {
            this.imageDocument.Title = txtTitle.Text;
        }

        private void CodeItemCtrl_Modified(object sender, EventArgs e)
        {
            SetChangeStatus();
        }

        private void SetChangeStatus()
        {
            lblEditStatus.Text = this.IsModified ? "Edited" : "Saved";
            lblEditStatus.ForeColor = this.IsModified ? Color.DarkRed : Color.Black;
        }

        private void SetModified(object sender, EventArgs e)
        {
            this.IsModified = true;

            if (this.Modified != null)
                this.Modified(this, new EventArgs());
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image Files *.png (*.png)|*.png";
            openDialog.DefaultExt = "*.png";
            openDialog.Title = string.Format("Open Image");
            openDialog.AddExtension = true;
            openDialog.FilterIndex = 0;
            openDialog.CheckPathExists = true;

            DialogResult result = openDialog.ShowDialog(this);
            string filePath = openDialog.FileName;

            if (result == DialogResult.OK)
            {
                pictureBox.Image.Dispose();
                
                File.Copy(filePath, this.imageDocument.FilePath, true);
                LoadIfExists();
            }
        }

        private void CenterPictureBox(PictureBox picBox)
        {
            picBox.Location = new Point((picBox.Parent.ClientSize.Width / 2) - (pictureBox.Image.Width / 2),
                                        (picBox.Parent.ClientSize.Height / 2) - (pictureBox.Image.Height / 2));
        }

        private bool mustScroll = false;

        private void imagePanel_Resize(object sender, EventArgs e)
        {
            if (pictureBox.Width > imagePanel.Width || pictureBox.Height > imagePanel.Height)
            {
                if (!mustScroll)
                {
                    mustScroll = true;
                    ScrollingPicture();
                }
            }
            else
            {
                if (mustScroll)
                {
                    mustScroll = false;
                    CenteredPicture();
                }
            }
        }

        private void ScrollingPicture()
        {
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            imagePanel.AutoScroll = true;
            //pictureBox.Refresh();
            //imagePanel.Refresh();
        }

        private void CenteredPicture()
        {
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.Anchor = AnchorStyles.None;

            CenterPictureBox(pictureBox);
            //pictureBox.Refresh();
        }
    }
}
