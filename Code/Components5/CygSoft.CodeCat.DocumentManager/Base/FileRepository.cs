using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Base
{
    public class FileRepository : IFileRepository
    {
        protected BaseFilePathGenerator filePathGenerator;

        public FileRepository(BaseFilePathGenerator filePathGenerator)
        {
            this.filePathGenerator = filePathGenerator;
        }

        public bool Loaded { get; private set; }

        public string FilePath => filePathGenerator.FilePath;

        public string FileName => filePathGenerator.FileName;

        public string FileTitle => Path.GetFileNameWithoutExtension(filePathGenerator.FileName);

        public string Extension => filePathGenerator.FileExtension;

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public bool DirectoryExists()
        {
            return Directory.Exists(Path.GetDirectoryName(this.FilePath));
        }

        public bool FileExists()
        {
            return File.Exists(this.FilePath);
        }

        public string GetDirectory()
        {
            return Path.GetDirectoryName(this.FilePath);
        }

        public void Open()
        {
            throw new NotImplementedException();
        }
    }
}
