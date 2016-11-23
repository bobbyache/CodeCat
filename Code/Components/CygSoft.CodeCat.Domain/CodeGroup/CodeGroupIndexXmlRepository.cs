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

namespace CygSoft.CodeCat.Domain.CodeGroup
{
    internal class CodeGroupIndexXmlRepository : IDocumentIndexRepository
    {
        private readonly string filePath;
        private readonly string folder;

        public CodeGroupIndexXmlRepository(DocumentIndexPathGenerator indexPathGenerator)
        {
            this.filePath = indexPathGenerator.FilePath;
            this.folder = Path.GetDirectoryName(indexPathGenerator.FilePath);
        }

        public List<IDocument> LoadDocuments()
        {
            List<IDocument> documents = new List<IDocument>();

            XDocument indexDocument = XDocument.Load(this.filePath);
            foreach (XElement documentElement in indexDocument.Element("CodeGroup").Element("Documents").Elements())
            {
                IDocument templateDocument = null;

                string documentId = (string)documentElement.Attribute("Id");
                string documentTitle = (string)documentElement.Attribute("Title");
                string documentDesc = (string)documentElement.Attribute("Description");
                int documentOrdinal = int.Parse((string)documentElement.Attribute("Ordinal"));
                string documentType = documentElement.Attribute("DocType") != null ? (string)documentElement.Attribute("DocType") : "CODESNIPPET";
                string documentExt = documentElement.Attribute("Ext") != null ? (string)documentElement.Attribute("Ext") : null;
                string documentSyntax = documentElement.Attribute("Syntax") != null ? (string)documentElement.Attribute("Syntax") : null;

                templateDocument = DocumentFactory.Create(DocumentFactory.GetDocumentType(documentType), this.folder, documentTitle, documentId, documentOrdinal, documentDesc, documentExt, documentSyntax);

                documents.Add(templateDocument);
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

            XElement rootElement = new XElement("CodeGroup", new XElement("Documents"));
            XDocument document = new XDocument(rootElement);
            document.Save(this.filePath);
        }

        private void WriteFile(List<IDocument> documents)
        {
            XDocument indexDocument = XDocument.Load(this.filePath);
            XElement filesElement = indexDocument.Element("CodeGroup").Element("Documents");
            filesElement.RemoveNodes();

            foreach (IDocument docFile in documents)
            {
                if (docFile is ICodeDocument)
                {
                    ICodeDocument codeDoc = docFile as ICodeDocument;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", codeDoc.Id),
                        new XAttribute("Title", codeDoc.Title),
                        new XAttribute("DocType", codeDoc.DocumentType),
                        new XAttribute("Description", codeDoc.Description == null ? "" : codeDoc.Description),
                        new XAttribute("Ext", codeDoc.FileExtension),
                        new XAttribute("Syntax", codeDoc.Syntax),
                        new XAttribute("Ordinal", codeDoc.Ordinal.ToString())
                        ));
                }
                else if (docFile is IUrlGroupDocument)
                {
                    IUrlGroupDocument urlFile = docFile as IUrlGroupDocument;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", urlFile.Id),
                        new XAttribute("Title", urlFile.Title),
                        new XAttribute("DocType", urlFile.DocumentType),
                        new XAttribute("Description", urlFile.Description == null ? "" : urlFile.Description),
                        new XAttribute("Ext", urlFile.FileExtension),
                        new XAttribute("Ordinal", urlFile.Ordinal.ToString())
                        ));
                }
                else if (docFile is IPdfDocument)
                {
                    IPdfDocument urlFile = docFile as IPdfDocument;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", urlFile.Id),
                        new XAttribute("Title", urlFile.Title),
                        new XAttribute("DocType", urlFile.DocumentType),
                        new XAttribute("Description", urlFile.Description == null ? "" : urlFile.Description),
                        new XAttribute("Ext", urlFile.FileExtension),
                        new XAttribute("Ordinal", urlFile.Ordinal.ToString())
                        ));
                }
                else if (docFile is IImageDocument)
                {
                    IImageDocument urlFile = docFile as IImageDocument;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", urlFile.Id),
                        new XAttribute("Title", urlFile.Title),
                        new XAttribute("DocType", urlFile.DocumentType),
                        new XAttribute("Description", urlFile.Description == null ? "" : urlFile.Description),
                        new XAttribute("Ext", urlFile.FileExtension),
                        new XAttribute("Ordinal", urlFile.Ordinal.ToString())
                        ));
                }
                else if (docFile is IImageSetDocument)
                {
                    IImageSetDocument urlFile = docFile as IImageSetDocument;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", urlFile.Id),
                        new XAttribute("Title", urlFile.Title),
                        new XAttribute("DocType", urlFile.DocumentType),
                        new XAttribute("Description", urlFile.Description == null ? "" : urlFile.Description),
                        new XAttribute("Ext", urlFile.FileExtension),
                        new XAttribute("Ordinal", urlFile.Ordinal.ToString())
                        ));
                }
            }

            indexDocument.Save(this.filePath);
        }
    }
}
