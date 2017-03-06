using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class SnapshotDescDialog : Form
    {
        public string Description
        {
            get { return textBox.Text.Trim(); }
            set { textBox.Text = value; }
        }

        public SnapshotDescDialog()
        {
            InitializeComponent();
        }

    }
}
