using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
