using CygSoft.CodeCat.Infrastructure.Graphics;
using System.Drawing;
using System.Reflection;
using System.Resources;

namespace CygSoft.CodeCat.UI.Resources
{
    public class ImageResources : IImageResources
    {
        private ResourceManager resourceManager;

        public ImageResources()
        {
            resourceManager = new ResourceManager($"{this.GetType().Namespace}.UiResource", Assembly.GetAssembly(this.GetType()));
        }

        public Image GetImage(string key)
        {
            return (Image)resourceManager.GetObject(key);
        }
    }
}
