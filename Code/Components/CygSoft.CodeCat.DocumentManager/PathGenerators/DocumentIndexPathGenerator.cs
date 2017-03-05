using CygSoft.CodeCat.DocumentManager.Base;
using System;
using System.IO;

namespace CygSoft.CodeCat.DocumentManager.PathGenerators
{
    public class DocumentIndexPathGenerator : BaseFilePathGenerator
    {
        private string folder;
        private string extension;
        private string id;

        public DocumentIndexPathGenerator(string folder, string extension)
        {
            this.id = Guid.NewGuid().ToString();
            this.folder = folder;
            this.extension = extension;
        }

        public DocumentIndexPathGenerator(string folder, string extension, string id)
        {
            this.id = id;
            this.folder = folder;
            this.extension = extension;
        }

        public override string FileExtension
        {
            get { return base.CleanExtension(this.extension); }
        }

        public override string FileName
        {
            get { return id + "." + base.CleanExtension(extension); }
        }

        public override string FilePath
        {
            get { return Path.Combine(folder, id, id + "." + base.CleanExtension(extension)); }
        }

        public override string Id
        {
            get { return id; }
        }
    }
}
