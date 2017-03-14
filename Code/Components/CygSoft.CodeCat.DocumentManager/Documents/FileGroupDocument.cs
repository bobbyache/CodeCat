using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Documents.FileGroup;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class FileGroupDocument : TopicSection, IFileGroupDocument
    {
        internal FileGroupDocument(string folder, string title)
            : base(new DocumentPathGenerator(folder, "filgrp"), title, null)
        {
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.FileGroup);
        }

        internal FileGroupDocument(string folder, string id, string title, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, "filgrp", id), title, description, ordinal)
        {
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.FileGroup);
        }

        private List<IFileGroupFile> fileList = new List<IFileGroupFile>();
        private List<IFileGroupFile> removedList = new List<IFileGroupFile>();

        public IFileGroupFile[] Items 
        {
            get { return fileList.ToArray(); }
        }

        public string[] Categories
        {
            get { return this.Items.Select(r => r.Category).Distinct().ToArray(); }
        }

        protected override void OpenFile()
        {
            XDocument xDocument = XDocument.Load(this.FilePath);
            IEnumerable<XElement> elements = xDocument.Element("FileGroup").Elements("Files").Elements();

            List<IFileGroupFile> files = ExtractFromXml(elements);
            this.fileList = files.OfType<IFileGroupFile>().ToList();
        }

        private List<IFileGroupFile> ExtractFromXml(IEnumerable<XElement> elements)
        {
            List<IFileGroupFile> files = new List<IFileGroupFile>();

            foreach (XElement element in elements)
            {
                IFileGroupFile item = new FileGroupFile(
                    this.Folder,
                    (string)element.Attribute("Id"),
                    (string)element.Attribute("Title"),
                    (string)element.Attribute("FileName"),
                    element.Attribute("AllowOpenOrExecute") != null ? Convert.ToBoolean(element.Attribute("AllowOpenOrExecute").Value) : false,
                    element.Attribute("Category") != null ? (string)element.Attribute("Category") : "Unknown",
                    (string)element.Attribute("Description"),
                    DateTime.Parse((string)element.Attribute("Created")),
                    DateTime.Parse((string)element.Attribute("Modified"))
                );

                files.Add(item);
            }
            return files.ToList();
        }

        protected override void SaveFile()
        {
            foreach (IFileGroupFile file in fileList)
                file.Save();

            foreach (IFileGroupFile file in removedList)
                file.Delete();

            removedList.Clear();

            if (!File.Exists(this.FilePath))
                CreateFile();
            WriteFile(this.Items);
        }

        protected override void OnBeforeRevert()
        {
            foreach (IFileGroupFile file in removedList)
                file.Revert();

            foreach (IFileGroupFile file in fileList)
                file.Revert();

            removedList.Clear();

            base.OnBeforeRevert();
        }

        protected override void OnAfterDelete()
        {
            base.OnAfterDelete();
            foreach (IFileGroupFile file in this.fileList)
            {
                if (File.Exists(file.FilePath))
                    File.Delete(file.FilePath);
                if (File.Exists(file.ModifiedFilePath))
                    File.Delete(file.ModifiedFilePath);
            }
        }

        private void CreateFile()
        {
            XElement rootElement = new XElement("FileGroup", new XElement("Files"));
            XDocument xDocument = new XDocument(rootElement);
            xDocument.Save(this.FilePath);
        }

        private void WriteFile(IFileGroupFile[] items)
        {
            XDocument indexDocument = XDocument.Load(this.FilePath);
            XElement element = indexDocument.Element("FileGroup").Element("Files");
            element.RemoveNodes();

            AppendToContainerElement(element, items);
            indexDocument.Save(this.FilePath);
        }

        private void AppendToContainerElement(XElement containerElement, IFileGroupFile[] items)
        {
            foreach (IFileGroupFile item in items)
            {
                containerElement.Add(new XElement("FileItem",
                    new XAttribute("Id", item.Id),
                    new XAttribute("Title", item.Title),
                    new XAttribute("FileName", item.FileName),
                    new XAttribute("AllowOpenOrExecute", item.AllowOpenOrExecute),
                    new XAttribute("Category", item.Category != null ? item.Category : "Unknown"),
                    new XAttribute("Description", item.Description),
                    new XAttribute("Created", item.DateCreated.ToString()),
                    new XAttribute("Modified", item.DateModified.ToString())
                ));
            }
        }

        public void Add(IFileGroupFile file)
        {
            fileList.Add(file);
        }

        public void Remove(IFileGroupFile file)
        {
            fileList.Remove(file);
            removedList.Add(file);
        }

        public bool ValidateFileName(string fileName, string id = "")
        {
            if (File.Exists(Path.Combine(this.Folder, fileName)))
            {
                foreach (IFileGroupFile checkFile in this.fileList)
                {
                    if (checkFile.Id != id && checkFile.HasFileName(fileName))
                        return false;
                }
                foreach (IFileGroupFile checkFile in this.removedList)
                {
                    if (checkFile.Id != id && checkFile.HasFileName(fileName))
                        return false;
                }
            }
            return true;
        }

        public IFileGroupFile CreateNewFile(string fileName, string sourceFilePath)
        {
            IFileGroupFile fileGroupFile = new FileGroupFile(this.Folder, fileName, sourceFilePath);
            return fileGroupFile;
        }
    }
}
