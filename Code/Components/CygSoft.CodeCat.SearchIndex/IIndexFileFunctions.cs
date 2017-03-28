using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Search.KeywordIndex
{
    public interface IIndexFileFunctions
    {
        bool Exists(string filePath);
        string Open(string filePath);
        void Save(string fileText, string filePath);
        bool CheckFormat(string fileText);
        bool CheckVersion(string fileText, int expectedVersion);
    }
}
