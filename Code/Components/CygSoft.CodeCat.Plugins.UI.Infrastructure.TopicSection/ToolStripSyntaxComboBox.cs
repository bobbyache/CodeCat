using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.Plugins.UI.Infrastructure.TopicSection
{
    // https://www.codeproject.com/Articles/10670/Image-ComboBox-Control
    // https://social.msdn.microsoft.com/Forums/vstudio/en-US/ff2fa457-ebdb-46eb-840f-fb29e036b474/image-for-toolstripcombobox-toolstriptextbox-or-other-toolstripcontrolhost?forum=csharpgeneral

    public class ToolStripSyntaxComboBox : ToolStripComboBox
    {
        public string Syntax
        {
            get
            {
                if (this.SelectedItem == null)
                    return null;

                string currentSyntax = this.SelectedItem.ToString();
                string syntax = string.IsNullOrEmpty(currentSyntax) ? null /* ConfigSettings.DefaultSyntax.ToUpper() */ : currentSyntax.ToUpper();
                return syntax;
            }
            set
            {
                string syntax = string.IsNullOrEmpty(value) ? null /* ConfigSettings.DefaultSyntax.ToUpper() */ : value.ToUpper();
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
