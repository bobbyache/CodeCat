using System;

namespace CygSoft.CodeCat.Plugin.Infrastructure
{
    public class TopicSectionEventArgs : EventArgs
    {
        public IPluginControl TopicSection { get; private set; }

        public TopicSectionEventArgs(IPluginControl topicSection)
        {
            this.TopicSection = topicSection;
        }
    }
}
