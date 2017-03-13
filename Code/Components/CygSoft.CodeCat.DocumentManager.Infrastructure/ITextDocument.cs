namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface ITextDocument : ITopicSection
    {
        string Text { get; set; }
    }
}
