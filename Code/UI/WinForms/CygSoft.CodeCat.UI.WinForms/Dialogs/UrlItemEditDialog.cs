using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    // Grouped Listview: https://msdn.microsoft.com/en-us/library/system.windows.forms.listview.groups%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396

    public partial class UrlItemEditDialog : Form
    {
        private IUrlItem urlItem;

        public UrlItemEditDialog(IUrlItem urlItem, string[] categories)
        {
            InitializeComponent();
            this.urlItem = urlItem;
            txtUrl.Text = urlItem.Url;
            txtTitle.Text = urlItem.Title;
            txtDescription.Text = urlItem.Description;
            cboCategory.Items.AddRange(categories);
            cboCategory.Sorted = true;
            cboCategory.SelectedItem = string.IsNullOrEmpty(urlItem.Category) ? "Unknown" : urlItem.Category;
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
                urlItem.Title = txtTitle.Text;
                urlItem.Url = txtUrl.Text;
                urlItem.Description = txtDescription.Text;
                urlItem.DateModified = DateTime.Now;
                urlItem.Category = string.IsNullOrEmpty(cboCategory.Text) ? "Unknown" : cboCategory.Text.ToString();
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
