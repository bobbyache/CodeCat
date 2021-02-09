using System;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Topics;
using System.Windows.Forms;
using System.Drawing;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class BaseCodeTopicSectionControl : BaseTopicSectionControl
    {
        public event EventHandler FontModified;
        public event EventHandler SyntaxModified;

        private readonly ToolStripLabel lblSyntax = new ToolStripLabel("lblSyntax");
        private readonly ToolStripSyntaxComboBox cboSyntax = new ToolStripSyntaxComboBox();
        private readonly ToolStripFontSizeComboBox cboFontSize = new ToolStripFontSizeComboBox();
        public override int ImageKey => IconRepository.Get(cboSyntax.SelectedItem.ToString()).Index;
        public override Icon ImageIcon => IconRepository.Get(cboSyntax.SelectedItem.ToString()).Icon;
        public override Image IconImage => IconRepository.Get(cboSyntax.SelectedItem.ToString()).Image;

        public Single FontSize => Convert.ToSingle(cboFontSize.SelectedItem);

        public string Syntax
        {
            get { return cboSyntax.Syntax; }
            set { cboSyntax.Syntax = value; }
        }
        public BaseCodeTopicSectionControl() : this(null, null, null) { }
        public BaseCodeTopicSectionControl(AppFacade application, ITopicDocument topicDocument, ICodeTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();

            if (topicDocument == null)
                return;

            lblSyntax.Text = "Syntax";

            HeaderToolstrip.Items.Add(lblSyntax);
            HeaderToolstrip.Items.Add(cboSyntax);
            FooterToolstrip.Items.Insert(0, cboFontSize);

            cboFontSize.SetFont(ConfigSettings.DefaultFontSize);

            cboSyntax.LoadSyntaxes(application.GetSyntaxes());
            cboSyntax.Syntax = CodeTopicSection().Syntax;
            base.SetStateImage(IconRepository.Get(Syntax).Image);

            cboFontSize.SelectedIndexChanged += (s, e) => { FontModified?.Invoke(this, new EventArgs()); };
            cboSyntax.SelectedIndexChanged += SyntaxChanged;
        }
        private void SyntaxChanged(object sender, EventArgs e)
        {
            SyntaxModified?.Invoke(this, new EventArgs());
            base.SetStateImage(IconRepository.Get(Syntax).Image);
            base.Modify();
        }

        private ICodeTopicSection CodeTopicSection() => base.topicSection as ICodeTopicSection;
    }
}
