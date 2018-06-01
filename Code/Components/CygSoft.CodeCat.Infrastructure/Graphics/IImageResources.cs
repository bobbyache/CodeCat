using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CygSoft.CodeCat.Infrastructure.Graphics
{
    public interface IImageResources
    {
        Image GetImage(string key);
        Icon GetIcon(string key);
        IImageOutput GetKeywordIndexItemImage(string syntaxImageKey);
        ImageList ImageList { get; }
        Icon QikGroupIcon { get; }
        Icon CodeGroupIcon { get; }
        IImageOutput Get(string key, bool isFileExtensionKey = false);
        void AddCategoryInfo();
        void AddDocuments();
        void AddSyntaxes(ISyntaxFile[] syntaxFiles);
        void AddFileExtensions(IEnumerable<string> fileExtensions);
    }
}
