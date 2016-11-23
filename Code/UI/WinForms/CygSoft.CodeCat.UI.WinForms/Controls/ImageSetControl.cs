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
using System.IO;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class ImageSetControl : UserControl, IDocumentItemControl
    {
        private IImageSetDocument imageDocument;
        private ICodeGroupDocumentGroup codeGroupFile;
        private IImageItem currentImage;

        public ImageSetControl(AppFacade application, ICodeGroupDocumentGroup codeGroupFile, IImageSetDocument imageDocument)
        {
            InitializeComponent();

            this.Id = imageDocument.Id;

            imageBox.GridScale = Cyotek.Windows.Forms.ImageBoxGridScale.None;
            this.imageDocument = imageDocument;
            this.codeGroupFile = codeGroupFile;

            btnImport.Image = Resources.GetImage(Constants.ImageKeys.OpenProject);
            btnMoveLeft.Image = Resources.GetImage(Constants.ImageKeys.MoveLeft);
            btnMoveRight.Image = Resources.GetImage(Constants.ImageKeys.MoveRight);
            lblScrollPosition.Image = Resources.GetImage(Constants.ImageKeys.ObjectPosition);
            lblSize.Image = Resources.GetImage(Constants.ImageKeys.ObjectSize);
            lblZoomLevel.Image = Resources.GetImage(Constants.ImageKeys.ObjectZoom);

            // set initial data
            txtTitle.Text = imageDocument.Title;
            this.IsModified = false;
            LoadIfExists(imageDocument.FirstImage);

            // add these events after any initial control data has been modified.
            txtTitle.TextChanged += SetModified;
            this.Modified += CodeItemCtrl_Modified;
            codeGroupFile.BeforeSave += codeGroupFile_BeforeContentSaved;
            codeGroupFile.AfterSave += codeGroupFile_ContentSaved;
        }

        public event EventHandler Modified;

        public string Id { get; private set; }

        public string Title
        {
            get { return txtTitle.Text; }
        }

        public int ImageKey { get { return IconRepository.ImageKeyFor(IconRepository.IMG); } }
        public Icon ImageIcon { get { return IconRepository.GetIcon(IconRepository.IMG); } }
        public Image IconImage { get { return IconRepository.GetImage(IconRepository.IMG); } }

        public bool IsModified { get; private set; }

        public bool FileExists { get { return false; } }

        public void Revert()
        {
        }

        private void CodeItemCtrl_Modified(object sender, EventArgs e)
        {
            SetChangeStatus();
        }

        private void SetModified(object sender, EventArgs e)
        {
            this.IsModified = true;

            if (this.Modified != null)
                this.Modified(this, new EventArgs());
        }

        private void SetChangeStatus()
        {
            lblEditStatus.Text = this.IsModified ? "Edited" : "Saved";
            lblEditStatus.ForeColor = this.IsModified ? Color.DarkRed : Color.Black;
        }

        private void LoadIfExists(IImageItem imageItem)
        {
            if (this.imageDocument.Exists)
            {
                Image image = LoadBitmap(imageItem.FilePath);
                imageBox.Image = image;
                imageBox.Text = imageItem.Description;
                //imageBox.Zoom = 100;
                imageBox.ZoomToFit();

                this.currentImage = imageItem;
                UpdateStatusBar();
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

        private void UpdateStatusBar()
        {
            if (this.imageDocument != null && this.currentImage != null)
            {
                lblScrollPosition.Text = this.FormatPoint(imageBox.AutoScrollPosition);
                lblSize.Text = this.FormatRectangle(imageBox.GetImageViewPort());
                lblZoomLevel.Text = string.Format("{0}%", imageBox.Zoom);
                lblImagePosition.Text = string.Format("Position {0} of {1}", this.currentImage.Ordinal, this.imageDocument.Images.Count());
            }
        }

        private string FormatRectangle(RectangleF rect)
        {
            return string.Format("X:{0}, Y:{1}, W:{2}, H:{3}", (int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }

        private string FormatPoint(Point point)
        {
            return string.Format("X:{0}, Y:{1}", point.X, point.Y);
        }

        private void imageBox_Resize(object sender, EventArgs e)
        {
            UpdateStatusBar();
        }

        private void imageBox_Scroll(object sender, ScrollEventArgs e)
        {
            UpdateStatusBar();
        }

        private void imageBox_ZoomChanged(object sender, EventArgs e)
        {
            UpdateStatusBar();
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

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            if (this.imageDocument.CanMovePrevious(this.currentImage))
            {
                this.imageDocument.MovePrevious(this.currentImage);
                UpdateStatusBar();
            }
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            if (this.imageDocument.CanMoveNext(this.currentImage))
            {
                this.imageDocument.MoveNext(this.currentImage);
                UpdateStatusBar();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (!this.imageDocument.IsFirstImage(this.currentImage))
            {
                IImageItem imageItem = this.imageDocument.PreviousImage(this.currentImage);
                LoadIfExists(imageItem);
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (!this.imageDocument.IsLastImage(this.currentImage))
            {
                IImageItem imageItem = this.imageDocument.NextImage(this.currentImage);
                LoadIfExists(imageItem);
            }
        }
    }
}