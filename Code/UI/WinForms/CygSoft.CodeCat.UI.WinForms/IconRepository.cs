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

    // TODO:  IconRepository. This class requires streamlining. There are a number methods and properties that can be streamlined for the rest of the application. Consider moving into infrastructure or domain.
    public static class IconRepository
    {
        public const string QikKey = "QIK";
        public const string CodeGroupKey = "CodeGroup";
        public const string PDF = "PDF";

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
                    Bitmap qikBitMap = new Bitmap(Resources.GetImage(Constants.ImageKeys.QikGroup));
                    IntPtr iconPtr = qikBitMap.GetHicon();
                    qikIcon = Icon.FromHandle(iconPtr);
                }
                return qikIcon;
            }
        }

        public static Icon PdfIcon
        {
            get
            {
                Icon icon = Etier.IconHelper.IconReader.GetFileIcon(".pdf",
                    Etier.IconHelper.IconReader.IconSize.Small,
                    false);
                return icon;
            }
        }

        public static Image PdfImage
        {
            get
            {
                Icon icon = Etier.IconHelper.IconReader.GetFileIcon(".pdf",
                    Etier.IconHelper.IconReader.IconSize.Small,
                    false);
                return icon.ToBitmap();
            }
        }

        public static Icon ImageTypeIconByExtension(string extension)
        {
            Icon icon = Etier.IconHelper.IconReader.GetFileIcon(extension,
                Etier.IconHelper.IconReader.IconSize.Small,
                false);
            return icon;
        }

        public static Image ImageTypeImageByExtension(string extension)
        {
            Icon icon = Etier.IconHelper.IconReader.GetFileIcon(extension,
                Etier.IconHelper.IconReader.IconSize.Small,
                false);
            return icon.ToBitmap();
        }

        private static Icon codeGroupIcon;
        public static Icon CodeGroupIcon
        {
            get
            {
                if (codeGroupIcon == null)
                {
                    Bitmap codeGroupBitmap = new Bitmap(Resources.GetImage(Constants.ImageKeys.CodeGroup));
                    IntPtr iconPtr = codeGroupBitmap.GetHicon();
                    codeGroupIcon = Icon.FromHandle(iconPtr);
                }
                return codeGroupIcon;
            }
        }

        public static void Load(SyntaxFile[] syntaxFiles)
        {
            iconDictonary.Add(CodeGroupKey, CodeGroupIcon);

            if (!imageList.Images.ContainsKey(CodeGroupKey))
                imageList.Images.Add(CodeGroupKey, CodeGroupIcon);

            if (!imageList.Images.ContainsKey(PDF))
                imageList.Images.Add(PDF, PdfIcon);

            foreach (SyntaxFile syntaxFile in syntaxFiles)
            {
                string syntax = syntaxFile.Syntax.ToUpper();

                if (syntax == QikKey)
                {
                    iconDictonary.Add(syntax, QikIcon);

                    if (!imageList.Images.ContainsKey(syntax))
                        imageList.Images.Add(syntax, QikIcon);
                }
                else
                {
                    Icon icon = Etier.IconHelper.IconReader.GetFileIcon("." + syntaxFile.Extension,
                        Etier.IconHelper.IconReader.IconSize.Small,
                        false);
                    
                    iconDictonary.Add(syntax, icon);

                    if (!imageList.Images.ContainsKey(syntax))
                        imageList.Images.Add(syntax, icon);
                }
            }

            iconDictonary.Add(PDF, PdfIcon);
        }

        public static Image GetImage(string syntax)
        {
            Icon icon = null;
            string uSyntax = syntax.ToUpper();

            if (uSyntax == QikKey)
                icon = QikIcon;
            else if (uSyntax == CodeGroupKey)
                icon = CodeGroupIcon;
            else
            {
                if (iconDictonary.ContainsKey(uSyntax))
                    icon = iconDictonary[uSyntax];
            }

            if (icon != null)
                return icon.ToBitmap();
            return null;
        }

        public static Icon GetIcon(string syntax)
        {
            string uSyntax = syntax.ToUpper();

            if (uSyntax == QikKey)
                return QikIcon;
            else if (uSyntax == CodeGroupKey)
                return CodeGroupIcon;
            else
                return iconDictonary[uSyntax];
        }

        public static int ImageKeyFor(string syntax)
        {
            string uSyntax = syntax.ToUpper();
            return imageList.Images.IndexOfKey(uSyntax);
        }
    }
}
