﻿using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Documents.ImageSet;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Linq;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
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

        protected override void OnBeforeRevert()
        {
            this.documentIndex.Revert();
            base.OnBeforeRevert();
        }

        protected override void OnAfterDelete()
        {
            var imageFiles = this.documentIndex.DocumentFiles;
            foreach (var imageFile in imageFiles)
            {
                if (imageFile.Exists)
                    imageFile.Delete();
            }

            base.OnAfterDelete();
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

        public IImgDocument Add()
        {
            IImgDocument img = this.Add("Blank Image", "png");
            return img;
        }

        public IImgDocument Add(string description, string extension)
        {
            ImagePathGenerator imagePathGenerator = new ImagePathGenerator(this.Folder, extension);
            ImgDocument imgDocument = new ImgDocument(imagePathGenerator);
            imgDocument.Description = description;
            this.documentIndex.AddDocumentFile(imgDocument);

            ImageAdded?.Invoke(this, new EventArgs());

            return imgDocument;
        }


        public void Remove(IImgDocument imageItem)
        {
            this.documentIndex.RemoveDocumentFile(imageItem.Id);
            ImageRemoved?.Invoke(this, new EventArgs());
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
            ImageMovedUp?.Invoke(this, new EventArgs());
        }

        public virtual void MoveNext(IImgDocument documentFile)
        {
            this.documentIndex.MoveDown(documentFile);
            ImageMovedDown?.Invoke(this, new EventArgs());
        }

    }
}
