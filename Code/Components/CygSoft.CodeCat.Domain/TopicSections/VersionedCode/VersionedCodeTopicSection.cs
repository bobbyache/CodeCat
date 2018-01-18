using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.DocumentManager.TopicSections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.TopicSections.VersionedCode
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
            public string FilePath { get; private set; }

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
                this.FilePath = versionPathGenerator.FilePath;
            }

            public CodeTopicSectionVersion(string id, DateTime timeTaken, string description, string filePath)
            {
                this.Id = id;
                this.TimeTaken = timeTaken;
                this.Description = description;
                this.FilePath = filePath;
            }

            public string Text()
            {
                return File.ReadAllText(FilePath);
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

        public VersionedCodeTopicSection(string folder, string title, string extension, string syntax) : base(folder, title, extension, syntax)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.VersionedCode);
        }

        public VersionedCodeTopicSection(string folder, string id, string title, string extension, int ordinal, string description, string syntax)
            : base(folder, id, title, extension, ordinal, description, syntax)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.VersionedCode);
        }

        private string GetVersionIndexPath()
        {
            return Path.Combine(Path.GetDirectoryName(this.FilePath), Path.GetFileNameWithoutExtension(this.FilePath) + ".xml");
        }

        public IFileVersion CreateVersion(string description = "")
        {
            VersionPathGenerator versionPathGenerator = new VersionPathGenerator(this.FilePath, DateTime.Now);
            CodeTopicSectionVersion fileVersion = new CodeTopicSectionVersion(versionPathGenerator, description);
            File.WriteAllText(fileVersion.FilePath, this.Text);

            this.fileVersions.Add(fileVersion);
            this.fileVersions.Sort(new VersionFileComparer());

            VersionedCodeIndexXmlRepository repo = new VersionedCodeIndexXmlRepository(GetVersionIndexPath());
            repo.WriteVersions(fileVersions);

            this.SnapshotTaken?.Invoke(this, new EventArgs());
            return fileVersion;
        }

        protected override void OnOpen()
        {
            VersionedCodeIndexXmlRepository repo = new VersionedCodeIndexXmlRepository(GetVersionIndexPath());

            if (repo.HasVersionFile)
                this.fileVersions = repo.LoadVersions();
            base.OnOpen();
        }

        public void DeleteVersion(string versionId)
        {
            IFileVersion fileVersion = fileVersions.Where(s => s.Id == versionId).SingleOrDefault();

            File.Delete(fileVersion.FilePath);
            fileVersions.Remove(fileVersion);

            if (fileVersions.Any())
            {
                fileVersions.Sort(new VersionFileComparer());

                VersionedCodeIndexXmlRepository repo = new VersionedCodeIndexXmlRepository(GetVersionIndexPath());
                repo.WriteVersions(fileVersions);
            }
            else
            {
                string path = GetVersionIndexPath();
                if (File.Exists(path))
                    File.Delete(path);
            }
                
            this.SnapshotDeleted?.Invoke(this, new EventArgs());
        }

        protected override void OnDelete()
        {
            base.OnDelete();

            var filePaths = fileVersions.Select(f => f.FilePath);
            foreach (var filePath in filePaths)
                File.Delete(filePath);
            File.Delete(this.FilePath);
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
