using System;

namespace CygSoft.CodeCat.Infrastructure
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
