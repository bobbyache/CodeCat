using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Base;
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
        public StubDocumentFile(string id, string title, string description = null)
            : base(id, "txt", title, description)
        {
        }

        public StubDocumentFile(string id, int ordinal, string title, string description = null)
            : base(id, "txt", ordinal, title, description)
        {
        }

        protected override IFileVersion NewVersion(DateTime timeStamp, string description)
        {
            return new StubVersionFile(this.FilePath, timeStamp, description);
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
