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
