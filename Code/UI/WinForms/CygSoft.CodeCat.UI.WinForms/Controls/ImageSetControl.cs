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
        public event EventHandler Modified;

        private IImageSetDocument imageDocument;
        private ICodeGroupDocumentSet codeGroupDocumentSet;
        private IImgDocument currentImage;

        #region Public Properties

        public string Id { get; private set; }

        public string Title
        {
            get { return txtTitle.Text; }
        }

        public int ImageKey { get { return IconRepository.Get(IconRepository.Documents.ImageSet).Index; } }
        public Icon ImageIcon { get { return IconRepository.Get(IconRepository.Documents.ImageSet).Icon; } }
        public Image IconImage { get { return IconRepository.Get(IconRepository.Documents.ImageSet).Image; } }

        public bool IsModified { get; private set; }

        public bool FileExists { get { return false; } }

        #endregion Public Properties

        #region Constructors

        public ImageSetControl(AppFacade application, ICodeGroupDocumentSet codeGroupDocumentSet, IImageSetDocument imageDocument)
        {
            InitializeComponent();

            this.Id = imageDocument.Id;

            imageBox.GridScale = Cyotek.Windows.Forms.ImageBoxGridScale.None;
            this.imageDocument = imageDocument;
            this.codeGroupDocumentSet = codeGroupDocumentSet;

            CreateControlGraphics();

            // set initial data
            txtTitle.Text = imageDocument.Title;
            this.IsModified = false;

            LoadInitialImage();

            // add these events after any initial control data has been modified.
            InitializeEvents();
        }

        #endregion Constructors

        #region Public Methods

        public void Revert()
        {
            this.imageDocument.Revert();
        }

        #endregion Public Methods

        #region Private Methods

        private void InitializeEvents()
        {
            txtTitle.TextChanged += (s, e) => { SetModified(); };
            this.Modified += CodeItemCtrl_Modified;
            codeGroupDocumentSet.BeforeSave += codeGroupDocumentSet_BeforeContentSaved;
            codeGroupDocumentSet.AfterSave += codeGroupDocumentSet_ContentSaved;
            imageDocument.ImageRemoved += imageDocument_ImageRemoved;
        }

        private void CreateControlGraphics()
        {
            btnAddImage.Image = Resources.GetImage(Constants.ImageKeys.AddSnippet);
            btnDeleteImage.Image = Resources.GetImage(Constants.ImageKeys.DeleteSnippet);
            btnImport.Image = Resources.GetImage(Constants.ImageKeys.OpenProject);
            btnMoveLeft.Image = Resources.GetImage(Constants.ImageKeys.MoveLeft);
            btnMoveRight.Image = Resources.GetImage(Constants.ImageKeys.MoveRight);
            lblScrollPosition.Image = Resources.GetImage(Constants.ImageKeys.ObjectPosition);
            lblSize.Image = Resources.GetImage(Constants.ImageKeys.ObjectSize);
            lblZoomLevel.Image = Resources.GetImage(Constants.ImageKeys.ObjectZoom);
            btnDisplayText.Image = Resources.GetImage(Constants.ImageKeys.ShowText);
            btnEditImageText.Image = Resources.GetImage(Constants.ImageKeys.EditText);
        }

        private void LoadInitialImage()
        {
            if (imageDocument.ImageCount > 0)
            {
                LoadImage(imageDocument.FirstImage);
            }
            else
            {
                AddBlankImage();
            }
        }

        private void LoadImage(IImgDocument imageItem)
        {
            if (imageItem != null)
            {
                Image image = imageItem.GetDisplayImage();
                imageBox.Image = image;
                imageBox.Text = imageItem.Description;
                imageBox.Zoom = 100;
                this.currentImage = imageItem;
                UpdateStatusBar();
            }
        }

        private void ReplaceImage()
        {
            if (ImageSetExists())
            {
                Image image = Clipboard.GetImage();
                this.currentImage.SetImage(image);
                LoadImage(this.currentImage);
                SetModified();
            }
        }

        private void ImportClipboardImage()
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

        private void AddBlankImage()
        {
            this.currentImage = imageDocument.Add();
            LoadImage(currentImage);
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

        private bool ImageSetExists()
        {
            return imageDocument.ImageCount > 0 && this.currentImage != null;
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

                    this.currentImage.SetImage(filePath);
                    LoadImage(this.currentImage);
                    SetModified();
                }
            }
        }

        private void ShowContextMenu(Control ctrl, Point location)
        {
            ctxClipboardImportMenu.Enabled = Clipboard.ContainsImage();
            imageContextMenu.Show(ctrl, location);
        }

        private string FormatRectangle(RectangleF rect)
        {
            return string.Format("X:{0}, Y:{1}, W:{2}, H:{3}", (int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }

        private string FormatPoint(Point point)
        {
            return string.Format("X:{0}, Y:{1}", point.X, point.Y);
        }

        #endregion Private Methods

        #region Private Events

        private void CodeItemCtrl_Modified(object sender, EventArgs e)
        {
            SetChangeStatus();
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            AddBlankImage();
            //this.imageDocument.Add("this is some text that should display for the description BOYO !!!", "png");
        }

        private void btnEditImageText_Click(object sender, EventArgs e)
        {
            if (this.currentImage != null)
            {
                ImageDescriptionDialog dialog = new ImageDescriptionDialog();
                dialog.Description = this.currentImage.Description;

                DialogResult result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    this.currentImage.Description = dialog.Description;
                    imageBox.Text = this.currentImage.Description;
                    SetModified();
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Import();
        }

        private void imageBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (ImageSetExists())
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    ShowContextMenu(imageBox, e.Location);
            }
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
                LoadImage(imageDocument.FirstImage);
            else
            {
                AddBlankImage();
            }
            SetModified();
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

        private void codeGroupDocumentSet_ContentSaved(object sender, FileEventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void codeGroupDocumentSet_BeforeContentSaved(object sender, FileEventArgs e)
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
                    LoadImage(imageItem);
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
                    LoadImage(imageItem);
                }
            }
        }

        private void ctxFileImportMenu_Click(object sender, EventArgs e)
        {
            Import();
        }

        private void ctxClipboardImportMenu_Click(object sender, EventArgs e)
        {
            ImportClipboardImage();
        }

        #endregion Private Events
    }
}
