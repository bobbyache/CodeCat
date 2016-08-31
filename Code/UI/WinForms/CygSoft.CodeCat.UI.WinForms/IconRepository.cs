﻿using CygSoft.CodeCat.Domain.Code;
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
        public const string QikKey = "QIK";
        public const string CodeGroupKey = "CodeGroup";

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

            foreach (SyntaxFile syntaxFile in syntaxFiles)
            {
                string syntax = syntaxFile.Syntax.ToUpper();

                if (syntax == QikKey)
                {
                    iconDictonary.Add(syntax, QikIcon);

                    if (!imageList.Images.ContainsKey(syntax))
                        imageList.Images.Add(syntax, QikIcon);
                }
                //else if (syntax == CodeGroupKey)
                //{
                //    iconDictonary.Add(syntax, CodeGroupIcon);

                //    if (!imageList.Images.ContainsKey(syntax))
                //        imageList.Images.Add(syntax, CodeGroupIcon);
                //}
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
                icon = iconDictonary[uSyntax];

            return icon.ToBitmap();
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
