using System;

namespace CygSoft.CodeCat.Plugins.TopicSection.Infrastructure
{
    //TODO: IPersistableTarget and a TopicSection? Need to look and identifiy whether these two can't be merged.
    public interface ITopicDocument : IWorkItem
    {
        event EventHandler<TopicSectionEventArgs> TopicSectionAdded;
        event EventHandler<TopicSectionEventArgs> TopicSectionMovedLeft;
        event EventHandler<TopicSectionEventArgs> TopicSectionMovedRight;
        event EventHandler<TopicSectionEventArgs> TopicSectionRemoved;

        string Text { get; set; }
        string Syntax { get; set; }
        bool TopicSectionExists(string id);

        // don't like this... should not be returning "all" documents.
        // should be treating a collection of Template and a collection of Script.
        ITopicSection[] TopicSections { get; }
        ITopicSection GetTopicSection(string id);

        ITopicSection AddTopicSection(TopicSectionType documentType, string title = "New Document", string syntax = null, string extension = null);
        void RemoveTopicSection(string id);

        void MoveTopicSectionLeft(string id);
        void MoveTopicSectionRight(string id);
    }
}
