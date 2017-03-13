using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Base;
using System;

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
        ITopicSection[] Documents { get; }
        ITopicSection GetDocument(string id);

        ITopicSection AddDocument(DocumentTypeEnum documentType, string syntax = null, string extension = null);
        void RemoveDocument(string id);

        void MoveDocumentLeft(string id);
        void MoveDocumentRight(string id);
    }
}
