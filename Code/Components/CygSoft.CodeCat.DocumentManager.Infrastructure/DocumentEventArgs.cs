using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public class DocumentEventArgs : EventArgs
    {
        public ITopicSection Document { get; private set; }

        public DocumentEventArgs(ITopicSection document)
        {
            this.Document = document;
        }
    }
}
