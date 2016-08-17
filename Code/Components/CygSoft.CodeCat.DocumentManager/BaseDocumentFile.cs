using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager
{
    public class BaseDocumentFile : IDocumentFile
    {
        public string Id { get { return this.FileName; } }
        public string FilePath { get; private set; }
        public string Text { get; set; }
        public string FileName { get { return Path.GetFileName(this.FilePath); } }
        public string Folder
        {
            get { return Path.GetDirectoryName(this.FilePath); }
        }

        public int Ordinal { get; set; }

        public BaseDocumentFile(string filePath)
        {
            this.FilePath = filePath;
        }
    }
}
