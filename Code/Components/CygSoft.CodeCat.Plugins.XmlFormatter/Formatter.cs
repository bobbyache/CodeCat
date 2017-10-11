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
using System.IO;
using System.Reflection;

namespace CygSoft.CodeCat.Plugins.XmlFormatter
{
    public partial class Formatter: UserControl, IGeneratorPlugin
    {
        public Formatter()
        {
            InitializeComponent();
            ApplySyntaxColoring();

            fromTextbox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
                {
                    try
                    {
                        fromTextbox.Document.Text = System.Xml.Linq.XElement.Parse(Clipboard.GetText()).ToString();
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

        private void ApplySyntaxColoring()
        {
            //if (File.Exists(GetSyntaxFilePath("SQLServer2K_SQL.syn")))
            //    fromDocument.SyntaxFile = GetSyntaxFilePath("SQLServer2K_SQL.syn");
            //if (File.Exists(GetSyntaxFilePath("C#.syn")))
            //    toDocument.SyntaxFile = GetSyntaxFilePath("C#.syn");
        }

        private string GetSyntaxFilePath(string syntaxFile)
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), syntaxFile);
        }

        public string Id
        {
            get
            {
                return "XmlFormatter";
            }
        }

        public string Title
        {
            get
            {
                return "Format Xml";
            }
        }

        public event EventHandler Generated;

        private void BtnClear_Click(object sender, EventArgs e)
        {
            fromTextbox.Document.Text = string.Empty;
        }
    }
}
