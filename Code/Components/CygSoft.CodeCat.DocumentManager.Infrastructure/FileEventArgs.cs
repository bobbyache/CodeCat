using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public class FileEventArgs : EventArgs
    {
        public IFile File { get; private set; }

        public FileEventArgs(IFile file)
        {
            this.File = file;
        }
    }
}
