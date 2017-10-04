using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using System.IO;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    // TODO: Rename this to PdfDocumentCtrl. Also consider moving all items controls into their own directory?
    // TODO: Try and understand this for when your PDF document just dies on you when changing panes.
    // https://sourceforge.net/p/dockpanelsuite/discussion/402316/thread/f29acfe2/
    public partial class PdfDocumentControl :  UserControl, ITopicSectionBaseControl
    {
        public event EventHandler Modified;

        private IPdfViewerTopicSection topicSection;
        private ITopicDocument topicDocument;

        public string Id { get; private set; }
        public string Title { get { return txtTitle.Text; } }
        public int ImageKey { get { return IconRepository.Get(IconRepository.TopicSections.PDF).Index; } }
        public Icon ImageIcon { get { return IconRepository.Get(IconRepository.TopicSections.PDF).Icon; } }
        public Image IconImage { get { return IconRepository.Get(IconRepository.TopicSections.PDF).Image; } }
        public bool IsModified { get; private set; }
        public bool FileExists { get { return false; } }

        public PdfDocumentControl(AppFacade application, ITopicDocument topicDocument, IPdfViewerTopicSection topicSection)
        {
            InitializeComponent();
            this.topicSection = topicSection;
            this.topicDocument = topicDocument;

            btnImport.Image = Gui.Resources.GetImage(Constants.ImageKeys.OpenProject);
            btnReload.Image = Gui.Resources.GetImage(Constants.ImageKeys.NewProject);
            Id = topicSection.Id;

            ResetFieldValues();
            RegisterDataFieldEvents();
            RegisterFileEvents();
            LoadIfExists();
        }

        public void Revert()
        {
        }

        private void LoadIfExists()
        {
            if (topicSection.Exists)
                //pdfControl.src = this.pdfDocument.FilePath;
                pdfControl.LoadFile(topicSection.FilePath);
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
            Modified += CodeItemCtrl_Modified;
        }

        private void codeGroupDocumentSet_ContentSaved(object sender, TopicEventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void codeGroupDocumentSet_BeforeContentSaved(object sender, TopicEventArgs e)
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
            if (!topicSection.FolderExists)
            {
                Gui.Dialogs.MustSaveGroupBeforeActionMessageBox(this);
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
                File.Copy(filePath, topicSection.FilePath, true);
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
