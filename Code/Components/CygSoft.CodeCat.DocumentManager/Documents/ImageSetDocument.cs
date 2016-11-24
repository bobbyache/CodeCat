using CygSoft.CodeCat.DocumentManager.Base;
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

        private PositionableList<IImageItem> imageItemList = new PositionableList<IImageItem>();

        public IImageItem[] Images
        {
            get { return imageItemList.ItemsList.ToArray(); }
        }

        public IImageItem FirstImage
        {
            get { return imageItemList.FirstItem; }
        }


        public bool IsLastImage(IImageItem imageItem)
        {
            return imageItemList.LastItem.Ordinal <= imageItem.Ordinal;
        }

        public IImageItem NextImage(IImageItem imageItem)
        {
            if (imageItemList.LastItem.Ordinal > imageItem.Ordinal)
            {
                return imageItemList.ItemsList.Where(img => img.Ordinal == imageItem.Ordinal + 1).SingleOrDefault();
            }
            else
            {
                return imageItem;
            }
        }

        public bool IsFirstImage(IImageItem imageItem)
        {
            return imageItemList.FirstItem.Ordinal >= imageItem.Ordinal;
        }

        public IImageItem PreviousImage(IImageItem imageItem)
        {
            if (imageItemList.FirstItem.Ordinal <= imageItem.Ordinal)
            {
                return imageItemList.ItemsList.Where(img => img.Ordinal == imageItem.Ordinal - 1).SingleOrDefault();
            }
            else
            {
                return imageItem;
            }
        }

        internal ImageSetDocument(string folder, string title)
            : base(new DocumentPathGenerator(folder, "imgset"), title, null)
        {
            this.DocumentType = DocumentFactory.GetDocumentType(DocumentTypeEnum.ImageSet);
        }

        internal ImageSetDocument(string folder, string id, string title, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, "imgset", id), title, description, ordinal)
        {
            this.DocumentType = DocumentFactory.GetDocumentType(DocumentTypeEnum.ImageSet);
        }

        protected override IFileVersion NewVersion(DateTime timeStamp, string description)
        {
            return null;
        }

        protected override void OpenFile()
        {
            List<IImageItem> imageItems = new List<IImageItem>();

            XDocument document = XDocument.Load(this.FilePath);

            foreach (XElement element in document.Element("ImageSet").Elements("Images").Elements())
            {
                IImageItem item = new ImageItem(
                    this.Folder,
                    (string)element.Attribute("Id"),
                    (string)element.Attribute("Extension"),
                    (int)element.Attribute("Ordinal"),
                    (string)element.Element("Description"),
                    DateTime.Parse((string)element.Attribute("Created")),
                    DateTime.Parse((string)element.Attribute("Modified"))
                );

                imageItems.Add(item);
            }

            this.imageItemList.InitializeList(imageItems.OfType<IImageItem>().ToList());
        }

        private void CreateFile()
        {
            XElement rootElement = new XElement("ImageSet", new XElement("Images"));
            XDocument document = new XDocument(rootElement);
            document.Save(this.FilePath);
        }

        protected override void SaveFile()
        {
            if (!File.Exists(this.FilePath))
                CreateFile();
            WriteFile(this.Images);
        }

        private void WriteFile(IImageItem[] items)
        {
            XDocument indexDocument = XDocument.Load(this.FilePath);
            XElement element = indexDocument.Element("ImageSet").Element("Images");
            element.RemoveNodes();

            foreach (IImageItem item in items)
            {
                element.Add(new XElement("Image",
                    new XAttribute("Id", item.Id),
                    new XAttribute("Extension", item.Extension),
                    new XAttribute("Ordinal", item.Ordinal),
                    new XElement("Description", new XCData(item.Description)),
                    new XAttribute("Created", item.DateCreated.ToString()),
                    new XAttribute("Modified", item.DateModified.ToString())
                ));
            }

            indexDocument.Save(this.FilePath);
        }

        public void Add(IImageItem imageItem)
        {
            imageItemList.Insert(imageItem);
            if (ImageAdded != null)
                ImageAdded(this, new EventArgs());
        }

        public void Remove(IImageItem urlItem)
        {
            imageItemList.Remove(urlItem);
            if (ImageRemoved != null)
                ImageRemoved(this, new EventArgs());
        }

        public bool CanMovePrevious(IImageItem documentFile)
        {
            return imageItemList.CanMoveUp(documentFile);
        }

        public bool CanMoveNext(IImageItem documentFile)
        {
            return imageItemList.CanMoveDown(documentFile);
        }

        public virtual void MovePrevious(IImageItem documentFile)
        {
            imageItemList.MoveUp(documentFile);
            if (ImageMovedUp != null)
                ImageMovedUp(this, new EventArgs());
        }

        public virtual void MoveNext(IImageItem documentFile)
        {
            imageItemList.MoveDown(documentFile);
            if (ImageMovedDown != null)
                ImageMovedDown(this, new EventArgs());
        }

    }
}
