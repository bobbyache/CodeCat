using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Stubs.DocumentManager
{
    internal class StubDocumentFile : BaseDocumentFile
    {
        public StubDocumentFile(string filePath) : base(filePath)
        {

        }

        protected override IFileSnapshot NewSnapshot(DateTime timeStamp, string description, string text)
        {
            return new StubSnapshotFile(this.FilePath, timeStamp, description, text);
        }
    }
}
