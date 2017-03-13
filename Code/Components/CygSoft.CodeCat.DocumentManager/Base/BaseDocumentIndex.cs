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

        protected PositionableList<ITopicSection> topicSections = new PositionableList<ITopicSection>();
        private List<ITopicSection> removedDocumentFiles = new List<ITopicSection>();
        protected IDocumentIndexRepository indexRepository;

        public ITopicSection[] DocumentFiles
        {
            get { return topicSections.ItemsList.ToArray(); }
        }

        public ITopicSection FirstDocument
        {
            get { return topicSections.FirstItem; }
        }

        public ITopicSection LastDocument
        {
            get { return topicSections.LastItem; }
        }

        protected abstract List<ITopicSection> LoadDocumentFiles();
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
            return this.topicSections.ItemsList.Exists(r => r.Id == id);

        }

        // IDocumentFile could be of a different type, so it needs to be created
        // elsewhere such as a IDocumentFile factory.
        public ITopicSection AddDocumentFile(ITopicSection topicSection)
        {
            try
            {
                this.topicSections.Insert(topicSection);
                AfterAddDocumentFile();
                DocumentAdded?.Invoke(this, new DocumentEventArgs(topicSection));

                return topicSection;
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
                ITopicSection topicSection = this.topicSections.ItemsList.Where(f => f.Id == id).SingleOrDefault();
                removedDocumentFiles.Add(topicSection);
                topicSections.Remove(topicSection);
                topicSection.Ordinal = -1;

                DocumentRemoved?.Invoke(this, new DocumentEventArgs(topicSection));
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
            foreach (ITopicSection topicSection in this.DocumentFiles)
                topicSection.Revert();
        }

        protected virtual void OnAfterRevert()
        {
            this.removedDocumentFiles.Clear();
        }

        public ITopicSection GetDocumentFile(string id)
        {
            try
            {
                return this.topicSections.ItemsList.Where(f => f.Id == id).SingleOrDefault();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool CanMoveDown(ITopicSection topicSection)
        {
            return topicSections.CanMoveDown(topicSection);
        }

        public bool CanMoveTo(ITopicSection topicSection, int ordinal)
        {
            return topicSections.CanMoveTo(topicSection, ordinal);
        }

        public bool CanMoveUp(ITopicSection topicSection)
        {
            return topicSections.CanMoveUp(topicSection);
        }

        public virtual void MoveDown(ITopicSection topicSection)
        {
            topicSections.MoveDown(topicSection);
            DocumentMovedDown?.Invoke(this, new DocumentEventArgs(topicSection));
        }

        public void MoveTo(ITopicSection topicSection, int ordinal)
        {
            topicSections.MoveTo(topicSection, ordinal);
        }

        public virtual void MoveUp(ITopicSection topicSection)
        {
            topicSections.MoveUp(topicSection);
            DocumentMovedUp?.Invoke(this, new DocumentEventArgs(topicSection));
        }

        public void MoveLast(ITopicSection topicSection)
        {
            topicSections.MoveLast(topicSection);
        }

        public void MoveFirst(ITopicSection topicSection)
        {
            topicSections.MoveFirst(topicSection);
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
                List<ITopicSection> docFiles = LoadDocumentFiles();

                foreach (ITopicSection topicSection in docFiles)
                    topicSection.Open();

                this.topicSections.InitializeList(docFiles);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SaveDocumentFiles()
        {
            foreach (ITopicSection topicSection in this.DocumentFiles)
                topicSection.Save();
        }

        private void DeleteDocumentFiles()
        {
            try
            {
                foreach (ITopicSection topicSection in this.topicSections.ItemsList)
                {
                    topicSection.Delete();
                }
                topicSections.Clear();

                foreach (ITopicSection topicSection in this.removedDocumentFiles)
                {
                    topicSection.Delete();
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
            foreach (ITopicSection document in removedDocumentFiles)
            {
                document.Delete();
            }
            removedDocumentFiles.Clear();
        }
    }
}
