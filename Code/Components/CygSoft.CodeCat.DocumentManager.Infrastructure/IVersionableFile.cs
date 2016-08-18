using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IVersionableFile : IFile
    {
        IFileVersion[] Versions { get; }
        bool HasVersions { get; }

        void CreateVersion(string description = "");
        void DeleteVersion(string versionId);
    }
}
