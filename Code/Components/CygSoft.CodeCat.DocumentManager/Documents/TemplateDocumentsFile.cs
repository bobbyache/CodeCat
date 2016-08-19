using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class TemplateDocumentsFile : BaseMultiDocumentFile
    {
        public TemplateDocumentsFile(string id) : base(id, "xml")
        {

        }

        protected override List<IDocumentFile> LoadDocumentFiles()
        {
            
            throw new NotImplementedException();
        }

        protected override void RemoveDocumentFile(Infrastructure.IDocumentFile documentFile)
        {
            throw new NotImplementedException();
        }

        protected override void CreateFile()
        {
            throw new NotImplementedException();
        }

        protected override void OpenFile()
        {
            throw new NotImplementedException();
        }

        protected override void SaveFile()
        {
            throw new NotImplementedException();
        }
    }
}
