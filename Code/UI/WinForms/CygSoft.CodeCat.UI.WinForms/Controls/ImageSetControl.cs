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

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class ImageSetControl : UserControl, IDocumentItemControl
    {
        private IImageSetDocument imageDocument;
        private ICodeGroupDocumentGroup codeGroupFile;

        public ImageSetControl(AppFacade application, ICodeGroupDocumentGroup codeGroupFile, IImageSetDocument imageDocument)
        {
            InitializeComponent();

            this.Id = imageDocument.Id;

            imageBox.GridScale = Cyotek.Windows.Forms.ImageBoxGridScale.None;
            this.imageDocument = imageDocument;
            this.codeGroupFile = codeGroupFile;

            verticalSplitter.Panel1Collapsed = true;
            horizontalSplitter.Panel2Collapsed = true;

            lblScrollPosition.Image = Resources.GetImage(Constants.ImageKeys.ObjectPosition);
            lblSize.Image = Resources.GetImage(Constants.ImageKeys.ObjectSize);
            lblZoomLevel.Image = Resources.GetImage(Constants.ImageKeys.ObjectZoom);

            ResetFieldValues();

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

        private void ResetFieldValues()
        {
            txtTitle.Text = imageDocument.Title;
            this.IsModified = false;
        }

        private void SetChangeStatus()
        {
            lblEditStatus.Text = this.IsModified ? "Edited" : "Saved";
            lblEditStatus.ForeColor = this.IsModified ? Color.DarkRed : Color.Black;
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

        private void btnShowList_Click(object sender, EventArgs e)
        {
            verticalSplitter.Panel1Collapsed = !btnShowList.Checked;
        }

        private void btnShowDescription_Click(object sender, EventArgs e)
        {
            horizontalSplitter.Panel2Collapsed = !btnShowDescription.Checked;
        }
    }
}
