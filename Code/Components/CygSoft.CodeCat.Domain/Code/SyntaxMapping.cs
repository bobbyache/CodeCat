using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Code
{
    internal class SyntaxMapping
    {
        public string Language { get; private set; }
        public string FilePath { get; private set; }

        public SyntaxMapping(string language, string filePath)
        {
            this.Language = language;
            this.FilePath = filePath;
        }
    }
}
