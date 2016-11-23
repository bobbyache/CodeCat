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

        public ImageItem()
        {
            this.identifyingGuid = Guid.NewGuid();
            this.DateCreated = DateTime.Now;
            this.DateModified = this.DateCreated;
        }

        public ImageItem(string id, string extension, int ordinal, string description, DateTime dateCreated, DateTime dateModified)
        {
            this.Description = description;
            this.DateCreated = dateCreated;
            this.DateModified = dateModified;
            this.Extension = extension;
            this.identifyingGuid = new Guid(id);
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
                    (string)element.Attribute("Id"),
                    (string)element.Attribute("Extension"),
                    (int)element.Attribute("Ordinal"),
                    (string)element.Attribute("Description"),
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
                    new XAttribute("Description", item.Description),
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

        public bool CanMoveDown(IImageItem documentFile)
        {
            return imageItemList.CanMoveDown(documentFile);
        }

        public bool CanMoveUp(IImageItem documentFile)
        {
            return imageItemList.CanMoveUp(documentFile);
        }

        public virtual void MoveDown(IImageItem documentFile)
        {
            imageItemList.MoveDown(documentFile);
            if (ImageMovedDown != null)
                ImageMovedDown(this, new EventArgs());
        }

        public virtual void MoveUp(IImageItem documentFile)
        {
            imageItemList.MoveUp(documentFile);
            if (ImageMovedUp != null)
                ImageMovedUp(this, new EventArgs());
        }

    }
}
