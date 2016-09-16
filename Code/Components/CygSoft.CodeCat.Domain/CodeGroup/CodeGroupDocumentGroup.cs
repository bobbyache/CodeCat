using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.CodeGroup
{
    public class CodeGroupDocumentGroup : IPersistableTarget, ICodeGroupDocumentGroup
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
        private CodeGroupIndex documentIndex = null;

        public CodeGroupDocumentGroup(CodeGroupKeywordIndexItem indexItem, string folderPath)
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
        public ICodeDocument[] Documents { get { return this.documentIndex.DocumentFiles.OfType<ICodeDocument>().ToArray(); } }

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

        public bool DocumentExists(string id)
        {
            return this.documentIndex.DocumentExists(id);
        }

        public ICodeDocument GetDocument(string id)
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
            IDocument document = this.documentIndex.GetDocumentFile(id);
            this.documentIndex.MoveDown(document);
        }

        public void MoveDocumentLeft(string id)
        {
            IDocument document = this.documentIndex.GetDocumentFile(id);
            this.documentIndex.MoveUp(document);
        }

        public ICodeDocument AddDocument(string syntax)
        {
            return this.documentIndex.AddDocumentFile(DocumentFactory.CreateCodeDocument(documentIndex.Folder, 
                "New Document", "txt", syntax)) as ICodeDocument;
        }

        public void RemoveDocument(string id)
        {
            this.documentIndex.RemoveDocumentFile(id);
        }

        private void IncrementHitCount()
        {
            this.HitCount++;
        }

        private void documentIndex_AfterOpen(object sender, FileEventArgs e)
        {
            if (AfterOpen != null)
                AfterOpen(this, e);
        }

        private void documentIndex_BeforeOpen(object sender, FileEventArgs e)
        {
            if (BeforeOpen != null)
                BeforeOpen(this, e);
        }

        private void documentIndex_BeforeRevert(object sender, FileEventArgs e)
        {
            if (BeforeRevert != null)
                BeforeRevert(this, e);
        }

        private void documentIndex_AfterRevert(object sender, FileEventArgs e)
        {
            if (AfterRevert != null)
                AfterRevert(this, e);
        }

        private void documentIndex_AfterDelete(object sender, FileEventArgs e)
        {
            if (AfterDelete != null)
                AfterDelete(this, e);
        }

        private void documentIndex_BeforeDelete(object sender, FileEventArgs e)
        {
            if (BeforeDelete != null)
                BeforeDelete(this, e);
        }

        private void documentIndex_AfterClose(object sender, FileEventArgs e)
        {
            if (AfterClose != null)
                AfterClose(this, e);
        }

        private void documentIndex_BeforeClose(object sender, FileEventArgs e)
        {
            if (BeforeClose != null)
                BeforeClose(this, e);
        }


        private void documentIndex_AfterSave(object sender, FileEventArgs e)
        {
            if (AfterSave != null)
                AfterSave(this, e);
        }

        private void documentIndex_BeforeSave(object sender, FileEventArgs e)
        {
            if (BeforeSave != null)
                BeforeSave(this, e);
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

    }
}
