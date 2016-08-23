//using CygSoft.CodeCat.DocumentManager.Base;
//using CygSoft.CodeCat.DocumentManager.Infrastructure;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;

//namespace CygSoft.CodeCat.DocumentManager.Documents
//{
//    public class TemplateDocumentsFile : BaseMultiDocumentFile
//    {
//        public string ScriptFileName { get { return "script.qik"; } }
//        public string ScriptFilePath { get { return Path.Combine(this.Folder, this.ScriptFileName); } }

//        private ScriptDocument scriptFile = null;
//        public ScriptDocument ScriptFile { get { return this.scriptFile; } }

//        public TemplateDocumentsFile(string id) : base(id, "xml")
//        {
//        }

//        protected override List<IDocumentFile> LoadDocumentFiles()
//        {
//            List<TemplateDocument> templateDocuments = new List<TemplateDocument>();

//            XDocument indexDocument = XDocument.Load(this.FilePath);
//            foreach (XElement fileElement in indexDocument.Element("QikFile").Element("Templates").Elements())
//            {
//                string id = (string)fileElement.Attribute("Id");
//                string title = (string)fileElement.Attribute("Title");
//                string syntax = (string)fileElement.Attribute("Syntax");

//                TemplateDocument templateDocument = new TemplateDocument(id, title, syntax);
//                templateDocuments.Add(templateDocument);
//            }

//            return templateDocuments.OfType<IDocumentFile>().ToList();
//        }

//        protected override void LoadNonDocumentFiles()
//        {
//            this.scriptFile.Open(Path.Combine(this.Folder, this.ScriptFileName));
//        }

//        protected override void CreateFile()
//        {
//            CreateIndex();
//        }

//        protected override void SaveFile()
//        {
//            base.SaveFile();
//            this.scriptFile.Save();
//            this.SaveIndex();
//        }

//        protected override void DeleteFile()
//        {
//            base.DeleteFile();
//            scriptFile.Delete();
//        }

//        private void SaveIndex()
//        {
//            IDocumentFile[] docFiles = base.DocumentFiles.ToArray();

//            XDocument indexDocument = XDocument.Load(this.FilePath);
//            XElement filesElement = indexDocument.Element("QikFile").Element("Templates");
//            filesElement.RemoveNodes();

//            foreach (IDocumentFile docFile in docFiles)
//            {
//                if (docFile is TemplateDocument)
//                {
//                    TemplateDocument document = docFile as TemplateDocument;
//                    filesElement.Add(new XElement("Template",
//                        new XAttribute("File", document.FileName),
//                        new XAttribute("Title", document.Title),
//                        new XAttribute("Syntax", document.Syntax)));
//                }
//            }

//            indexDocument.Save(this.FilePath);
//        }

//        private void CreateIndex()
//        {
//            if (!Directory.Exists(this.Folder))
//            {
//                Directory.CreateDirectory(this.Folder);

//                XElement rootElement = new XElement("QikFile", new XElement("Templates"));
//                XDocument document = new XDocument(rootElement);
//                document.Save(this.FilePath);
//            }
//        }
//    }
//}
