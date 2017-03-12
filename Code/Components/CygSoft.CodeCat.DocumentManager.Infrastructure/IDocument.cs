using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IDocument  : IPositionedItem
    {
        event EventHandler<TopicSectionEventArgs> BeforeDelete;
        event EventHandler<TopicSectionEventArgs> AfterDelete;
        event EventHandler<TopicSectionEventArgs> BeforeOpen;
        event EventHandler<TopicSectionEventArgs> AfterOpen;
        event EventHandler<TopicSectionEventArgs> BeforeSave;
        event EventHandler<TopicSectionEventArgs> AfterSave;
        event EventHandler<TopicSectionEventArgs> BeforeClose;
        event EventHandler<TopicSectionEventArgs> AfterClose;
        event EventHandler<TopicSectionEventArgs> BeforeRevert;
        event EventHandler<TopicSectionEventArgs> AfterRevert;

        string Id { get; }
        string FilePath { get; }
        string FileName { get; }
        string FileExtension { get; }
        string Folder { get; }
        bool FolderExists { get; }
        bool Exists { get; }
        bool Loaded { get; }
        string Title { get; set; }
        string DocumentType { get; set; }
        string Description { get; set; }

        void Open();
        void Delete();
        void Save();
        void Close();
        void Revert();
    }
}
