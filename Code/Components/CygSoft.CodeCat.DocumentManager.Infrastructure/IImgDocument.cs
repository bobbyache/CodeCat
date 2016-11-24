using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IImgDocument : IDocument, IPositionedItem
    {
        bool IsModified { get; }
        string ModifyFilePath { get; }
        string DisplayFilePath { get; }

        Bitmap GetDisplayImage();
        void SetImage(string fromFile);
        void SetImage(Image image);
    }
}
