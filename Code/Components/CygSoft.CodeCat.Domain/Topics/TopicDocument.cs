﻿using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.IO;
using System.Linq;

namespace CygSoft.CodeCat.Domain.Topics
{
    public class TopicDocument : IPersistableTarget, ITopicDocument
    {
        public event EventHandler<TopicEventArgs> BeforeDelete;
        public event EventHandler<TopicEventArgs> AfterDelete;
        public event EventHandler<TopicEventArgs> BeforeOpen;
        public event EventHandler<TopicEventArgs> AfterOpen;
        public event EventHandler<TopicEventArgs> BeforeSave;
        public event EventHandler<TopicEventArgs> AfterSave;
        public event EventHandler<TopicEventArgs> BeforeClose;
        public event EventHandler<TopicEventArgs> AfterClose;
        public event EventHandler<TopicEventArgs> BeforeRevert;
        public event EventHandler<TopicEventArgs> AfterRevert;

        public event EventHandler<TopicSectionEventArgs> TopicSectionAdded;
        public event EventHandler<TopicSectionEventArgs> TopicSectionRemoved;
        public event EventHandler<TopicSectionEventArgs> TopicSectionMovedLeft;
        public event EventHandler<TopicSectionEventArgs> TopicSectionMovedRight;

        private IKeywordIndexItem indexItem;
        private TopicIndex documentIndex = null;

        public TopicDocument(ITopicKeywordIndexItem indexItem, string folderPath)
        {
            this.indexItem = indexItem;

            DocumentIndexPathGenerator indexPathGenerator = new DocumentIndexPathGenerator(folderPath, "xml", indexItem.Id);
            IDocumentIndexRepository repository = new TopicIndexXmlRepository(indexPathGenerator);

            this.documentIndex = new TopicIndex(repository, indexPathGenerator);

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

        public string Text { get; set; }

        // would like to remove this at some point see... TemplateFiles property below...
        public ITopicSection[] TopicSections { get { return this.documentIndex.TopicSections.ToArray(); } }

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
                    return (this.indexItem as TopicKeywordIndexItem).Syntax;
                return null;
            }
            set
            {
                TopicKeywordIndexItem indexItem = this.indexItem as TopicKeywordIndexItem;
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

        public bool TopicSectionExists(string id)
        {
            return this.documentIndex.TopicSectionExists(id);
        }

        public ITopicSection GetTopicSection(string id)
        {
            return this.documentIndex.GetTopicSection(id) as ICodeTopicSection;
        }


        public void Revert()
        {
            this.documentIndex.Revert();
        }

        public virtual void Open()
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

        public void MoveTopicSectionRight(string id)
        {
            ITopicSection topicSection = this.documentIndex.GetTopicSection(id);
            this.documentIndex.MoveDown(topicSection);
        }

        public void MoveTopicSectionLeft(string id)
        {
            ITopicSection topicSection = this.documentIndex.GetTopicSection(id);
            this.documentIndex.MoveUp(topicSection);
        }

        public ITopicSection AddTopicSection(TopicSectionType documentType, string syntax = null, string extension = "txt")
        {
            return this.documentIndex.AddTopicSection(TopicSectionFactory.Create(documentType, documentIndex.Folder,
                "New Document", null, 0, null, extension, syntax));
        }

        public void RemoveTopicSection(string id)
        {
            this.documentIndex.RemoveTopicSection(id);
        }

        private void IncrementHitCount()
        {
            this.HitCount++;
        }

        private void documentIndex_AfterOpen(object sender, TopicEventArgs e)
        {
            AfterOpen?.Invoke(this, e);
        }

        private void documentIndex_BeforeOpen(object sender, TopicEventArgs e)
        {
            BeforeOpen?.Invoke(this, e);
        }

        private void documentIndex_BeforeRevert(object sender, TopicEventArgs e)
        {
            BeforeRevert?.Invoke(this, e);
        }

        private void documentIndex_AfterRevert(object sender, TopicEventArgs e)
        {
            AfterRevert?.Invoke(this, e);
        }

        private void documentIndex_AfterDelete(object sender, TopicEventArgs e)
        {
            AfterDelete?.Invoke(this, e);
        }

        private void documentIndex_BeforeDelete(object sender, TopicEventArgs e)
        {
            BeforeDelete?.Invoke(this, e);
        }

        private void documentIndex_AfterClose(object sender, TopicEventArgs e)
        {
            AfterClose?.Invoke(this, e);
        }

        private void documentIndex_BeforeClose(object sender, TopicEventArgs e)
        {
            BeforeClose?.Invoke(this, e);
        }

        private void documentIndex_AfterSave(object sender, TopicEventArgs e)
        {
            AfterSave?.Invoke(this, e);
        }

        private void documentIndex_BeforeSave(object sender, TopicEventArgs e)
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