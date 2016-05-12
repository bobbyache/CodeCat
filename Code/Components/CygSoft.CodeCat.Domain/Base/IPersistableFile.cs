using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Base
{
    public interface IPersistableFile
    {
        string Id { get; }
        string Title { get; set; }
        string FilePath { get; }
        string FolderPath { get; }
        string FileTitle { get; }
        bool FileExists { get; }
        string CommaDelimitedKeywords { get; set; }
        void Save();
        void Delete();
        IKeywordIndexItem IndexItem { get; }

        event EventHandler ContentSaved;
    }
}
