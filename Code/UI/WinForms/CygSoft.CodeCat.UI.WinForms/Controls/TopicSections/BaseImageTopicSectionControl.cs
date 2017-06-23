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
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class BaseImageTopicSectionControl : BaseTopicSectionControl
    {
        private ToolStripLabel lblScrollPosition;
        private ToolStripLabel lblSize;
        private ToolStripLabel lblZoomLevel;

        private ToolStripButton btnRefresh;
        private ToolStripButton btnImport;
        private ToolStripButton btnDisplayText;
        private ToolStripButton btnEditText;

        public BaseImageTopicSectionControl()
            : this(null, null, null)
        {

        }

        public BaseImageTopicSectionControl(AppFacade application, ITopicDocument topicDocument, ITopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();

            if (topicSection == null)
                return;

            lblScrollPosition = CreateImagePropertyToolStripLabel("lblScrollPosition", Constants.ImageKeys.ObjectPosition);
            lblSize = CreateImagePropertyToolStripLabel("lblSize", Constants.ImageKeys.ObjectSize);
            lblZoomLevel = CreateImagePropertyToolStripLabel("lblZoomLevel", Constants.ImageKeys.ObjectZoom);

            
            btnImport = Gui.ToolBar.CreateButton(HeaderToolstrip, "Import", Constants.ImageKeys.OpenProject, (s, e) => { });
            HeaderToolstrip.Items.Add(new ToolStripSeparator());
            btnDisplayText = Gui.ToolBar.CreateButton(HeaderToolstrip, "Display Text", Constants.ImageKeys.ShowText, btnDisplayText_Click);
            btnDisplayText.CheckOnClick = true;
            btnDisplayText.Checked = true;
            btnDisplayText.CheckState = CheckState.Checked;
            btnEditText = Gui.ToolBar.CreateButton(HeaderToolstrip, "Edit Text", Constants.ImageKeys.EditText, btnEditImageText_Click);
            HeaderToolstrip.Items.Add(new ToolStripSeparator());
            btnRefresh = Gui.ToolBar.CreateButton(HeaderToolstrip, "Refresh", Constants.ImageKeys.Refresh, (s, e) => { });
        }

        private ToolStripLabel CreateImagePropertyToolStripLabel(string name, string imageKey)
        {
            ToolStripLabel label = new ToolStripLabel();

            label.Alignment = ToolStripItemAlignment.Right;
            label.Image = Gui.Resources.GetImage(imageKey);
            label.ImageTransparentColor = Color.Magenta;
            label.Name = name;
            label.Size = new Size(29, 22);
            label.Text = "0";

            this.FooterToolstrip.Items.Add(label);

            return label;
        }

        private void btnEditImageText_Click(object sender, EventArgs e)
        {
            //if (pageImage != null)
            //{
            //    ImageDescriptionDialog dialog = new ImageDescriptionDialog();
            //    dialog.Description = pageImage.Description;

            //    DialogResult result = dialog.ShowDialog(this);

            //    if (result == DialogResult.OK)
            //    {
            //        pageImage.Description = dialog.Description;
            //        imageBox.Text = pageImage.Description;
            //        SetModified();
            //    }
            //}
        }

        private void ShowContextMenu(Control ctrl, Point location)
        {
            ctxClipboardImportMenu.Enabled = Clipboard.ContainsImage();
            imageContextMenu.Show(ctrl, location);
        }

        private void Import()
        {
            //if (ImageSetExists())
            //{
            //    if (!topicSection.FolderExists)
            //    {
            //        Gui.Dialogs.MustSaveGroupBeforeAction(this);
            //        return;
            //    }

            //    OpenFileDialog openDialog = new OpenFileDialog();
            //    openDialog.Filter = "Image Files *.png (*.png)|*.png";
            //    openDialog.DefaultExt = "*.png";
            //    openDialog.Title = string.Format("Open Image");
            //    openDialog.AddExtension = true;
            //    openDialog.FilterIndex = 0;
            //    openDialog.CheckPathExists = true;

            //    DialogResult result = openDialog.ShowDialog(this);
            //    string filePath = openDialog.FileName;

            //    if (result == DialogResult.OK)
            //    {
            //        if (imageBox.Image != null)
            //            imageBox.Image.Dispose();

            //        pageImage.SetImage(filePath);
            //        LoadImage(pageImage);
            //        SetModified();
            //    }
            //}
        }

        private void SaveAs()
        {
            //if (ImageSetExists())
            //{
            //    if (!topicSection.FolderExists)
            //    {
            //        Gui.Dialogs.MustSaveGroupBeforeAction(this);
            //        return;
            //    }

            //    SaveFileDialog saveDialog = new SaveFileDialog();
            //    saveDialog.Filter = "Image Files *.png (*.png)|*.png";
            //    saveDialog.DefaultExt = "*.png";
            //    saveDialog.Title = string.Format("Save Image As...");
            //    saveDialog.AddExtension = true;
            //    saveDialog.FilterIndex = 0;
            //    saveDialog.CheckPathExists = true;

            //    DialogResult result = saveDialog.ShowDialog(this);
            //    string filePath = saveDialog.FileName;

            //    if (result == DialogResult.OK)
            //    {
            //        if (imageBox.Image != null)
            //        {
            //            imageBox.Image.Save(filePath);
            //        }
            //    }
            //}
        }

        private void btnDisplayText_Click(object sender, EventArgs e)
        {
            //if (btnDisplayText.Checked)
            //    imageBox.TextDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.Client;
            //else
            //    imageBox.TextDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.None;
        }

        private void SetChangeStatus()
        {
            //lblEditStatus.Text = IsModified ? "Edited" : "Saved";
            //lblEditStatus.ForeColor = IsModified ? Color.DarkRed : Color.Black;
        }

        private void UpdateStatusBar()
        {
            //if (ImageSetExists())
            //{
            //    lblScrollPosition.Text = FormatPoint(imageBox.AutoScrollPosition);
            //    lblSize.Text = FormatRectangle(imageBox.GetImageViewPort());
            //    lblZoomLevel.Text = string.Format("{0}%", imageBox.Zoom);
            //    lblImagePosition.Text = string.Format("Position {0} of {1}", pageImage.Ordinal, topicSection.ImageCount);
            //}
            //else
            //{
            //    lblScrollPosition.Text = "";
            //    lblSize.Text = FormatRectangle(new RectangleF(0, 0, 0, 0));
            //    lblZoomLevel.Text = string.Format("{0}%", 0);
            //    lblImagePosition.Text = string.Format("Position {0} of {1}", 0, 0);
            //}
        }


        //private void LoadImage(IPagerImage imageItem)
        //{
        //    if (imageItem != null)
        //    {
        //        Image image = imageItem.GetDisplayImage();
        //        imageBox.Image = image;
        //        imageBox.Text = imageItem.Description;
        //        imageBox.Zoom = 100;
        //        pageImage = imageItem;
        //        UpdateStatusBar();
        //    }
        //}

        //private void ReplaceImage()
        //{
        //    if (ImageSetExists())
        //    {
        //        Image image = Clipboard.GetImage();
        //        pageImage.SetImage(image);
        //        LoadImage(pageImage);
        //        SetModified();
        //    }
        //}

        //private string FormatRectangle(RectangleF rect)
        //{
        //    return string.Format("X:{0}, Y:{1}, W:{2}, H:{3}", (int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        //}

        //private string FormatPoint(Point point)
        //{
        //    return string.Format("X:{0}, Y:{1}", point.X, point.Y);
        //}
    }
}
