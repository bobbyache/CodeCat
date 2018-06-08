using CygSoft.CodeCat.Plugin.Infrastructure;
using System;

namespace CygSoft.CodeCat.Infrastructure
{
    public interface ITopic : IFile
    {
        event EventHandler<TopicSectionEventArgs> TopicSectionAdded;
        event EventHandler<TopicSectionEventArgs> TopicSectionRemoved;
        event EventHandler<TopicSectionEventArgs> TopicSectionMovedUp;
        event EventHandler<TopicSectionEventArgs> TopicSectionMovedDown;

        string Id { get; }

        // files within the document group
        IPluginControl[] TopicSections { get; }

        bool TopicSectionExists(string id);

        IPluginControl AddTopicSection(IPluginControl topicSection); // necessary, because document files could be of different types...
                                                          // need to be created elsewhere like a document factory.
        void RemoveTopicSection(string id);
        IPluginControl GetTopicSection(string id);

        IPluginControl FirstTopicSection { get; }
        IPluginControl LastTopicSection { get; }

        bool CanMoveDown(IPluginControl topicSection);
        bool CanMoveTo(IPluginControl topicSection, int ordinal);
        bool CanMoveUp(IPluginControl topicSection);
        void MoveDown(IPluginControl topicSection);
        void MoveTo(IPluginControl topicSection, int ordinal);
        void MoveUp(IPluginControl topicSection);
        void MoveLast(IPluginControl topicSection);
        void MoveFirst(IPluginControl topicSection);
    }
}
