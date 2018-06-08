using CygSoft.CodeCat.Plugin.Infrastructure;
using System.Collections.Generic;

namespace CygSoft.CodeCat.Infrastructure
{
    public interface IDocumentIndexRepository
    {
        List<ITopicSection> LoadDocuments();
        void WriteDocuments(List<ITopicSection> documents);
    }
}
