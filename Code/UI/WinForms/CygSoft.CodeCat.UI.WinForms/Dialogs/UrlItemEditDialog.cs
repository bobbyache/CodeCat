using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    // Grouped Listview: https://msdn.microsoft.com/en-us/library/system.windows.forms.listview.groups%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396

    public partial class UrlItemEditDialog : Form
    {
        private IWebReference webReference;

        public UrlItemEditDialog(IWebReference webReference, string[] categories)
        {
            InitializeComponent();
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
                Dialogs.NoInputValueForMandatoryField(this, "Title");
                return false;
            }
            if (txtUrl.Text.Trim() == "")
            {
                Dialogs.NoInputValueForMandatoryField(this, "Url (hyperlink)");
                return false;
            }
            if (txtDescription.Text.Trim() == "")
            {
                Dialogs.NoInputValueForMandatoryField(this, "Description");
                return false;
            }
            return true;
        }
    }
}
