﻿using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Qik.Document;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using CygSoft.Qik.LanguageEngine;
using CygSoft.Qik.LanguageEngine.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.Qik
{
    public class QikFile : IPersistableTarget
    {
        public event EventHandler ContentSaved;
        public event EventHandler ContentClosed;
        public event EventHandler ContentDeleted;
        public event EventHandler ContentReverted;
        public event EventHandler BeforeContentSaved;

        public event EventHandler<DocumentEventArgs> DocumentAdded;
        public event EventHandler<DocumentEventArgs> DocumentRemoved;
        public event EventHandler<DocumentEventArgs> DocumentMovedLeft;
        public event EventHandler<DocumentEventArgs> DocumentMovedRight;

        private IKeywordIndexItem indexItem;
        private QikDocumentIndex documentIndex = null;

        public QikFile(QikKeywordIndexItem indexItem, string folderPath)
        {
            this.indexItem = indexItem;
            this.Compiler = new Compiler();
            this.documentIndex = new QikDocumentIndex(folderPath, indexItem.Id);
            this.documentIndex.DocumentAdded += documentIndex_DocumentAdded;
            this.documentIndex.DocumentRemoved += documentIndex_DocumentRemoved;
            this.documentIndex.DocumentMovedUp += documentIndex_DocumentMovedLeft;
            this.documentIndex.DocumentMovedDown += documentIndex_DocumentMovedRight;
        }

        private void documentIndex_DocumentMovedRight(object sender, DocumentEventArgs e)
        {
            if (DocumentMovedRight != null)
                DocumentMovedRight(this, e);
        }

        private void documentIndex_DocumentMovedLeft(object sender, DocumentEventArgs e)
        {
            if (DocumentMovedLeft != null)
                DocumentMovedLeft(this, e);
        }

        private void documentIndex_DocumentRemoved(object sender, DocumentEventArgs e)
        {
            if (DocumentRemoved != null)
                DocumentRemoved(this, e);
        }

        private void documentIndex_DocumentAdded(object sender, DocumentEventArgs e)
        {
            if (DocumentAdded != null)
                DocumentAdded(this, e);
        }

        public IKeywordIndexItem IndexItem
        {
            get { return this.indexItem; }
        }

        public ICompiler Compiler { get; private set; }

        public string Text { get; set; }
        public ICodeDocument[] Documents { get { return this.documentIndex.DocumentFiles.OfType<ICodeDocument>().ToArray(); } }

        public string Id { get { return this.IndexItem.Id; } }
        public string FilePath { get { return this.documentIndex.FilePath; } }
        public string FileTitle { get { return this.documentIndex.FileName; } }
        public string FolderPath { get { return this.documentIndex.Folder; } }

        public bool FileExists { get { return this.documentIndex.Exists; } }

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
                    return (this.indexItem as QikKeywordIndexItem).Syntax;
                return null;
            }
            set
            {
                QikKeywordIndexItem indexItem = this.indexItem as QikKeywordIndexItem;
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

        public bool TemplateExists(string id)
        {
            return this.documentIndex.DocumentExists(id);
        }

        public ICodeDocument GetTemplate(string id)
        {
            return this.documentIndex.GetDocumentFile(id) as ICodeDocument;
        }

        public ICodeDocument ScriptFile { get { return this.documentIndex.ScriptDocument; } }

        public void Revert()
        {
            if (Open())
            {
                if (ContentReverted != null)
                    ContentReverted(this, new EventArgs());
            }
        }

        public bool Open()
        {
            this.documentIndex.Open();
            return true;
        }

        public void Close()
        {
            if (ContentClosed != null)
                ContentClosed(this, new EventArgs());
        }

        public void Save()
        {
            if (BeforeContentSaved != null)
                BeforeContentSaved(this, new EventArgs());

            this.documentIndex.Save();
            
            if (this.ContentSaved != null)
                ContentSaved(this, new EventArgs());
        }

        public void Delete()
        {
            this.documentIndex.Delete();
            if (ContentDeleted != null)
                ContentDeleted(this, new EventArgs());
        }

        public void MoveDocumentRight(string id)
        {
            IDocument document = this.documentIndex.GetDocumentFile(id);
            
            // can't move the script file and we can't move behind the script file.
            if (document.Id != this.ScriptFile.Id && !IsSecondLast(document.Id))
                this.documentIndex.MoveDown(document);
        }

        public void MoveDocumentLeft(string id)
        {
            IDocument document = this.documentIndex.GetDocumentFile(id);
            if (document.Id != this.ScriptFile.Id)
                this.documentIndex.MoveUp(document);
        }

        public ICodeDocument AddTemplate(string syntax)
        {
            return this.documentIndex.AddDocumentFile(DocumentFactory.CreateCodeDocument(documentIndex.Folder, "New Template", "tpl", syntax)) as ICodeDocument;
        }

        public void RemoveTemplate(string id)
        {
            this.documentIndex.RemoveDocumentFile(id);
        }

        private void IncrementHitCount()
        {
            this.HitCount++;
        }

        private bool IsSecondLast(string id)
        {
            int count = documentIndex.DocumentFiles.Count();

            if (count == 2)
                return true;
            else if (documentIndex.DocumentFiles.ElementAt(count - 2).Id == id)
            {
                return true;
            }
            return false;
        }
    }
}
