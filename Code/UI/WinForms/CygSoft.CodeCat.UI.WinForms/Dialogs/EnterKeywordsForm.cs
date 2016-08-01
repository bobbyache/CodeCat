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
    public partial class EnterKeywordsForm : Form
    {
        public string Keywords
        {
            get { return this.textBox1.Text.Trim().ToUpper(); }
        }
        public EnterKeywordsForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void EnterKeywordsForm_Activated(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
    }
}
