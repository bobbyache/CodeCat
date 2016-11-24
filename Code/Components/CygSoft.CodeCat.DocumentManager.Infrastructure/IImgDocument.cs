using System;
using System.Collections.Generic;
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
    }
}
