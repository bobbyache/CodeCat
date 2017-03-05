using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IRichTextDocument : IVersionableFile, IPositionedItem
    {
        event EventHandler RequestSaveRtf;

        string Title { get; set; }
        string DocumentType { get; set; }
        string Description { get; set; }
    }
}
