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
using CygSoft.CodeCat.UI.WinForms.Controls.TopicSections;
using PdfiumViewer;

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

        public PdfDocumentControl(AppFacade application, ITopicDocument topicDocument, IPdfViewerTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();

            btnImport = Gui.ToolBar.CreateButton(HeaderToolstrip, "Import", Constants.ImageKeys.OpenProject, btnImport_Click);
            btnReload = Gui.ToolBar.CreateButton(HeaderToolstrip, "Reload", Constants.ImageKeys.NewProject, btnReload_Click);
            
            LoadIfExists();
        }

        private void LoadIfExists()
        {

            if (topicSection.Exists)
                pdfViewer1.Document = PdfDocument.Load(topicSection.FilePath);
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
