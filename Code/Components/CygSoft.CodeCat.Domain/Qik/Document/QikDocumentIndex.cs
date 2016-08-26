using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Documents;
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
    public class QikDocumentIndex : BaseDocumentIndex
    {
        public IQikScriptDocument ScriptDocument 
        { 
            get 
            {
                return base.DocumentFiles.OfType<QikScriptDocument>().SingleOrDefault();
            } 
        }

        public QikDocumentIndex(string folder, string id) : 
            base(new DocumentIndexPathGenerator(folder, "xml", id))
        {
            // creating this document for the very first time... so we need to ensure that we create a script document!
            ICodeDocument scriptDoc = DocumentFactory.CreateQikScriptDocument(this.Folder, "Qik Script", "qik", "Qik");
            scriptDoc.Ordinal = 1;  // should always be the last item, but is the first over here.
            this.AddDocumentFile(scriptDoc);
        }

        public QikDocumentIndex(string folder)
            : base(new DocumentIndexPathGenerator(folder, "xml"))
        {
        }

        protected override List<DocumentManager.Infrastructure.IDocument> LoadDocumentFiles()
        {
            List<ICodeDocument> documents = new List<ICodeDocument>();

            XDocument indexDocument = XDocument.Load(this.FilePath);
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
                    ICodeDocument scriptDocument = DocumentFactory.CreateQikScriptDocument(this.Folder, documentId, documentTitle, documentExt,
                        documentSyntax, documentOrdinal, documentDesc);
                    documents.Add(scriptDocument);
                }
                else
                {
                    ICodeDocument templateDocument = DocumentFactory.CreateCodeDocument(this.Folder, documentId, documentTitle, documentExt,
                        documentSyntax, documentOrdinal, documentDesc);
                    documents.Add(templateDocument);
                }
            }

            return documents.OfType<IDocument>().ToList();
        }

        protected override void SaveDocumentIndex()
        {
            if (!Directory.Exists(this.Folder))
                WriteStartFile();
            WriteExistingFile();
        }

        protected override void AfterAddDocumentFile()
        {
            base.documentFiles.MoveLast(this.ScriptDocument as IDocument);
        }

        private void WriteStartFile()
        {
            Directory.CreateDirectory(this.Folder);

            XElement rootElement = new XElement("QikFile", new XElement("Documents"));
            XDocument document = new XDocument(rootElement);
            document.Save(this.FilePath);
        }

        private void WriteExistingFile()
        {
            ICodeDocument[] docFiles = base.DocumentFiles.OfType<ICodeDocument>().ToArray();

            XDocument indexDocument = XDocument.Load(this.FilePath);
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

            indexDocument.Save(this.FilePath);
        }
    }
}
