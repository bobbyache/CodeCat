﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;

namespace CygSoft.CodeCat.Domain.Code
{
    public class CodeFile : IPersistableTarget
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
        public event EventHandler ContentClosed;
        public event EventHandler ContentDeleted;
        public event EventHandler ContentReverted;
        public event EventHandler BeforeContentSaved;

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

        public CodeFile(CodeKeywordIndexItem indexItem, string folderPath)
        {
            this.snapshots = new List<CodeSnapshot>();
            this.indexItem = indexItem;
            this.FolderPath = folderPath;
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
            get 
            {
                if (this.indexItem != null)
                    return (this.indexItem as CodeKeywordIndexItem).Syntax;
                return null;
            }
            set
            {
                CodeKeywordIndexItem indexItem = this.indexItem as CodeKeywordIndexItem;
                if (indexItem != null)
                    indexItem.Syntax = value;
            }
        }

        public string CommaDelimitedKeywords
        {
            get { return this.IndexItem.CommaDelimitedKeywords; }
            set
            {
                this.IndexItem.SetKeywords(value);
            }
        }

        public int HitCount
        {
            get;
            private set;
        }

        public void Revert()
        {
            bool opened = this.ReadData();

            if (this.ContentReverted != null)
                this.ContentReverted(this, new EventArgs());
        }

        public bool Open()
        {
            bool opened = this.ReadData();
            if (opened)
            {
                this.IncrementHitCount();
                this.WriteData();
            }
            return opened;
        }

        public void Close()
        {
            if (ContentClosed != null)
                ContentClosed(this, new EventArgs());
        }

        private bool ReadData()
        {
            if (File.Exists(GetFilePath()))
            {
                XDocument file = XDocument.Load(GetFilePath());
                this.Text = (string)file.Element("Snippet").Element("Code").Value;

                XAttribute syntaxAttribute = file.Element("Snippet").Attribute("Syntax");
                if (syntaxAttribute != null)
                    this.Syntax = (string)syntaxAttribute.Value;

                XAttribute hitCountAttribute = file.Element("Snippet").Attribute("Hits");
                if (hitCountAttribute != null)
                    this.HitCount = int.Parse(hitCountAttribute.Value);

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
            if (this.BeforeContentSaved != null)
                BeforeContentSaved(this, new EventArgs());

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
            if (ContentDeleted != null)
                ContentDeleted(this, new EventArgs());
        }

        private void IncrementHitCount()
        {
            this.HitCount++;
        }

        private void WriteData()
        {
            XElement snapshotsElement = new XElement("Snapshots");

            XElement snippetElement = new XElement("Snippet",
                    new XAttribute("ID", IndexItem.Id),
                    new XAttribute("Syntax", this.Syntax),
                    new XAttribute("Hits", this.HitCount),
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
