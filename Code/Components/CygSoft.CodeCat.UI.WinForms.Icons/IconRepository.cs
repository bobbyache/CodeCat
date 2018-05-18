using CygSoft.CodeCat.Syntax.Infrastructure;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Icons
{
    public static class IconRepository
    {
        //public static class TopicSections
        //{
        //    public const string QikGroup = "Document.QikGroup";
        //    public const string CodeGroup = "Document.CodeGroup";
        //    public const string PDF = "Document.PDF";
        //    public const string SingleImage = "Document.IMG";
        //    public const string WebReferences = "Document.UrlGroup";
        //    public const string ImageSet = "Document.PNG";
        //    public const string RTF = "Document.RTF";
        //    public const string FileAttachments = "Document.HTML";
        //    public const string Unknown = "Document.Unknown";
        //    public const string CodeFile = "Document.CodeFile";
        //    public const string EventDiary = "Document.EventDiary";
        //}

        private static ImageLibrary imageLibrary = new ImageLibrary();

        public static ImageList ImageList { get { return imageLibrary.ImageList; } }

        public static Icon QikGroupIcon { get { return Get(TopicSections.QikGroup).Icon; } }
        public static Icon CodeGroupIcon { get { return Get(TopicSections.CodeGroup).Icon; } }
        public static Icon FileGroupIcon { get { return Get(TopicSections.FileAttachments).Icon; } }


        //public static ImageOutput GetKeywordIndexItemImage(IKeywordIndexItem item)
        public static ImageOutput GetKeywordIndexItemImage(string imageKey)
        {
            //    string imageKey = null;

            //    if (item is ICodeKeywordIndexItem)
            //        imageKey = (item as ICodeKeywordIndexItem).Syntax;

            //    else if (item is IQikTemplateKeywordIndexItem)
            //        imageKey = IconRepository.TopicSections.QikGroup;

            //    else if (item is ITopicKeywordIndexItem)
            //        imageKey = IconRepository.TopicSections.CodeGroup;
            return Get(imageKey, false);
        }

        public static ImageOutput Get(string key, bool isFileExtensionKey = false)
        {
            if (isFileExtensionKey)
                return imageLibrary[key.ToUpper(), true];

            return imageLibrary[key.ToUpper()];
        }

        public static void AddCategoryInfo()
        {
            //imageLibrary.Add(Constants.ImageKeys.OpenCategory, Gui.Resources.GetImage(Constants.ImageKeys.OpenCategory));
            //imageLibrary.Add(Constants.ImageKeys.ClosedCategory, Gui.Resources.GetImage(Constants.ImageKeys.ClosedCategory));
        }

        public static void AddDocuments()
        {
            //imageLibrary.Add(TopicSections.CodeFile, imageLibrary.IconByExtension("cpp"));
            //imageLibrary.Add(TopicSections.CodeGroup, Gui.Resources.GetImage(Constants.ImageKeys.CodeGroup));
            //imageLibrary.Add(TopicSections.QikGroup, Gui.Resources.GetImage(Constants.ImageKeys.QikGroup));
            //imageLibrary.Add(TopicSections.FileAttachments, Gui.Resources.GetImage(Constants.ImageKeys.Attachment));
            //imageLibrary.Add(TopicSections.WebReferences, Gui.Resources.GetImage(Constants.ImageKeys.Web));
            //imageLibrary.Add(TopicSections.ImageSet, Gui.Resources.GetImage(Constants.ImageKeys.ImageSet));
            //imageLibrary.Add(TopicSections.EventDiary, Gui.Resources.GetImage(Constants.ImageKeys.EventDiary));
            //imageLibrary.Add(TopicSections.Unknown, imageLibrary.IconByExtension("dat"));
            //imageLibrary.Add(TopicSections.RTF, imageLibrary.IconByExtension("rtf"));
            //imageLibrary.Add(TopicSections.PDF, imageLibrary.IconByExtension("pdf"));
            //imageLibrary.Add(TopicSections.SingleImage, imageLibrary.IconByExtension("png"));
        }

        public static void AddSyntaxes(ISyntaxFile[] syntaxFiles)
        {
            foreach (ISyntaxFile syntaxFile in syntaxFiles)
            {
                string syntax = syntaxFile.Syntax.ToUpper();

                if (syntax == TopicSections.QikGroup)
                    imageLibrary.Add(syntax, CreateBlankBitmap() /*Gui.Resources.GetImage(Constants.ImageKeys.QikGroup) */);
                else
                {
                    Icon icon = imageLibrary[syntaxFile.Extension, true].Icon;
                    imageLibrary.Add(syntax, icon);
                }
            }
        }

        private static Bitmap CreateBlankBitmap()
        {
            int width = 16, height = 16;
            return new Bitmap(width, height);
        }

        public static void AddFileExtensions(IEnumerable<string> fileExtensions)
        {
            imageLibrary.AddExtensions(fileExtensions.ToArray());
        }
    }
}
