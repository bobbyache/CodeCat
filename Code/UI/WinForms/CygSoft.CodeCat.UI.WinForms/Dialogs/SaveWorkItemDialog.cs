using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class SaveWorkItemDialog : Form
    {
        public SaveWorkItemDialog(IContentDocument[] unsavedWorkItemForms)
        {
            InitializeComponent();
            lstUnsavedWorktItems.Items.AddRange(unsavedWorkItemForms.Select(doc => doc.Text).ToArray());
        }
    }
}
