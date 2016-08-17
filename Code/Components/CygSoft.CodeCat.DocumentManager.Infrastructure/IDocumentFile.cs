using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IDocumentFile  : IPositionedItem
    {
        string Id { get; }
        string FilePath { get; }
        string Text { get; set; }
        string FileName { get; }
        string Folder { get; }
    }
}
