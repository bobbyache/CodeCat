using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

                templateDocument = DocumentFactory.Create(DocumentFactory.GetDocumentType(documentType), this.folder, documentTitle, documentId, documentOrdinal, documentDesc, documentExt, documentSyntax);

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
            XDocument document = new XDocument(rootElement);
            document.Save(this.filePath);
        }

        private void WriteFile(List<ITopicSection> documents)
        {
            XDocument indexDocument = XDocument.Load(this.filePath);
            XElement filesElement = indexDocument.Element("CodeGroup").Element("Documents");
            filesElement.RemoveNodes();

            foreach (ITopicSection docFile in documents)
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
                    IPdfDocument pdfDoc = docFile as IPdfDocument;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", pdfDoc.Id),
                        new XAttribute("Title", pdfDoc.Title),
                        new XAttribute("DocType", pdfDoc.DocumentType),
                        new XAttribute("Description", pdfDoc.Description == null ? "" : pdfDoc.Description),
                        new XAttribute("Ext", pdfDoc.FileExtension),
                        new XAttribute("Ordinal", pdfDoc.Ordinal.ToString())
                        ));
                }
                else if (docFile is IImageDocument)
                {
                    IImageDocument imgFile = docFile as IImageDocument;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", imgFile.Id),
                        new XAttribute("Title", imgFile.Title),
                        new XAttribute("DocType", imgFile.DocumentType),
                        new XAttribute("Description", imgFile.Description == null ? "" : imgFile.Description),
                        new XAttribute("Ext", imgFile.FileExtension),
                        new XAttribute("Ordinal", imgFile.Ordinal.ToString())
                        ));
                }
                else if (docFile is IImageSetDocument)
                {
                    IImageSetDocument imgSetFile = docFile as IImageSetDocument;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", imgSetFile.Id),
                        new XAttribute("Title", imgSetFile.Title),
                        new XAttribute("DocType", imgSetFile.DocumentType),
                        new XAttribute("Description", imgSetFile.Description == null ? "" : imgSetFile.Description),
                        new XAttribute("Ext", imgSetFile.FileExtension),
                        new XAttribute("Ordinal", imgSetFile.Ordinal.ToString())
                        ));
                }
                else if (docFile is IRichTextDocument)
                {
                    IRichTextDocument rtfFile = docFile as IRichTextDocument;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", rtfFile.Id),
                        new XAttribute("Title", rtfFile.Title),
                        new XAttribute("DocType", rtfFile.DocumentType),
                        new XAttribute("Description", rtfFile.Description == null ? "" : rtfFile.Description),
                        new XAttribute("Ext", rtfFile.FileExtension),
                        new XAttribute("Ordinal", rtfFile.Ordinal.ToString())
                        ));
                }
                else if (docFile is IFileGroupDocument)
                {
                    IFileGroupDocument fileGrpFile = docFile as IFileGroupDocument;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", fileGrpFile.Id),
                        new XAttribute("Title", fileGrpFile.Title),
                        new XAttribute("DocType", fileGrpFile.DocumentType),
                        new XAttribute("Description", fileGrpFile.Description == null ? "" : fileGrpFile.Description),
                        new XAttribute("Ext", fileGrpFile.FileExtension),
                        new XAttribute("Ordinal", fileGrpFile.Ordinal.ToString())
                        ));
                }
            }

            indexDocument.Save(this.filePath);
        }
    }
}
