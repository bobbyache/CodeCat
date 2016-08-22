using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IMultiDocumentFile : IFile
    {
        // files within the document group
        IDocumentFile[] DocumentFiles { get; }

        void SaveDocumentFiles();
        IDocumentFile AddDocumentFile(IDocumentFile documentFile); // necessary, because document files could be of different types...
                                                          // need to be created elsewhere like a document factory.
        void DeleteDocumentFile(string id);
        IDocumentFile GetDocumentFile(string id);

        bool CanMoveDown(IDocumentFile documentFile);
        bool CanMoveTo(IDocumentFile documentFile, int ordinal);
        bool CanMoveUp(IDocumentFile documentFile);
        void MoveDown(IDocumentFile documentFile);
        void MoveTo(IDocumentFile documentFile, int ordinal);
        void MoveUp(IDocumentFile documentFile);
    }
}
