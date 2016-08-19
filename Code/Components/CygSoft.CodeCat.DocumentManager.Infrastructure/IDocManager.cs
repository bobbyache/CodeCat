using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public enum DocumentTypeEnum
    {
        TemplateDocument
    }

    public interface IDocManager
    {
        IVersionableFile CreateSingleDocument(DocumentTypeEnum documentType);
        IMultiDocumentFile CreateMultiDocument(DocumentTypeEnum documentType);        
    }
}
