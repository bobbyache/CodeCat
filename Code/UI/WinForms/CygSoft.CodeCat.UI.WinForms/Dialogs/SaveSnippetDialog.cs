using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class SaveSnippetDialog : Form
    {
        public SaveSnippetDialog(IContentDocument[] unsavedDocuments)
        {
            InitializeComponent();
            lstUnsavedDocs.Items.AddRange(unsavedDocuments.Select(doc => doc.Text).ToArray());
        }
    }
}
