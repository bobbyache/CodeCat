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

namespace CygSoft.CodeCat.Plugins.SqlExtraction
{
    public partial class SqlToCSharpStringGenerator: UserControl, IGeneratorPlugin
    {
        public SqlToCSharpStringGenerator()
        {
            InitializeComponent();
            lblCaption.Text = this.Title;
        }

        public string Id
        {
            get
            {
                return "SqlToCSharpString";
            }
        }

        public string Title
        {
            get
            {
                return "Unformated SQL to C# multi-line string";
            }
        }

        public event EventHandler Generated;
    }
}
