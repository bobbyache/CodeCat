namespace CygSoft.CodeCat.Plugins.UI.Infrastructure.TopicSection
{
    public interface IWorkItem : IKeywordTarget, IFile
    {
        string Title { get; set; }

        // do not set file content here, this is the job of specialized file classes.

        string Id { get; }
    }
}
