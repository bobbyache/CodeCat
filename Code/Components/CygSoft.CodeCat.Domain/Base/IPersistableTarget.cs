using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;

namespace CygSoft.CodeCat.Domain.Base
{
    public interface IPersistableTarget : IKeywordTarget
    {
        int HitCount { get; }
        string Title { get; set; }

        event EventHandler<DocumentIndexEventArgs> BeforeDelete;
        event EventHandler<DocumentIndexEventArgs> AfterDelete;
        event EventHandler<DocumentIndexEventArgs> BeforeOpen;
        event EventHandler<DocumentIndexEventArgs> AfterOpen;
        event EventHandler<DocumentIndexEventArgs> BeforeSave;
        event EventHandler<DocumentIndexEventArgs> AfterSave;
        event EventHandler<DocumentIndexEventArgs> BeforeClose;
        event EventHandler<DocumentIndexEventArgs> AfterClose;
        event EventHandler<DocumentIndexEventArgs> BeforeRevert;
        event EventHandler<DocumentIndexEventArgs> AfterRevert;

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
