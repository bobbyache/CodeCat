namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface ICodeDocument : ITextDocument
    {
        string Syntax { get; set; }
    }
}
