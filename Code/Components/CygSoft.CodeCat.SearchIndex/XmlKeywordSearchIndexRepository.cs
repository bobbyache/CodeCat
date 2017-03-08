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

        public IKeywordSearchIndex OpenIndex(string filePath, int expectedVersion)
        {
            var indexFile = NewIndexFile(filePath);
            string fileText = indexFile.Open();

            if (!indexFile.FileExists)
                throw new FileNotFoundException("Index file not found.");

            if (!indexFile.CorrectFormat)
                throw new InvalidDataException("The file format for the target file does not match the format expected or the file is corrupt.");

            if (!indexFile.CorrectVersion)
                throw new InvalidFileIndexVersionException("The file version is not compatible with the current application version.");

            List<IndexItem> items = LoadIndexItems(fileText, expectedVersion);
            IKeywordSearchIndex Index = new KeywordSearchIndex(filePath, expectedVersion, items.Cast<IKeywordIndexItem>().ToList());
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

            var indexFile = NewIndexFile(Index.FilePath);
            indexFile.Save(xmlDocument.ToString());
        }

        protected virtual IKeywordSearchIndexFile NewIndexFile(string filePath)
        {
            return new KeywordSearchIndexFile(filePath);
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

        public IKeywordSearchIndex CreateIndex(string filePath, int expectedVersion)
        {
            CreateNew(filePath, expectedVersion);
            IKeywordSearchIndex Index = new KeywordSearchIndex(filePath, expectedVersion);
            return Index;
        }

        private void CreateNew(string xmlFile, int expectedVersion)
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            XmlElement root = xmlDocument.CreateElement(this.RootElement);
            XmlAttribute version = xmlDocument.CreateAttribute("Version");
            version.Value = expectedVersion.ToString();

            root.Attributes.Append(version);
            xmlDocument.InsertBefore(xmlDeclaration, xmlDocument.DocumentElement);
            xmlDocument.AppendChild(root);
            xmlDocument.Save(xmlFile);
        }

        protected abstract List<IndexItem> LoadIndexItems(string fileText, int expectedVersion);
        
        public void ImportItems(string filePath, int expectedVersion, IKeywordIndexItem[] importItems)
        {
            IndexItem[] imports = importItems.OfType<IndexItem>().ToArray();

            XDocument xDocument = XDocument.Load(filePath);
            CheckVersion(xDocument.Root, expectedVersion);

            // ensure ids do not already exist.
            List<IndexItem> existingItems = LoadIndexItems(filePath, expectedVersion);

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

        protected virtual void CheckVersion(XElement xElement, int expectedVersion)
        {
            try
            {
                XAttribute xVersion = xElement.Attribute("Version");
                if (Int32.Parse(xVersion.Value) != expectedVersion)
                    throw new Exception();
            }
            catch (Exception)
            {
                throw new ApplicationException("Project file is incompatible with the file version that the application expects.");
            }
        }
    }
}
