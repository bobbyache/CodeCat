using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Stubs.DocumentManager
{
    internal class StubDocManager : BaseDocManager
    {
        public override bool IndexFileExists
        {
            get { throw new NotImplementedException(); }
        }

        protected override void CreateIndexFile()
        {
        }

        protected override void LoadIndexFile()
        {
        }

        protected override void UpdateDocumentIndex()
        {
            throw new NotImplementedException();
        }

        protected override void LoadDocumentFiles()
        {
        }

        protected override void CreateNewDocumentFile()
        {
            
        }

        protected override void RemoveDocumentFile()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateDocumentFile()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateDocumentFiles()
        {
            throw new NotImplementedException();
        }

        protected override void RemoveDocumentFile(IDocumentFile documentFile)
        {
            throw new NotImplementedException();
        }
    }
}
