using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Stubs.DocumentManager
{
    internal class StubDocumentFile : BaseDocument
    {
        public StubDocumentFile(string folder, string id, string title, string description = null, int ordinal = -1)
            : base(new DocumentPathGenerator(folder, "txt", id), title, description, ordinal)
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
