using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Documents.FileGroup;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CygSoft.CodeCat.DocumentManager.Documents.FileGroup
{
    public class FileAttachmentsTopicSection : TopicSection, IFileAttachmentsTopicSection
    {
        internal FileAttachmentsTopicSection(string folder, string title)
            : base(new DocumentPathGenerator(folder, "filgrp"), title, null)
        {
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.FileAttachments);
        }

        internal FileAttachmentsTopicSection(string folder, string id, string title, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, "filgrp", id), title, description, ordinal)
        {
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.FileAttachments);
        }

        private List<IFileAttachment> fileAttachmentList = new List<IFileAttachment>();
        private List<IFileAttachment> removedFileAttachmentList = new List<IFileAttachment>();

        public IFileAttachment[] Items 
        {
            get { return fileAttachmentList.ToArray(); }
        }

        public string[] Categories
        {
            get { return this.Items.Select(r => r.Category).Distinct().ToArray(); }
        }

        protected override void OpenFile()
        {
            XDocument xDocument = XDocument.Load(this.FilePath);
            IEnumerable<XElement> elements = xDocument.Element("FileGroup").Elements("Files").Elements();

            List<IFileAttachment> files = ExtractFromXml(elements);
            this.fileAttachmentList = files.OfType<IFileAttachment>().ToList();
        }

        private List<IFileAttachment> ExtractFromXml(IEnumerable<XElement> elements)
        {
            List<IFileAttachment> files = new List<IFileAttachment>();

            foreach (XElement element in elements)
            {
                IFileAttachment fileAttachment = new FileAttachment(
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

                files.Add(fileAttachment);
            }
            return files.ToList();
        }

        protected override void SaveFile()
        {
            foreach (IFileAttachment fileAttachment in fileAttachmentList)
                fileAttachment.Save();

            foreach (IFileAttachment fileAttachment in removedFileAttachmentList)
                fileAttachment.Delete();

            removedFileAttachmentList.Clear();

            if (!File.Exists(this.FilePath))
                CreateFile();
            WriteFile(this.Items);
        }

        protected override void OnBeforeRevert()
        {
            foreach (IFileAttachment file in removedFileAttachmentList)
                file.Revert();

            foreach (IFileAttachment file in fileAttachmentList)
                file.Revert();

            removedFileAttachmentList.Clear();

            base.OnBeforeRevert();
        }

        protected override void OnAfterDelete()
        {
            base.OnAfterDelete();
            foreach (IFileAttachment file in this.fileAttachmentList)
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

        private void WriteFile(IFileAttachment[] fileAttachments)
        {
            XDocument indexDocument = XDocument.Load(this.FilePath);
            XElement element = indexDocument.Element("FileGroup").Element("Files");
            element.RemoveNodes();

            AppendToContainerElement(element, fileAttachments);
            indexDocument.Save(this.FilePath);
        }

        private void AppendToContainerElement(XElement containerElement, IFileAttachment[] fileAttachments)
        {
            foreach (IFileAttachment fileAttachment in fileAttachments)
            {
                containerElement.Add(new XElement("FileItem",
                    new XAttribute("Id", fileAttachment.Id),
                    new XAttribute("Title", fileAttachment.Title),
                    new XAttribute("FileName", fileAttachment.FileName),
                    new XAttribute("AllowOpenOrExecute", fileAttachment.AllowOpenOrExecute),
                    new XAttribute("Category", fileAttachment.Category != null ? fileAttachment.Category : "Unknown"),
                    new XAttribute("Description", fileAttachment.Description),
                    new XAttribute("Created", fileAttachment.DateCreated.ToString()),
                    new XAttribute("Modified", fileAttachment.DateModified.ToString())
                ));
            }
        }

        public void Add(IFileAttachment fileAttachment)
        {
            fileAttachmentList.Add(fileAttachment);
        }

        public void Remove(IFileAttachment fileAttachment)
        {
            fileAttachmentList.Remove(fileAttachment);
            removedFileAttachmentList.Add(fileAttachment);
        }

        public bool ValidateFileName(string fileName, string id = "")
        {
            if (File.Exists(Path.Combine(this.Folder, fileName)))
            {
                foreach (IFileAttachment fileAttachment in this.fileAttachmentList)
                {
                    if (fileAttachment.Id != id && fileAttachment.HasFileName(fileName))
                        return false;
                }
                foreach (IFileAttachment removedFileAttachment in this.removedFileAttachmentList)
                {
                    if (removedFileAttachment.Id != id && removedFileAttachment.HasFileName(fileName))
                        return false;
                }
            }
            return true;
        }

        public IFileAttachment CreateNewFile(string fileName, string sourceFilePath)
        {
            IFileAttachment fileAttachment = new FileAttachment(this.Folder, fileName, sourceFilePath);
            return fileAttachment;
        }
    }
}
