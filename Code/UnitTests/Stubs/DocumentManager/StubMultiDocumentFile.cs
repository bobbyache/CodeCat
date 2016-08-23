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
    public class StubMultiDocumentFile : BaseMultiDocumentFile
    {
        public StubMultiDocumentFile(string id) : base(id, "xml")
        {
        }

        protected override void CreateFile()
        {
            //throw new NotImplementedException();
        }

        protected override List<IDocumentFile> LoadDocumentFiles()
        {
            List<IDocumentFile> documentFiles = new List<IDocumentFile>
            {
                new StubDocumentFile("37c1dba5-9da3-4222-af34-43f98c674d82", 3, "Title"),
                new StubDocumentFile("f562810b-a1f7-4cf8-b370-dbaf87ff8759", 1, "Title"),
                new StubDocumentFile("11334214-ca43-406b-9cae-f986c3c63332", 2, "Title")
            };
            return documentFiles;
        }

        protected override void SaveFile()
        {
            //throw new NotImplementedException();
        }
    }
}
