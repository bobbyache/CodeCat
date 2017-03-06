using System;
using System.Collections.Generic;
using System.Text;
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
                checkedListBox.Items.Clear();

                foreach (string item in value)
                {
                    checkedListBox.Items.Add(item);
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
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (checkedListBox.CheckedItems.Count > 0)
            {
                StringBuilder builder = new StringBuilder();

                foreach (var item in checkedListBox.CheckedItems)
                    builder.Append(item.ToString() + ",");
                
                if (builder.Length > 0)
                    builder.Remove(builder.Length - 1, 1);

                Clipboard.Clear();
                Clipboard.SetText(builder.ToString());
            }
        }
    }
}
