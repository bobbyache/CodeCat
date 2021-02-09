using System;
using System.Drawing;
using System.Windows.Forms;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using System.IO;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using PdfiumViewer;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    // Try this: https://github.com/pvginkel/PdfiumViewer
    // TODO: Try and understand this for when your PDF document just dies on you when changing panes.
    // https://sourceforge.net/p/dockpanelsuite/discussion/402316/thread/f29acfe2/
    public partial class PdfDocumentControl : BaseTopicSectionControl
    {
        public override int ImageKey { get { return IconRepository.Get(IconRepository.TopicSections.PDF).Index; } }
        public override Icon ImageIcon { get { return IconRepository.Get(IconRepository.TopicSections.PDF).Icon; } }
        public override Image IconImage { get { return IconRepository.Get(IconRepository.TopicSections.PDF).Image; } }

        public PdfDocumentControl(AppFacade application, ITopicDocument topicDocument, IPdfViewerTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();

            Gui.ToolBar.CreateButton(HeaderToolstrip, "Import", Constants.ImageKeys.OpenProject, ImportButton_Click);
            Gui.ToolBar.CreateButton(HeaderToolstrip, "Reload", Constants.ImageKeys.NewProject, ReloadButton_Click);
            
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

        private void ImportButton_Click(object sender, EventArgs e)
        {
            if (!topicSection.FolderExists)
            {
                Gui.Dialogs.MustSaveGroupBeforeActionMessageBox(this);
                return;
            }

            var openDialog = new OpenFileDialog
            {
                Filter = "PDF Files *.pdf (*.pdf)|*.pdf",
                DefaultExt = "*.pdf",
                Title = string.Format("Open PDF"),
                AddExtension = true,
                FilterIndex = 0,
                CheckPathExists = true
            };

            var result = openDialog.ShowDialog(this);
            var filePath = openDialog.FileName;

            if (result == DialogResult.OK)
            {
                File.Copy(filePath, topicSection.FilePath, true);
                LoadIfExists();
            }
        }

        private void ReloadButton_Click(object sender, EventArgs e)
        {
            // hack to reload the control when it loses itself when changing panes.
            LoadIfExists();
        }

        
    }
}
