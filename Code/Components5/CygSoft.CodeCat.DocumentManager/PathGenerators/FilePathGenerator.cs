using CygSoft.CodeCat.DocumentManager.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.PathGenerators
{
    public class FilePathGenerator : BaseFilePathGenerator
    {
        private string folder;
        private string fileName;
        private string id;

        public FilePathGenerator(string folder, string fileName)
        {
            this.id = Guid.NewGuid().ToString();
            this.folder = folder;
            this.fileName = fileName;
        }

        public FilePathGenerator(string folder, string fileName, string id)
        {
            this.id = id;
            this.folder = folder;
            this.fileName = fileName;
        }

        public override string Id
        {
            get { return id; }
        }

        public override string FileExtension { get { return Path.GetExtension(fileName); } }
        public override string FileName { get { return fileName; } }
        public override string FilePath { get { return Path.Combine(folder, fileName); } }

        public string ModifiedFileName { get { return "TEMP_" + this.Id + "." + this.FileExtension; } }
        public string ModifiedFilePath { get { return Path.Combine(this.FolderPath, this.ModifiedFileName); } }
        public string ModifiedFileExtension { get { return Path.GetExtension(this.ModifiedFileName); } }

        public void RenameFile(string fileName)
        {
            this.fileName = fileName;
        }
    }
}
