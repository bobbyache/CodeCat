using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.TopicSections.VersionedCode
{
    internal class VersionedCodeIndexXmlRepository : IVersionedFileRepository
    {
        private readonly string filePath;
        private readonly string folder;

        public VersionedCodeIndexXmlRepository(string filePath)
        {
            this.filePath = filePath;
            this.folder = Path.GetDirectoryName(filePath);
        }

        public string FilePath { get; set; }

        public bool HasVersionFile
        {
            get { return File.Exists(this.filePath); }
        }

        public List<IFileVersion> LoadVersions()
        {
            List<IFileVersion> fileVersions = new List<IFileVersion>();

            if (HasVersionFile)
            {
                XDocument xDocument = XDocument.Load(this.filePath);

                foreach (XElement element in xDocument.Element("VersionHistory").Elements("Versions").Elements())
                {
                    string id = (string)element.Attribute("Id");
                    string extension = (string)element.Attribute("Extension");

                    VersionedCodeTopicSection.CodeTopicSectionVersion version =
                        new VersionedCodeTopicSection.CodeTopicSectionVersion(
                                (string)element.Attribute("Id"),
                                DateTime.Parse((string)element.Attribute("TimeTaken")),
                                (string)element.Element("Description"),
                                Path.Combine(this.folder, (string)element.Attribute("Id") + ".txt")
                            );

                    fileVersions.Add(version);
                }
            }
            return fileVersions;
        }

        public void WriteVersions(List<IFileVersion> fileVersions)
        {
            if (!File.Exists(this.filePath))
                CreateFile();
            WriteFile(fileVersions);
        }

        private void CreateFile()
        {
            Directory.CreateDirectory(this.folder);

            XElement rootElement = new XElement("VersionHistory", new XElement("Versions"));
            XDocument xDocument = new XDocument(rootElement);
            xDocument.Save(this.filePath);
        }

        private void WriteFile(List<IFileVersion> fileVersions)
        {
            XDocument indexDocument = XDocument.Load(this.filePath);
            XElement filesElement = indexDocument.Element("VersionHistory").Element("Versions");
            filesElement.RemoveNodes();

            foreach (IFileVersion fileVersion in fileVersions)
            {
                filesElement.Add(new XElement("Version",
                    new XAttribute("Id", fileVersion.Id),
                    new XAttribute("TimeTaken", fileVersion.TimeTaken),
                    new XElement("Description", new XCData(fileVersion.Description))
                ));
            }

            indexDocument.Save(this.filePath);
        }
    }
}
