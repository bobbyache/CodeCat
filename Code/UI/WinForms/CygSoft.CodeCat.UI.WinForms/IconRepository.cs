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
using static CygSoft.CodeCat.UI.Resources.ImageResources;

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

        public IImageOutput Get(string key, bool isFileExtensionKey = false)
        {
            return imageResources.Get(key, isFileExtensionKey);
        }

        public void AddCategoryInfo()
        {
            imageResources.AddCategoryInfo();
        }

        public void AddDocuments()
        {
            imageResources.AddDocuments();
        }

        public void AddSyntaxes(ISyntaxFile[] syntaxFiles)
        {
            imageResources.AddSyntaxes(syntaxFiles);
        }

        public void AddFileExtensions(IEnumerable<string> fileExtensions)
        {
            imageResources.AddFileExtensions(fileExtensions);
        }
    }
}
