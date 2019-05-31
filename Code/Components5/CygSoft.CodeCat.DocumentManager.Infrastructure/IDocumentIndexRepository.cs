using System.Collections.Generic;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IDocumentIndexRepository
    {
        List<ITopicSection> LoadDocuments();
        void WriteDocuments(List<ITopicSection> documents);
    }
}
