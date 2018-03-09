using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface ITopic : IFile
    {
        event EventHandler<TopicSectionEventArgs> TopicSectionAdded;
        event EventHandler<TopicSectionEventArgs> TopicSectionRemoved;
        event EventHandler<TopicSectionEventArgs> TopicSectionMovedUp;
        event EventHandler<TopicSectionEventArgs> TopicSectionMovedDown;

        string Id { get; }

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
