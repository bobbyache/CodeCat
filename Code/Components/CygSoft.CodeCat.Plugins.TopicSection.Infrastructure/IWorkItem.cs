using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;

namespace CygSoft.CodeCat.Plugins.TopicSection.Infrastructure
{
    public interface IWorkItem : IKeywordTarget, IFile
    {
        string Title { get; set; }

        // do not set file content here, this is the job of specialized file classes.

        string Id { get; }
    }
}
