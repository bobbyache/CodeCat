using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class UrlItem : IUrlItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public string HostName
        {
            get
            {
                if (Uri.IsWellFormedUriString(this.Url, UriKind.Absolute))
                {
                    return new Uri(this.Url).Host;
                }
                return string.Empty;
            }
        }

        private Guid identifyingGuid;

        public UrlItem()
        {
            this.identifyingGuid = Guid.NewGuid();
            this.DateCreated = DateTime.Now;
            this.DateModified = this.DateCreated;
        }

        public UrlItem(string id, string title, string category, string description, string url, DateTime dateCreated, DateTime dateModified)
        {
            this.Title = title;
            this.Url = url;
            this.Description = description;
            this.Category = category;
            this.DateCreated = dateCreated;
            this.DateModified = dateModified;
            this.identifyingGuid = new Guid(id);
        }

        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }

        public string Id
        {
            get { return this.identifyingGuid.ToString(); }
            set
            {
                this.identifyingGuid = new Guid(value);
            }
        }
    }

    public class UrlGroupDocument : BaseDocument, IUrlGroupDocument
    {
        internal UrlGroupDocument(string folder, string title)
            : base(new DocumentPathGenerator(folder, "urlgrp"), title, null)
        {
            this.DocumentType = DocumentFactory.GetDocumentType(DocumentTypeEnum.UrlGroup);
        }

        internal UrlGroupDocument(string folder, string id, string title, int ordinal, string description) : base(new DocumentPathGenerator(folder, "urlgrp", id), title, description, ordinal)
        {
            this.DocumentType = DocumentFactory.GetDocumentType(DocumentTypeEnum.UrlGroup);
        }

        private List<IUrlItem> urlItemList = new List<IUrlItem>();

        public IUrlItem[] Items 
        {
            get { return urlItemList.ToArray(); }
        }

        public string[] Categories
        {
            get { return this.Items.Select(r => r.Category).Distinct().ToArray(); }
        }

        protected override IFileVersion NewVersion(DateTime timeStamp, string description)
        {
            //return new TextDocumentVersion(new VersionPathGenerator(this.FilePath, timeStamp), description, this.Text);
            return null;
        }

        protected override void OpenFile()
        {
            List<IUrlItem> urlItems = new List<IUrlItem>();

            XDocument document = XDocument.Load(this.FilePath);

            foreach (XElement element in document.Element("UrlGroup").Elements("Urls").Elements())
            {
                IUrlItem item = new UrlItem(
                    (string)element.Attribute("Id"), 
                    (string)element.Attribute("Title"),
                    element.Attribute("Category") != null ? (string)element.Attribute("Category") : "Unknown", 
                    (string)element.Attribute("Description"), 
                    (string)element.Attribute("Url"), 
                    DateTime.Parse((string)element.Attribute("Created")), 
                    DateTime.Parse((string)element.Attribute("Modified"))
                );

                urlItems.Add(item);
            }

            this.urlItemList = urlItems.OfType<IUrlItem>().ToList();
        }

        protected override void SaveFile()
        {
            if (!File.Exists(this.FilePath))
                CreateFile();
            WriteFile(this.Items);
        }

        private void CreateFile()
        {
            XElement rootElement = new XElement("UrlGroup", new XElement("Urls"));
            XDocument document = new XDocument(rootElement);
            document.Save(this.FilePath);
        }

        private void WriteFile(IUrlItem[] items)
        {
            XDocument indexDocument = XDocument.Load(this.FilePath);
            XElement element = indexDocument.Element("UrlGroup").Element("Urls");
            element.RemoveNodes();

            foreach (IUrlItem item in items)
            {
                element.Add(new XElement("UrlItem",
                    new XAttribute("Id", item.Id),
                    new XAttribute("Title", item.Title),
                    new XAttribute("Category", item.Category != null ? item.Category : "Unknown"),
                    new XAttribute("Description", item.Description),
                    new XAttribute("Url", item.Url),
                    new XAttribute("Created", item.DateCreated.ToString()),
                    new XAttribute("Modified", item.DateModified.ToString())
                ));
            }

            indexDocument.Save(this.FilePath);
        }


        public void Add(IUrlItem urlItem)
        {
            urlItemList.Add(urlItem);
        }

        public void Remove(IUrlItem urlItem)
        {
            urlItemList.Remove(urlItem);
        }
    }
}
