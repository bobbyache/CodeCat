using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.DocumentManager.Infrastructure;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class SimpleCodeTopicSectionControl : BaseCodeTopicSectionControl
    {
        public string TemplateText { get { return this.syntaxDocument.Text; } }
        public string SyntaxFile { get { return application.GetSyntaxFile(base.Syntax); } }

        public SimpleCodeTopicSectionControl()
            : this(null, null, null)
        {

        }

        public SimpleCodeTopicSectionControl(AppFacade application, ITopicDocument topicDocument, ICodeTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();

            if (topicDocument == null)
                return;

            syntaxBox.Document.Text = CodeTopicSection().Text;
            syntaxDocument.SyntaxFile = SyntaxFile;

            syntaxBox.TextChanged += (s, e) =>
            {
                if (!base.IsModified)
                    base.Modify();
            };

            FontModified += Base_FontModified;
            SyntaxModified += Base_SyntaxModified;
            Reverted += Base_Reverted;
            ContentSaved += Base_ContentSaved;
            UnregisterFieldEvents += Base_UnregisterFieldEvents;
            RegisterFieldEvents += Base_RegisterFieldEvents;
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

        private ICodeTopicSection CodeTopicSection()
        {
            return base.topicSection as ICodeTopicSection;
        }

        private void Base_Reverted(object sender, EventArgs e)
        {
            syntaxBox.Document.Text = CodeTopicSection().Text;
        }

        private void Base_ContentSaved(object sender, EventArgs e)
        {
            this.CodeTopicSection().Text = syntaxDocument.Text;
            this.CodeTopicSection().Syntax = Syntax;
        }
    }
}
