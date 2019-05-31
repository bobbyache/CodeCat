using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IPdfViewerTopicSection : ITopicSection
    {
        IDisposable Document { get; set; }
    }
}
