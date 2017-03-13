using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IDocumentIndex
    {
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

        event EventHandler<DocumentEventArgs> DocumentAdded;
        event EventHandler<DocumentEventArgs> DocumentRemoved;
        event EventHandler<DocumentEventArgs> DocumentMovedUp;
        event EventHandler<DocumentEventArgs> DocumentMovedDown;

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

        // files within the document group
        ITopicSection[] DocumentFiles { get; }

        bool DocumentExists(string id);

        ITopicSection AddDocumentFile(ITopicSection topicSection); // necessary, because document files could be of different types...
                                                          // need to be created elsewhere like a document factory.
        void RemoveDocumentFile(string id);
        ITopicSection GetDocumentFile(string id);

        ITopicSection FirstDocument { get; }
        ITopicSection LastDocument { get; }

        bool CanMoveDown(ITopicSection topicSection);
        bool CanMoveTo(ITopicSection topicSection, int ordinal);
        bool CanMoveUp(ITopicSection topicSection);
        void MoveDown(ITopicSection topicSection);
        void MoveTo(ITopicSection topicSection, int ordinal);
        void MoveUp(ITopicSection topicSection);
        void MoveLast(ITopicSection topicSection);
        void MoveFirst(ITopicSection topicSection);
    }
}
