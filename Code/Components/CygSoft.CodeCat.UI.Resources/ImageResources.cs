using CygSoft.CodeCat.Infrastructure.Graphics;
using System.Drawing;
using System.Reflection;
using System.Resources;

namespace CygSoft.CodeCat.UI.Resources
{
    public class ImageResources : IImageResources
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

        private ResourceManager resourceManager;

        public ImageResources()
        {
            resourceManager = new ResourceManager($"{this.GetType().Namespace}.UiResource", Assembly.GetAssembly(this.GetType()));
        }

        public Image GetImage(string key)
        {
            return (Image)resourceManager.GetObject(key);
        }

        public Icon GetIcon(string key)
        {
            Image image = GetImage(key);
            return ImageLibrary.IconFromImage(image);
        }
    }
}
