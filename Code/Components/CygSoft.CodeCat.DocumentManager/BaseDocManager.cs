using CygSoft.CodeCat.DocumentManager.Documents;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager
{
    public abstract class BaseDocManager : IDocManager
    {
        //public IDocumentFile CreateDocument(DocumentTypeEnum documentType)
        //{
        //    Guid guid = Guid.NewGuid();

        //    switch (documentType)
        //    {
        //        case DocumentTypeEnum.TestDocument:
        //            return new TestDocument(guid.ToString());
                    
        //        default:
        //            throw new NotImplementedException();
        //    }
        //}

        //public IVersionableFile CreateSingleFile(

        public IVersionableFile CreateSingleDocument(DocumentTypeEnum documentType)
        {
            throw new NotImplementedException();
        }

        public IMultiDocumentFile CreateMultiDocument(DocumentTypeEnum documentType)
        {
            throw new NotImplementedException();
        }
    }
}
