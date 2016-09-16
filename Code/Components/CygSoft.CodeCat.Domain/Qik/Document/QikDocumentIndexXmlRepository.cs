using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.Qik.Document
{
    internal class QikDocumentIndexXmlRepository : IDocumentIndexRepository
    {
        private readonly string filePath;
        private readonly string folder;

        public QikDocumentIndexXmlRepository(DocumentIndexPathGenerator indexPathGenerator)
        {
            this.filePath = indexPathGenerator.FilePath;
            this.folder =  Path.GetDirectoryName(indexPathGenerator.FilePath);
        }

        public List<IDocument> LoadDocuments()
        {
            List<ICodeDocument> documents = new List<ICodeDocument>();

            XDocument indexDocument = XDocument.Load(this.filePath);
            foreach (XElement documentElement in indexDocument.Element("QikFile").Element("Documents").Elements())
            {
                string documentId = (string)documentElement.Attribute("Id");
                string documentTitle = (string)documentElement.Attribute("Title");
                string documentDesc = (string)documentElement.Attribute("Description");
                string documentExt = (string)documentElement.Attribute("Ext");
                string documentSyntax = (string)documentElement.Attribute("Syntax");
                int documentOrdinal = int.Parse((string)documentElement.Attribute("Ordinal"));

                if (documentExt == "qik")
                {
                    ICodeDocument scriptDocument = DocumentFactory.CreateQikScriptDocument(this.folder, documentId, documentTitle, documentExt,
                        documentSyntax, documentOrdinal, documentDesc);
                    documents.Add(scriptDocument);
                }
                else
                {
                    ICodeDocument templateDocument = DocumentFactory.CreateCodeDocument(this.folder, documentId, documentTitle, documentExt,
                        documentSyntax, documentOrdinal, documentDesc);
                    documents.Add(templateDocument);
                }
            }

            return documents.OfType<IDocument>().ToList();
        }

        public void WriteDocuments(List<IDocument> documents)
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

        private void WriteFile(List<IDocument> documents)
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
