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
        private List<IFileSnapshot> snapshots = new List<IFileSnapshot>();

        public BaseVersionableFile(string filePath)
            : base(filePath)
        {
        }

        protected abstract IFileSnapshot NewSnapshot(DateTime timeStamp, string description, string text);

        public IFileSnapshot[] Snapshots
        {
            get { return this.snapshots.ToArray(); }
        }

        public bool HasSnapshots
        {
            get { return this.snapshots.Count > 0; }
        }

        public void TakeSnapshot(string description = "")
        {
            DateTime snapshotTime = DateTime.Now;
            IFileSnapshot snapshot = this.NewSnapshot(snapshotTime, description, this.Text);
            this.snapshots.Add(snapshot);
        }

        public void DeleteSnapshot(string snapshotId)
        {
            throw new NotImplementedException();
        }
    }
}
