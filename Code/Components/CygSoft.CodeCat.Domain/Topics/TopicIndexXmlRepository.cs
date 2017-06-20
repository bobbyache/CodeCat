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

            foreach (ITopicSection imagePagerTopicSection in topicSections)
            {
                if (imagePagerTopicSection is ICodeTopicSection)
                {
                    ICodeTopicSection codeTopicSection = imagePagerTopicSection as ICodeTopicSection;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", codeTopicSection.Id),
                        new XAttribute("Title", codeTopicSection.Title),
                        new XAttribute("DocType", codeTopicSection.DocumentType),
                        new XAttribute("Description", codeTopicSection.Description == null ? "" : codeTopicSection.Description),
                        new XAttribute("Ext", codeTopicSection.FileExtension),
                        new XAttribute("Syntax", codeTopicSection.Syntax),
                        new XAttribute("Ordinal", codeTopicSection.Ordinal.ToString())
                        ));
                }
                else if (imagePagerTopicSection is IWebReferencesTopicSection)
                {
                    IWebReferencesTopicSection webReferencesTopicSection = imagePagerTopicSection as IWebReferencesTopicSection;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", webReferencesTopicSection.Id),
                        new XAttribute("Title", webReferencesTopicSection.Title),
                        new XAttribute("DocType", webReferencesTopicSection.DocumentType),
                        new XAttribute("Description", webReferencesTopicSection.Description == null ? "" : webReferencesTopicSection.Description),
                        new XAttribute("Ext", webReferencesTopicSection.FileExtension),
                        new XAttribute("Ordinal", webReferencesTopicSection.Ordinal.ToString())
                        ));
                }
                else if (imagePagerTopicSection is IPdfViewerTopicSection)
                {
                    IPdfViewerTopicSection pdfViewerTopicSection = imagePagerTopicSection as IPdfViewerTopicSection;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", pdfViewerTopicSection.Id),
                        new XAttribute("Title", pdfViewerTopicSection.Title),
                        new XAttribute("DocType", pdfViewerTopicSection.DocumentType),
                        new XAttribute("Description", pdfViewerTopicSection.Description == null ? "" : pdfViewerTopicSection.Description),
                        new XAttribute("Ext", pdfViewerTopicSection.FileExtension),
                        new XAttribute("Ordinal", pdfViewerTopicSection.Ordinal.ToString())
                        ));
                }
                else if (imagePagerTopicSection is ISingleImageTopicSection)
                {
                    ISingleImageTopicSection singleImageTopicSection = imagePagerTopicSection as ISingleImageTopicSection;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", singleImageTopicSection.Id),
                        new XAttribute("Title", singleImageTopicSection.Title),
                        new XAttribute("DocType", singleImageTopicSection.DocumentType),
                        new XAttribute("Description", singleImageTopicSection.Description == null ? "" : singleImageTopicSection.Description),
                        new XAttribute("Ext", singleImageTopicSection.FileExtension),
                        new XAttribute("Ordinal", singleImageTopicSection.Ordinal.ToString())
                        ));
                }
                else if (imagePagerTopicSection is IImagePagerTopicSection)
                {
                    IImagePagerTopicSection imgSetFile = imagePagerTopicSection as IImagePagerTopicSection;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", imgSetFile.Id),
                        new XAttribute("Title", imgSetFile.Title),
                        new XAttribute("DocType", imgSetFile.DocumentType),
                        new XAttribute("Description", imgSetFile.Description == null ? "" : imgSetFile.Description),
                        new XAttribute("Ext", imgSetFile.FileExtension),
                        new XAttribute("Ordinal", imgSetFile.Ordinal.ToString())
                        ));
                }
                else if (imagePagerTopicSection is IRichTextEditorTopicSection)
                {
                    IRichTextEditorTopicSection richTextEditorTopicSection = imagePagerTopicSection as IRichTextEditorTopicSection;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", richTextEditorTopicSection.Id),
                        new XAttribute("Title", richTextEditorTopicSection.Title),
                        new XAttribute("DocType", richTextEditorTopicSection.DocumentType),
                        new XAttribute("Description", richTextEditorTopicSection.Description == null ? "" : richTextEditorTopicSection.Description),
                        new XAttribute("Ext", richTextEditorTopicSection.FileExtension),
                        new XAttribute("Ordinal", richTextEditorTopicSection.Ordinal.ToString())
                        ));
                }
                else if (imagePagerTopicSection is IFileAttachmentsTopicSection)
                {
                    IFileAttachmentsTopicSection fileAttachmentsTopicSection = imagePagerTopicSection as IFileAttachmentsTopicSection;

                    filesElement.Add(new XElement("Document",
                        new XAttribute("Id", fileAttachmentsTopicSection.Id),
                        new XAttribute("Title", fileAttachmentsTopicSection.Title),
                        new XAttribute("DocType", fileAttachmentsTopicSection.DocumentType),
                        new XAttribute("Description", fileAttachmentsTopicSection.Description == null ? "" : fileAttachmentsTopicSection.Description),
                        new XAttribute("Ext", fileAttachmentsTopicSection.FileExtension),
                        new XAttribute("Ordinal", fileAttachmentsTopicSection.Ordinal.ToString())
                        ));
                }
            }

            indexDocument.Save(this.filePath);
        }
    }
}
