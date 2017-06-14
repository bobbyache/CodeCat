using System;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Topics;
using System.Windows.Forms;
using System.Drawing;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class CodeItemCtrl : TopicSectionBaseControl
    {
        public event EventHandler FontModified;

        private ToolStripLabel lblSyntax = new ToolStripLabel("lblSyntax");
        private ToolStripSyntaxComboBox cboSyntax = new ToolStripSyntaxComboBox();
        private ToolStripFontSizeComboBox cboFontSize = new ToolStripFontSizeComboBox();

        public override int ImageKey { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Index; } }
        public override Icon ImageIcon { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Icon; } }
        public override Image IconImage { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Image; } }

        public string SyntaxFile {  get { return application.GetSyntaxFile(cboSyntax.Syntax); } }

        public string Syntax
        {
            get { return cboSyntax.Syntax; }
            set { cboSyntax.Syntax = value; }
        }

        public Single FontSize { get { return Convert.ToSingle(cboFontSize.SelectedItem); } }

        public string TemplateText { get { return this.syntaxDocument.Text; } }

        public CodeItemCtrl(AppFacade application, ITopicDocument topicDocument, ICodeTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();

            lblSyntax.Text = "Syntax";

            HeaderToolstrip.Items.Add(lblSyntax);
            HeaderToolstrip.Items.Add(cboSyntax);
            FooterToolstrip.Items.Insert(0, cboFontSize);

            cboFontSize.SetFont(ConfigSettings.DefaultFontSize);

            cboSyntax.LoadSyntaxes(application.GetSyntaxes());
            cboSyntax.Syntax = CodeTopicSection().Syntax;

            syntaxDocument.SyntaxFile = SyntaxFile;
            syntaxBoxControl.Document.Text = CodeTopicSection().Text;

            syntaxBoxControl.TextChanged += (s, e) =>
            {
                if (!base.IsModified)
                    base.Modify();
            };

            cboFontSize.SelectedIndexChanged += (s, e) =>
            {
                if (syntaxBoxControl.FontSize != FontSize)
                    syntaxBoxControl.FontSize = FontSize;
                FontModified?.Invoke(this, new EventArgs());
            };

            cboSyntax.SelectedIndexChanged += cboSyntax_SelectedIndexChanged;
            Reverted += Base_Reverted;
            ContentSaved += Base_ContentSaved;
            UnregisterFieldEvents += Base_UnregisterFieldEvents;
            RegisterFieldEvents += Base_RegisterFieldEvents;
        }

        private ICodeTopicSection CodeTopicSection()
        {
            return base.topicSection as ICodeTopicSection;
        }

        private void Base_RegisterFieldEvents(object sender, EventArgs e)
        {
            syntaxBoxControl.TextChanged += SetModified;
        }

        private void Base_UnregisterFieldEvents(object sender, EventArgs e)
        {
            syntaxBoxControl.TextChanged -= SetModified;
        }

        private void Base_Reverted(object sender, EventArgs e)
        {
            syntaxBoxControl.Document.Text = CodeTopicSection().Text;
        }

        private void Base_ContentSaved(object sender, EventArgs e)
        {
            this.CodeTopicSection().Text = syntaxDocument.Text;
            this.CodeTopicSection().Syntax = Syntax;
        }

        private void cboSyntax_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (syntaxBoxControl.Document.SyntaxFile != SyntaxFile)
                syntaxBoxControl.Document.SyntaxFile = SyntaxFile;
            base.Modify(IconRepository.Get(Syntax).Image);
        }
    }
}
