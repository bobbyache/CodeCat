using System;
using System.Text.RegularExpressions;
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
    public partial class SqlToCSharpStringGenerator : UserControl, IGeneratorPlugin
    {
        public SqlToCSharpStringGenerator()
        {
            InitializeComponent();
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
        private string Indent(int numTabs)
        {
            string spaces = string.Empty;
            for (int x = 0; x < numTabs; x++)
                spaces += "    ";
            return spaces;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            toTextbox.Document.Text = string.Empty;
            fromTextbox.Document.Text = string.Empty;
        }

        private void BtnPartialFormat_Click(object sender, EventArgs e)
        {
            string text = fromTextbox.Document.Text;

            text = ReplaceRegEx(text, "[\t\n\r\v\f]", "");
            text = ReplaceRegEx(text, " +(?= )", "");
            text = Replace(text, "FROM", "\nFROM");
            text = Replace(text, "INNER JOIN", "\nINNER JOIN");
            text = Replace(text, "LEFT JOIN", "\nLEFT JOIN");
            text = Replace(text, "RIGHT JOIN", "\nRIGHT JOIN");
            text = Replace(text, "SELECT", "\nSELECT");
            text = Replace(text, "WHERE", "\nWHERE\n");
            text = Replace(text, "AND", "AND\n");
            text = Replace(text, "NOT IN", "NOT IN\n");
            text = Replace(text, " IN ", " IN\n ");
            text = Replace(text, "ORDER BY", "\nORDER BY\n ");

            text = Replace(text, "from", "\nfrom");
            text = Replace(text, "inner join", "\ninner join");
            text = Replace(text, "left join", "\nleft join");
            text = Replace(text, "right join", "\nright join");
            text = Replace(text, "select", "\nselect");
            text = Replace(text, "where", "\nwhere\n");
            text = Replace(text, "and", "and\n");
            text = Replace(text, "not in", "not in\n");
            text = Replace(text, " in ", " in\n ");
            text = Replace(text, "order by", "\norder by\n ");

            fromTextbox.Document.Text = text;
        }



        private string ReplaceRegEx(string input, string pattern, string replaceText)
        {
            Regex regEx = new Regex(pattern);
            string output = regEx.Replace(input, replaceText);
            return output;
        }

        private string Replace(string input, string text, string replaceText)
        {
            return input.Replace(text, replaceText);
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            string targetText = GenerateCodeFormSql(fromTextbox.Document.Text);
            toTextbox.Document.Text = targetText;

            Clipboard.Clear();
            Clipboard.SetText(toTextbox.Document.Text);

            if (mnuAutoCopyResult.Checked)
                Clipboard.SetText(toTextbox.Document.Text);

            Generated?.Invoke(this, new EventArgs());
        }

        private string GenerateCodeFormSql(string sourceText)
        {
            string[] lines = sourceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            int lineCount = lines.Length;

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(Indent(2) + "public static string GetSql()");
            builder.AppendLine(Indent(2) + "{");

            builder.AppendLine(Indent(3) + "return");

            for (int x = 0; x < lines.Length; x++)
            {
                string codeLine = "\"" + lines[x].TrimEnd() + " \"";
                if (x != lineCount - 1)
                    codeLine += " +";
                else
                    codeLine += ";";
                builder.AppendLine(Indent(4) + codeLine);
            }
            builder.AppendLine(Indent(2) + "}");
            return builder.ToString();
        }

        private void mnuToggleOrientation_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Orientation == Orientation.Horizontal)
                splitContainer1.Orientation = Orientation.Vertical;
            else
                splitContainer1.Orientation = Orientation.Horizontal;

        }
    }
}
