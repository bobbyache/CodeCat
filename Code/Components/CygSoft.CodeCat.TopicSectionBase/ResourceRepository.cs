using System;
using System.Drawing;
using System.Reflection;
using System.Resources;

namespace CygSoft.CodeCat.UI.WinForms.TopicSectionBase
{
    internal class ResourceRepository
    {
        private ResourceManager resourceManager;

        public ResourceRepository(Type controlType)
        {
            resourceManager = new ResourceManager($"{controlType.Namespace}.Resources", Assembly.GetAssembly(controlType));
        }

        public Image GetImage(string key)
        {
            
            return (Image)resourceManager.GetObject(key);
        }

        public Icon GetIcon(string key)
        {
            return IconFromImage(GetImage(key));
        }

        private Icon IconFromImage(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            IntPtr iconPtr = bitmap.GetHicon();
            Icon icon = Icon.FromHandle(iconPtr);

            return icon;
        }
    }
}
