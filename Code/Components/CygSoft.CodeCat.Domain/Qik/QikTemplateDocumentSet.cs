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

        public event EventHandler<TopicSectionEventArgs> TopicSectionAdded;
        public event EventHandler<TopicSectionEventArgs> TopicSectionRemoved;
        public event EventHandler<TopicSectionEventArgs> TopicSectionMovedLeft;
        public event EventHandler<TopicSectionEventArgs> TopicSectionMovedRight;

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
            
            this.documentIndex.TopicSectionAdded += documentIndex_TopicSectionAdded;
            this.documentIndex.TopicSectionRemoved += documentIndex_TopicSectionRemoved;
            this.documentIndex.TopicSectionMovedUp += documentIndex_TopicSectionMovedLeft;
            this.documentIndex.TopicSectionMovedDown += documentIndex_TopicSectionMovedRight;
        }

        public IKeywordIndexItem IndexItem
        {
            get { return this.indexItem; }
        }

        public ICompiler Compiler { get; private set; }

        public string Text { get; set; }

        // would like to remove this at some point see... TemplateFiles property below...
        public ICodeTopicSection[] TopicSections { get { return this.documentIndex.TopicSections.OfType<ICodeTopicSection>().ToArray(); } }

        public ICodeTopicSection[] TemplateSections
        {
            get
            {
                List<ICodeTopicSection> codeDocuments = this.documentIndex.TopicSections.OfType<ICodeTopicSection>().ToList();
                IQikScriptTopicSection qikScriptTopicSection = codeDocuments.OfType<IQikScriptTopicSection>().SingleOrDefault();
                codeDocuments.Remove(qikScriptTopicSection);
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
            return this.documentIndex.TopicSectionExists(id);
        }

        public ICodeTopicSection GetTemplateSection(string id)
        {
            return this.documentIndex.GetTopicSection(id) as ICodeTopicSection;
        }

        public ICodeTopicSection ScriptSection { get { return this.documentIndex.QikScriptSection; } }

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

        public void MoveTemplateSectionRight(string id)
        {
            ITopicSection topicSection = this.documentIndex.GetTopicSection(id);
            
            // can't move the script file and we can't move behind the script file.
            if (topicSection.Id != this.ScriptSection.Id && !IsSecondLast(topicSection.Id))
                this.documentIndex.MoveDown(topicSection);
        }

        public void MoveTemplateSectionLeft(string id)
        {
            ITopicSection topicSection = this.documentIndex.GetTopicSection(id);
            if (topicSection.Id != this.ScriptSection.Id)
                this.documentIndex.MoveUp(topicSection);
        }

        public ICodeTopicSection AddTemplateSection(string syntax)
        {
            return this.documentIndex.AddTopicSection(TopicSectionFactory.Create(TopicSectionType.Code, documentIndex.Folder, 
                "New Template", null, 0, null, "tpl", syntax)) as ICodeTopicSection;
        }

        public void RemoveTemplateSection(string id)
        {
            this.documentIndex.RemoveTopicSection(id);
        }

        private void IncrementHitCount()
        {
            this.HitCount++;
        }

        private bool IsSecondLast(string id)
        {
            int count = documentIndex.TopicSections.Count();

            if (count == 2)
                return true;

            else if (documentIndex.TopicSections.ElementAt(count - 2).Id == id)
                return true;
            
            return false;
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

        private void documentIndex_TopicSectionMovedRight(object sender, TopicSectionEventArgs e)
        {
            TopicSectionMovedRight?.Invoke(this, e);
        }

        private void documentIndex_TopicSectionMovedLeft(object sender, TopicSectionEventArgs e)
        {
            TopicSectionMovedLeft?.Invoke(this, e);
        }

        private void documentIndex_TopicSectionRemoved(object sender, TopicSectionEventArgs e)
        {
            TopicSectionRemoved?.Invoke(this, e);
        }

        private void documentIndex_TopicSectionAdded(object sender, TopicSectionEventArgs e)
        {
            TopicSectionAdded?.Invoke(this, e);
        }
    }
}
