using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ActivationIssue
{
    public partial class ActivationForm : DockContent
    {
        public ActivationForm()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            textBox1.Text = "Activated";
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            textBox1.Text = "Deactivated";
        }
        //=========================================================================
        public void DockForm_DockStateChanged(object sender, EventArgs e)
        {
            if (IsFloat)
            {
                Pane.IsActivatedChanged -= new EventHandler(Pane_IsActivatedChanged);
                FloatPane.IsActivatedChanged += new EventHandler(FloatPane_IsActivatedChanged);
                richTextBox1.Text = "window is now listening to floating messages.";
            }
            else
            {
                FloatPane.IsActivatedChanged -= new EventHandler(FloatPane_IsActivatedChanged);
                Pane.IsActivatedChanged += new EventHandler(Pane_IsActivatedChanged);
                richTextBox1.Text = "window is now listening to pane messages.";
            }
        }

        //=========================================================================
        public void FloatPane_IsActivatedChanged(object sender, EventArgs e)
        {
            bool contentActive = FloatPane.ActiveContent == this;   //Pane.IsActivated;
            bool paneActive = FloatPane.IsActivated;

            if (!IsFloat)
                return;

            if (contentActive && paneActive)
            {
                textBox2.Text = "Float activated";
                textBox3.Text = "";
            }
            else
            {
                textBox2.Text = "Float deactivated";
                textBox3.Text = "";
            }
        }

        //=========================================================================
        public void Pane_IsActivatedChanged(object sender, EventArgs e)
        {
            bool contentActive = Pane.ActiveContent == this;    //Pane.IsActivated;
            bool paneActive = Pane.IsActivated;
            if (IsFloat)
                return;

            if (contentActive && paneActive)
            {
                textBox3.Text = "Pane activated";
                textBox2.Text = "";
            }
            else
            {
                textBox3.Text = "Pane deactivated";
                textBox2.Text = "";
            }
        }

        private void ActivationForm_Load(object sender, EventArgs e)
        {
            DockStateChanged += new EventHandler(DockForm_DockStateChanged);
            if (IsFloat)
            {
                FloatPane.IsActivatedChanged += new EventHandler(FloatPane_IsActivatedChanged);
                richTextBox1.Text = "window is now listening to floating messages.";
            }
            else
            {
                Pane.IsActivatedChanged += new EventHandler(Pane_IsActivatedChanged);
                richTextBox1.Text = "window is now listening to pane messages.";
            }
        }
    }
}
