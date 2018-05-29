using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using CygSoft.CodeCat.UI.Resources.Infrastructure;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using PdfiumViewer;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    // Try this: https://github.com/pvginkel/PdfiumViewer

    // TODO: Try and understand this for when your PDF document just dies on you when changing panes.
    // https://sourceforge.net/p/dockpanelsuite/discussion/402316/thread/f29acfe2/
    public partial class PdfDocumentControl : BaseTopicSectionControl
    {
        private ToolStripButton btnReload;
        private ToolStripButton btnImport;

        public override int ImageKey { get { return IconRepository.Get(IconRepository.TopicSections.PDF).Index; } }
        public override Icon ImageIcon { get { return IconRepository.Get(IconRepository.TopicSections.PDF).Icon; } }
        public override Image IconImage { get { return IconRepository.Get(IconRepository.TopicSections.PDF).Image; } }

        public PdfDocumentControl(IAppFacade application, IImageResources imageResources, ITopicDocument topicDocument, IPdfViewerTopicSection topicSection)
            : base(application, imageResources, topicDocument, topicSection)
        {
            InitializeComponent();

            btnImport = Gui.ToolBar.CreateButton(HeaderToolstrip, "Import", imageResources.GetImage(ImageKeys.OpenProject), btnImport_Click);
            btnReload = Gui.ToolBar.CreateButton(HeaderToolstrip, "Reload", imageResources.GetImage(ImageKeys.NewProject), btnReload_Click);
            
            LoadIfExists();
        }

        private void LoadIfExists()
        {
            if (topicSection.Exists)
            {
                pdfViewer1.Document = PdfDocument.Load(topicSection.FilePath);
                ((IPdfViewerTopicSection)topicSection).Document = pdfViewer1.Document;
            }
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
