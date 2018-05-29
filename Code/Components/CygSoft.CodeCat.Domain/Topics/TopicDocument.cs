using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Domain.TopicSections;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Linq;

namespace CygSoft.CodeCat.Domain.Topics
{
    public class TopicDocument : BaseFile, ITopicDocument
    {
        public event EventHandler<TopicSectionEventArgs> TopicSectionAdded;
        public event EventHandler<TopicSectionEventArgs> TopicSectionRemoved;
        public event EventHandler<TopicSectionEventArgs> TopicSectionMovedLeft;
        public event EventHandler<TopicSectionEventArgs> TopicSectionMovedRight;

        private IKeywordIndexItem indexItem;
        private TopicIndex documentIndex = null;

        public TopicDocument(DocumentIndexPathGenerator filePathGenerator, ITopicKeywordIndexItem indexItem) : base(filePathGenerator)
        {
            this.indexItem = indexItem;

            IDocumentIndexRepository repository = new TopicIndexXmlRepository(filePathGenerator);

            this.documentIndex = new TopicIndex(repository, filePathGenerator);
            
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

        public bool TopicSectionExists(string id)
        {
            return this.documentIndex.TopicSectionExists(id);
        }

        public ITopicSection GetTopicSection(string id)
        {
            return this.documentIndex.GetTopicSection(id) as ICodeTopicSection;
        }


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

        public ITopicSection AddTopicSection(TopicSectionType documentType, string title = "New Document", string syntax = null, string extension = "txt")
        {
            return this.documentIndex.AddTopicSection(TopicSectionFactory.Create(documentType, documentIndex.Folder,
                title, null, 0, null, extension, syntax));
        }

        public void RemoveTopicSection(string id)
        {
            this.documentIndex.RemoveTopicSection(id);
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
