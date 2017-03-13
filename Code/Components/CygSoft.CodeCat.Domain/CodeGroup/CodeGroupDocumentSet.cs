using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.IO;
using System.Linq;

namespace CygSoft.CodeCat.Domain.CodeGroup
{
    public class CodeGroupDocumentSet : IPersistableTarget, ICodeGroupDocumentSet
    {
        public event EventHandler<DocumentIndexEventArgs> BeforeDelete;
        public event EventHandler<DocumentIndexEventArgs> AfterDelete;
        public event EventHandler<DocumentIndexEventArgs> BeforeOpen;
        public event EventHandler<DocumentIndexEventArgs> AfterOpen;
        public event EventHandler<DocumentIndexEventArgs> BeforeSave;
        public event EventHandler<DocumentIndexEventArgs> AfterSave;
        public event EventHandler<DocumentIndexEventArgs> BeforeClose;
        public event EventHandler<DocumentIndexEventArgs> AfterClose;
        public event EventHandler<DocumentIndexEventArgs> BeforeRevert;
        public event EventHandler<DocumentIndexEventArgs> AfterRevert;

        public event EventHandler<DocumentEventArgs> DocumentAdded;
        public event EventHandler<DocumentEventArgs> DocumentRemoved;
        public event EventHandler<DocumentEventArgs> DocumentMovedLeft;
        public event EventHandler<DocumentEventArgs> DocumentMovedRight;

        private IKeywordIndexItem indexItem;
        private CodeGroupIndex documentIndex = null;

        public CodeGroupDocumentSet(CodeGroupKeywordIndexItem indexItem, string folderPath)
        {
            this.indexItem = indexItem;

            DocumentIndexPathGenerator indexPathGenerator = new DocumentIndexPathGenerator(folderPath, "xml", indexItem.Id);
            IDocumentIndexRepository repository = new CodeGroupIndexXmlRepository(indexPathGenerator);

            this.documentIndex = new CodeGroupIndex(repository, indexPathGenerator);

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

        public string Text { get; set; }

        // would like to remove this at some point see... TemplateFiles property below...
        public ITopicSection[] Documents { get { return this.documentIndex.DocumentFiles.ToArray(); } }

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
                    return (this.indexItem as CodeGroupKeywordIndexItem).Syntax;
                return null;
            }
            set
            {
                CodeGroupKeywordIndexItem indexItem = this.indexItem as CodeGroupKeywordIndexItem;
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

        public bool DocumentExists(string id)
        {
            return this.documentIndex.DocumentExists(id);
        }

        public ITopicSection GetDocument(string id)
        {
            return this.documentIndex.GetDocumentFile(id) as ICodeDocument;
        }


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
            ITopicSection topicSection = this.documentIndex.GetDocumentFile(id);
            this.documentIndex.MoveDown(topicSection);
        }

        public void MoveDocumentLeft(string id)
        {
            ITopicSection topicSection = this.documentIndex.GetDocumentFile(id);
            this.documentIndex.MoveUp(topicSection);
        }

        public ITopicSection AddDocument(DocumentTypeEnum documentType, string syntax = null, string extension = "txt")
        {
            return this.documentIndex.AddDocumentFile(DocumentFactory.Create(documentType, documentIndex.Folder,
                "New Document", null, 0, null, extension, syntax));
        }

        public void RemoveDocument(string id)
        {
            this.documentIndex.RemoveDocumentFile(id);
        }

        private void IncrementHitCount()
        {
            this.HitCount++;
        }

        private void documentIndex_AfterOpen(object sender, DocumentIndexEventArgs e)
        {
            AfterOpen?.Invoke(this, e);
        }

        private void documentIndex_BeforeOpen(object sender, DocumentIndexEventArgs e)
        {
            BeforeOpen?.Invoke(this, e);
        }

        private void documentIndex_BeforeRevert(object sender, DocumentIndexEventArgs e)
        {
            BeforeRevert?.Invoke(this, e);
        }

        private void documentIndex_AfterRevert(object sender, DocumentIndexEventArgs e)
        {
            AfterRevert?.Invoke(this, e);
        }

        private void documentIndex_AfterDelete(object sender, DocumentIndexEventArgs e)
        {
            AfterDelete?.Invoke(this, e);
        }

        private void documentIndex_BeforeDelete(object sender, DocumentIndexEventArgs e)
        {
            BeforeDelete?.Invoke(this, e);
        }

        private void documentIndex_AfterClose(object sender, DocumentIndexEventArgs e)
        {
            AfterClose?.Invoke(this, e);
        }

        private void documentIndex_BeforeClose(object sender, DocumentIndexEventArgs e)
        {
            BeforeClose?.Invoke(this, e);
        }

        private void documentIndex_AfterSave(object sender, DocumentIndexEventArgs e)
        {
            AfterSave?.Invoke(this, e);
        }

        private void documentIndex_BeforeSave(object sender, DocumentIndexEventArgs e)
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
