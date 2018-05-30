using CygSoft.CodeCat.Syntax.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        //Icon FileGroupIcon { get; }
        IImageOutput Get(string key, bool isFileExtensionKey = false);
        void AddCategoryInfo();
        void AddDocuments();
        void AddSyntaxes(ISyntaxFile[] syntaxFiles);
        void AddFileExtensions(IEnumerable<string> fileExtensions);
    }
}
