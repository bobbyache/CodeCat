using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IFileVersion
    {
        string Id { get; }
        DateTime TimeTaken { get; }
        string Description { get; }
        string Title { get; }
        string FilePath { get; }
        string Text();
    }

    public interface IVersionableFile
    {
        event EventHandler SnapshotTaken;
        event EventHandler SnapshotDeleted;

        IFileVersion[] Versions { get; }
        bool HasVersions { get; }

        bool HasVersion(string versionId);
        IFileVersion GetVersion(string versionId);
        IFileVersion LatestVersion();
        IFileVersion CreateVersion(string description = "");
        void DeleteVersion(string versionId);
    }
}
