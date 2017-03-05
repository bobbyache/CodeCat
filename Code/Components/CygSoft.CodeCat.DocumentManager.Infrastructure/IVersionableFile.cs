namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IVersionableFile : IFile
    {
        IFileVersion[] Versions { get; }
        bool HasVersions { get; }

        bool HasVersion(string versionId);
        IFileVersion GetVersion(string versionId);
        IFileVersion LatestVersion();
        IFileVersion CreateVersion(string description = "");
        void DeleteVersion(string versionId);
    }
}
