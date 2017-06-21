using CygSoft.CodeCat.Domain.TopicSections.SearchableSnippet;
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
        public ISearchableSnippetKeywordIndexItem CodeSnippet { get; private set; }

        public SearchableSnippetEditDialog(ISearchableSnippetKeywordIndexItem codeSnippet)
        {
            InitializeComponent();
            CodeSnippet = codeSnippet;
            txtTitle.Text = codeSnippet?.Title;
            txtKeywords.Text = codeSnippet?.CommaDelimitedKeywords;
            syntaxDocument.Text = codeSnippet?.Text;
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
                CodeSnippet.AddKeywords(txtKeywords.Text);
                CodeSnippet.Title = txtTitle.Text.Trim();
                CodeSnippet.Text = syntaxDocument.Text;

                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidateFields()
        {
            if (txtTitle.Text.Trim() == "")
            {
                Gui.Dialogs.NoInputValueForMandatoryField(this, "Title");
                return false;
            }

            if (txtKeywords.Text.Trim() == "")
            {
                Gui.Dialogs.NoInputValueForMandatoryField(this, "Keywords");
                return false;
            }

            if (syntaxDocument.Text.Trim() == "")
            {
                Gui.Dialogs.NoInputValueForMandatoryField(this, "Code");
                return false;
            }

            return true;
        }
    }
}
