using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public class DocumentIndexEventArgs : EventArgs
    {
        public IDocumentIndex File { get; private set; }

        public DocumentIndexEventArgs(IDocumentIndex file)
        {
            this.File = file;
        }
    }
}
