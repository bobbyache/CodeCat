using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;

namespace CygSoft.CodeCat.Domain.Base
{
    public interface IWorkItem : IKeywordTarget
    {
        string Title { get; set; }

        event EventHandler<TopicEventArgs> BeforeDelete;
        event EventHandler<TopicEventArgs> AfterDelete;
        event EventHandler<TopicEventArgs> BeforeOpen;
        event EventHandler<TopicEventArgs> AfterOpen;
        event EventHandler<TopicEventArgs> BeforeSave;
        event EventHandler<TopicEventArgs> AfterSave;
        event EventHandler<TopicEventArgs> BeforeClose;
        event EventHandler<TopicEventArgs> AfterClose;
        event EventHandler<TopicEventArgs> BeforeRevert;
        event EventHandler<TopicEventArgs> AfterRevert;

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
