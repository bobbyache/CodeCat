using System;

namespace CygSoft.CodeCat.Infrastructure.TopicSections
{
    public interface IRichTextEditorTopicSection : ITopicSection
    {
        event EventHandler RequestSaveRtf;
    }
}
