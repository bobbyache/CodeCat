using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager
{
    public class BaseSnapshotFile : BaseFile, IFileSnapshot
    {
        private SnapshotFileNamer snapshotFileNamer = null;

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

        public override string Id { get { return snapshotFileNamer.Id; } }
        public override string FileName { get { return snapshotFileNamer.FileName; } }
        public override string FilePath { get { return snapshotFileNamer.FilePath; } }

        public BaseSnapshotFile(string filePath, DateTime timeStamp, string description, string text)
            : base(filePath)
        {
            this.snapshotFileNamer = new SnapshotFileNamer(filePath, timeStamp);
            this.TimeTaken = timeStamp;
            this.Description = description;
            this.Text = text;
        }
    }
}
