using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CygSoft.CodeCat.Category.Infrastructure;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public delegate void ItemExpandingHandler(object sender, ItemExpandingEventArgs e);
    public delegate void ItemRenamedHandler(object sender, ItemRenamedEventArgs e);
    public delegate void ItemMovedHandler(object sender, ItemMovedEventArgs e);
    public delegate bool ItemIsExpandableDelegate(ITitledEntity item);

    public partial class CategoryTreeControl : UserControl
    {
        public CategoryTreeControl()
        {
            InitializeComponent();
        }
    }
}
