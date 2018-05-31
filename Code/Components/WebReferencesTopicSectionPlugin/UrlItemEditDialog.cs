using CygSoft.CodeCat.Infrastructure.TopicSections;
using System;
using System.Windows.Forms;

namespace WebReferencesTopicSectionPlugin
{
    // Grouped Listview: https://msdn.microsoft.com/en-us/library/system.windows.forms.listview.groups%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396

    public partial class UrlItemEditDialog : Form
    {
        private IWebReference webReference;
        private Dialogs dialogs;

        public UrlItemEditDialog(Dialogs dialogs, IWebReference webReference, string[] categories)
        {
            InitializeComponent();
            this.dialogs = dialogs;
            this.webReference = webReference;
            txtUrl.Text = webReference.Url;
            txtTitle.Text = webReference.Title;
            txtDescription.Text = webReference.Description;
            cboCategory.Items.AddRange(categories);
            cboCategory.Sorted = true;
            cboCategory.SelectedItem = string.IsNullOrEmpty(webReference.Category) ? "Unknown" : webReference.Category;
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
                webReference.Title = txtTitle.Text;
                webReference.Url = txtUrl.Text;
                webReference.Description = txtDescription.Text;
                webReference.DateModified = DateTime.Now;
                webReference.Category = string.IsNullOrEmpty(cboCategory.Text) ? "Unknown" : cboCategory.Text.ToString();
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidateFields()
        {
            if (txtTitle.Text.Trim() == "")
            {
                dialogs.MissingRequiredFieldMessageBox(this.ParentForm, "Title");
                return false;
            }
            if (txtUrl.Text.Trim() == "")
            {
                dialogs.MissingRequiredFieldMessageBox(this, "Url (hyperlink)");
                return false;
            }
            return true;
        }
    }
}
