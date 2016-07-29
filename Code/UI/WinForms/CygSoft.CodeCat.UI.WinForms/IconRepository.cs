using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public static class IconRepository
    {
        private static Dictionary<string, Icon> iconDictonary = new Dictionary<string, Icon>();

        public static void Load(SyntaxFile[] syntaxFiles)
        {
            foreach (SyntaxFile syntaxFile in syntaxFiles)
            {
                Icon icon = Etier.IconHelper.IconReader.GetFileIcon("." + syntaxFile.Extension, 
                    Etier.IconHelper.IconReader.IconSize.Small, 
                    false);
                iconDictonary.Add(syntaxFile.Syntax.ToUpper(), icon);
                
            }
        }

        public static Icon GetIcon(string syntax)
        {
            return iconDictonary[syntax.ToUpper()];
        }
    }
}
