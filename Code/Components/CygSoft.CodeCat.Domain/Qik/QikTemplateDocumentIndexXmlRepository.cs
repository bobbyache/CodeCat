using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.Qik
{
    internal class QikTemplateDocumentIndexXmlRepository : IDocumentIndexRepository
    {
        private readonly string filePath;
        private readonly string folder;

        public QikTemplateDocumentIndexXmlRepository(DocumentIndexPathGenerator indexPathGenerator)
        {
            this.filePath = indexPathGenerator.FilePath;
            this.folder =  Path.GetDirectoryName(indexPathGenerator.FilePath);
        }

        public List<ITopicSection> LoadDocuments()
        {
            List<ITopicSection> topicSections = new List<ITopicSection>();

            XDocument indexDocument = XDocument.Load(this.filePath);
            foreach (XElement documentElement in indexDocument.Element("QikFile").Element("Documents").Elements())
            {
                string documentId = (string)documentElement.Attribute("Id");
                string documentTitle = (string)documentElement.Attribute("Title");
                string documentDesc = (string)documentElement.Attribute("Description");
                string documentExt = (string)documentElement.Attribute("Ext");
                string documentSyntax = (string)documentElement.Attribute("Syntax");
                int documentOrdinal = int.Parse((string)documentElement.Attribute("Ordinal"));

                string documentType = null;

                if (documentExt == "qik")
                    documentType = documentElement.Attribute("DocType") != null ? (string)documentElement.Attribute("DocType") : "QIKSCRIPT";
                else
                    documentType = documentElement.Attribute("DocType") != null ? (string)documentElement.Attribute("DocType") : "CODESNIPPET";

                ITopicSection scriptDocument = DocumentFactory.Create(DocumentFactory.GetDocumentType(documentType), this.folder, documentTitle, documentId, documentOrdinal, documentDesc, documentExt, documentSyntax);
                topicSections.Add(scriptDocument);
            }

            return topicSections.OfType<ITopicSection>().ToList();
        }

        public void WriteDocuments(List<ITopicSection> documents)
        {
            if (!Directory.Exists(this.folder))
                CreateFile();
            WriteFile(documents);
        }

        private void CreateFile()
        {
            Directory.CreateDirectory(this.folder);

            XElement rootElement = new XElement("QikFile", new XElement("Documents"));
            XDocument document = new XDocument(rootElement);
            document.Save(this.filePath);
        }

        private void WriteFile(List<ITopicSection> documents)
        {
            ICodeDocument[] docFiles = documents.OfType<ICodeDocument>().ToArray();

            XDocument indexDocument = XDocument.Load(this.filePath);
            XElement filesElement = indexDocument.Element("QikFile").Element("Documents");
            filesElement.RemoveNodes();

            foreach (ICodeDocument docFile in docFiles)
            {
                filesElement.Add(new XElement("Document",
                    new XAttribute("Id", docFile.Id),
                    new XAttribute("Title", docFile.Title),
                    new XAttribute("DocType", docFile.DocumentType),
                    new XAttribute("Description", docFile.Description == null ? "" : docFile.Description),
                    new XAttribute("Ext", docFile.FileExtension),
                    new XAttribute("Syntax", docFile.Syntax),
                    new XAttribute("Ordinal", docFile.Ordinal.ToString())
                    ));
            }

            indexDocument.Save(this.filePath);
        }
    }
}
