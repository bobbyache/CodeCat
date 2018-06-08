﻿using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Plugin.Infrastructure;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.Topics
{
    internal class TopicIndexXmlRepository : IDocumentIndexRepository
    {
        private readonly string filePath;
        private readonly string folder;

        public TopicIndexXmlRepository(DocumentIndexPathGenerator indexPathGenerator)
        {
            this.filePath = indexPathGenerator.FilePath;
            this.folder = Path.GetDirectoryName(indexPathGenerator.FilePath);
        }

        public List<IPluginControl> LoadDocuments()
        {
            List<IPluginControl> topicSections = new List<IPluginControl>();

            XDocument indexDocument = XDocument.Load(this.filePath);
            foreach (XElement documentElement in indexDocument.Element("CodeGroup").Element("Documents").Elements())
            {
                IPluginControl templateDocument = null;

                string documentId = (string)documentElement.Attribute("Id");
                string documentTitle = (string)documentElement.Attribute("Title");
                string documentDesc = (string)documentElement.Attribute("Description");
                int documentOrdinal = int.Parse((string)documentElement.Attribute("Ordinal"));
                string documentType = documentElement.Attribute("DocType") != null ? (string)documentElement.Attribute("DocType") : "CODESNIPPET";
                string documentExt = documentElement.Attribute("Ext") != null ? (string)documentElement.Attribute("Ext") : null;
                string documentSyntax = documentElement.Attribute("Syntax") != null ? (string)documentElement.Attribute("Syntax") : null;

                //templateDocument = TopicSectionFactory.Create(SectionTypes.GetDocumentType(documentType), this.folder, documentTitle, documentId, documentOrdinal, documentDesc, documentExt, documentSyntax);

                topicSections.Add(templateDocument);
            }

            return topicSections.OfType<IPluginControl>().ToList();
        }

        public void WriteDocuments(List<IPluginControl> topicSections)
        {
            if (!Directory.Exists(this.folder))
                CreateFile();
            WriteFile(topicSections);
        }

        private void CreateFile()
        {
            Directory.CreateDirectory(this.folder);

            XElement rootElement = new XElement("CodeGroup", new XElement("Documents"));
            XDocument xDocument = new XDocument(rootElement);
            xDocument.Save(this.filePath);
        }

        private void WriteFile(List<IPluginControl> topicSections)
        {
            XDocument indexDocument = XDocument.Load(this.filePath);
            XElement filesElement = indexDocument.Element("CodeGroup").Element("Documents");
            filesElement.RemoveNodes();

            foreach (IPluginControl topicSection in topicSections)
            {
                filesElement.Add(new XElement("Document",
                    new XAttribute("Id", topicSection.Id),
                    new XAttribute("Title", topicSection.Title),
                    new XAttribute("DocType", topicSection.DocumentType),
                    new XAttribute("Description", topicSection.Description == null ? "" : topicSection.Description),
                    new XAttribute("Ext", topicSection.FileExtension),
                    new XAttribute("Ordinal", topicSection.Ordinal.ToString())
                    ));
            }

            indexDocument.Save(this.filePath);
        }
    }
}
