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
    public partial class SnapshotDescDialog : Form
    {
        public string Description
        {
            get { return this.textBox.Text.Trim(); }
            set { this.textBox.Text = value; }
        }

        public SnapshotDescDialog()
        {
            InitializeComponent();
        }

    }
}
