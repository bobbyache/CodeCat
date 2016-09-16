using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IDocumentIndexRepository
    {
        List<IDocument> LoadDocuments();
        void WriteDocuments(List<IDocument> documents);
    }
}
