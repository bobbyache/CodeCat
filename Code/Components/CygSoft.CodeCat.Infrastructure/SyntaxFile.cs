using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Infrastructure
{
    public class SyntaxFile
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
