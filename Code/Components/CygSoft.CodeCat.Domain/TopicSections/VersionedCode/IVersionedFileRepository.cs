using System.Collections.Generic;
using CygSoft.CodeCat.DocumentManager.Infrastructure;

namespace CygSoft.CodeCat.Domain.TopicSections.VersionedCode
{
    public interface IVersionedFileRepository
    {
        string FilePath { get; set; }

        bool HasVersionFile { get; }

        List<IFileVersion> LoadVersions();
        void WriteVersions(List<IFileVersion> fileVersions);
    }
}