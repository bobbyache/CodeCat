using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager
{
    public abstract class BaseDocManager : IDocManager, IPositionableDocManager
    {
        private PositionableList<IDocumentFile> documentFiles = new PositionableList<IDocumentFile>();

        public string Id { get; private set; }
        public string ParentFolder { get; private set; }
        public string IndexFilePath { get { return Path.Combine(this.Folder, "document_index.xml"); } }
        public string IndexFileTitle { get { return "document_index.xml"; } }
        public abstract bool IndexFileExists { get; }
        public bool Loaded { get; private set; }

        public string Folder
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ParentFolder))
                    return Path.Combine(this.ParentFolder, this.Id);
                return string.Empty;
            }
        }

        public IDocumentFile[] DocumentFiles
        {
            get { return documentFiles.ItemsList.ToArray(); }
        }

        protected abstract void LoadIndexFile();
        protected abstract void CreateIndexFile();
        protected abstract void LoadDocumentFiles();
        protected abstract void RemoveDocumentFile();
        protected abstract void UpdateDocumentFiles();
        protected abstract void UpdateDocumentIndex();
        protected abstract void UpdateDocumentFile();
        protected abstract void CreateNewDocumentFile();
        protected abstract void RemoveDocumentFile(IDocumentFile documentFile);

        public void Create(string parentFolder, string id)
        {
            this.Id = id;
            this.ParentFolder = parentFolder;

            try
            {
                this.CreateIndexFile();
                this.Loaded = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Open(string parentFolder, string id)
        {
            this.Id = id;
            this.ParentFolder = parentFolder;

            try
            {
                this.LoadIndexFile();
                this.LoadDocumentFiles();
                this.Loaded = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Delete()
        {
            try
            {
                this.RemoveDocumentFile();
                this.Loaded = false;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SaveIndex()
        {
            try
            {
                this.UpdateDocumentIndex();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SaveDocumentFiles()
        {
            try
            {
                this.UpdateDocumentFiles();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        // need to do this because not all documents will have syntax etc.
        // some of them might be completely different files like image files.
        public void CreateDocumentFile(IDocumentFile documentFile)
        {
            try
            {
                this.documentFiles.Insert(documentFile);
                this.CreateNewDocumentFile();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public IDocumentFile GetDocumentFile(string id)
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

        public void DeleteDocumentFile(string id)
        {
            try
            {
                IDocumentFile documentFile = this.documentFiles.ItemsList.Where(f => f.Id == id).SingleOrDefault();
                this.RemoveDocumentFile(documentFile);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool CanMoveDown(IDocumentFile documentFile)
        {
            return documentFiles.CanMoveDown(documentFile);
        }

        public bool CanMoveTo(IDocumentFile documentFile, int ordinal)
        {
            return documentFiles.CanMoveTo(documentFile, ordinal);
        }

        public bool CanMoveUp(IDocumentFile documentFile)
        {
            return documentFiles.CanMoveUp(documentFile);
        }

        public void MoveDown(IDocumentFile documentFile)
        {
            documentFiles.MoveDown(documentFile);
        }

        public void MoveTo(IDocumentFile documentFile, int ordinal)
        {
            documentFiles.MoveTo(documentFile, ordinal);
        }

        public void MoveUp(IDocumentFile documentFile)
        {
            documentFiles.MoveUp(documentFile);
        }
    }
}
