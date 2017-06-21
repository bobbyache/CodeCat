using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public class SyntaxComboBox : ComboBox
    {
        public string Syntax
        {
            get
            {
                if (this.SelectedItem == null)
                    return null;

                string currentSyntax = this.SelectedItem.ToString();
                string syntax = string.IsNullOrEmpty(currentSyntax) ? ConfigSettings.DefaultSyntax.ToUpper() : currentSyntax.ToUpper();
                return syntax;
            }
            set
            {
                string syntax = string.IsNullOrEmpty(value) ? ConfigSettings.DefaultSyntax.ToUpper() : value.ToUpper();
                int index = this.FindStringExact(syntax);
                if (index >= 0)
                    this.SelectedIndex = index;
            }
        }

        public void LoadSyntaxes(string[] syntaxes)
        {
            this.Items.Clear();
            this.Items.AddRange(syntaxes);
        }
    }
}
