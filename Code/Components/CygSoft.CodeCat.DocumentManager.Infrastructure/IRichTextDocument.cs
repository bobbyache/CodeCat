using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IRichTextDocument : IDocument, IPositionedItem
    {
        event EventHandler RequestSaveRtf;
    }
}
