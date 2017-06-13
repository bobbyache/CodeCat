using System;
using System.Drawing;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Topics;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class CodeItemCtrl : TopicSectionBaseControl, IDocumentItemControl
    {
        public CodeItemCtrl(AppFacade application, ITopicDocument topicDocument, ICodeTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();

            syntaxDocument.SyntaxFile = base.SyntaxFile;
            
            syntaxBoxControl.Document.Text = CodeTopicSection().Text;

            syntaxBoxControl.TextChanged += (s, e) =>
            {
                if (!base.IsModified)
                    base.Modify();
            };

            Reverted += Base_Reverted;
            Modified += Base_Modified;
            ContentSaved += Base_ContentSaved;
            UnregisterFieldEvents += Base_UnregisterFieldEvents;
            RegisterFieldEvents += Base_RegisterFieldEvents;
        }

        private void Base_Modified(object sender, EventArgs e)
        {
            syntaxBoxControl.TextChanged -= SetModified;
            if (syntaxBoxControl.FontSize != base.FontSize)
                syntaxBoxControl.FontSize = base.FontSize;
            if (syntaxBoxControl.Document.SyntaxFile != base.SyntaxFile)
                syntaxBoxControl.Document.SyntaxFile = base.SyntaxFile;
            syntaxBoxControl.TextChanged += SetModified;
        }

        public string TemplateText { get { return this.syntaxDocument.Text; } }
        public bool FileExists { get { return topicSection.Exists; } }

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
    }
}
