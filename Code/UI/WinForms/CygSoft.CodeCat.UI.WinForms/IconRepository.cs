using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using CygSoft.CodeCat.Syntax.Infrastructure;
using CygSoft.CodeCat.UI.Resources;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms
{
    public class IconRepository : IIconRepository
    {
        public static class TopicSections
        {
            public const string QikGroup = "Document.QikGroup";
            public const string CodeGroup = "Document.CodeGroup";
            public const string PDF = "Document.PDF";
            public const string SingleImage = "Document.IMG";
            public const string WebReferences = "Document.UrlGroup";
            public const string ImageSet = "Document.PNG";
            public const string RTF = "Document.RTF";
            public const string FileAttachments = "Document.HTML";
            public const string Unknown = "Document.Unknown";
            public const string CodeFile = "Document.CodeFile";
            public const string EventDiary = "Document.EventDiary";
        }

        private IImageResources imageResources;
        public IImageResources ImageResources
        {
            get
            {
                if (imageResources == null)
                    imageResources = new ImageResources();
                return imageResources;
            }
        }

        private ImageLibrary imageLibrary = new ImageLibrary();

        public ImageList ImageList { get { return imageLibrary.ImageList; } }

        public Icon QikGroupIcon { get { return Get(TopicSections.QikGroup).Icon; } }
        public Icon CodeGroupIcon { get { return Get(TopicSections.CodeGroup).Icon; } }
        public Icon FileGroupIcon { get { return Get(TopicSections.FileAttachments).Icon; } }


        public IImageOutput GetKeywordIndexItemImage(IKeywordIndexItem item)
        {
            string imageKey = null;

            if (item is ICodeKeywordIndexItem)
                imageKey = (item as ICodeKeywordIndexItem).Syntax;

            else if (item is IQikTemplateKeywordIndexItem)
                imageKey = IconRepository.TopicSections.QikGroup;

            else if (item is ITopicKeywordIndexItem)
                imageKey = IconRepository.TopicSections.CodeGroup;

            return Get(imageKey, false);
        }

        public IImageOutput Get(string key, bool isFileExtensionKey = false)
        {
            if (isFileExtensionKey)
                return imageLibrary[key.ToUpper(), true];

            return imageLibrary[key.ToUpper()];
        }

        public void AddCategoryInfo()
        {
            imageLibrary.Add(ImageKeys.OpenCategory, ImageResources.GetImage(ImageKeys.OpenCategory));
            imageLibrary.Add(ImageKeys.ClosedCategory, ImageResources.GetImage(ImageKeys.ClosedCategory));
        }

        public void AddDocuments()
        {
            imageLibrary.Add(TopicSections.CodeFile, imageLibrary.IconByExtension("cpp"));
            imageLibrary.Add(TopicSections.CodeGroup, ImageResources.GetImage(ImageKeys.CodeGroup));
            imageLibrary.Add(TopicSections.QikGroup, ImageResources.GetImage(ImageKeys.QikGroup));
            imageLibrary.Add(TopicSections.FileAttachments, ImageResources.GetImage(ImageKeys.Attachment));
            imageLibrary.Add(TopicSections.WebReferences, ImageResources.GetImage(ImageKeys.Web));
            imageLibrary.Add(TopicSections.ImageSet, ImageResources.GetImage(ImageKeys.ImageSet));
            imageLibrary.Add(TopicSections.EventDiary, ImageResources.GetImage(ImageKeys.EventDiary));
            imageLibrary.Add(TopicSections.Unknown, imageLibrary.IconByExtension("dat"));
            imageLibrary.Add(TopicSections.RTF, imageLibrary.IconByExtension("rtf"));
            imageLibrary.Add(TopicSections.PDF, imageLibrary.IconByExtension("pdf"));
            imageLibrary.Add(TopicSections.SingleImage, imageLibrary.IconByExtension("png"));
        }

        public void AddSyntaxes(ISyntaxFile[] syntaxFiles)
        {
            foreach (ISyntaxFile syntaxFile in syntaxFiles)
            {
                string syntax = syntaxFile.Syntax.ToUpper();

                if (syntax == TopicSections.QikGroup)
                    imageLibrary.Add(syntax, ImageResources.GetImage(ImageKeys.QikGroup));
                else
                {
                    Icon icon = imageLibrary[syntaxFile.Extension, true].Icon;
                    imageLibrary.Add(syntax, icon);
                }
            }
        }

        public void AddFileExtensions(IEnumerable<string> fileExtensions)
        {
            imageLibrary.AddExtensions(fileExtensions.ToArray());
        }
    }
}
