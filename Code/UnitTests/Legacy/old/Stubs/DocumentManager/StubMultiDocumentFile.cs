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
    public class StubMultiDocumentFile : BaseDocumentIndex
    {
        public StubMultiDocumentFile(string folder, string id) : base(null, new DocumentIndexPathGenerator(folder, "xml", id))
        {
        }

        protected override List<IDocument> LoadDocumentFiles()
        {
            List<IDocument> documentFiles = new List<IDocument>
            {
                new StubDocumentFile(@"C:\Documents", "37c1dba5-9da3-4222-af34-43f98c674d82", "Title", null, 3),
                new StubDocumentFile(@"C:\Documents", "f562810b-a1f7-4cf8-b370-dbaf87ff8759", "Title", null, 1),
                new StubDocumentFile(@"C:\Documents", "11334214-ca43-406b-9cae-f986c3c63332", "Title", null, 2)
            };
            return documentFiles;
        }


        protected override void SaveDocumentIndex()
        {
        }


    }
}
