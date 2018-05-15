using System;

namespace CygSoft.CodeCat.Plugins.TopicSection.Infrastructure
{
    public interface ITopicSection  : IPositionedItem, IFile
    {
        string Id { get; }

        string Title { get; set; }
        string DocumentType { get; set; }
        string Description { get; set; }
    }
}
