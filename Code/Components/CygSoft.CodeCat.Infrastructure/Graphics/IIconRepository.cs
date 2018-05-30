using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using CygSoft.CodeCat.Syntax.Infrastructure;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CygSoft.CodeCat.Infrastructure.Graphics
{
    public interface IIconRepository
    {
        Icon CodeGroupIcon { get; }
        Icon FileGroupIcon { get; }
        IImageResources ImageResources { get; }
        Icon QikGroupIcon { get; }

        void AddCategoryInfo();
        void AddDocuments();
        void AddFileExtensions(IEnumerable<string> fileExtensions);
        void AddSyntaxes(ISyntaxFile[] syntaxFiles);
        IImageOutput Get(string key, bool isFileExtensionKey = false);
    }
}
