using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CygSoft.CodeCat.Plugins.Generators;

namespace CygSoft.CodeCat.Plugins.ManualXess
{
    public partial class ManualXessGenerator: UserControl, IGeneratorPlugin
    {
        public ManualXessGenerator()
        {
            InitializeComponent();
            lblCaption.Text = this.Title;
        }

        public string Id
        {
            get
            {
                return "ManualXessGenerator";
            }
        }

        public string Title
        {
            get
            {
                return "Quick Adhoc Batch Generator";
            }
        }

        public event EventHandler Generated;
    }
}
