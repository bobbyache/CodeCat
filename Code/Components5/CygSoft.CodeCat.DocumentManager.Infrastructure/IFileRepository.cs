using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IFileRepository
    {
        bool Loaded { get; }

        string FilePath { get; }

        string FileName { get; }

        string FileTitle { get; }

        string Extension { get; }

        string GetDirectory();
        bool DirectoryExists();
        bool FileExists();

        void Open();
        void Close();
        void Create();
        void Delete();
    }
}
