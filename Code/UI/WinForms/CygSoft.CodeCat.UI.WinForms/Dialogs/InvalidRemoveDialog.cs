using CygSoft.CodeCat.Infrastructure;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class InvalidRemoveDialog : Form
    {
        public InvalidRemoveDialog(IKeywordIndexItem[] invalidItems)
        {
            InitializeComponent();
            lstInvalidItems.Items.AddRange(invalidItems.Select(doc => doc.Title).ToArray());
        }
    }
}
