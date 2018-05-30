using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using CygSoft.CodeCat.UI.WinForms.CustomControls;
using CygSoft.CodeCat.UI.WinForms.TopicSectionBase;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.TopicSectionBase
{
    public partial class BaseCodeTopicSectionControl : BaseTopicSectionControl
    {
        public event EventHandler FontModified;
        public event EventHandler SyntaxModified;

        private ToolStripLabel lblSyntax = new ToolStripLabel("lblSyntax");
        private ToolStripSyntaxComboBox cboSyntax = new ToolStripSyntaxComboBox();
        private ToolStripFontSizeComboBox cboFontSize = new ToolStripFontSizeComboBox();

        public override int ImageKey { get { return imageResources.Get(cboSyntax.SelectedItem.ToString()).Index; } }
        public override Icon ImageIcon { get { return imageResources.Get(cboSyntax.SelectedItem.ToString()).Icon; } }
        public override Image IconImage { get { return imageResources.Get(cboSyntax.SelectedItem.ToString()).Image; } }

        

        public string Syntax
        {
            get { return cboSyntax.Syntax; }
            set { cboSyntax.Syntax = value; }
        }

        public Single FontSize { get { return Convert.ToSingle(cboFontSize.SelectedItem); } }

        

        public BaseCodeTopicSectionControl()
            : this(null, null, null, null, null, -1)
        {

        }

        public BaseCodeTopicSectionControl(IAppFacade application, IImageResources imageResources, 
            ITopicDocument topicDocument, ICodeTopicSection topicSection, string defaultSyntax, int defaultFontSize)
            : base(application, imageResources, topicDocument, topicSection)
        {
            InitializeComponent();

            if (topicDocument == null)
                return;

            lblSyntax.Text = "Syntax";

            HeaderToolstrip.Items.Add(lblSyntax);
            HeaderToolstrip.Items.Add(cboSyntax);
            FooterToolstrip.Items.Insert(0, cboFontSize);

            cboFontSize.SetFont(defaultFontSize);

            cboSyntax.DefaultSyntax = defaultSyntax;
            cboSyntax.LoadSyntaxes(application.GetSyntaxes());
            cboSyntax.Syntax = CodeTopicSection().Syntax;
            base.SetStateImage(imageResources.Get(Syntax).Image);

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
            base.SetStateImage(imageResources.Get(Syntax).Image);
            base.Modify();
        }
    }
}
