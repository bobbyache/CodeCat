using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Search.KeywordIndex
{
    public class KeywordSearchIndexRepository : IKeywordSearchIndexRepository
    {
        public IKeywordSearchIndex OpenIndex(string filePath, int currentVersion)
        {
            IKeywordSearchIndex Index = new KeywordSearchIndex(filePath, currentVersion, LoadIndexItems(filePath, currentVersion));
            return Index;
        }

        public void SaveIndex(IKeywordSearchIndex Index)
        {
            XDocument xmlDocument = XDocument.Load(Index.FilePath, LoadOptions.SetBaseUri | LoadOptions.SetLineInfo);
            XElement xElement = xmlDocument.Element("CodeCat_CodeIndex");

            xElement.Nodes().Remove();

            foreach (IKeywordIndexItem item in Index.All())
            {
                //XmlKeywordIndexItem indexItem = item as XmlKeywordIndexItem;
                //xElement.Add(indexItem.Serialize());

                xElement.Add(new XElement("IndexItem",
                    new XAttribute("ID", item.Id),
                    new XElement("Title", item.Title),
                    new XElement("Syntax", item.Syntax),
                    new XElement("DateCreated", item.DateCreated),
                    new XElement("DateModified", item.DateModified),
                    new XElement("Keywords", item.CommaDelimitedKeywords)
                    ));
            }

            xmlDocument.Save(Index.FilePath);
        }

        public IKeywordSearchIndex SaveIndexAs(IKeywordSearchIndex Index, string filePath)
        {
            IKeywordSearchIndex newIndex = CloneIndex(Index, filePath);
            CreateIndex(filePath, Index.CurrentVersion);
            SaveIndex(newIndex);
            return newIndex;
        }

        public IKeywordSearchIndex CloneIndex(IKeywordSearchIndex sourceIndex, string filePath)
        {
            IKeywordSearchIndex newIndex = new KeywordSearchIndex(filePath, sourceIndex.CurrentVersion, sourceIndex.All().ToList());
            return newIndex;
        }

        public IKeywordSearchIndex CreateIndex(string filePath, int currentVersion)
        {
            CreateNew(filePath, currentVersion);
            IKeywordSearchIndex Index = new KeywordSearchIndex(filePath, currentVersion);
            return Index;
        }

        private void CreateNew(string xmlFile, int currentVersion)
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            XmlElement root = xmlDocument.CreateElement("CodeCat_CodeIndex");
            XmlAttribute version = xmlDocument.CreateAttribute("Version");
            version.Value = currentVersion.ToString();

            root.Attributes.Append(version);
            xmlDocument.InsertBefore(xmlDeclaration, xmlDocument.DocumentElement);
            xmlDocument.AppendChild(root);
            xmlDocument.Save(xmlFile);
        }

        private void SaveProjectAs(string sourcePath, string destinationFilePath)
        {
            File.Copy(sourcePath, destinationFilePath, true);
        }

        private List<IKeywordIndexItem> LoadIndexItems(string filePath, int currentVersion)
        {
            XElement xElement = XElement.Load(filePath);
            CheckVersion(xElement, currentVersion);

            IEnumerable<IKeywordIndexItem> items = from h in xElement.Elements("IndexItem")
                                                   select new KeywordIndexItem
                                                       (
                                                           (string)h.Attribute("ID"),
                                                           (string)h.Element("Title"),
                                                           (string)h.Element("Syntax"),
                                                           (DateTime)h.Element("DateCreated"),
                                                           (DateTime)h.Element("DateModified"),
                                                           (string)h.Element("Keywords")
                                                       );
            return items.ToList<IKeywordIndexItem>();
        }

        private void CheckVersion(XElement xElement, int currentVersion)
        {
            try
            {
                XAttribute xVersion = xElement.Attribute("Version");
                if (Int32.Parse(xVersion.Value) != currentVersion)
                    throw new Exception();
            }
            catch (Exception)
            {
                throw new ApplicationException("Project file is incompatible with the current application release.");
            }
        }
    }
}
