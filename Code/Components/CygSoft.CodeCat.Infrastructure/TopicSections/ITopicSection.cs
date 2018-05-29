using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Files.Infrastructure;
using System;

namespace CygSoft.CodeCat.Infrastructure.TopicSections
{
    public interface ITopicSection  : IPositionedItem, IFile
    {
        string Id { get; }

        string Title { get; set; }
        string DocumentType { get; set; }
        string Description { get; set; }
    }
}
