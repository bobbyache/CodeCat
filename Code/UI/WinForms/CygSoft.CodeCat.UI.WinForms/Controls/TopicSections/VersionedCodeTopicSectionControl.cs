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

        public VersionedCodeTopicSectionControl()
            : this(null, null, null)
        {

        }

        public VersionedCodeTopicSectionControl(AppFacade application, ITopicDocument topicDocument, IVersionedCodeTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();

            tabControl.Alignment = TabAlignment.Left;

            btnTakeSnapshot = Gui.ToolBar.CreateButton(HeaderToolstrip, "Add Snapshot", Constants.ImageKeys.AddSnapshot);
            btnDeleteSnapshot = Gui.ToolBar.CreateButton(HeaderToolstrip, "Delete Snapshot", Constants.ImageKeys.DeleteSnapshot);            
        }
    }
}
