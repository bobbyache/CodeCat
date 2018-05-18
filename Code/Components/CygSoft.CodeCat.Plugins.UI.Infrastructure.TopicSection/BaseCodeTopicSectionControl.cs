using CygSoft.CodeCat.Plugins.TopicSection.Infrastructure;
using CygSoft.CodeCat.UI.Resources.Infrastructure;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CygSoft.CodeCat.Plugins.TopicSection.UI.WinForms
{
    public partial class BaseCodeTopicSectionControl : BaseTopicSectionControl
    {
        public event EventHandler FontModified;
        public event EventHandler SyntaxModified;

        private ToolStripLabel lblSyntax = new ToolStripLabel("lblSyntax");
        private ToolStripSyntaxComboBox cboSyntax = new ToolStripSyntaxComboBox();
        private ToolStripFontSizeComboBox cboFontSize = new ToolStripFontSizeComboBox();

        public override int ImageKey { get { return -1; /* IconRepository.Get(cboSyntax.SelectedItem.ToString()).Index; */ } }
        public override Icon ImageIcon { get { return null; /* IconRepository.Get(cboSyntax.SelectedItem.ToString()).Icon; */ } }
        public override Image IconImage { get { return null; /* IconRepository.Get(cboSyntax.SelectedItem.ToString()).Image; */ } }

        public string Syntax
        {
            get { return cboSyntax.Syntax; }
            set { cboSyntax.Syntax = value; }
        }

        public Single FontSize { get { return Convert.ToSingle(cboFontSize.SelectedItem); } }

        

        public BaseCodeTopicSectionControl()
            : this(null, null, null, null)
        {

        }

        public BaseCodeTopicSectionControl(/* AppFacade */ object application, IImageResources imageResources, ITopicDocument topicDocument, ICodeTopicSection topicSection)
            : base(application, imageResources, topicDocument, topicSection)
        {
            InitializeComponent();

            if (topicDocument == null)
                return;

            lblSyntax.Text = "Syntax";

            HeaderToolstrip.Items.Add(lblSyntax);
            HeaderToolstrip.Items.Add(cboSyntax);
            FooterToolstrip.Items.Insert(0, cboFontSize);

            cboFontSize.SetFont(-1 /* ConfigSettings.DefaultFontSize */);

            // ********** THIS WILL HAVE TO BE REIMPLEMENTED *************
            cboSyntax.LoadSyntaxes(null /* application.GetSyntaxes() */);

            cboSyntax.Syntax = CodeTopicSection().Syntax;
            base.SetStateImage(null /*IconRepository.Get(Syntax).Image */);

            cboFontSize.SelectedIndexChanged += (s, e) => { FontModified?.Invoke(this, new EventArgs()); };
            cboSyntax.SelectedIndexChanged += cboSyntax_SelectedIndexChanged;
        }

        private ICodeTopicSection CodeTopicSection()
        {
            return base.topicSection as ICodeTopicSection;
        }

        private void cboSyntax_SelectedIndexChanged(object sender, EventArgs e)
        {
            SyntaxModified?.Invoke(this, new EventArgs());
            base.SetStateImage(null /* IconRepository.Get(Syntax).Image */);
            base.Modify();
        }
    }
}
