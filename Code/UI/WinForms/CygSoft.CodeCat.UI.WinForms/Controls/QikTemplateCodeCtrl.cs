using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class QikTemplateCodeCtrl : UserControl
    {
        public QikTemplateCodeCtrl()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return this.txtTitle.Text; }
            set { this.txtTitle.Text = value; }
        }

        public string TemplateText
        {
            get { return this.syntaxBoxControl1.Document.Text; }
            set { this.syntaxBoxControl1.Document.Text = value; }
        }

        //public string Syntax
        //{
        //    get { return this.cboSyntax.Text; }
        //    set { this.txtTitle.Text = value; }
        //}
    }
}
