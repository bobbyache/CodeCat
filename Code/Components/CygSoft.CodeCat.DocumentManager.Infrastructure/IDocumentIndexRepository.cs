using System.Collections.Generic;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IDocumentIndexRepository
    {
        List<IDocument> LoadDocuments();
        void WriteDocuments(List<IDocument> documents);
    }
}
