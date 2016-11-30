using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public partial class SelectKeywordsDialog : Form
    {
        public string[] Keywords
        {
            get 
            {
                List<string> items = new List<string>();
                foreach (var item in checkedListBox.CheckedItems)
                {
                    items.Add(item.ToString());
                }
                return items.ToArray();
            }
            set
            {
                this.checkedListBox.Items.Clear();

                foreach (string item in value)
                {
                    this.checkedListBox.Items.Add(item);
                }
            }
        }

        public SelectKeywordsDialog()
        {
            InitializeComponent();
            checkedListBox.CheckOnClick = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (this.checkedListBox.CheckedItems.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                foreach (var item in this.checkedListBox.CheckedItems)
                {
                    builder.Append(item.ToString() + ",");
                }
                if (builder.Length > 0)
                    builder.Remove(builder.Length - 1, 1);

                Clipboard.Clear();
                Clipboard.SetText(builder.ToString());
            }
        }
    }
}
