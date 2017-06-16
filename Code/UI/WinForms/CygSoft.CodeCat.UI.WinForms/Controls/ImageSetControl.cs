using System;
using System.Drawing;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System.IO;
using System.Diagnostics;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class ImageSetControl : UserControl, ITopicSectionBaseControl
    {
        public event EventHandler Modified;

        private IImagePagerTopicSection topicSection;
        private ITopicDocument topicDocument;
        private IPagerImage pageImage;

        #region Public Properties

        public string Id { get; private set; }
        public string Title { get { return txtTitle.Text; } }
        public int ImageKey { get { return IconRepository.Get(IconRepository.TopicSections.ImageSet).Index; } }
        public Icon ImageIcon { get { return IconRepository.Get(IconRepository.TopicSections.ImageSet).Icon; } }
        public Image IconImage { get { return IconRepository.Get(IconRepository.TopicSections.ImageSet).Image; } }
        public bool IsModified { get; private set; }
        public bool FileExists { get { return false; } }

        #endregion Public Properties

        #region Constructors

        public ImageSetControl(AppFacade application, ITopicDocument topicDocument, IImagePagerTopicSection topicSection)
        {
            InitializeComponent();

            Id = topicSection.Id;

            imageBox.GridScale = Cyotek.Windows.Forms.ImageBoxGridScale.None;
            this.topicSection = topicSection;
            this.topicDocument = topicDocument;

            CreateControlGraphics();

            // set initial data
            txtTitle.Text = topicSection.Title;
            IsModified = false;

            LoadInitialImage();

            // add these events after any initial control data has been modified.
            InitializeEvents();
        }

        #endregion Constructors

        #region Public Methods

        public void Revert()
        {
            topicSection.Revert();
        }

        #endregion Public Methods

        #region Private Methods

        private void InitializeEvents()
        {
            txtTitle.TextChanged += (s, e) => { SetModified(); };
            Modified += CodeItemCtrl_Modified;
            topicDocument.BeforeSave += codeGroupDocumentSet_BeforeContentSaved;
            topicDocument.AfterSave += codeGroupDocumentSet_ContentSaved;
            topicSection.ImageRemoved += imageDocument_ImageRemoved;
        }

        private void CreateControlGraphics()
        {
            btnAddImage.Image = Gui.Resources.GetImage(Constants.ImageKeys.AddSnippet);
            btnDeleteImage.Image = Gui.Resources.GetImage(Constants.ImageKeys.DeleteSnippet);
            btnImport.Image = Gui.Resources.GetImage(Constants.ImageKeys.OpenProject);
            btnMoveLeft.Image = Gui.Resources.GetImage(Constants.ImageKeys.MoveLeft);
            btnMoveRight.Image = Gui.Resources.GetImage(Constants.ImageKeys.MoveRight);
            lblScrollPosition.Image = Gui.Resources.GetImage(Constants.ImageKeys.ObjectPosition);
            lblSize.Image = Gui.Resources.GetImage(Constants.ImageKeys.ObjectSize);
            lblZoomLevel.Image = Gui.Resources.GetImage(Constants.ImageKeys.ObjectZoom);
            btnDisplayText.Image = Gui.Resources.GetImage(Constants.ImageKeys.ShowText);
            btnEditImageText.Image = Gui.Resources.GetImage(Constants.ImageKeys.EditText);
            btnRefresh.Image = Gui.Resources.GetImage(Constants.ImageKeys.Refresh);
        }

        private void LoadInitialImage()
        {
            if (topicSection.ImageCount > 0)
                LoadImage(topicSection.FirstImage);
            else
                AddBlankImage();
        }

        private void LoadImage(IPagerImage imageItem)
        {
            if (imageItem != null)
            {
                Image image = imageItem.GetDisplayImage();
                imageBox.Image = image;
                imageBox.Text = imageItem.Description;
                imageBox.Zoom = 100;
                pageImage = imageItem;
                UpdateStatusBar();
            }
        }

        private void ReplaceImage()
        {
            if (ImageSetExists())
            {
                Image image = Clipboard.GetImage();
                pageImage.SetImage(image);
                LoadImage(pageImage);
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
                    DialogResult result = Gui.Dialogs.ReplaceCurrentItemPrompt(this);
                    if (result == DialogResult.Yes)
                        ReplaceImage();
                }
            }
        }

        private void AddBlankImage()
        {
            pageImage = topicSection.Add();
            LoadImage(pageImage);
        }

        private void SetModified()
        {
            IsModified = true;
            Modified?.Invoke(this, new EventArgs());
        }

        private void SetChangeStatus()
        {
            lblEditStatus.Text = IsModified ? "Edited" : "Saved";
            lblEditStatus.ForeColor = IsModified ? Color.DarkRed : Color.Black;
        }

        private void UpdateStatusBar()
        {
            if (ImageSetExists())
            {
                lblScrollPosition.Text = FormatPoint(imageBox.AutoScrollPosition);
                lblSize.Text = FormatRectangle(imageBox.GetImageViewPort());
                lblZoomLevel.Text = string.Format("{0}%", imageBox.Zoom);
                lblImagePosition.Text = string.Format("Position {0} of {1}", pageImage.Ordinal, topicSection.ImageCount);
            }
            else
            {
                lblScrollPosition.Text = "";
                lblSize.Text = FormatRectangle(new RectangleF(0, 0, 0, 0));
                lblZoomLevel.Text = string.Format("{0}%", 0);
                lblImagePosition.Text = string.Format("Position {0} of {1}", 0, 0);
            }
        }

        private bool ImageSetExists()
        {
            return topicSection.ImageCount > 0 && pageImage != null;
        }

        private void Import()
        {
            if (ImageSetExists())
            {
                if (!topicSection.FolderExists)
                {
                    Gui.Dialogs.MustSaveGroupBeforeAction(this);
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

                    pageImage.SetImage(filePath);
                    LoadImage(pageImage);
                    SetModified();
                }
            }
        }

        private void SaveAs()
        {
            if (ImageSetExists())
            {
                if (!topicSection.FolderExists)
                {
                    Gui.Dialogs.MustSaveGroupBeforeAction(this);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Image Files *.png (*.png)|*.png";
                saveDialog.DefaultExt = "*.png";
                saveDialog.Title = string.Format("Save Image As...");
                saveDialog.AddExtension = true;
                saveDialog.FilterIndex = 0;
                saveDialog.CheckPathExists = true;

                DialogResult result = saveDialog.ShowDialog(this);
                string filePath = saveDialog.FileName;

                if (result == DialogResult.OK)
                {
                    if (imageBox.Image != null)
                    {
                        imageBox.Image.Save(filePath);
                    }
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
        }

        private void btnEditImageText_Click(object sender, EventArgs e)
        {
            if (pageImage != null)
            {
                ImageDescriptionDialog dialog = new ImageDescriptionDialog();
                dialog.Description = pageImage.Description;

                DialogResult result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    pageImage.Description = dialog.Description;
                    imageBox.Text = pageImage.Description;
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
                if (e.Button == MouseButtons.Right)
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
                DialogResult result = Gui.Dialogs.DeleteItemDialog(this, "image");

                if (result == DialogResult.Yes)
                    topicSection.Remove(pageImage);
            }
        }

        private void imageDocument_ImageRemoved(object sender, EventArgs e)
        {
            if (topicSection.ImageCount > 0)
                LoadImage(topicSection.FirstImage);
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

        private void codeGroupDocumentSet_ContentSaved(object sender, TopicEventArgs e)
        {
            IsModified = false;
            SetChangeStatus();
        }

        private void codeGroupDocumentSet_BeforeContentSaved(object sender, TopicEventArgs e)
        {
            topicSection.Title = txtTitle.Text;
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            if (ImageSetExists())
            {
                if (topicSection.CanMovePrevious(pageImage))
                {
                    topicSection.MovePrevious(pageImage);
                    SetModified();
                    UpdateStatusBar();
                }
            }
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            if (ImageSetExists())
            {
                if (topicSection.CanMoveNext(pageImage))
                {
                    topicSection.MoveNext(pageImage);
                    SetModified();
                    UpdateStatusBar();
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (ImageSetExists())
            {
                if (!topicSection.IsFirstImage(pageImage))
                {
                    IPagerImage imageItem = topicSection.PreviousImage(pageImage);
                    LoadImage(imageItem);
                }
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (ImageSetExists())
            {
                if (!topicSection.IsLastImage(pageImage))
                {
                    IPagerImage nextPagerImage = topicSection.NextImage(pageImage);
                    LoadImage(nextPagerImage);
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

        private void ctxClipboardSaveAsMenu_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void ctxCopyMenu_Click(object sender, EventArgs e)
        {
            if (imageBox.Image != null)
                Clipboard.SetImage(imageBox.Image);
        }

        private void ctxEditMenu_Click(object sender, EventArgs e)
        {
            if (File.Exists(ConfigSettings.MsPaintEditorPath) && File.Exists(pageImage.FilePath))
            {
                Process.Start(ConfigSettings.MsPaintEditorPath, pageImage.FilePath);
            }
            else
            {
                Gui.Dialogs.CannotLoadImageEditor(this);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadImage(pageImage);
        }

        #endregion Private Events
    }
}
