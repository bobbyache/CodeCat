﻿using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.CodeGroup
{
    public interface ICodeGroupDocumentGroup : IPersistableTarget
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
        ICodeDocument[] Documents { get; }
        ICodeDocument GetDocument(string id);

        ICodeDocument AddDocument(string syntax);
        void RemoveDocument(string id);

        void MoveDocumentLeft(string id);
        void MoveDocumentRight(string id);
    }
}
