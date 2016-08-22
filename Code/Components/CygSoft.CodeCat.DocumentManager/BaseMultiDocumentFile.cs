using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager
{
    public abstract class BaseMultiDocumentFile : BaseFile, IMultiDocumentFile
    {
        private PositionableList<IDocumentFile> documentFiles = new PositionableList<IDocumentFile>();

        public IDocumentFile[] DocumentFiles
        {
            get { return documentFiles.ItemsList.ToArray(); }
        }

        protected abstract List<IDocumentFile> LoadDocumentFiles();

        public BaseMultiDocumentFile(string id, string fileExtension) : base(fileExtension)
        {
            this.Id = id;
        }

        protected override void OpenFile()
        {
            try
            {
                List<IDocumentFile> docFiles = LoadDocumentFiles();

                foreach (IDocumentFile documentFile in docFiles)
                    documentFile.Open(Path.Combine(this.Folder, documentFile.Id + "." + documentFile.FileExtension));

                this.documentFiles.InitializeList(docFiles);
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
                foreach (IDocumentFile documentFile in this.DocumentFiles)
                    documentFile.Save();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        // IDocumentFile could be of a different type, so it needs to be created
        // elsewhere such as a IDocumentFile factory.
        public IDocumentFile AddDocumentFile(IDocumentFile documentFile)
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
                IDocumentFile documentFile = this.documentFiles.ItemsList.Where(f => f.Id == id).SingleOrDefault();
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
            List<IDocumentFile> docs = documentFiles.ItemsList;
            foreach (IDocumentFile doc in docs)
            {
                doc.Delete();
            }
            documentFiles.Clear();
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
