using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IDocumentIndex
    {
        event EventHandler<DocumentIndexEventArgs> BeforeDelete;
        event EventHandler<DocumentIndexEventArgs> AfterDelete;
        event EventHandler<DocumentIndexEventArgs> BeforeOpen;
        event EventHandler<DocumentIndexEventArgs> AfterOpen;
        event EventHandler<DocumentIndexEventArgs> BeforeSave;
        event EventHandler<DocumentIndexEventArgs> AfterSave;
        event EventHandler<DocumentIndexEventArgs> BeforeClose;
        event EventHandler<DocumentIndexEventArgs> AfterClose;
        event EventHandler<DocumentIndexEventArgs> BeforeRevert;
        event EventHandler<DocumentIndexEventArgs> AfterRevert;

        event EventHandler<DocumentEventArgs> DocumentAdded;
        event EventHandler<DocumentEventArgs> DocumentRemoved;
        event EventHandler<DocumentEventArgs> DocumentMovedUp;
        event EventHandler<DocumentEventArgs> DocumentMovedDown;

        string Id { get; }
        string FilePath { get; }
        string FileName { get; }
        string FileExtension { get; }
        string Folder { get; }
        bool FolderExists { get; }
        bool Exists { get; }
        bool Loaded { get; }

        void Open();
        void Delete();
        void Save();
        void Close();
        void Revert();

        // files within the document group
        IDocument[] DocumentFiles { get; }

        bool DocumentExists(string id);

        IDocument AddDocumentFile(IDocument documentFile); // necessary, because document files could be of different types...
                                                          // need to be created elsewhere like a document factory.
        void RemoveDocumentFile(string id);
        IDocument GetDocumentFile(string id);

        IDocument FirstDocument { get; }
        IDocument LastDocument { get; }

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
