using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
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
    public abstract class XmlKeywordSearchIndexRepository<IndexItem> : IKeywordSearchIndexRepository where IndexItem : XmlKeywordIndexItem, new()
    {
        public string RootElement { get; private set; }

        public XmlKeywordSearchIndexRepository(string rootElement)
        {
            this.RootElement = rootElement;
        }

        public IKeywordSearchIndex OpenIndex(string filePath, int currentVersion)
        {
            List<IndexItem> items = LoadIndexItems(filePath, currentVersion);
            IKeywordSearchIndex Index = new KeywordSearchIndex(filePath, currentVersion, items.Cast<IKeywordIndexItem>().ToList());
            return Index;
        }

        public void SaveIndex(IKeywordSearchIndex Index)
        {
            XDocument xmlDocument = XDocument.Load(Index.FilePath, LoadOptions.SetBaseUri | LoadOptions.SetLineInfo);
            XElement xElement = xmlDocument.Element(this.RootElement);

            xElement.Nodes().Remove();

            foreach (IndexItem item in Index.All())
            {
                xElement.Add(item.Serialize());
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
            XmlElement root = xmlDocument.CreateElement(this.RootElement);
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

        protected abstract List<IndexItem> LoadIndexItems(string filePath, int currentVersion);


        public void ImportItems(string filePath, int currentVersion, IKeywordIndexItem[] importItems)
        {
            IndexItem[] imports = importItems.OfType<IndexItem>().ToArray();

            XDocument xDocument = XDocument.Load(filePath);
            CheckVersion(xDocument.Root, currentVersion);

            // ensure ids do not already exist.
            List<IndexItem> existingItems = LoadIndexItems(filePath, currentVersion);

            bool exist = existingItems.Join(importItems, ex => ex.Id, im => im.Id,
                (ex, im) => ex.Id).Count() > 0;

            if (exist)
                throw new ApplicationException("Matching index IDs already exist on the target index.");

            //var duplicateItems = existingItems.Join(importItems, ex => ex.Id, im => im.Id,
            //    (ex, im) => new { ex.Id, ex.Title,  SourceId = im.Id, SourceTitle = im.Title });

            //foreach (var item in duplicateItems)
            //{
            //    Console.WriteLine(item.Id);
            //}

            foreach (var importItem in imports)
            {
                xDocument.Root.Add(importItem.Serialize());
            }

            xDocument.Save(filePath);
        }

        protected void CheckVersion(XElement xElement, int currentVersion)
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
