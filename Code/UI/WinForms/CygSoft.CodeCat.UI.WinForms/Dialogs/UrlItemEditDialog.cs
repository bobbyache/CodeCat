using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.txtUrl.Text = urlItem.Url;
            this.txtTitle.Text = urlItem.Title;
            this.txtDescription.Text = urlItem.Description;
            this.cboCategory.Items.AddRange(categories);
            this.cboCategory.Sorted = true;
            this.cboCategory.SelectedItem = string.IsNullOrEmpty(urlItem.Category) ? "Unknown" : urlItem.Category;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
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
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
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
