using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager
{
    public class BaseFile : IFile
    {
        public virtual string Id { get { return Path.GetFileNameWithoutExtension(this.FileName); } }
        public virtual string FilePath { get; private set; }
        public string Text { get; set; }
        public virtual string FileName { get { return Path.GetFileName(this.FilePath); } }
        public virtual string Folder
        {
            get { return Path.GetDirectoryName(this.FilePath); }
        }

        public BaseFile(string filePath)
        {
            this.FilePath = filePath;
        }
    }
}
