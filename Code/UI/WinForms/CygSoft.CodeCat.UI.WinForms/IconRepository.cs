using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.Syntax.Infrastructure;
using CygSoft.CodeCat.UI.Resources;
using System.Collections.Generic;

namespace CygSoft.CodeCat.UI.WinForms
{
    public class IconRepository : IIconRepository
    {
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

        public IconRepository()
        {
            imageResources = new ImageResources();
        }
    }
}
