using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CygSoft.CodeCat.Domain
{
    internal class Project
    {
        private const string CODE_LIBRARY_ELEMENT = "CodeLibrary";
        private const string CODE_LIBRARY_INDEX_FILE = "_code.xml";
        private const string CODE_LIBRARY_FOLDER = "_code";

        public string FilePath { get; private set; }
        public string FileTitle { get { return Path.GetFileName(this.FilePath); } }
        public string FolderPath { get { return Path.GetDirectoryName(this.FilePath); } }
        public int CurrentVersion { get; private set; }

        public void Open(string filePath, int currentVersion)
        {
            this.FilePath = filePath;
            this.CurrentVersion = currentVersion;
        }

        public void Create(string filePath, int currentVersion)
        {
            this.FilePath = filePath;
            this.CurrentVersion = currentVersion;
            CreateNew(filePath);
            Directory.CreateDirectory(this.GetLibraryFolder());            
        }

        public string GetIndexPath()
        {
            return Path.Combine(this.FolderPath, CODE_LIBRARY_INDEX_FILE);
        }

        public string GetLibraryFolder()
        {
            return Path.Combine(this.FolderPath, CODE_LIBRARY_FOLDER);
        }

        private void CreateNew(string filePath)
        {
            XmlDocument document = new XmlDocument();

            XmlDeclaration xmlDeclaration = document.CreateXmlDeclaration("1.0", "utf-8", null);
            XmlElement root = document.CreateElement("CodeCat_Project");

            root.Attributes.Append(CreateVersionAttributes(document));
            root.AppendChild(CreateCodeLibraryElement(document));

            document.InsertBefore(xmlDeclaration, document.DocumentElement);
            document.AppendChild(root);
            document.Save(filePath);
        }

        private XmlAttribute CreateVersionAttributes(XmlDocument document)
        {
            XmlAttribute version = document.CreateAttribute("Version");
            version.Value = this.CurrentVersion.ToString();
            return version;
        }

        private XmlElement CreateCodeLibraryElement(XmlDocument document)
        {
            XmlElement element = document.CreateElement(CODE_LIBRARY_ELEMENT);
            XmlAttribute libraryAttribute = document.CreateAttribute("Library");
            libraryAttribute.Value = "CODE";
            XmlAttribute fileAttribute = document.CreateAttribute("File");
            fileAttribute.Value = CODE_LIBRARY_INDEX_FILE;
            
            element.Attributes.Append(libraryAttribute);
            element.Attributes.Append(fileAttribute);

            return element;
        }
    }
}
