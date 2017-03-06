using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using CygSoft.Qik.LanguageEngine;
using CygSoft.Qik.LanguageEngine.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CygSoft.CodeCat.Domain.Qik
{
    internal class QikTemplateDocumentSet : IPersistableTarget, IQikTemplateDocumentSet
    {
        public event EventHandler<FileEventArgs> BeforeDelete;
        public event EventHandler<FileEventArgs> AfterDelete;
        public event EventHandler<FileEventArgs> BeforeOpen;
        public event EventHandler<FileEventArgs> AfterOpen;
        public event EventHandler<FileEventArgs> BeforeSave;
        public event EventHandler<FileEventArgs> AfterSave;
        public event EventHandler<FileEventArgs> BeforeClose;
        public event EventHandler<FileEventArgs> AfterClose;
        public event EventHandler<FileEventArgs> BeforeRevert;
        public event EventHandler<FileEventArgs> AfterRevert;

        public event EventHandler<DocumentEventArgs> DocumentAdded;
        public event EventHandler<DocumentEventArgs> DocumentRemoved;
        public event EventHandler<DocumentEventArgs> DocumentMovedLeft;
        public event EventHandler<DocumentEventArgs> DocumentMovedRight;

        private IKeywordIndexItem indexItem;
        private QikTemplateDocumentIndex documentIndex = null;

        public QikTemplateDocumentSet(QikTemplateKeywordIndexItem indexItem, string folderPath)
        {
            this.indexItem = indexItem;
            this.Compiler = new Compiler();

            DocumentIndexPathGenerator indexPathGenerator = new DocumentIndexPathGenerator(folderPath, "xml", indexItem.Id);
            IDocumentIndexRepository repository = new QikTemplateDocumentIndexXmlRepository(indexPathGenerator);

            this.documentIndex = new QikTemplateDocumentIndex(repository, indexPathGenerator);

            this.documentIndex.BeforeSave += documentIndex_BeforeSave;
            this.documentIndex.AfterSave += documentIndex_AfterSave;
            this.documentIndex.BeforeClose += documentIndex_BeforeClose;
            this.documentIndex.AfterClose += documentIndex_AfterClose;
            this.documentIndex.BeforeDelete += documentIndex_BeforeDelete;
            this.documentIndex.AfterDelete += documentIndex_AfterDelete;
            this.documentIndex.BeforeRevert += documentIndex_BeforeRevert;
            this.documentIndex.AfterRevert += documentIndex_AfterRevert;
            this.documentIndex.BeforeOpen += documentIndex_BeforeOpen;
            this.documentIndex.AfterOpen += documentIndex_AfterOpen;
            
            this.documentIndex.DocumentAdded += documentIndex_DocumentAdded;
            this.documentIndex.DocumentRemoved += documentIndex_DocumentRemoved;
            this.documentIndex.DocumentMovedUp += documentIndex_DocumentMovedLeft;
            this.documentIndex.DocumentMovedDown += documentIndex_DocumentMovedRight;
        }

        public IKeywordIndexItem IndexItem
        {
            get { return this.indexItem; }
        }

        public ICompiler Compiler { get; private set; }

        public string Text { get; set; }

        // would like to remove this at some point see... TemplateFiles property below...
        public ICodeDocument[] Documents { get { return this.documentIndex.DocumentFiles.OfType<ICodeDocument>().ToArray(); } }

        public ICodeDocument[] TemplateFiles
        {
            get
            {
                List<ICodeDocument> codeDocuments = this.documentIndex.DocumentFiles.OfType<ICodeDocument>().ToList();
                IQikScriptDocument scriptDoc = codeDocuments.OfType<IQikScriptDocument>().SingleOrDefault();
                codeDocuments.Remove(scriptDoc);
                return codeDocuments.ToArray();
            }
        }

        public string Id { get { return this.IndexItem.Id; } }
        public string FilePath { get { return this.documentIndex.FilePath; } }
        public bool Exists { get { return this.documentIndex.Exists; } }

        public string FileName
        {
            get { return this.documentIndex.FileName; }
        }

        public string FileExtension
        {
            get { return this.documentIndex.FileExtension; }
        }

        public string Folder
        {
            get { return this.documentIndex.Folder; }
        }

        public virtual bool FolderExists
        {
            get { return Directory.Exists(Path.GetDirectoryName(this.FilePath)); }
        }

        public bool Loaded
        {
            get { return this.documentIndex.Loaded; }
        }

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
                    return (this.indexItem as QikTemplateKeywordIndexItem).Syntax;
                return null;
            }
            set
            {
                QikTemplateKeywordIndexItem indexItem = this.indexItem as QikTemplateKeywordIndexItem;
                if (indexItem != null)
                    indexItem.Syntax = value;
            }
        }

        public string CommaDelimitedKeywords
        {
            get { return this.IndexItem.CommaDelimitedKeywords; }
            set { this.IndexItem.SetKeywords(value); }
        }

        public int HitCount { get; private set; }

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
            this.documentIndex.Revert();
        }

        public void Open()
        {
            this.documentIndex.Open();
        }

        public void Close()
        {
            this.documentIndex.Close();
        }

        public void Save()
        {
            this.documentIndex.Save();
        }

        public void Delete()
        {
            this.documentIndex.Delete();
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
            return this.documentIndex.AddDocumentFile(DocumentFactory.Create(DocumentTypeEnum.CodeSnippet, documentIndex.Folder, 
                "New Template", null, 0, null, "tpl", syntax)) as ICodeDocument;
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
                return true;
            
            return false;
        }

        private void documentIndex_AfterOpen(object sender, FileEventArgs e)
        {
            AfterOpen?.Invoke(this, e);
        }

        private void documentIndex_BeforeOpen(object sender, FileEventArgs e)
        {
            BeforeOpen?.Invoke(this, e);
        }

        private void documentIndex_BeforeRevert(object sender, FileEventArgs e)
        {
            BeforeRevert?.Invoke(this, e);
        }

        private void documentIndex_AfterRevert(object sender, FileEventArgs e)
        {
            AfterRevert?.Invoke(this, e);
        }

        private void documentIndex_AfterDelete(object sender, FileEventArgs e)
        {
            AfterDelete?.Invoke(this, e);
        }

        private void documentIndex_BeforeDelete(object sender, FileEventArgs e)
        {
            BeforeDelete?.Invoke(this, e);
        }

        private void documentIndex_AfterClose(object sender, FileEventArgs e)
        {
            AfterClose?.Invoke(this, e);
        }

        private void documentIndex_BeforeClose(object sender, FileEventArgs e)
        {
            BeforeClose?.Invoke(this, e);
        }

        private void documentIndex_AfterSave(object sender, FileEventArgs e)
        {
            AfterSave?.Invoke(this, e);
        }

        private void documentIndex_BeforeSave(object sender, FileEventArgs e)
        {
            BeforeSave?.Invoke(this, e);
        }

        private void documentIndex_DocumentMovedRight(object sender, DocumentEventArgs e)
        {
            DocumentMovedRight?.Invoke(this, e);
        }

        private void documentIndex_DocumentMovedLeft(object sender, DocumentEventArgs e)
        {
            DocumentMovedLeft?.Invoke(this, e);
        }

        private void documentIndex_DocumentRemoved(object sender, DocumentEventArgs e)
        {
            DocumentRemoved?.Invoke(this, e);
        }

        private void documentIndex_DocumentAdded(object sender, DocumentEventArgs e)
        {
            DocumentAdded?.Invoke(this, e);
        }
    }
}
