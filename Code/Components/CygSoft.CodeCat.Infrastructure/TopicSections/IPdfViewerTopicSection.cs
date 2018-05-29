using System;

namespace CygSoft.CodeCat.Infrastructure.TopicSections
{
    public interface IPdfViewerTopicSection : ITopicSection
    {
        IDisposable Document { get; set; }
    }
}
