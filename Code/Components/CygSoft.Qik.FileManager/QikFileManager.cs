using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.Qik.FileManager
{
    public class QikFileManager
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

        public string[] Templates { get { return this.templateFiles.Values.Select(f => f.FileName).ToArray(); } }

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

        private ScriptFile scriptFile = null;
        private Dictionary<string, TemplateFile> templateFiles = new Dictionary<string, TemplateFile>();

        public void Create(string parentFolder, string id)
        {
            this.ParentFolder = parentFolder;
            this.Id = id;

            Directory.CreateDirectory(this.Folder);

            // do this to ensure that the file is closed afterwards before use...
            using (FileStream fileStream = File.Create(this.ScriptFilePath)) { }

            XElement rootElement = new XElement("QikFile", new XElement("Templates"));
            XDocument document = new XDocument(rootElement);
            document.Save(this.IndexFilePath);

            Load(parentFolder, id);
        }

        public void Load(string parentFolder, string id)
        {
            this.ParentFolder = parentFolder;
            this.Id = id;

            templateFiles = new Dictionary<string, TemplateFile>();

            XDocument indexDocument = XDocument.Load(this.IndexFilePath);
            foreach (XElement fileElement in indexDocument.Element("QikFile").Element("Templates").Elements())
            {

                string file = (string)fileElement.Attribute("File");
                string title = (string)fileElement.Attribute("Title");
                string syntax = (string)fileElement.Attribute("Syntax");

                TemplateFile templateFile = new TemplateFile(Path.Combine(this.Folder, file), title, syntax);
                templateFile.Load();
                templateFiles.Add(file, templateFile);
            }

            // load the script file
            this.scriptFile = new ScriptFile(this.ScriptFilePath);
            this.scriptFile.Load();
        }

        public string ScriptText
        {
            get { return this.scriptFile.Text; }
            set { this.scriptFile.Text = value; }
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

        public string AddTemplate(string title, string syntax)
        {
            string fileName = Guid.NewGuid().ToString() + ".tpl";
            this.templateFiles.Add(fileName, new TemplateFile(Path.Combine(this.Folder, fileName), title, syntax));
            return fileName;
        }

        public void RemoveTemplate(string fileName)
        {
            this.templateFiles.Remove(fileName);
        }

        public void SetTemplateSyntax(string fileName, string syntax)
        {
            this.templateFiles[fileName].Syntax = syntax;
        }

        public string GetTemplateSyntax(string fileName)
        {
            return this.templateFiles[fileName].Syntax;
        }

        public void SetTemplateText(string fileName, string text)
        {
            this.templateFiles[fileName].Text = text;
        }

        public string GetTemplateText(string fileName)
        {
            return this.templateFiles[fileName].Text;
        }

        public void SetTemplateTitle(string fileName, string title)
        {
            this.templateFiles[fileName].Title = title;
        }

        public string GetTemplateTitle(string fileName)
        {
            return this.templateFiles[fileName].Title;
        }

        private void DeleteTemplateFiles()
        {
            string[] templates = Directory.EnumerateFiles(this.Folder, "*.tpl").ToArray();
            foreach (string template in templates)
                File.Delete(template);
        }

        private void SaveTemplateFiles()
        {
            TemplateFile[] files = templateFiles.Values.ToArray();
            foreach (TemplateFile file in files)
                file.Save();
        }

        private void RefreshIndex()
        {
            TemplateFile[] files = templateFiles.Values.ToArray();

            XDocument indexDocument = XDocument.Load(this.IndexFilePath);
            XElement filesElement = indexDocument.Element("QikFile").Element("Templates");
            filesElement.RemoveNodes();

            foreach (TemplateFile file in files)
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
