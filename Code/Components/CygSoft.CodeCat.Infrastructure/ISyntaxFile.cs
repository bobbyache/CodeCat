namespace CygSoft.CodeCat.Infrastructure
{
    public interface ISyntaxFile
    {
        string Extension { get; }
        string FilePath { get; }
        string Syntax { get; }
    }
}