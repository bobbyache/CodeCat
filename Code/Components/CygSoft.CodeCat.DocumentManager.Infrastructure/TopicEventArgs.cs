using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public class TopicEventArgs : EventArgs
    {
        public ITopic Topic { get; private set; }

        public TopicEventArgs(ITopic topic)
        {
            this.Topic = topic;
        }
    }
}
