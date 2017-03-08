using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Search.KeywordIndex
{
    public interface IKeywordSearchIndexFile
    {
        string FilePath { get; }
        bool FileExists { get; }

        string Open();
        void Save(string fileText);
        void SaveAs(string fileText, string filePath);

        bool CorrectFormat { get; }
        bool CorrectVersion { get; }
    }
}
