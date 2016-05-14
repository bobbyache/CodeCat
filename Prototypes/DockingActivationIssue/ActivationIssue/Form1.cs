using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ActivationIssue
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AddWindowToLayout(new ActivationForm());
            AddWindowToLayout(new ActivationForm());
            AddWindowToLayout(new ActivationForm());
        }

        //==========================================================================
        public void AddWindowToLayout(DockContent window)
        {
            if (panel1.DocumentStyle == DocumentStyle.SystemMdi)
            {
                window.MdiParent = this;
                window.Show();
            }
            else
            {
                window.Show(panel1);
            }
        }
    }
}
