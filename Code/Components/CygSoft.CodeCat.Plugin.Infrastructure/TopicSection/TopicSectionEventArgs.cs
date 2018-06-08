using System;

namespace CygSoft.CodeCat.Plugin.Infrastructure
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
