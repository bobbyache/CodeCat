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
        public const string QikKey = "qik";

        private static ImageList imageList = new ImageList();
        private static Dictionary<string, Icon> iconDictonary = new Dictionary<string, Icon>();

        public static ImageList ImageList { get { return imageList; } }

        private static Icon qikIcon;
        public static Icon QikIcon
        {
            get
            {
                if (qikIcon == null)
                {
                    Bitmap qikBitMap = new Bitmap(Resources.GetImage(Constants.ImageKeys.QikFile));
                    IntPtr iconPtr = qikBitMap.GetHicon();
                    qikIcon = Icon.FromHandle(iconPtr);
                }
                return qikIcon;
            }
        }

        public static void Load(SyntaxFile[] syntaxFiles)
        {
            foreach (SyntaxFile syntaxFile in syntaxFiles)
            {
                Icon icon = Etier.IconHelper.IconReader.GetFileIcon("." + syntaxFile.Extension, 
                    Etier.IconHelper.IconReader.IconSize.Small, 
                    false);
                iconDictonary.Add(syntaxFile.Syntax.ToUpper(), icon);

                if (!imageList.Images.ContainsKey(syntaxFile.Syntax))
                {
                    imageList.Images.Add(syntaxFile.Syntax, icon);
                }
            }
            if (!imageList.Images.ContainsKey(QikKey))
            {
                imageList.Images.Add(QikKey, Resources.GetImage(Constants.ImageKeys.QikFile));
            }
        }

        public static Icon GetIcon(string syntax)
        {
            return iconDictonary[syntax.ToUpper()];
        }

        public static int ImageKeyFor(string syntax)
        {
            return imageList.Images.IndexOfKey(syntax);
        }
    }
}
