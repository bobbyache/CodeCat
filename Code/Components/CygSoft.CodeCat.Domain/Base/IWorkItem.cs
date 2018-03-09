using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;

namespace CygSoft.CodeCat.Domain.Base
{
    public interface IWorkItem : IKeywordTarget, IFile
    {
        string Title { get; set; }

        // do not set file content here, this is the job of specialized file classes.

        string Id { get; }
        string FilePath { get; }
        string FileName { get; }
        string FileExtension { get; }
        string Folder { get; }
        bool FolderExists { get; }
        bool Exists { get; }
        bool Loaded { get; }

        void Open();
        void Delete();
        void Save();
        void Close();
        void Revert();
    }
}
