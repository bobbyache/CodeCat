using System;

namespace CygSoft.CodeCat.Infrastructure
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
