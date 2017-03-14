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

        event EventHandler<TopicSectionEventArgs> TopicSectionAdded;
        event EventHandler<TopicSectionEventArgs> TopicSectionRemoved;
        event EventHandler<TopicSectionEventArgs> TopicSectionMovedUp;
        event EventHandler<TopicSectionEventArgs> TopicSectionMovedDown;

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
        ITopicSection[] TopicSections { get; }

        bool TopicSectionExists(string id);

        ITopicSection AddTopicSection(ITopicSection topicSection); // necessary, because document files could be of different types...
                                                          // need to be created elsewhere like a document factory.
        void RemoveTopicSection(string id);
        ITopicSection GetTopicSection(string id);

        ITopicSection FirstTopicSection { get; }
        ITopicSection LastTopicSection { get; }

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
