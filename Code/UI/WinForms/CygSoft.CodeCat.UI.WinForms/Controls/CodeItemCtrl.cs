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
        private ToolStripLabel lblSyntax = new ToolStripLabel("lblSyntax");
        private ToolStripComboBox cboSyntax = new ToolStripComboBox("cboSyntax");

        public override int ImageKey { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Index; } }
        public override Icon ImageIcon { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Icon; } }
        public override Image IconImage { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Image; } }

        public string TemplateText { get { return this.syntaxDocument.Text; } }

        public string SyntaxFile
        {
            get
            {
                string currentSyntax = cboSyntax.SelectedItem.ToString();
                string syn = string.IsNullOrEmpty(currentSyntax) ? ConfigSettings.DefaultSyntax.ToUpper() : currentSyntax.ToUpper();
                string syntaxFile = application.GetSyntaxFile(syn);
                return syntaxFile;
            }
        }

        public string Syntax
        {
            get
            {
                string currentSyntax = cboSyntax.SelectedItem.ToString();
                string syntax = string.IsNullOrEmpty(currentSyntax) ? ConfigSettings.DefaultSyntax.ToUpper() : currentSyntax.ToUpper();
                return syntax;
            }
            set
            {
                string syntax = string.IsNullOrEmpty(value) ? ConfigSettings.DefaultSyntax.ToUpper() : value.ToUpper();
                int index = cboSyntax.FindStringExact(syntax);
                if (index >= 0)
                    cboSyntax.SelectedIndex = index;
            }
        }

        public CodeItemCtrl(AppFacade application, ITopicDocument topicDocument, ICodeTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();
            lblSyntax.Text = "Syntax";

            HeaderToolstrip.Items.Add(lblSyntax);
            HeaderToolstrip.Items.Add(cboSyntax);
            InitializeSyntaxList();
            Syntax = CodeTopicSection().Syntax;
            syntaxDocument.SyntaxFile = SyntaxFile;
            syntaxBoxControl.Document.Text = CodeTopicSection().Text;

            syntaxBoxControl.TextChanged += (s, e) =>
            {
                if (!base.IsModified)
                    base.Modify();
            };

            cboSyntax.SelectedIndexChanged += cboSyntax_SelectedIndexChanged;
            FontModified += Base_FontChanged;
            Reverted += Base_Reverted;
            ContentSaved += Base_ContentSaved;
            UnregisterFieldEvents += Base_UnregisterFieldEvents;
            RegisterFieldEvents += Base_RegisterFieldEvents;
        }

        private void Base_FontChanged(object sender, EventArgs e)
        {
            if (syntaxBoxControl.FontSize != base.FontSize)
                syntaxBoxControl.FontSize = base.FontSize;
        }

        private void InitializeSyntaxList()
        {
            cboSyntax.Items.Clear();
            cboSyntax.Items.AddRange(application.GetSyntaxes());
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
