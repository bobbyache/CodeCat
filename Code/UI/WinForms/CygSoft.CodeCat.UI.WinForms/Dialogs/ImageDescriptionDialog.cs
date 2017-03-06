using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class ImageDescriptionDialog : Form
    {
        public ImageDescriptionDialog()
        {
            InitializeComponent();
        }

        public string Description
        {
            get { return txtDescription.Text; }
            set 
            {
                string txt = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                txtDescription.Text = txt; 
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void ImageDescriptionDialog_Activated(object sender, EventArgs e)
        {
            txtDescription.Focus();
        }
    }
}
