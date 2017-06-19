using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.Drawing;
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
            : this(null, null, null)
        {

        }

        public VersionedCodeTopicSectionControl(AppFacade application, ITopicDocument topicDocument, IVersionedCodeTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();

            tabControl.Alignment = TabAlignment.Left;

            btnTakeSnapshot = Gui.ToolBar.CreateButton(HeaderToolstrip, "Add Snapshot", Constants.ImageKeys.AddSnapshot, CreateSnapshot);
            btnDeleteSnapshot = Gui.ToolBar.CreateButton(HeaderToolstrip, "Delete Snapshot", Constants.ImageKeys.DeleteSnapshot);

            if (topicDocument == null)
                return;

            //syntaxBox.Document.Text = VersionedCodeTopicSection.Text;
            syntaxDocument.Text = VersionedCodeTopicSection.Text;
            syntaxDocument.SyntaxFile = SyntaxFile;

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
            throw new NotImplementedException();
        }

        private void Base_SyntaxModified(object sender, EventArgs e)
        {
            if (syntaxBox.Document.SyntaxFile != SyntaxFile)
                syntaxBox.Document.SyntaxFile = SyntaxFile;
        }

        private void Base_FontModified(object sender, EventArgs e)
        {
            if (syntaxBox.FontSize != FontSize)
                syntaxBox.FontSize = FontSize;
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
