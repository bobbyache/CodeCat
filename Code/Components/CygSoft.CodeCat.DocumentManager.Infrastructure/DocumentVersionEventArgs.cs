using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public class DocumentVersionEventArgs : EventArgs
    {
        public IVersionableFile File { get; private set; }
        public IFileVersion FileVersion { get; private set; }

        public DocumentVersionEventArgs(IVersionableFile file, IFileVersion fileVersion)
        {
            this.File = file;
            this.FileVersion = fileVersion;
        }
    }
}
