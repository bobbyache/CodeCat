using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public class TopicSectionEventArgs : EventArgs
    {
        public ITopicSection File { get; private set; }

        public TopicSectionEventArgs(ITopicSection file)
        {
            this.File = file;
        }
    }
}
