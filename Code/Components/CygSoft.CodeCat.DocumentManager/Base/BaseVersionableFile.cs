using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.CodeCat.DocumentManager.Base
{
    public abstract class BaseVersionableFile : BaseFile, IVersionableFile
    {

        # region Custom Comparer
        private class VersionFileComparer : IComparer<IFileVersion>
        {
            public int Compare(IFileVersion x, IFileVersion y)
            {
                if (x.TimeTaken < y.TimeTaken)
                    return 1;
                if (x.TimeTaken > y.TimeTaken)
                    return -1;
                return 0;
            }
        }
        #endregion

        private List<IFileVersion> fileVersions = null;

        public BaseVersionableFile(BaseFilePathGenerator filePathGenerator) : base(filePathGenerator)
        {
            this.fileVersions = new List<IFileVersion>();
        }

        public IFileVersion[] Versions
        {
            get 
            {
                this.fileVersions.Sort(new VersionFileComparer());
                return this.fileVersions.ToArray(); 
            }
        }

        public bool HasVersions
        {
            get { return this.fileVersions.Count > 0; }
        }

        protected abstract IFileVersion NewVersion(DateTime timeStamp, string description);

        protected override void OnAfterDelete()
        {
            foreach (IFileVersion fileVersion in fileVersions)
                fileVersion.Delete();
        }

        public IFileVersion CreateVersion(string description = "")
        {
            DateTime versionTime = DateTime.Now;

            // -- note: template method "NewVersion" is called here, to be handled by the downstream sub-type.
            IFileVersion fileVersion = this.NewVersion(versionTime, description);
            // -----------------

            fileVersion.Save();
            this.fileVersions.Add(fileVersion);

            return fileVersion;
        }

        public void DeleteVersion(string versionId)
        {
            IFileVersion fileVersion = fileVersions.Where(s => s.Id == versionId).SingleOrDefault();
            fileVersion.Delete();
            this.fileVersions.Remove(fileVersion);
        }


        public bool HasVersion(string versionId)
        {
            return fileVersions.Exists(r => r.Id == versionId);
        }

        public IFileVersion GetVersion(string versionId)
        {
            return fileVersions.Where(s => s.Id == versionId).SingleOrDefault();
        }

        public IFileVersion LatestVersion()
        {
            fileVersions.Sort(new VersionFileComparer());
            return fileVersions.Last();
        }
    }
}
