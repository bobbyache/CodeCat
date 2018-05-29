using System.Collections.Generic;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IVersionedFileRepository
    {
        string FilePath { get; set; }

        bool HasVersionFile { get; }

        List<IFileVersion> LoadVersions();
        void WriteVersions(List<IFileVersion> fileVersions);
    }
}
