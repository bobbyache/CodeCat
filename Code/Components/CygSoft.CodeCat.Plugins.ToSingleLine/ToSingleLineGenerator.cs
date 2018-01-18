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

namespace CygSoft.CodeCat.Plugins.ToSingleLine
{
    public partial class ToSingleLineGenerator : UserControl, IGeneratorPlugin
    {
        public ToSingleLineGenerator()
        {
            InitializeComponent();

            fromTextbox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
                {
                    try
                    {
                        fromTextbox.Document.Text = ToSingleLine(Clipboard.GetText());
                        if (mnuAutoCopyResult.Checked)
                        {
                            Clipboard.Clear();
                            Clipboard.SetText(fromTextbox.Document.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, $"XML could not be copied.\n{ex.Message}", "Xml Formatter");
                    }
                    e.Handled = true;
                }
            };
        }

        private string ToSingleLine(string text, bool spaceDelimited = false)
        {
            if (spaceDelimited)
            {
                IEnumerable<string> lines = text.Split(Environment.NewLine.ToCharArray()).Select(t => t.Trim());
                return string.Join("", lines.ToArray());
            }
            else
            {
                IEnumerable<string> lines = text.Split(Environment.NewLine.ToCharArray()).Select(t => t.Trim());
                string output = string.Join("", lines.ToArray());
                return output.Replace(",", ", ");
            }
        }

        public string Id
        {
            get
            {
                return "ToSingleLine";
            }
        }

        public string Title
        {
            get
            {
                return "Compact code to single line";
            }
        }

        public event EventHandler Generated;

        private void BtnClear_Click(object sender, EventArgs e)
        {
            fromTextbox.Document.Text = string.Empty;
        }
    }
}
