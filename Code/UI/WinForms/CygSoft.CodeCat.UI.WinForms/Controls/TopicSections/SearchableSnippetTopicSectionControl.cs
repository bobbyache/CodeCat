using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using CygSoft.CodeCat.Domain.TopicSections.SearchableSnippet;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class SearchableSnippetTopicSectionControl : BaseCodeTopicSectionControl
    {
        public string SyntaxFile { get { return application.GetSyntaxFile(base.Syntax); } }

        private ICodeTopicSection SearchableSnippetTopicSection
        {
            get { return base.topicSection as ISearchableSnippetTopicSection; }
        }

        public SearchableSnippetTopicSectionControl()
            : this(null, null, null)
        {

        }

        public SearchableSnippetTopicSectionControl(AppFacade application, ITopicDocument topicDocument, ISearchableSnippetTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();

            if (topicDocument == null)
                return;

            btnFind.Image = Gui.Resources.GetImage(Constants.ImageKeys.FindSnippets);

            syntaxBox.Document.Text = SearchableSnippetTopicSection.Text;
            syntaxDocument.SyntaxFile = SyntaxFile;

            HeaderToolstrip.Items.Add(CreateButton());
            HeaderToolstrip.Items.Add(CreateButton());
            HeaderToolstrip.Items.Add(CreateButton());
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

        private void Base_Reverted(object sender, EventArgs e)
        {
            syntaxBox.Document.Text = string.Empty;
        }

        private void Base_ContentSaved(object sender, EventArgs e)
        {
            this.SearchableSnippetTopicSection.Text = string.Empty;
            this.SearchableSnippetTopicSection.Syntax = Syntax;
        }

        private ToolStripButton CreateButton()
        {
            ToolStripButton btn = new ToolStripButton();
            btn.Alignment = ToolStripItemAlignment.Right;
            btn.Text = "Test";
            return btn;
        }
    }
}
