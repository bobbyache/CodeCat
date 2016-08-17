using CygSoft.CodeCat.Infrastructure.Qik;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.Qik.FileManager
{
    public class QikFileManager : CygSoft.Qik.FileManager.IQikFileManager
    {
        public QikFileManager(string parentFolder, string id)
        {
            this.ParentFolder = parentFolder;
            this.Id = id;
        }
            
        public string ParentFolder { get; private set; }
        public string Id { get; private set; }
        
        public string ScriptFileName { get { return "script.qik"; } }
        public string IndexFilePath { get { return Path.Combine(this.Folder, "index.xml"); } }
        public string IndexFileTitle { get { return "index.xml"; } }
        public string ScriptFilePath { get { return Path.Combine(this.Folder, this.ScriptFileName); } }

        public ITemplateFile[] Templates { get { return this.templateFiles.Values.Select(f => f).ToArray(); } }

        public bool IndexFileExists { get { return File.Exists(this.IndexFilePath); } }

        public string Folder 
        { 
            get 
            { 
                if (!string.IsNullOrEmpty(this.ParentFolder))
                    return Path.Combine(this.ParentFolder, this.Id);
                return string.Empty;
            } 
        }
        
        private Dictionary<string, ITemplateFile> templateFiles = new Dictionary<string, ITemplateFile>();

        private ScriptFile scriptFile = null;
        public IQikScriptFile ScriptFile 
        { 
            get 
            {
                if (this.scriptFile == null)
                {
                    this.scriptFile = new ScriptFile(this.ScriptFilePath);
                }
                return this.scriptFile; 
            } 
        }

        public bool TemplateExists(string fileName)
        {
            return templateFiles.ContainsKey(fileName);
        }

        public ITemplateFile GetTemplate(string fileName)
        {
            if (TemplateExists(fileName))
                return templateFiles[fileName];
            return null;
        }

        public void Create(string parentFolder, string id)
        {
            this.ParentFolder = parentFolder;
            this.Id = id;

            CreateIndex();
            Save();
            Load(parentFolder, id);
        }

        private void CreateIndex()
        {
            if (!Directory.Exists(this.Folder))
            {
                Directory.CreateDirectory(this.Folder);

                XElement rootElement = new XElement("QikFile", new XElement("Templates"));
                XDocument document = new XDocument(rootElement);
                document.Save(this.IndexFilePath);
            }
        }

        public void Load(string parentFolder, string id)
        {
            this.ParentFolder = parentFolder;
            this.Id = id;

            templateFiles = new Dictionary<string, ITemplateFile>();

            XDocument indexDocument = XDocument.Load(this.IndexFilePath);
            foreach (XElement fileElement in indexDocument.Element("QikFile").Element("Templates").Elements())
            {

                string file = (string)fileElement.Attribute("File");
                string title = (string)fileElement.Attribute("Title");
                string syntax = (string)fileElement.Attribute("Syntax");

                TemplateFile templateFile = new TemplateFile(Path.Combine(this.Folder, file), title, syntax);
                templateFile.Open();
                templateFiles.Add(file, templateFile);
            }

            // load the script file
            (this.ScriptFile as ScriptFile).Open();
        }

        public void Save()
        {
            this.scriptFile.Save();

            DeleteTemplateFiles();
            SaveTemplateFiles();
            RefreshIndex();
        }

        public void Delete()
        {
            Directory.Delete(this.Folder, true);
        }

        public ITemplateFile AddTemplate(string title, string syntax)
        {
            string fileName = Guid.NewGuid().ToString() + ".tpl";
            ITemplateFile templateFile = new TemplateFile(Path.Combine(this.Folder, fileName), title, syntax);
            this.templateFiles.Add(fileName, templateFile);
            return templateFile;
        }

        public void RemoveTemplate(string fileName)
        {
            this.templateFiles.Remove(fileName);
        }

        private void DeleteTemplateFiles()
        {
            // easier just to enumerate and delete rather then track through the template files...
            string[] templates = Directory.EnumerateFiles(this.Folder, "*.tpl").ToArray();
            foreach (string template in templates)
                File.Delete(template);
        }

        private void SaveTemplateFiles()
        {
            ITemplateFile[] files = templateFiles.Values.ToArray();
            foreach (TemplateFile file in files)
                file.Save();
        }

        private void RefreshIndex()
        {
            ITemplateFile[] files = templateFiles.Values.ToArray();

            XDocument indexDocument = XDocument.Load(this.IndexFilePath);
            XElement filesElement = indexDocument.Element("QikFile").Element("Templates");
            filesElement.RemoveNodes();

            foreach (ITemplateFile file in files)
            {
                filesElement.Add(new XElement("Template",
                    new XAttribute("File", file.FileName),
                    new XAttribute("Title", file.Title),
                    new XAttribute("Syntax", file.Syntax)));
            }

            indexDocument.Save(this.IndexFilePath);
        }
    }

}
