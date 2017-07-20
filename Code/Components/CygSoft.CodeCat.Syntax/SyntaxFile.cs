using CygSoft.CodeCat.Syntax.Infrastructure;

namespace CygSoft.CodeCat.Syntax
{
    public class SyntaxFile : ISyntaxFile
    {
        public string Syntax { get; private set; }
        public string FilePath { get; private set; }
        public string Extension { get; private set; }

        public SyntaxFile(string syntax, string filePath, string extension)
        {
            this.Syntax = syntax;
            this.FilePath = filePath;
            this.Extension = extension;
        }
    }
}
