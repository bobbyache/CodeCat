using CygSoft.CodeCat.Plugin.Infrastructure;
using System.Collections.Generic;

namespace CygSoft.CodeCat.Infrastructure
{
    public interface IDocumentIndexRepository
    {
        List<IPluginControl> LoadDocuments();
        void WriteDocuments(List<IPluginControl> documents);
    }
}
