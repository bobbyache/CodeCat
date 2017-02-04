using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Search.KeywordIndex
{
    internal class FilePathValidator : IKeywordSearchIndexFileValidator
    {
        public bool CorrectFormat(string filePath)
        {
            throw new NotImplementedException();
        }

        public bool CorrectVersion(string filePath)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
