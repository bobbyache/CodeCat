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
        public StubDocumentFile(string id) : base(id, "txt")
        {
        }

        public StubDocumentFile(string id, int ordinal)
            : base(id, "txt", ordinal)
        {
        }

        protected override IFileVersion NewVersion(DateTime timeStamp, string description, string text)
        {
            return new StubVersionFile(this.FilePath, timeStamp, description, text);
        }

        protected override void CreateFile()
        {
            //throw new NotImplementedException();
        }

        protected override void OpenFile()
        {
            //throw new NotImplementedException();
        }

        protected override void SaveFile()
        {
            //throw new NotImplementedException();
        }
    }
}
