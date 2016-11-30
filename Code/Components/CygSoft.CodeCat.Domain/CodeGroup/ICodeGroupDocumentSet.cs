using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.CodeGroup
{
    public interface ICodeGroupDocumentSet : IPersistableTarget
    {
        event EventHandler<DocumentEventArgs> DocumentAdded;
        event EventHandler<DocumentEventArgs> DocumentMovedLeft;
        event EventHandler<DocumentEventArgs> DocumentMovedRight;
        event EventHandler<DocumentEventArgs> DocumentRemoved;

        string Text { get; set; }
        string Syntax { get; set; }
        bool DocumentExists(string id);

        // don't like this... should not be returning "all" documents.
        // should be treating a collection of Template and a collection of Script.
        IDocument[] Documents { get; }
        IDocument GetDocument(string id);

        IDocument AddDocument(DocumentTypeEnum documentType, string syntax = null, string extension = null);
        void RemoveDocument(string id);

        void MoveDocumentLeft(string id);
        void MoveDocumentRight(string id);
    }
}
