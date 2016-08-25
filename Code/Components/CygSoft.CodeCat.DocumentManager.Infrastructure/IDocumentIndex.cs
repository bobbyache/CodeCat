using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IDocumentIndex : IFile
    {
        // files within the document group
        IDocument[] DocumentFiles { get; }

        bool DocumentExists(string id);

        IDocument AddDocumentFile(IDocument documentFile); // necessary, because document files could be of different types...
                                                          // need to be created elsewhere like a document factory.
        void RemoveDocumentFile(string id);
        IDocument GetDocumentFile(string id);

        bool CanMoveDown(IDocument documentFile);
        bool CanMoveTo(IDocument documentFile, int ordinal);
        bool CanMoveUp(IDocument documentFile);
        void MoveDown(IDocument documentFile);
        void MoveTo(IDocument documentFile, int ordinal);
        void MoveUp(IDocument documentFile);
        void MoveLast(IDocument documentFile);
        void MoveFirst(IDocument documentFile);
    }
}
