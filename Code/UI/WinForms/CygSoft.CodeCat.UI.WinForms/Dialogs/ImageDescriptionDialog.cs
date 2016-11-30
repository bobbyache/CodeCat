using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
            get { return this.txtDescription.Text; }
            set 
            {
                string txt = Regex.Replace(value, "(?<!\r)\n", "\r\n");
                this.txtDescription.Text = txt; 
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void ImageDescriptionDialog_Activated(object sender, EventArgs e)
        {
            txtDescription.Focus();
        }
    }
}
