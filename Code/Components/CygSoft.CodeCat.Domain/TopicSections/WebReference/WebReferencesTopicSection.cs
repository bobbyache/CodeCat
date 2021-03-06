﻿using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.DocumentManager.TopicSections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.TopicSections.WebReference
{
    public class WebReferencesTopicSection : TopicSection, IWebReferencesTopicSection
    {
        public event EventHandler Paste;
        public event EventHandler PasteConflict;

        public WebReferencesTopicSection(string folder, string title)
            : base(new DocumentPathGenerator(folder, "urlgrp"), title, null)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.WebReferences);
        }

        public WebReferencesTopicSection(string folder, string id, string title, int ordinal, string description) : base(new DocumentPathGenerator(folder, "urlgrp", id), title, description, ordinal)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.WebReferences);
        }

        private List<IWebReference> webReferenceList = new List<IWebReference>();

        public IWebReference[] WebReferences { get { return webReferenceList.ToArray(); } }
        public string[] Categories { get { return this.WebReferences.Select(r => r.Category).Distinct().ToArray(); } }

        public IWebReference CreateWebReference()
        {
            IWebReference webReference = new WebReference();
            return webReference;
        }

        public IWebReference CreateWebReference(string url, string title, string description, string category = "Unknown")
        {
            return new WebReference()
            {
                Title = title,
                Url = url,
                Description = description,
                Category = category
            };
        }

        protected override void OnOpen()
        {
            XDocument xDocument = XDocument.Load(this.FilePath);
            IEnumerable<XElement> elements = xDocument.Element("UrlGroup").Elements("Urls").Elements();

            List<IWebReference> webReferences = ExtractFromXml(elements);
            webReferenceList = webReferences.OfType<IWebReference>().ToList();

            base.OnOpen();
        }

        private List<IWebReference> ExtractFromXml(IEnumerable<XElement> elements)
        {
            List<IWebReference> webReferences = new List<IWebReference>();

            foreach (XElement element in elements)
            {
                IWebReference item = new WebReference(
                    (string)element.Attribute("Id"),
                    (string)element.Attribute("Title"),
                    element.Attribute("Category") != null ? (string)element.Attribute("Category") : "Unknown",
                    (string)element.Attribute("Description"),
                    (string)element.Attribute("Url"),
                    DateTime.Parse((string)element.Attribute("Created")),
                    DateTime.Parse((string)element.Attribute("Modified"))
                );

                webReferences.Add(item);
            }
            return webReferences.ToList();
        }

        protected override void OnSave()
        {
            if (!File.Exists(FilePath))
                CreateFile();
            WriteFile(WebReferences);
        }

        private void CreateFile()
        {
            XElement rootElement = new XElement("UrlGroup", new XElement("Urls"));
            XDocument xDocument = new XDocument(rootElement);
            xDocument.Save(FilePath);
        }

        private void WriteFile(IWebReference[] webReferences)
        {
            XDocument indexDocument = XDocument.Load(FilePath);
            XElement element = indexDocument.Element("UrlGroup").Element("Urls");
            element.RemoveNodes();

            AppendToContainerElement(element, webReferences);
            indexDocument.Save(FilePath);
        }

        private void AppendToContainerElement(XElement containerElement, IWebReference[] webReferences)
        {
            foreach (IWebReference webReference in webReferences)
            {
                containerElement.Add(new XElement("UrlItem",
                    new XAttribute("Id", webReference.Id),
                    new XAttribute("Title", webReference.Title),
                    new XAttribute("Category", webReference.Category != null ? webReference.Category : "Unknown"),
                    new XAttribute("Description", webReference.Description),
                    new XAttribute("Url", webReference.Url),
                    new XAttribute("Created", webReference.DateCreated.ToString()),
                    new XAttribute("Modified", webReference.DateModified.ToString())
                ));
            }
        }

        public void Add(IWebReference webReference)
        {
            webReferenceList.Add(webReference);
        }

        public void Remove(IWebReference webReference)
        {
            webReferenceList.Remove(webReference);
        }

        public void Remove(IEnumerable<IWebReference> webReferences)
        {
            foreach (IWebReference webReference in webReferences)
                webReferenceList.Remove(webReference);
        }

        public string GetXml(string[] ids)
        {
            XDocument indexDocument = new XDocument();
            XElement element = new XElement("UrlCopy");
            indexDocument.Add(element);

            IWebReference[] webReferences = WebReferences.Join(ids, itm => itm.Id, id => id,
                (itm, id) => itm).ToArray();

            AppendToContainerElement(element, webReferences);

            return indexDocument.ToString();
        }

        public bool IsFullUrl(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }

        public bool IsValidWebReferenceXml(string xml)
        {
            bool isValid = true;

            try
            {
                XElement parentElement = XElement.Parse(xml);
                List<IWebReference> webReferences = ExtractFromXml(parentElement.Elements());
                isValid = webReferences.Count > 0;
            }
            catch (Exception)
            {
                isValid = false;
            }
            return isValid;
        }

        public void AddXml(string xml)
        {
            try
            {
                XElement parentElement = XElement.Parse(xml);
                List<IWebReference> webReferences = ExtractFromXml(parentElement.Elements());

                // check that none exist within the list already...
                var conflictCount = webReferences.Join(webReferenceList, nw => nw.Id, ex => ex.Id,
                    (nw, ex) => nw).Count();

                if (conflictCount == 0)
                {
                    foreach (IWebReference item in webReferences)
                        webReferenceList.Add(item);

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
