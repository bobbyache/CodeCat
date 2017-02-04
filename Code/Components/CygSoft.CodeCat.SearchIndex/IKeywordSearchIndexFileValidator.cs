using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Search.KeywordIndex
{
    public interface IKeywordSearchIndexFileValidator
    {
        bool Exists(string filePath);
        bool CorrectFormat(string filePath);
        bool CorrectVersion(string filePath);
    }
}
