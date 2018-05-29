using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using CygSoft.CodeCat.UI.Resources.Infrastructure;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class VersionedCodeTopicSectionControl : BaseCodeTopicSectionControl
    {
        private ToolStripButton btnTakeSnapshot;
        private ToolStripButton btnDeleteSnapshot;

        public string TemplateText { get { return this.syntaxDocument.Text; } }
        public string SyntaxFile { get { return application.GetSyntaxFile(base.Syntax); } }

        private IVersionedCodeTopicSection VersionedCodeTopicSection
        {
            get { return base.topicSection as IVersionedCodeTopicSection; }
        }

        public VersionedCodeTopicSectionControl()
            : this(null, null, null, null)
        {

        }

        public VersionedCodeTopicSectionControl(IAppFacade application, IImageResources imageResources, ITopicDocument topicDocument, IVersionedCodeTopicSection topicSection)
            : base(application, imageResources, topicDocument, topicSection)
        {
            InitializeComponent();

            tabControl.Alignment = TabAlignment.Left;

            btnTakeSnapshot = Gui.ToolBar.CreateButton(HeaderToolstrip, "Add Snapshot", imageResources.GetImage(ImageKeys.AddSnapshot), CreateSnapshot);
            btnDeleteSnapshot = Gui.ToolBar.CreateButton(HeaderToolstrip, "Delete Snapshot", imageResources.GetImage(ImageKeys.DeleteSnapshot), DeleteSnapshot);

            if (topicDocument == null)
                return;

            //syntaxBox.Document.Text = VersionedCodeTopicSection.Text;
            syntaxDocument.Text = VersionedCodeTopicSection.Text;
            syntaxDocument.SyntaxFile = SyntaxFile;
            snapshotListCtrl1.SyntaxFile = SyntaxFile;

            snapshotListCtrl1.Attach(topicSection);

            syntaxBox.TextChanged += (s, e) =>
            {
                if (!base.IsModified)
                    base.Modify();
            };

            snapshotListCtrl1.SnapshotSelectionChanged += (s, e) =>
            {
                btnDeleteSnapshot.Enabled = (snapshotListCtrl1.SelectedSnapshot != null && tabControl.SelectedTab == tabPageSnapshots);
            };

            FontModified += Base_FontModified;
            SyntaxModified += Base_SyntaxModified;
            Reverted += Base_Reverted;
            ContentSaved += Base_ContentSaved;
            UnregisterFieldEvents += Base_UnregisterFieldEvents;
            RegisterFieldEvents += Base_RegisterFieldEvents;
        }

        private void CreateSnapshot(object sender, EventArgs e)
        {
            if (this.IsModified)
            {
                Gui.Dialogs.InvalidSnapshotRequestMessageBox(this);
                return;
            }

            // ok to continue...
            SnapshotDescDialog frm = new SnapshotDescDialog();
            DialogResult result = frm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                VersionedCodeTopicSection.CreateVersion(frm.Description);
            }
        }

        private void DeleteSnapshot(object sender, EventArgs e)
        {
            this.snapshotListCtrl1.DeleteSnapshot();
        }

        private void Base_SyntaxModified(object sender, EventArgs e)
        {
            if (syntaxBox.Document.SyntaxFile != SyntaxFile)
            {
                syntaxBox.Document.SyntaxFile = SyntaxFile;
                snapshotListCtrl1.SyntaxFile = SyntaxFile;
            }
        }

        private void Base_FontModified(object sender, EventArgs e)
        {
            if (syntaxBox.FontSize != FontSize)
            {
                syntaxBox.FontSize = FontSize;
                snapshotListCtrl1.EditorFontSize = FontSize;
            }
        }

        private void Base_RegisterFieldEvents(object sender, EventArgs e)
        {
            syntaxBox.TextChanged += SetModified;
        }

        private void Base_UnregisterFieldEvents(object sender, EventArgs e)
        {
            syntaxBox.TextChanged -= SetModified;
        }

        private void Base_Reverted(object sender, EventArgs e)
        {
            syntaxBox.Document.Text = VersionedCodeTopicSection.Text;
        }

        private void Base_ContentSaved(object sender, EventArgs e)
        {
            this.VersionedCodeTopicSection.Text = syntaxDocument.Text;
            this.VersionedCodeTopicSection.Syntax = Syntax;
        }
    }
}
