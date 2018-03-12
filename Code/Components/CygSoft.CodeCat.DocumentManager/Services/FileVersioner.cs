using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Services
{
    public class FileVersioner
    {
        private List<IFileVersion> fileVersions = new List<IFileVersion>();
        private IVersionedFileRepository versionedFileRepository;
    }
}
