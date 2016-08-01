using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Infrastructure
{
    public interface IPersistableTarget
    {
        string Id { get; }
        string Title { get; set; }
        string FilePath { get; }
        string FolderPath { get; }
        string FileTitle { get; }
        bool FileExists { get; }
        int HitCount { get; }
        string CommaDelimitedKeywords { get; set; }

        bool Open();
        void Close();
        void Save();
        void Delete();  

        event EventHandler ContentSaved;
        event EventHandler ContentClosed;
        event EventHandler ContentDeleted;
    }
}
