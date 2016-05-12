using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Search.KeywordIndex;

namespace CygSoft.CodeCat.Domain.Code
{
    public class CodeFile : IPersistableFile
    {
        private class SnapshotDateComparer : IComparer<CodeSnapshot>
        {

            public int Compare(CodeSnapshot x, CodeSnapshot y)
            {
                if (x.DateCreated < y.DateCreated)
                    return 1;
                if (x.DateCreated > y.DateCreated)
                    return -1;
                return 0;
            }
        }

        public event EventHandler ContentSaved;
        public event EventHandler SnapshotTaken;
        public event EventHandler SnapshotDeleted;

        private IKeywordIndexItem indexItem;

        private List<CodeSnapshot> snapshots;

        public CodeSnapshot[] Snapshots 
        { 
            get 
            {
                this.snapshots.Sort(new SnapshotDateComparer());
                return this.snapshots.ToArray();
            } 
        }

        public bool HasSnapshots { get { return this.snapshots.Count > 0; } }

        public IKeywordIndexItem IndexItem
        {
            get { return this.indexItem; }
        }

        public string Text { get; set; }

        public CodeFile(IKeywordIndexItem item, string folderPath)
        {
            this.snapshots = new List<CodeSnapshot>();
            this.indexItem = item;
            this.FolderPath = folderPath;

            this.ReadData();
        }

        public string Id { get { return this.IndexItem.Id; } }
        public string FilePath { get { return GetFilePath(); } }
        public string FileTitle { get { return this.IndexItem.FileTitle; } }
        public string FolderPath { get; private set; }

        public bool FileExists { get { return File.Exists(GetFilePath()); } }

        public string Title
        {
            get { return this.IndexItem.Title; }
            set { this.IndexItem.Title = value; }
        }

        public string Syntax
        {
            get; set;
        }

        public string CommaDelimitedKeywords
        {
            get { return this.IndexItem.CommaDelimitedKeywords; }
            set 
            {
                this.IndexItem.SetKeywords(value);
            }
        }

        private bool ReadData()
        {
            if (File.Exists(GetFilePath()))
            {
                XDocument file = XDocument.Load(GetFilePath());
                this.Text = (string)file.Element("Snippet").Element("Code").Value;

                XAttribute languageElement = file.Element("Snippet").Attribute("Syntax");
                if (languageElement != null)
                    this.Syntax = (string)languageElement.Value;

                this.snapshots.Clear();

                if ((string)file.Element("Snippet").Element("Snapshots") != null)
                {
                    foreach (XElement element in file.Element("Snippet").Element("Snapshots").Elements())
                    {
                        this.snapshots.Add(new CodeSnapshot(
                                (string)element.Attribute("ID"),
                                (string)element.Element("Description"),
                                (string)element.Element("Code"),
                                DateTime.Parse((string)element.Attribute("SnapDate")),
                                DateTime.Parse((string)element.Attribute("SnapDate"))
                            ));
                    }
                }

                return true;
            }
            return false;
        }

        public void Save()
        {
            this.WriteData();
            if (this.ContentSaved != null)
                ContentSaved(this, new EventArgs());
        }

        public void TakeSnapshot(string description = "")
        {
            CodeSnapshot snapshot = new CodeSnapshot(this.Text);
            if (description.Trim() != string.Empty)
                snapshot.Description = description;
            this.snapshots.Add(snapshot);

            this.WriteData();

            if (SnapshotTaken != null)
                SnapshotTaken(this, new EventArgs());
        }

        public void DeleteSnapshot(string snapshotId)
        {
            CodeSnapshot snapshot = this.snapshots.Where(s => s.Id == snapshotId).SingleOrDefault();
            this.snapshots.Remove(snapshot);

            this.WriteData();

            if (SnapshotDeleted != null)
                SnapshotDeleted(this, new EventArgs());
        }

        public void Delete()
        {
            File.Delete(this.FilePath);
        }

        private void WriteData()
        {
            XElement snapshotsElement = new XElement("Snapshots");

            XElement snippetElement = new XElement("Snippet",
                    new XAttribute("ID", IndexItem.Id),
                    new XAttribute("Syntax", this.Syntax),
                    new XElement("Code", new XCData(this.Text))
                 );

            foreach (CodeSnapshot snapshot in this.snapshots)
            {
                snapshotsElement.Add(new XElement("Snapshot", 
                    new XAttribute("ID", snapshot.Id),
                    new XAttribute("SnapDate", snapshot.DateCreated),
                    new XElement("Description", snapshot.Description),
                    new XElement("Code", new XCData(snapshot.Text))));
            }

            snippetElement.Add(snapshotsElement);

            XDocument document = new XDocument(snippetElement);
            document.Save(GetFilePath());
        }

        private string GetFilePath()
        {
            return Path.Combine(this.FolderPath, IndexItem.FileTitle);
        }
    }
}
