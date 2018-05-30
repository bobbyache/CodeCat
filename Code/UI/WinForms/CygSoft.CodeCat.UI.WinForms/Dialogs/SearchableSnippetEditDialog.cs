using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.TopicSections.SearchableSnippet;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Dialogs
{
    public partial class SearchableSnippetEditDialog : Form
    {
        private IAppFacade application;
        private IIconRepository iconRepository;
        public ISearchableSnippetKeywordIndexItem CodeSnippet { get; private set; }

        public SearchableSnippetEditDialog(IAppFacade application, IIconRepository iconRepository, ISearchableSnippetKeywordIndexItem codeSnippet, string[] categories)
        {
            InitializeComponent();

            if (iconRepository == null)
                throw new ArgumentNullException("Image Repository is a required constructor parameter and cannot be null");

            if (application == null)
                return;

            this.Icon = iconRepository.Get(codeSnippet.Syntax).Icon;
            this.application = application;
            CodeSnippet = codeSnippet;
            txtTitle.Text = codeSnippet?.Title;
            txtKeywords.Text = codeSnippet?.CommaDelimitedKeywords;
            syntaxDocument.SyntaxFile = application.GetSyntaxFile(codeSnippet.Syntax);
            syntaxDocument.Text = codeSnippet?.Text;

            InitializeCategoryCombo(categories);
            

            cboSyntax.LoadSyntaxes(application.GetSyntaxes());
            cboSyntax.Syntax = CodeSnippet.Syntax;

            cboSyntax.SelectedIndexChanged += cboSyntax_SelectedIndexChanged;
        }

        private void cboSyntax_SelectedIndexChanged(object sender, EventArgs e)
        {
            syntaxDocument.SyntaxFile = application.GetSyntaxFile(cboSyntax.Syntax);
            this.Icon = iconRepository.Get(cboSyntax.Syntax).Icon;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                CodeSnippet.SetKeywords(txtKeywords.Text);
                CodeSnippet.Title = txtTitle.Text.Trim();
                CodeSnippet.Text = syntaxDocument.Text;
                CodeSnippet.Syntax = cboSyntax.Syntax;
                CodeSnippet.Category = string.IsNullOrEmpty(cboCategory.Text) ? "Unknown" : cboCategory.Text.ToString();

                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidateFields()
        {
            if (txtTitle.Text.Trim() == "")
            {
                Gui.Dialogs.MissingRequiredFieldMessageBox(this, "Title");
                return false;
            }

            if (txtKeywords.Text.Trim() == "")
            {
                Gui.Dialogs.MissingRequiredFieldMessageBox(this, "Keywords");
                return false;
            }

            if (syntaxDocument.Text.Trim() == "")
            {
                Gui.Dialogs.MissingRequiredFieldMessageBox(this, "Code");
                return false;
            }

            return true;
        }

        private void InitializeCategoryCombo(string[] categories)
        {
            cboCategory.Items.AddRange(categories);
            cboCategory.Sorted = true;
            cboCategory.SelectedItem = string.IsNullOrEmpty(CodeSnippet.Category) ? "Unknown" : CodeSnippet.Category;
        }
    }
}
