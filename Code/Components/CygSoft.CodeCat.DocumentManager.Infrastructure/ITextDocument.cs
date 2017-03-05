namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface ITextDocument : IDocument
    {
        string Text { get; set; }
    }
}
