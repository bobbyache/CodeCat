using CygSoft.CodeCat.Infrastructure.Graphics;
using System.Drawing;

namespace CygSoft.CodeCat.UI.WinForms.Images
{
    public class ImageOutput : IImageOutput
    {
        private readonly Icon icon;
        private readonly Image image;
        private readonly int index;
        private readonly string imageKey;

        public Icon Icon { get { return this.icon; } }
        public Image Image { get { return this.image; } }
        public int Index { get { return this.index; } }
        public string ImageKey { get { return this.imageKey; } }

        public ImageOutput(Icon icon, Image image, int index, string imageKey)
        {
            this.icon = icon;
            this.image = image;
            this.index = index;
            this.imageKey = imageKey;
        }
    }
}
