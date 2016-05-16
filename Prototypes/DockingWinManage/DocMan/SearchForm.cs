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

namespace Weif_1_Test
{
    public partial class SearchForm : DockContent
    {
        public SearchForm()
        {
            InitializeComponent();

            this.HideOnClose = true;
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight;
        }
    }
}
