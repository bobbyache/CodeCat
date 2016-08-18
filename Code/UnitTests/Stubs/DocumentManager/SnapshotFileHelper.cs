using CygSoft.CodeCat.DocumentManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Stubs.DocumentManager
{
    public static class SnapshotFileHelper
    {
        public static string CreateId(string filePath, DateTime timeStamp)
        {
            SnapshotFileNamer fileNamer = new SnapshotFileNamer(filePath, timeStamp);
            return fileNamer.Id;
        }

        public static string CreateFileName(string filePath, DateTime timeStamp)
        {
            SnapshotFileNamer fileNamer = new SnapshotFileNamer(filePath, timeStamp);
            return fileNamer.FileName;
        }

        public static string CreateFilePath(string filePath, DateTime timeStamp)
        {
            SnapshotFileNamer fileNamer = new SnapshotFileNamer(filePath, timeStamp);
            return fileNamer.FilePath;
        }
    }
}
