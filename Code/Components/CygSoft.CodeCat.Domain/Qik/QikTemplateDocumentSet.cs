using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.TopicSections;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using CygSoft.CodeCat.Qik.LanguageEngine;
using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CygSoft.CodeCat.Domain.Qik
{
    internal class QikTemplateDocumentSet : BaseFile, IQikTemplateDocumentSet
    {
        public event EventHandler<TopicSectionEventArgs> TopicSectionAdded;
        public event EventHandler<TopicSectionEventArgs> TopicSectionRemoved;
        public event EventHandler<TopicSectionEventArgs> TopicSectionMovedLeft;
        public event EventHandler<TopicSectionEventArgs> TopicSectionMovedRight;

        private IKeywordIndexItem indexItem;
        private QikTemplateDocumentIndex documentIndex = null;

        public QikTemplateDocumentSet(DocumentIndexPathGenerator filePathGenerator, IQikTemplateKeywordIndexItem indexItem)
            : base(filePathGenerator)
        {
            this.indexItem = indexItem;
            this.Compiler = new Compiler();

            IDocumentIndexRepository repository = new QikTemplateDocumentIndexXmlRepository(filePathGenerator);

            this.documentIndex = new QikTemplateDocumentIndex(repository, filePathGenerator);
            
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

        public bool TemplateExists(string id)
        {
            return this.documentIndex.TopicSectionExists(id);
        }

        public ICodeTopicSection GetTemplateSection(string id)
        {
            return this.documentIndex.GetTopicSection(id) as ICodeTopicSection;
        }

        public ICodeTopicSection ScriptSection { get { return this.documentIndex.QikScriptSection; } }

        protected override void OnRevert()
        {
            this.documentIndex.Revert();
        }

        protected override void OnOpen()
        {
            this.documentIndex.Open();
        }

        protected override void OnClose()
        {
            this.documentIndex.Close();
        }

        protected override void OnSave()
        {
            this.documentIndex.Save();
        }

        protected override void OnDelete()
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

        private bool IsSecondLast(string id)
        {
            int count = documentIndex.TopicSections.Count();

            if (count == 2)
                return true;

            else if (documentIndex.TopicSections.ElementAt(count - 2).Id == id)
                return true;
            
            return false;
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
