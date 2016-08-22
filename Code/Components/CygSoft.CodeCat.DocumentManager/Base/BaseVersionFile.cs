using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Base
{
    public abstract class BaseVersionFile : BaseFile, IFileVersion
    {
        private VersionFileNamer versionFileNamer = null;

        public DateTime TimeTaken { get; private set; }
        public string Description { get; private set; }

        public string Title
        {
            get
            {
                string desc = string.Format("Version Snapshot: {0} {1}", this.TimeTaken.ToLongDateString(),
                    this.TimeTaken.ToLongTimeString());
                return desc;
            }
        }

        public override string Id { get { return versionFileNamer.Id; } }
        public override string FileName { get { return versionFileNamer.FileName; } }
        public override string FilePath { get { return versionFileNamer.FilePath; } }

        public BaseVersionFile(string filePath, DateTime timeStamp, string description) : base(Path.GetExtension(filePath))
        {
            this.versionFileNamer = new VersionFileNamer(filePath, timeStamp);
            base.FilePath = this.versionFileNamer.FilePath;
            base.Id = versionFileNamer.Id;
            this.TimeTaken = timeStamp;
            this.Description = description;
        }

        public BaseVersionFile(string id, string versionedFilePath, DateTime timeStamp, string description) : base(versionedFilePath)
        {
            this.versionFileNamer = new VersionFileNamer(versionedFilePath, timeStamp);
            base.FilePath = this.versionFileNamer.FilePath;
            base.Id = versionFileNamer.Id;
            this.TimeTaken = timeStamp;
            this.Description = description;
        }
    }
}
