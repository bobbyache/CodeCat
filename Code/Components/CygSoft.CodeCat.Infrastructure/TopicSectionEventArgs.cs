using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
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
