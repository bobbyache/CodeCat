using System;

namespace CygSoft.CodeCat.Plugins.UI.Infrastructure.TopicSection
{
    public class TopicSectionEventArgs : EventArgs
    {
        public ITopicSection TopicSection { get; private set; }

        public TopicSectionEventArgs(ITopicSection topicSection)
        {
            this.TopicSection = topicSection;
        }
    }
}
