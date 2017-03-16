using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IRichTextEditorTopicSection : ITopicSection, IPositionedItem
    {
        event EventHandler RequestSaveRtf;
    }
}
