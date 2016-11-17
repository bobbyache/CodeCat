using System;
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
    // TODO: Try and understand this for when your PDF document just dies on you when changing panes.
    // https://sourceforge.net/p/dockpanelsuite/discussion/402316/thread/f29acfe2/
    public partial class PdfDocumentControl :  UserControl, IDocumentItemControl
    {
        private IPdfDocument pdfDocument;
        private ICodeGroupDocumentGroup codeGroupFile;

        public PdfDocumentControl(AppFacade application, ICodeGroupDocumentGroup codeGroupFile, IPdfDocument pdfDocument)
        {
            InitializeComponent();
            this.pdfDocument = pdfDocument;
            this.codeGroupFile = codeGroupFile;

            this.btnImport.Image = Resources.GetImage(Constants.ImageKeys.OpenProject);
            this.btnReload.Image = Resources.GetImage(Constants.ImageKeys.NewProject);
            this.Id = pdfDocument.Id;

            ResetFieldValues();
            RegisterDataFieldEvents();
            RegisterFileEvents();
            LoadIfExists();
        }

        private void LoadIfExists()
        {
            if (this.pdfDocument.Exists)
                pdfControl.LoadFile(this.pdfDocument.FilePath);
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
            txtTitle.Text = pdfDocument.Title;
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
            this.pdfDocument.Title = txtTitle.Text;
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
            if (!Directory.Exists(Path.GetDirectoryName(this.pdfDocument.FilePath)))
            {
                Dialogs.MustSaveGroupBeforeAction(this);
                return;
            }

            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "PDF Files *.pdf (*.pdf)|*.pdf";
            openDialog.DefaultExt = "*.pdf";
            openDialog.Title = string.Format("Open PDF");
            openDialog.AddExtension = true;
            openDialog.FilterIndex = 0;
            openDialog.CheckPathExists = true;

            DialogResult result = openDialog.ShowDialog(this);
            string filePath = openDialog.FileName;

            if (result == DialogResult.OK)
            {
                File.Copy(filePath, this.pdfDocument.FilePath, true);
                LoadIfExists();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            // hack to reload the control when it loses itself when changing panes.
            LoadIfExists();
        }
    }
}
