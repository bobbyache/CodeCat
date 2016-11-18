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
    public partial class UrlItemEditDialog : Form
    {
        private IUrlItem urlItem;

        public UrlItemEditDialog(IUrlItem urlItem)
        {
            InitializeComponent();
            this.urlItem = urlItem;
            this.txtUrl.Text = urlItem.Url;
            this.txtTitle.Text = urlItem.Title;
            this.txtDescription.Text = urlItem.Description;
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
