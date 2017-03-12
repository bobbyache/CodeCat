using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.Qik.LanguageEngine.Infrastructure;
using System;

namespace CygSoft.CodeCat.Domain.Qik
{
    public interface IQikTemplateDocumentSet : IPersistableTarget
    {
        event EventHandler<DocumentEventArgs> DocumentAdded;
        event EventHandler<DocumentEventArgs> DocumentRemoved;
        event EventHandler<DocumentEventArgs> DocumentMovedLeft;
        event EventHandler<DocumentEventArgs> DocumentMovedRight;

        ICompiler Compiler { get; }

        string Text { get; set; }
        string Syntax { get; set; }
        bool TemplateExists(string id);

        // don't like this... should not be returning "all" documents.
        // should be treating a collection of Template and a collection of Script.
        ICodeDocument[] Documents { get; }

        ICodeDocument[] TemplateFiles { get; }

        ICodeDocument ScriptFile { get; }
        ICodeDocument GetTemplate(string id);

        ICodeDocument AddTemplate(string syntax);
        void RemoveTemplate(string id);

        void MoveDocumentLeft(string id);
        void MoveDocumentRight(string id);
    }
}
