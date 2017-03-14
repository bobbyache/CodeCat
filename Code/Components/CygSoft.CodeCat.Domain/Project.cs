using System.IO;
using System.Xml;

namespace CygSoft.CodeCat.Domain
{
    internal class Project
    {
        public string FilePath { get; private set; }
        public string FileTitle { get { return Path.GetFileName(this.FilePath); } }
        public string FolderPath { get { return Path.GetDirectoryName(this.FilePath); } }

        public int CurrentVersion { get; private set; }
        public string ProjectFileExtension { get { return "codecat"; } }

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
        }

        private void CreateNew(string filePath)
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            XmlElement root = xmlDocument.CreateElement("CodeCat_Project");

            root.Attributes.Append(CreateVersionAttributes(xmlDocument));

            xmlDocument.InsertBefore(xmlDeclaration, xmlDocument.DocumentElement);
            xmlDocument.AppendChild(root);
            xmlDocument.Save(filePath);
        }

        private XmlAttribute CreateVersionAttributes(XmlDocument xmlDocument)
        {
            XmlAttribute version = xmlDocument.CreateAttribute("Version");
            version.Value = this.CurrentVersion.ToString();
            return version;
        }
    }
}
