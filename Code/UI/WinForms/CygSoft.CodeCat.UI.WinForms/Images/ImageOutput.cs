using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.UI.WinForms.Images
{
    public class ImageOutput
    {
        private readonly Icon icon;
        private readonly Image image;
        private readonly int index;

        public Icon Icon { get { return this.icon; } }
        public Image Image { get { return this.image; } }
        public int Index { get { return this.index; } }

        public ImageOutput(Icon icon, Image image, int index)
        {
            this.icon = icon;
            this.image = image;
            this.index = index;
        }
    }
}
