using System;
using System.Drawing;
using System.Windows.Forms;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using System.IO;
using System.Diagnostics;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using CygSoft.CodeCat.Files.Infrastructure;
using CygSoft.CodeCat.UI.Resources.Infrastructure;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class ImageControl :  UserControl, ITopicSectionBaseControl
    {
        public event EventHandler Modified;

        private ISingleImageTopicSection topicSection;
        private ITopicDocument topicDocument;
        private IImageResources imageResources;

        public string Id { get; private set; }
        public string Title { get { return txtTitle.Text; } }
        public int ImageKey { get { return IconRepository.Get(IconRepository.TopicSections.SingleImage).Index; } }
        public Icon ImageIcon { get { return IconRepository.Get(IconRepository.TopicSections.SingleImage).Icon; } }
        public Image IconImage { get { return IconRepository.Get(IconRepository.TopicSections.SingleImage).Image; } }
        public bool IsModified { get; private set; }
        public bool FileExists { get { return false; } }

        public ImageControl(AppFacade application, IImageResources imageResources, ITopicDocument topicDocument, ISingleImageTopicSection topicSection)
        {
            InitializeComponent();

            this.imageResources = imageResources;

            imageBox.GridScale = Cyotek.Windows.Forms.ImageBoxGridScale.None;
            this.topicSection = topicSection;
            this.topicDocument = topicDocument;

            lblScrollPosition.Image = imageResources.GetImage(ImageKeys.ObjectPosition);
            lblSize.Image = imageResources.GetImage(ImageKeys.ObjectSize);
            lblZoomLevel.Image = imageResources.GetImage(ImageKeys.ObjectZoom);
            this.btnImport.Image = imageResources.GetImage(ImageKeys.OpenProject);
            btnRefresh.Image = imageResources.GetImage(ImageKeys.Refresh);

            this.Id = topicSection.Id;

            ResetFieldValues();
            RegisterDataFieldEvents();
            RegisterFileEvents();
            LoadIfExists();
        }

        public void Revert()
        {
        }

        private void UpdateStatusBar()
        {
            lblScrollPosition.Text = this.FormatPoint(imageBox.AutoScrollPosition);
            lblSize.Text = this.FormatRectangle(imageBox.GetImageViewPort());
            lblZoomLevel.Text = string.Format("{0}%", imageBox.Zoom);
        }

        private string FormatRectangle(RectangleF rect)
        {
            return string.Format("X:{0}, Y:{1}, W:{2}, H:{3}", (int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }

        private string FormatPoint(Point point)
        {
            return string.Format("X:{0}, Y:{1}", point.X, point.Y);
        }

        private void imageBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                ShowContextMenu(imageBox, e.Location);
        }

        private void ShowContextMenu(Control ctrl, Point location)
        {
            ctxClipboardImportMenu.Enabled = Clipboard.ContainsImage();
            imageContextMenu.Show(ctrl, location);
        }

        private void LoadIfExists()
        {
            if (topicSection.Exists)
            {
                Image image = LoadBitmap(this.topicSection.FilePath);
                imageBox.Image = image;
                imageBox.Zoom = 100;

                UpdateStatusBar();
            }
        }

        private Bitmap LoadBitmap(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                var memoryStream = new MemoryStream(reader.ReadBytes((int)stream.Length));
                return new Bitmap(memoryStream);
            }
        }

        private void RegisterFileEvents()
        {
            topicDocument.BeforeSave += codeGroupDocumentSet_BeforeContentSaved;
            topicDocument.AfterSave += codeGroupDocumentSet_ContentSaved;
        }

        private void ResetFieldValues()
        {
            txtTitle.Text = topicSection.Title;
            IsModified = false;
        }

        private void RegisterDataFieldEvents()
        {
            txtTitle.TextChanged += SetModified;
            this.Modified += CodeItemCtrl_Modified;
        }

        private void codeGroupDocumentSet_ContentSaved(object sender, FileEventArgs e)
        {
            IsModified = false;
            SetChangeStatus();
        }

        private void codeGroupDocumentSet_BeforeContentSaved(object sender, FileEventArgs e)
        {
            this.topicSection.Title = txtTitle.Text;
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
            IsModified = true;
            Modified?.Invoke(this, new EventArgs());
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Import();
        }

        private void Import()
        {
            if (!topicSection.FolderExists)
            {
                Gui.Dialogs.MustSaveGroupBeforeActionMessageBox(this);
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

                File.Copy(filePath, this.topicSection.FilePath, true);
                LoadIfExists();
            }
        }
        private void SaveAs()
        {
            if (!topicSection.Exists)
            {
                Gui.Dialogs.MustSaveGroupBeforeActionMessageBox(this);
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


        private void ctxFileImportMenu_Click(object sender, EventArgs e)
        {
            Import();
        }

        private void ctxClipboardImportMenu_Click(object sender, EventArgs e)
        {
            ClipboardImport();
        }

        private void ClipboardImport()
        {
            if (Clipboard.ContainsImage())
            {
                if (imageBox.Image == null)
                    ReplaceImage();
                else
                {
                    DialogResult result = Gui.Dialogs.ReplaceCurrentItemQuestionMessageBox(this);
                    if (result == DialogResult.Yes)
                    {
                        ReplaceImage();
                    }
                }
            }
        }

        private void ReplaceImage()
        {
            Image image = Clipboard.GetImage();
            image.Save((topicSection.FilePath));
            image.Dispose();

            LoadIfExists();
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

        private void ctxClipboardSaveAsMenu_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void ctxClipboardCopyMenu_Click(object sender, EventArgs e)
        {
            if (imageBox.Image != null)
                Clipboard.SetImage(imageBox.Image);
        }

        private void ctxEditMenu_Click(object sender, EventArgs e)
        {
            if (File.Exists(ConfigSettings.MsPaintEditorPath) && File.Exists(topicSection.FilePath))
            {
                Process.Start(ConfigSettings.MsPaintEditorPath, topicSection.FilePath);
            }
            else
            {
                Gui.Dialogs.CannotLoadImageEditorMessageBox(this);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadIfExists();
        }
    }
}
