using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.DocumentManager.TopicSections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CygSoft.CodeCat.Domain.TopicSections.VersionedCode
{
    public class VersionedCodeTopicSection : CodeTopicSection, IVersionedCodeTopicSection
    {
        private List<IFileVersion> fileVersions = new List<IFileVersion>();
        private IVersionedFileRepository versionedFileRepository;

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
            this.versionedFileRepository = new VersionedCodeIndexXmlRepository(GetVersionIndexPath());
            this.versionedFileRepository.FilePath = GetVersionIndexPath();
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.VersionedCode);
        }

        public VersionedCodeTopicSection(string folder, string id, string title, string extension, int ordinal, string description, string syntax)
            : base(folder, id, title, extension, ordinal, description, syntax)
        {
            this.versionedFileRepository = new VersionedCodeIndexXmlRepository(GetVersionIndexPath());
            this.versionedFileRepository.FilePath = GetVersionIndexPath();
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.VersionedCode);
        }

        public VersionedCodeTopicSection(IVersionedFileRepository versionedFileRepository, string folder, string title, string extension, string syntax) : base(folder, title, extension, syntax)
        {
            this.versionedFileRepository = versionedFileRepository;
            this.versionedFileRepository.FilePath = GetVersionIndexPath();
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.VersionedCode);
        }

        public VersionedCodeTopicSection(IVersionedFileRepository versionedFileRepository, string folder, string id, string title, string extension, int ordinal, string description, string syntax)
            : base(folder, id, title, extension, ordinal, description, syntax)
        {
            this.versionedFileRepository = versionedFileRepository;
            this.versionedFileRepository.FilePath = GetVersionIndexPath();
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.VersionedCode);
        }

        private string GetVersionIndexPath()
        {
            return Path.Combine(Path.GetDirectoryName(this.FilePath), Path.GetFileNameWithoutExtension(this.FilePath) + ".xml");
        }

        public IFileVersion CreateVersion(string description = "")
        {
            return CreateVersion(DateTime.Now, description);
        }

        public IFileVersion CreateVersion(DateTime timeStamp, string description = "")
        {
            VersionPathGenerator versionPathGenerator = new VersionPathGenerator(this.FilePath, timeStamp);
            CodeTopicSectionVersion fileVersion = new CodeTopicSectionVersion(versionPathGenerator, description);
            CreateVersionFile(fileVersion);

            this.fileVersions.Add(fileVersion);
            this.fileVersions.Sort(new VersionFileComparer());

            versionedFileRepository.WriteVersions(fileVersions);

            this.SnapshotTaken?.Invoke(this, new EventArgs());
            return fileVersion;
        }

        protected virtual void CreateVersionFile(IFileVersion fileVersion)
        {
            File.WriteAllText(fileVersion.FilePath, this.Text);
        }

        protected virtual void DeleteVersionFile(IFileVersion fileVersion)
        {
            File.Delete(fileVersion.FilePath);
        }

        protected override void OnOpen()
        {
            if (versionedFileRepository.HasVersionFile)
                this.fileVersions = versionedFileRepository.LoadVersions();
            base.OnOpen();
        }

        public void DeleteVersion(string versionId)
        {
            IFileVersion fileVersion = fileVersions.Where(s => s.Id == versionId).SingleOrDefault();

            DeleteVersionFile(fileVersion);
            fileVersions.Remove(fileVersion);

            if (fileVersions.Any())
            {
                fileVersions.Sort(new VersionFileComparer());
                versionedFileRepository.WriteVersions(fileVersions);
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
            var filePaths = fileVersions.Select(f => f.FilePath);
            foreach (var filePath in filePaths)
                File.Delete(filePath);

            base.OnDelete();
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
            return fileVersions.First();
        }
    }
}
