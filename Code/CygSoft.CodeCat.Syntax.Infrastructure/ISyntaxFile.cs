namespace CygSoft.CodeCat.Syntax.Infrastructure
{
    public interface ISyntaxFile
    {
        string Extension { get; }
        string FilePath { get; }
        string Syntax { get; }
    }
}