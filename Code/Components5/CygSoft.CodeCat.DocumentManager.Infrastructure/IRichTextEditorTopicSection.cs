using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IRichTextEditorTopicSection : ITopicSection
    {
        event EventHandler RequestSaveRtf;
    }
}
