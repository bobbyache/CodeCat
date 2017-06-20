using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.DocumentManager.TopicSections;
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

        public List<ITopicSection> LoadDocuments()
        {
            List<ITopicSection> topicSections = new List<ITopicSection>();

            XDocument indexDocument = XDocument.Load(this.filePath);
            foreach (XElement documentElement in indexDocument.Element("CodeGroup").Element("Documents").Elements())
            {
                ITopicSection templateDocument = null;

                string documentId = (string)documentElement.Attribute("Id");
                string documentTitle = (string)documentElement.Attribute("Title");
                string documentDesc = (string)documentElement.Attribute("Description");
                int documentOrdinal = int.Parse((string)documentElement.Attribute("Ordinal"));
                string documentType = documentElement.Attribute("DocType") != null ? (string)documentElement.Attribute("DocType") : "CODESNIPPET";
                string documentExt = documentElement.Attribute("Ext") != null ? (string)documentElement.Attribute("Ext") : null;
                string documentSyntax = documentElement.Attribute("Syntax") != null ? (string)documentElement.Attribute("Syntax") : null;

                templateDocument = TopicSectionFactory.Create(SectionTypes.GetDocumentType(documentType), this.folder, documentTitle, documentId, documentOrdinal, documentDesc, documentExt, documentSyntax);

                topicSections.Add(templateDocument);
            }

            return topicSections.OfType<ITopicSection>().ToList();
        }

        public void WriteDocuments(List<ITopicSection> topicSections)
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

        private void WriteFile(List<ITopicSection> topicSections)
        {
            XDocument indexDocument = XDocument.Load(this.filePath);
            XElement filesElement = indexDocument.Element("CodeGroup").Element("Documents");
            filesElement.RemoveNodes();

            foreach (ITopicSection topicSection in topicSections)
            {
                if (topicSection is ICodeTopicSection)
                {
                    ICodeTopicSection writableTopicSection = topicSection as ICodeTopicSection;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", writableTopicSection.Id),
                        new XAttribute("Title", writableTopicSection.Title),
                        new XAttribute("DocType", writableTopicSection.DocumentType),
                        new XAttribute("Description", writableTopicSection.Description == null ? "" : writableTopicSection.Description),
                        new XAttribute("Ext", writableTopicSection.FileExtension),
                        new XAttribute("Syntax", writableTopicSection.Syntax),
                        new XAttribute("Ordinal", writableTopicSection.Ordinal.ToString())
                        ));
                }
                else
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
            }

            indexDocument.Save(this.filePath);
        }
    }
}
