using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IRichTextDocument : ITopicSection, IPositionedItem
    {
        event EventHandler RequestSaveRtf;
    }
}
