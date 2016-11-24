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
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System.IO;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class ImageSetControl : UserControl, IDocumentItemControl
    {
        private IImageSetDocument imageDocument;
        private ICodeGroupDocumentGroup codeGroupFile;
        private IImgDocument currentImage;

        public ImageSetControl(AppFacade application, ICodeGroupDocumentGroup codeGroupFile, IImageSetDocument imageDocument)
        {
            InitializeComponent();

            this.Id = imageDocument.Id;

            imageBox.GridScale = Cyotek.Windows.Forms.ImageBoxGridScale.None;
            this.imageDocument = imageDocument;
            this.codeGroupFile = codeGroupFile;

            btnDeleteImage.Image = Resources.GetImage(Constants.ImageKeys.DeleteSnippet);
            btnImport.Image = Resources.GetImage(Constants.ImageKeys.OpenProject);
            btnMoveLeft.Image = Resources.GetImage(Constants.ImageKeys.MoveLeft);
            btnMoveRight.Image = Resources.GetImage(Constants.ImageKeys.MoveRight);
            lblScrollPosition.Image = Resources.GetImage(Constants.ImageKeys.ObjectPosition);
            lblSize.Image = Resources.GetImage(Constants.ImageKeys.ObjectSize);
            lblZoomLevel.Image = Resources.GetImage(Constants.ImageKeys.ObjectZoom);

            // set initial data
            txtTitle.Text = imageDocument.Title;
            this.IsModified = false;

            if (imageDocument.ImageCount > 0)
            {
                LoadIfExists(imageDocument.FirstImage);
            }
            else
            {
                UpdateStatusBar();
            }

            // add these events after any initial control data has been modified.
            txtTitle.TextChanged += (s, e) => { SetModified(); };

            this.Modified += CodeItemCtrl_Modified;
            codeGroupFile.BeforeSave += codeGroupFile_BeforeContentSaved;
            codeGroupFile.AfterSave += codeGroupFile_ContentSaved;
            imageDocument.ImageRemoved += imageDocument_ImageRemoved;
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
            this.imageDocument.Revert();
        }

        private void CodeItemCtrl_Modified(object sender, EventArgs e)
        {
            SetChangeStatus();
        }

        private void SetModified()
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

        private void LoadIfExists(IImgDocument imageItem)
        {
            if (this.imageDocument.Exists)
            {
                Image image = LoadBitmap(imageItem.DisplayFilePath);
                imageBox.Image = image;
                imageBox.Text = imageItem.Description;
                imageBox.Zoom = 100;
                //imageBox.ZoomToFit();

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
            if (ImageSetExists())
            {
                lblScrollPosition.Text = this.FormatPoint(imageBox.AutoScrollPosition);
                lblSize.Text = this.FormatRectangle(imageBox.GetImageViewPort());
                lblZoomLevel.Text = string.Format("{0}%", imageBox.Zoom);
                lblImagePosition.Text = string.Format("Position {0} of {1}", this.currentImage.Ordinal, this.imageDocument.ImageCount);
            }
            else
            {
                lblScrollPosition.Text = "";
                lblSize.Text = this.FormatRectangle(new RectangleF(0, 0, 0, 0));
                lblZoomLevel.Text = string.Format("{0}%", 0);
                lblImagePosition.Text = string.Format("Position {0} of {1}", 0, 0);
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
            if (ImageSetExists())
            {
                if (this.imageDocument.CanMovePrevious(this.currentImage))
                {
                    this.imageDocument.MovePrevious(this.currentImage);
                    SetModified();
                    UpdateStatusBar();
                }
            }
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            if (ImageSetExists())
            {
                if (this.imageDocument.CanMoveNext(this.currentImage))
                {
                    this.imageDocument.MoveNext(this.currentImage);
                    SetModified();
                    UpdateStatusBar();
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (ImageSetExists())
            {
                if (!this.imageDocument.IsFirstImage(this.currentImage))
                {
                    IImgDocument imageItem = this.imageDocument.PreviousImage(this.currentImage);
                    LoadIfExists(imageItem);
                }
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (ImageSetExists())
            {
                if (!this.imageDocument.IsLastImage(this.currentImage))
                {
                    IImgDocument imageItem = this.imageDocument.NextImage(this.currentImage);
                    LoadIfExists(imageItem);
                }
            }
        }

        private void ctxFileImportMenu_Click(object sender, EventArgs e)
        {
            Import();
        }

        private void ctxClipboardImportMenu_Click(object sender, EventArgs e)
        {
            ClipboardImport();
        }

        private void Import()
        {
            if (ImageSetExists())
            {
                if (!Directory.Exists(Path.GetDirectoryName(this.imageDocument.FilePath)))
                {
                    Dialogs.MustSaveGroupBeforeAction(this);
                    return;
                }

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
                    if (imageBox.Image != null)
                        imageBox.Image.Dispose();

                    File.Copy(filePath, this.currentImage.ModifyFilePath, true);
                    LoadIfExists(this.currentImage);
                }
            }
        }

        private void ClipboardImport()
        {
            if (Clipboard.ContainsImage())
            {
                if (imageBox.Image == null)
                    ReplaceImage();
                else
                {
                    DialogResult result = Dialogs.ReplaceCurrentItemPrompt(this);
                    if (result == DialogResult.Yes)
                    {
                        ReplaceImage();
                    }
                }
            }
        }

        private void ReplaceImage()
        {
            if (ImageSetExists())
            {
                Image image = Clipboard.GetImage();
                image.Save((this.currentImage.ModifyFilePath));
                image.Dispose();

                LoadIfExists(this.currentImage);
                SetModified();
            }
        }

        private void imageBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (ImageSetExists())
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    ShowContextMenu(imageBox, e.Location);
            }
        }

        private void ShowContextMenu(Control ctrl, Point location)
        {
            ctxClipboardImportMenu.Enabled = Clipboard.ContainsImage();
            imageContextMenu.Show(ctrl, location);
        }

        private void btnDisplayText_Click(object sender, EventArgs e)
        {
            if (btnDisplayText.Checked)
                imageBox.TextDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.Client;
            else
                imageBox.TextDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.None;
        }

        private void btnDeleteImage_Click(object sender, EventArgs e)
        {
            if (ImageSetExists())
            {
                DialogResult result = Dialogs.DeleteDocumentDialogPrompt(this);

                if (result == DialogResult.Yes)
                    this.imageDocument.Remove(currentImage);
            }
        }

        private void imageDocument_ImageRemoved(object sender, EventArgs e)
        {
            if (imageDocument.ImageCount > 0)
                LoadIfExists(imageDocument.FirstImage);
            else
            {
                imageBox.Image = null;
                imageBox.Text = string.Empty;
            }
            SetModified();
            UpdateStatusBar();
        }

        private bool ImageSetExists()
        {
            return imageDocument.ImageCount > 0 && this.currentImage != null;
        }
    }
}
