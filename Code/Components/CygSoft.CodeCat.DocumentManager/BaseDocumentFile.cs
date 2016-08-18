using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager
{
    public abstract class BaseDocumentFile : BaseVersionableFile, IDocumentFile
    {
        public int Ordinal { get; set; }

        public BaseDocumentFile(string filePath) : base(filePath)
        {
        }
    }
}
