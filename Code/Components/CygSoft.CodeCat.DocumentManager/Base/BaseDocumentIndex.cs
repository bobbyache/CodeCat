using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CygSoft.CodeCat.DocumentManager.Base
{
    public abstract class BaseDocumentIndex : IDocumentIndex
    {
        public event EventHandler<DocumentEventArgs> DocumentAdded;
        public event EventHandler<DocumentEventArgs> DocumentRemoved;
        public event EventHandler<DocumentEventArgs> DocumentMovedUp;
        public event EventHandler<DocumentEventArgs> DocumentMovedDown;

        public event EventHandler<DocumentIndexEventArgs> BeforeDelete;
        public event EventHandler<DocumentIndexEventArgs> AfterDelete;
        public event EventHandler<DocumentIndexEventArgs> BeforeOpen;
        public event EventHandler<DocumentIndexEventArgs> AfterOpen;
        public event EventHandler<DocumentIndexEventArgs> BeforeSave;
        public event EventHandler<DocumentIndexEventArgs> AfterSave;
        public event EventHandler<DocumentIndexEventArgs> BeforeClose;
        public event EventHandler<DocumentIndexEventArgs> AfterClose;
        public event EventHandler<DocumentIndexEventArgs> BeforeRevert;
        public event EventHandler<DocumentIndexEventArgs> AfterRevert;

        public string Id { get; private set; }
        public string FilePath { get; protected set; }
        public string FileName { get; private set; }
        public string FileExtension { get; private set; }
        protected BaseFilePathGenerator filePathGenerator;

        public virtual string Folder
        {
            get { return Path.GetDirectoryName(this.FilePath); }
        }

        public virtual bool FolderExists
        {
            get { return Directory.Exists(Path.GetDirectoryName(this.FilePath)); }
        }

        public bool Exists { get { return File.Exists(this.FilePath); } }
        public bool Loaded { get; private set; }

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

        public BaseDocumentIndex(IDocumentIndexRepository indexRepository, BaseFilePathGenerator filePathGenerator)
        {
            this.indexRepository = indexRepository;
            this.filePathGenerator = filePathGenerator;
            this.Id = filePathGenerator.Id;
            this.FileExtension = filePathGenerator.FileExtension;
            this.FilePath = filePathGenerator.FilePath;
            this.FileName = filePathGenerator.FileName;
        }

        protected virtual void OnBeforeDelete() { }
        protected virtual void OnBeforeOpen() { }
        protected virtual void OnAfterOpen() { }
        protected virtual void OnBeforeSave() { }
        protected virtual void OnAfterSave() { }
        protected virtual void OnClose() { }

        protected virtual void OpenFile()
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

        protected virtual void SaveFile()
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
                DocumentAdded?.Invoke(this, new DocumentEventArgs(documentFile));

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

                DocumentRemoved?.Invoke(this, new DocumentEventArgs(documentFile));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        protected virtual void OnAfterDelete()
        {
            DeleteDocumentFiles();
            if (Directory.Exists(this.Folder))
                Directory.Delete(this.Folder);
        }

        protected virtual void OnBeforeRevert()
        {
            foreach (IDocument documentFile in this.DocumentFiles)
                documentFile.Revert();
        }

        protected virtual void OnAfterRevert()
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
            DocumentMovedDown?.Invoke(this, new DocumentEventArgs(documentFile));
        }

        public void MoveTo(IDocument documentFile, int ordinal)
        {
            documentFiles.MoveTo(documentFile, ordinal);
        }

        public virtual void MoveUp(IDocument documentFile)
        {
            documentFiles.MoveUp(documentFile);
            DocumentMovedUp?.Invoke(this, new DocumentEventArgs(documentFile));
        }

        public void MoveLast(IDocument documentFile)
        {
            documentFiles.MoveLast(documentFile);
        }

        public void MoveFirst(IDocument documentFile)
        {
            documentFiles.MoveFirst(documentFile);
        }

        public void Open()
        {
            try
            {
                BeforeOpen?.Invoke(this, new DocumentIndexEventArgs(this));
                OnBeforeOpen();
                OpenFile();
                OnAfterOpen();

                this.Loaded = true;
                AfterOpen?.Invoke(this, new DocumentIndexEventArgs(this));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Revert()
        {
            try
            {
                BeforeRevert?.Invoke(this, new DocumentIndexEventArgs(this));
                OnBeforeRevert();

                if (File.Exists(this.FilePath))
                    OpenFile();
                OnAfterRevert();

                this.Loaded = true;
                AfterRevert?.Invoke(this, new DocumentIndexEventArgs(this));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Delete()
        {
            BeforeDelete?.Invoke(this, new DocumentIndexEventArgs(this));
            OnBeforeDelete();

            if (File.Exists(this.FilePath))
                File.Delete(this.FilePath);

            OnAfterDelete();

            this.Loaded = false;
            AfterDelete?.Invoke(this, new DocumentIndexEventArgs(this));
        }

        public void Save()
        {
            try
            {
                BeforeSave?.Invoke(this, new DocumentIndexEventArgs(this));
                OnBeforeSave();
                SaveFile();
                OnAfterSave();
                this.Loaded = true;
                AfterSave?.Invoke(this, new DocumentIndexEventArgs(this));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void Close()
        {
            BeforeClose?.Invoke(this, new DocumentIndexEventArgs(this));
            this.OnClose();
            this.Loaded = false;
            AfterClose?.Invoke(this, new DocumentIndexEventArgs(this));
        }

        private void OpenDocumentFiles()
        {
            try
            {
                List<IDocument> docFiles = LoadDocumentFiles();

                foreach (IDocument documentFile in docFiles)
                    documentFile.Open();

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
