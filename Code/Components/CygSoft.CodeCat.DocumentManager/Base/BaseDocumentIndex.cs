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
        private PositionableList<IDocument> documentFiles = new PositionableList<IDocument>();

        public IDocument[] DocumentFiles
        {
            get { return documentFiles.ItemsList.ToArray(); }
        }

        protected abstract List<IDocument> LoadDocumentFiles();
        protected virtual void LoadNonDocumentFiles() { }

        public BaseDocumentIndex(string id, string fileExtension) : base(fileExtension)
        {
            this.Id = id;
        }

        protected override void OpenFile()
        {
            try
            {
                this.OpenDocumentFiles();
                this.LoadNonDocumentFiles();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        protected override void SaveFile()
        {
            SaveDocumentFiles();
        }

        // IDocumentFile could be of a different type, so it needs to be created
        // elsewhere such as a IDocumentFile factory.
        public IDocument AddDocumentFile(IDocument documentFile)
        {
            try
            {
                documentFile.Create(Path.Combine(this.Folder, documentFile.Id + "." + documentFile.FileExtension));
                this.documentFiles.Insert(documentFile);
                return documentFile;
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
                IDocument documentFile = this.documentFiles.ItemsList.Where(f => f.Id == id).SingleOrDefault();
                documentFile.Delete();
                documentFiles.Remove(documentFile);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        protected override void DeleteFile()
        {
            DeleteDocumentFiles();
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

        public void MoveDown(IDocument documentFile)
        {
            documentFiles.MoveDown(documentFile);
        }

        public void MoveTo(IDocument documentFile, int ordinal)
        {
            documentFiles.MoveTo(documentFile, ordinal);
        }

        public void MoveUp(IDocument documentFile)
        {
            documentFiles.MoveUp(documentFile);
        }


        private void OpenDocumentFiles()
        {
            try
            {
                List<IDocument> docFiles = LoadDocumentFiles();

                foreach (IDocument documentFile in docFiles)
                    documentFile.Open(Path.Combine(this.Folder, documentFile.Id + "." + documentFile.FileExtension));

                this.documentFiles.InitializeList(docFiles);

                this.LoadNonDocumentFiles();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SaveDocumentFiles()
        {
            try
            {
                foreach (IDocument documentFile in this.DocumentFiles)
                    documentFile.Save();
            }
            catch (Exception exception)
            {
                throw exception;
            }
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
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

    }
}
