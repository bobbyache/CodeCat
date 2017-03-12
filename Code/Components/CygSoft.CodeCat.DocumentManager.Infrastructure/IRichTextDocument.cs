using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IRichTextDocument : IFile, IPositionedItem
    {
        event EventHandler RequestSaveRtf;

        string Title { get; set; }
        string DocumentType { get; set; }
        string Description { get; set; }
    }
}
