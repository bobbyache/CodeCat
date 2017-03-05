using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public class DocumentEventArgs : EventArgs
    {
        public IDocument Document { get; private set; }

        public DocumentEventArgs(IDocument document)
        {
            this.Document = document;
        }
    }
}
