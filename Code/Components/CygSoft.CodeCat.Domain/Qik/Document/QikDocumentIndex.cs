using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Documents;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.Qik.Document
{
    public class QikDocumentIndex : BaseDocumentIndex
    {
        private ICodeDocument scriptDocument;
        public ICodeDocument ScriptDocument { get { return this.scriptDocument; } }

        public ICodeDocument[] TemplateDocuments
        {
            get { return this.DocumentFiles.OfType<CodeDocument>().ToArray(); }
        }

        public QikDocumentIndex(string id) : base(id, "xml")
        {

        }

        protected override void CreateFile()
        {
            if (!Directory.Exists(this.Folder))
            {
                Directory.CreateDirectory(this.Folder);

                XElement rootElement = new XElement("QikFile", new XElement("Templates"));
                XDocument document = new XDocument(rootElement);
                document.Save(this.FilePath);
            }
        }

        protected override List<DocumentManager.Infrastructure.IDocument> LoadDocumentFiles()
        {
            List<ICodeDocument> documents = new List<ICodeDocument>();

            XDocument indexDocument = XDocument.Load(this.FilePath);
            foreach (XElement documentElement in indexDocument.Element("QikFile").Element("Templates").Elements())
            {
                string documentId = (string)documentElement.Attribute("Id");
                string documentTitle = (string)documentElement.Attribute("Title");
                string documentDesc = (string)documentElement.Attribute("Description");
                string documentExt = (string)documentElement.Attribute("Ext");
                string documentSyntax = (string)documentElement.Attribute("Syntax");
                int documentOrdinal = int.Parse((string)documentElement.Attribute("Ordinal"));

                if (documentExt == "qik")
                {
                    ICodeDocument scriptDocument = DocumentFactory.CreateQikScriptDocument(documentId, documentTitle, documentExt,
                        documentSyntax, documentOrdinal, documentDesc);
                    documents.Add(scriptDocument);
                }
                else
                {
                    ICodeDocument templateDocument = DocumentFactory.CreateCodeDocument(documentId, documentTitle, documentExt,
                        documentSyntax, documentOrdinal, documentDesc);
                    documents.Add(templateDocument);
                }
            }

            ICodeDocument scriptDoc = documents.OfType<QikScriptDocument>().SingleOrDefault();
            this.scriptDocument = scriptDoc;

            return documents.OfType<IDocument>().ToList();
        }

        protected override void SaveDocumentIndex()
        {
            ICodeDocument[] docFiles = base.DocumentFiles.OfType<ICodeDocument>().ToArray();

            XDocument indexDocument = XDocument.Load(this.FilePath);
            XElement filesElement = indexDocument.Element("QikFile").Element("Templates");
            filesElement.RemoveNodes();

            foreach (ICodeDocument docFile in docFiles)
            {
                filesElement.Add(new XElement("Template",
                    new XAttribute("File", docFile.FileName),
                    new XAttribute("Title", docFile.Title),
                    new XAttribute("Description", docFile.Description),
                    new XAttribute("Ext", docFile.FileExtension),
                    new XAttribute("Syntax", docFile.Syntax),
                    new XAttribute("Ordinal", docFile.Ordinal.ToString())
                    ));
            }

            indexDocument.Save(this.FilePath);
        }
    }
}
