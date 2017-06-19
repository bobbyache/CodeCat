using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.TopicSections.VersionedCode
{
    public class VersionedCodeTopicSection : CodeTopicSection, IVersionedCodeTopicSection
    {
        private List<IFileVersion> fileVersions = new List<IFileVersion>();

        public event EventHandler SnapshotTaken;
        public event EventHandler SnapshotDeleted;

        #region Custom Comparer
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

        public class CodeTopicSectionVersion : IFileVersion
        {
            public string Id { get; private set; }
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

            public CodeTopicSectionVersion(VersionPathGenerator versionPathGenerator, string description)
            {
                this.Id = versionPathGenerator.Id;
                this.TimeTaken = versionPathGenerator.TimeStamp;
                this.Description = description;
            }
        }

        public bool HasVersions
        {
            get { return fileVersions.Any(); }
        }

        public IFileVersion[] Versions
        {
            get { return fileVersions.ToArray(); }
        }

        internal VersionedCodeTopicSection(string folder, string title, string extension, string syntax) : base(folder, title, extension, syntax)
        {
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.VersionedCode);
        }

        internal VersionedCodeTopicSection(string folder, string id, string title, string extension, int ordinal, string description, string syntax)
            : base(folder, id, title, extension, ordinal, description, syntax)
        {
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.VersionedCode);
        }

        public IFileVersion CreateVersion(string description = "")
        {
            VersionPathGenerator versionPathGenerator = new VersionPathGenerator(this.FilePath, DateTime.Now);
            CodeTopicSectionVersion fileVersion = new CodeTopicSectionVersion(versionPathGenerator, description);
            this.fileVersions.Add(fileVersion);
            this.fileVersions.Sort(new VersionFileComparer());
            this.SnapshotTaken?.Invoke(this, new EventArgs());
            return fileVersion;
        }

        public void DeleteVersion(string versionId)
        {
            IFileVersion fileVersion = fileVersions.Where(s => s.Id == versionId).SingleOrDefault();
            fileVersions.Remove(fileVersion);
            fileVersions.Sort(new VersionFileComparer());
            this.SnapshotDeleted?.Invoke(this, new EventArgs());
        }

        public IFileVersion GetVersion(string versionId)
        {
            IFileVersion fileVersion = fileVersions.Where(s => s.Id == versionId).SingleOrDefault();
            return fileVersion;
        }

        public bool HasVersion(string versionId)
        {
            return fileVersions.Exists(r => r.Id == versionId);
        }

        public IFileVersion LatestVersion()
        {
            fileVersions.Sort(new VersionFileComparer());
            return fileVersions.Last();
        }
    }
}
