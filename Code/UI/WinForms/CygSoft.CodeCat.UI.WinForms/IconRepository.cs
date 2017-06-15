using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.UI.WinForms.Images;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
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
        public static class Documents
        {
            public const string QikGroup = "Document.QikGroup";
            public const string CodeGroup = "Document.CodeGroup";
            public const string PDF = "Document.PDF";
            public const string SingleImage = "Document.IMG";
            public const string HyperlinkSet = "Document.UrlGroup";
            public const string ImageSet = "Document.PNG";
            public const string RTF = "Document.RTF";
            public const string FileSet = "Document.HTML";
            public const string Unknown = "Document.Unknown";
            public const string CodeFile = "Document.CodeFile";
        }

        private static ImageLibrary imageLibrary = new ImageLibrary();

        public static ImageList ImageList { get { return imageLibrary.ImageList; } }

        public static Icon QikGroupIcon { get { return Get(Documents.QikGroup).Icon; } }
        public static Icon CodeGroupIcon { get { return Get(Documents.CodeGroup).Icon; } }
        public static Icon FileGroupIcon { get { return Get(Documents.FileSet).Icon; } }

        public static ImageOutput Get(string key, bool isFileExtensionKey = false)
        {
            //if (IsDocument(key))
            //    Console.WriteLine("here we are");
            if (isFileExtensionKey)
                return imageLibrary[key.ToUpper(), true];

            return imageLibrary[key.ToUpper()];
        }

        //private static bool IsDocument(string key)
        //{
        //    if (key.StartsWith("Document."))
        //        return true;
        //    return false;
        //}

        public static void AddDocuments()
        {
            imageLibrary.Add(Documents.CodeFile, imageLibrary.IconByExtension("cpp"));
            imageLibrary.Add(Documents.CodeGroup, Gui.Resources.GetImage(Constants.ImageKeys.CodeGroup));
            imageLibrary.Add(Documents.QikGroup, Gui.Resources.GetImage(Constants.ImageKeys.QikGroup));
            imageLibrary.Add(Documents.FileSet, Gui.Resources.GetImage(Constants.ImageKeys.Attachment));
            imageLibrary.Add(Documents.HyperlinkSet, Gui.Resources.GetImage(Constants.ImageKeys.Web));
            imageLibrary.Add(Documents.ImageSet, Gui.Resources.GetImage(Constants.ImageKeys.ImageSet));
            imageLibrary.Add(Documents.Unknown, imageLibrary.IconByExtension("dat"));
            imageLibrary.Add(Documents.RTF, imageLibrary.IconByExtension("rtf"));
            imageLibrary.Add(Documents.PDF, imageLibrary.IconByExtension("pdf"));
            imageLibrary.Add(Documents.SingleImage, imageLibrary.IconByExtension("png"));
        }

        public static void AddSyntaxes(SyntaxFile[] syntaxFiles)
        {
            foreach (SyntaxFile syntaxFile in syntaxFiles)
            {
                string syntax = syntaxFile.Syntax.ToUpper();

                if (syntax == Documents.QikGroup)
                    imageLibrary.Add(syntax, Gui.Resources.GetImage(Constants.ImageKeys.QikGroup));
                else
                {
                    Icon icon = imageLibrary[syntaxFile.Extension, true].Icon;
                    imageLibrary.Add(syntax, icon);
                }
            }
        }

        public static void AddFileExtensions(IEnumerable<string> fileExtensions)
        {
            imageLibrary.AddExtensions(fileExtensions.ToArray());
        }
    }
}
