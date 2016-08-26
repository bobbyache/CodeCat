using CygSoft.CodeCat.DocumentManager.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.PathGenerators
{
    public class DocumentPathGenerator : BaseFilePathGenerator
    {
        private string folder;
        private string extension;
        private string id;

        public DocumentPathGenerator(string folder, string extension)
        {
            this.id = Guid.NewGuid().ToString();
            this.folder = folder;
            this.extension = extension;
        }

        public DocumentPathGenerator(string folder, string extension, string id)
        {
            this.id = id;
            this.folder = folder;
            this.extension = extension;
        }

        public override string FileExtension
        {
            get { return CleanExtension(this.extension); }
        }

        public override string FileName
        {
            get { return id + "." + CleanExtension(extension); }
        }

        public override string FilePath
        {
            get { return Path.Combine(folder, id + "." + CleanExtension(extension)); }
        }

        public override string Id
        {
            get { return id; }
        }

        private string CleanExtension(string ext)
        {
            if (ext.Length > 0)
            {
                if (ext.StartsWith("."))
                    return ext.Substring(1);
            }

            return ext;
        }
    }
}
