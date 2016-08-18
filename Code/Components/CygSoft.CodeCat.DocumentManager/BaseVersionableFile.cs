using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager
{
    public abstract class BaseVersionableFile : BaseFile, IVersionableFile
    {
        private List<IFileVersion> fileVersions = new List<IFileVersion>();

        public BaseVersionableFile(string filePath)
            : base(filePath)
        {
        }

        protected abstract IFileVersion NewSnapshot(DateTime timeStamp, string description, string text);

        public IFileVersion[] Versions
        {
            get { return this.fileVersions.ToArray(); }
        }

        public bool HasVersions
        {
            get { return this.fileVersions.Count > 0; }
        }

        public void CreateVersion(string description = "")
        {
            DateTime versionTime = DateTime.Now;
            IFileVersion fileVersion = this.NewSnapshot(versionTime, description, this.Text);
            this.fileVersions.Add(fileVersion);
        }

        public void DeleteVersion(string versionId)
        {
            throw new NotImplementedException();
        }
    }
}
