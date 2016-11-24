using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Base
{
    public abstract class BaseDocumentIndex : BaseFile, IDocumentIndex
    {
        public event EventHandler<DocumentEventArgs> DocumentAdded;
        public event EventHandler<DocumentEventArgs> DocumentRemoved;
        public event EventHandler<DocumentEventArgs> DocumentMovedUp;
        public event EventHandler<DocumentEventArgs> DocumentMovedDown;

        protected PositionableList<IDocument> documentFiles = new PositionableList<IDocument>();
        private List<IDocument> removedDocumentFiles = new List<IDocument>();
        protected IDocumentIndexRepository indexRepository;

        public IDocument[] DocumentFiles
        {
            get { return documentFiles.ItemsList.ToArray(); }
        }

        public IDocument FirstDocument
        {
            get { return documentFiles.FirstItem; }
        }

        public IDocument LastDocument
        {
            get { return documentFiles.LastItem; }
        }

        protected abstract List<IDocument> LoadDocumentFiles();
        protected abstract void SaveDocumentIndex();

        public BaseDocumentIndex(IDocumentIndexRepository indexRepository, BaseFilePathGenerator filePathGenerator): base(filePathGenerator)
        {
            this.indexRepository = indexRepository;
        }

        protected override void OpenFile()
        {
            try
            {
                this.OpenDocumentFiles();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        protected override void SaveFile()
        {
            try
            {
                // save the document index (abstract, must be implemented).
                SaveDocumentIndex();
                // saves all the physical document files.
                SaveDocumentFiles();
                DeleteRemovedDocumentFiles();
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }

        public bool DocumentExists(string id)
        {
            return this.documentFiles.ItemsList.Exists(r => r.Id == id);

        }
        // IDocumentFile could be of a different type, so it needs to be created
        // elsewhere such as a IDocumentFile factory.
        public IDocument AddDocumentFile(IDocument documentFile)
        {
            try
            {
                this.documentFiles.Insert(documentFile);
                AfterAddDocumentFile();
                if (DocumentAdded != null)
                    DocumentAdded(this, new DocumentEventArgs(documentFile));

                return documentFile;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        protected virtual void AfterAddDocumentFile() { }

        public void RemoveDocumentFile(string id)
        {
            try
            {
                IDocument documentFile = this.documentFiles.ItemsList.Where(f => f.Id == id).SingleOrDefault();
                removedDocumentFiles.Add(documentFile);
                documentFiles.Remove(documentFile);
                documentFile.Ordinal = -1;

                if (DocumentRemoved != null)
                    DocumentRemoved(this, new DocumentEventArgs(documentFile));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        protected override void OnAfterDelete()
        {
            DeleteDocumentFiles();
            if (Directory.Exists(this.Folder))
                Directory.Delete(this.Folder);
        }

        protected override void OnAfterRevert()
        {
            this.removedDocumentFiles.Clear();
        }

        public IDocument GetDocumentFile(string id)
        {
            try
            {
                return this.documentFiles.ItemsList.Where(f => f.Id == id).SingleOrDefault();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool CanMoveDown(IDocument documentFile)
        {
            return documentFiles.CanMoveDown(documentFile);
        }

        public bool CanMoveTo(IDocument documentFile, int ordinal)
        {
            return documentFiles.CanMoveTo(documentFile, ordinal);
        }

        public bool CanMoveUp(IDocument documentFile)
        {
            return documentFiles.CanMoveUp(documentFile);
        }

        public virtual void MoveDown(IDocument documentFile)
        {
            documentFiles.MoveDown(documentFile);
            if (DocumentMovedDown != null)
                DocumentMovedDown(this, new DocumentEventArgs(documentFile));
        }

        public void MoveTo(IDocument documentFile, int ordinal)
        {
            documentFiles.MoveTo(documentFile, ordinal);
        }

        public virtual void MoveUp(IDocument documentFile)
        {
            documentFiles.MoveUp(documentFile);
            if (DocumentMovedUp != null)
                DocumentMovedUp(this, new DocumentEventArgs(documentFile));
        }

        public void MoveLast(IDocument documentFile)
        {
            documentFiles.MoveLast(documentFile);
        }

        public void MoveFirst(IDocument documentFile)
        {
            documentFiles.MoveFirst(documentFile);
        }

        private void OpenDocumentFiles()
        {
            try
            {
                List<IDocument> docFiles = LoadDocumentFiles();

                foreach (IDocument documentFile in docFiles)
                    documentFile.Open();
                    //documentFile.Open(Path.Combine(this.Folder, documentFile.Id + "." + documentFile.FileExtension));

                this.documentFiles.InitializeList(docFiles);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SaveDocumentFiles()
        {
            foreach (IDocument documentFile in this.DocumentFiles)
                documentFile.Save();
        }

        private void DeleteDocumentFiles()
        {
            try
            {
                foreach (IDocument documentFile in this.documentFiles.ItemsList)
                {
                    documentFile.Delete();
                }
                documentFiles.Clear();

                foreach (IDocument documentFile in this.removedDocumentFiles)
                {
                    documentFile.Delete();
                }
                removedDocumentFiles.Clear();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void DeleteRemovedDocumentFiles()
        {
            foreach (IDocument document in removedDocumentFiles)
            {
                document.Delete();
            }
            removedDocumentFiles.Clear();
        }
    }
}
