using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Documents.ImageSet;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.DocumentManager.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class ImageItem : IImageItem
    {
        public string FileName { get { return this.Id + "." + this.Extension; } }
        public string FilePath { get { return Path.Combine(this.FolderPath, this.FileName); } }
        public string FolderPath { get; private set; }
        public string UnsavedFileName { get { return "TEMP_" + this.Id + "." + this.Extension; } }
        public string Extension { get; private set; }
        public string Description { get; set; }
        private Guid identifyingGuid;
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public int Ordinal { get; set; }

        public string Id
        {
            get { return this.identifyingGuid.ToString(); }
            set
            {
                this.identifyingGuid = new Guid(value);
            }
        }

        public ImageItem(string folderPath)
        {
            this.identifyingGuid = Guid.NewGuid();
            this.DateCreated = DateTime.Now;
            this.DateModified = this.DateCreated;
            this.FolderPath = folderPath;
        }

        public ImageItem(string folderPath, string id, string extension, int ordinal, string description, DateTime dateCreated, DateTime dateModified)
        {
            this.Description = description;
            this.DateCreated = dateCreated;
            this.DateModified = dateModified;
            this.Extension = extension;
            this.identifyingGuid = new Guid(id);
            this.FolderPath = folderPath;
        }
    }

    public class ImageSetDocument : BaseDocument, IImageSetDocument
    {
        public event EventHandler ImageAdded;
        public event EventHandler ImageRemoved;
        public event EventHandler ImageMovedUp;
        public event EventHandler ImageMovedDown;

        private ImageSetIndexPathGenerator indexPathGenerator;
        private IDocumentIndexRepository repository;
        private ImageSetIndex documentIndex;

        public int ImageCount { get { return this.documentIndex.DocumentFiles.Count(); } }

        public IImgDocument FirstImage
        {
            get { return this.documentIndex.FirstDocument as IImgDocument; }
        }


        internal ImageSetDocument(string folder, string title)
            : base(new DocumentPathGenerator(folder, "imgset"), title, null)
        {
            indexPathGenerator = new ImageSetIndexPathGenerator(folder, "imgset", this.Id);
            repository = new ImageSetIndexXmlRepository(indexPathGenerator);
            this.DocumentType = DocumentFactory.GetDocumentType(DocumentTypeEnum.ImageSet);
            this.documentIndex = new ImageSetIndex(repository, indexPathGenerator);
        }

        internal ImageSetDocument(string folder, string id, string title, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, "imgset", id), title, description, ordinal)
        {
            indexPathGenerator = new ImageSetIndexPathGenerator(folder, "imgset", this.Id);
            repository = new ImageSetIndexXmlRepository(indexPathGenerator);
            this.DocumentType = DocumentFactory.GetDocumentType(DocumentTypeEnum.ImageSet);
            this.documentIndex = new ImageSetIndex(repository, indexPathGenerator);
        }

        public bool IsLastImage(IImgDocument imageItem)
        {
            return this.documentIndex.LastDocument.Ordinal <= imageItem.Ordinal;
        }

        public IImgDocument NextImage(IImgDocument imageItem)
        {
            if (this.documentIndex.LastDocument.Ordinal > imageItem.Ordinal)
                return this.documentIndex.DocumentFiles.Where(img => img.Ordinal == imageItem.Ordinal + 1).OfType<IImgDocument>().SingleOrDefault();
            else
                return imageItem;
        }

        public bool IsFirstImage(IImgDocument imageItem)
        {
            return this.documentIndex.FirstDocument.Ordinal >= imageItem.Ordinal;
        }

        public IImgDocument PreviousImage(IImgDocument imageItem)
        {
            if (this.documentIndex.FirstDocument.Ordinal <= imageItem.Ordinal)
                return this.documentIndex.DocumentFiles.Where(img => img.Ordinal == imageItem.Ordinal - 1).OfType<IImgDocument>().SingleOrDefault();
            else
                return imageItem;
        }

        protected override IFileVersion NewVersion(DateTime timeStamp, string description)
        {
            return null;
        }

        protected override void OpenFile()
        {
            this.documentIndex.Open();
        }

        protected override void SaveFile()
        {
            this.documentIndex.Save();
        }


        public void Add(IImgDocument imageItem)
        {
            //imageItemList.Insert(imageItem);
            //if (ImageAdded != null)
            //    ImageAdded(this, new EventArgs());
        }

        public void Remove(IImgDocument urlItem)
        {
            //imageItemList.Remove(urlItem);
            //if (ImageRemoved != null)
            //    ImageRemoved(this, new EventArgs());
        }

        public bool CanMovePrevious(IImgDocument documentFile)
        {
            return this.documentIndex.CanMoveUp(documentFile);
        }

        public bool CanMoveNext(IImgDocument documentFile)
        {
            return this.documentIndex.CanMoveDown(documentFile);
        }

        public virtual void MovePrevious(IImgDocument documentFile)
        {
            this.documentIndex.MoveUp(documentFile);
            if (ImageMovedUp != null)
                ImageMovedUp(this, new EventArgs());
        }

        public virtual void MoveNext(IImgDocument documentFile)
        {
            this.documentIndex.MoveDown(documentFile);
            if (ImageMovedDown != null)
                ImageMovedDown(this, new EventArgs());
        }

    }
}
