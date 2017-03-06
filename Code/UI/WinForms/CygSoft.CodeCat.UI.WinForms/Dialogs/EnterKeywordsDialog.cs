using System;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class EnterKeywordsDialog : Form
    {
        public string Keywords
        {
            get { return textBox1.Text.Trim().ToUpper(); }
        }
        public EnterKeywordsDialog()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void EnterKeywordsDialog_Activated(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
    }
}
