using CygSoft.CodeCat.DocumentManager;
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

        protected override void LoadDocumentFiles()
        {
            //throw new NotImplementedException();
        }

        protected override void RemoveDocumentFile(IDocumentFile documentFile)
        {
            //throw new NotImplementedException();
        }

        protected override void SaveFile()
        {
            //throw new NotImplementedException();
        }

        protected override void OpenFile()
        {
            //throw new NotImplementedException();
        }
    }
}
