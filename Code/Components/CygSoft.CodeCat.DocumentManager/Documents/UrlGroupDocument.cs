using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                    return new Uri(this.Url).Host;
                
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
            set { this.identifyingGuid = new Guid(value); }
        }
    }

    public class UrlGroupDocument : TopicSection, IUrlGroupDocument
    {
        public event EventHandler Paste;
        public event EventHandler PasteConflict;

        internal UrlGroupDocument(string folder, string title)
            : base(new DocumentPathGenerator(folder, "urlgrp"), title, null)
        {
            this.DocumentType = DocumentFactory.GetDocumentType(TopicSectionType.UrlGroup);
        }

        internal UrlGroupDocument(string folder, string id, string title, int ordinal, string description) : base(new DocumentPathGenerator(folder, "urlgrp", id), title, description, ordinal)
        {
            this.DocumentType = DocumentFactory.GetDocumentType(TopicSectionType.UrlGroup);
        }

        private List<IUrlItem> urlItemList = new List<IUrlItem>();

        public IUrlItem[] Items { get { return urlItemList.ToArray(); } }
        public string[] Categories { get { return this.Items.Select(r => r.Category).Distinct().ToArray(); } }

        public IUrlItem CreateNewUrl()
        {
            IUrlItem item = new UrlItem();
            return item;
        }

        //protected override IFileVersion NewVersion(DateTime timeStamp, string description)
        //{
        //    return null;
        //}

        protected override void OpenFile()
        {
            XDocument document = XDocument.Load(this.FilePath);
            IEnumerable<XElement> elements = document.Element("UrlGroup").Elements("Urls").Elements();

            List<IUrlItem> urlItems = ExtractFromXml(elements);
            this.urlItemList = urlItems.OfType<IUrlItem>().ToList();
        }

        private List<IUrlItem> ExtractFromXml(IEnumerable<XElement> elements)
        {
            List<IUrlItem> urlItems = new List<IUrlItem>();

            foreach (XElement element in elements)
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
            return urlItems.ToList();
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

            AppendToContainerElement(element, items);
            indexDocument.Save(this.FilePath);
        }

        private void AppendToContainerElement(XElement containerElement, IUrlItem[] items)
        {
            foreach (IUrlItem item in items)
            {
                containerElement.Add(new XElement("UrlItem",
                    new XAttribute("Id", item.Id),
                    new XAttribute("Title", item.Title),
                    new XAttribute("Category", item.Category != null ? item.Category : "Unknown"),
                    new XAttribute("Description", item.Description),
                    new XAttribute("Url", item.Url),
                    new XAttribute("Created", item.DateCreated.ToString()),
                    new XAttribute("Modified", item.DateModified.ToString())
                ));
            }
        }

        public void Add(IUrlItem urlItem)
        {
            urlItemList.Add(urlItem);
        }

        public void Remove(IUrlItem urlItem)
        {
            urlItemList.Remove(urlItem);
        }

        public string CopyXmlFor(string[] ids)
        {
            XDocument indexDocument = new XDocument();
            XElement element = new XElement("UrlCopy");
            indexDocument.Add(element);

            IUrlItem[] selection = Items.Join(ids, itm => itm.Id, id => id,
                (itm, id) => itm).ToArray();

            AppendToContainerElement(element, selection);

            return indexDocument.ToString();
        }

        public void PasteXml(string xml)
        {
            try
            {
                XElement parentElement = XElement.Parse(xml);
                List<IUrlItem> urlItems = ExtractFromXml(parentElement.Elements());

                // check that none exist within the list already...
                var conflictCount = urlItems.Join(urlItemList, nw => nw.Id, ex => ex.Id,
                    (nw, ex) => nw).Count();

                if (conflictCount == 0)
                {
                    foreach (IUrlItem item in urlItems)
                        this.urlItemList.Add(item);

                    Paste?.Invoke(this, new EventArgs());
                }
                else
                {
                    PasteConflict?.Invoke(this, new EventArgs());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
