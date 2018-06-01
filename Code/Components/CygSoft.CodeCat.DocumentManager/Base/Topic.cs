using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CygSoft.CodeCat.DocumentManager.Base
{
    public abstract class Topic : BaseFile, ITopic
    {
        public event EventHandler<TopicSectionEventArgs> TopicSectionAdded;
        public event EventHandler<TopicSectionEventArgs> TopicSectionRemoved;
        public event EventHandler<TopicSectionEventArgs> TopicSectionMovedUp;
        public event EventHandler<TopicSectionEventArgs> TopicSectionMovedDown;

        public string Id { get; private set; }

        protected PositionableList<ITopicSection> topicSections = new PositionableList<ITopicSection>();
        private List<ITopicSection> removedTopicSections = new List<ITopicSection>();
        protected IDocumentIndexRepository indexRepository;

        public ITopicSection[] TopicSections
        {
            get { return topicSections.ItemsList.ToArray(); }
        }

        public ITopicSection FirstTopicSection
        {
            get { return topicSections.FirstItem; }
        }

        public ITopicSection LastTopicSection
        {
            get { return topicSections.LastItem; }
        }

        protected abstract List<ITopicSection> LoadTopicSections();
        protected abstract void SaveDocumentIndex();

        public Topic(IDocumentIndexRepository indexRepository, BaseFilePathGenerator filePathGenerator) : base(filePathGenerator)
        {
            this.indexRepository = indexRepository;
            this.Id = filePathGenerator.Id;
        }

        protected override void OnOpen()
        {
            this.OpenTopicSections();
        }

        protected override void SaveFile()
        {
            // save the document index (abstract, must be implemented).
            SaveDocumentIndex();
            // saves all the physical document files.
            SaveTopicSections();
            DeleteRemovedTopicSections();
        }

        public bool TopicSectionExists(string id)
        {
            return this.topicSections.ItemsList.Exists(r => r.Id == id);
        }

        // IDocumentFile could be of a different type, so it needs to be created
        // elsewhere such as a IDocumentFile factory.
        public ITopicSection AddTopicSection(ITopicSection topicSection)
        {
            this.topicSections.Insert(topicSection);
            AfterAddTopicSection();
            TopicSectionAdded?.Invoke(this, new TopicSectionEventArgs(topicSection));

            return topicSection;
        }

        protected virtual void AfterAddTopicSection() { }

        public void RemoveTopicSection(string id)
        {
            ITopicSection topicSection = this.topicSections.ItemsList.Where(f => f.Id == id).SingleOrDefault();
            removedTopicSections.Add(topicSection);
            topicSections.Remove(topicSection);
            topicSection.Ordinal = -1;

            TopicSectionRemoved?.Invoke(this, new TopicSectionEventArgs(topicSection));
        }

        protected override void OnAfterDelete()
        {
            DeleteTopicSections();
            if (Directory.Exists(this.Folder))
                Directory.Delete(this.Folder, true);
        }

        protected override void OnBeforeRevert()
        {
            foreach (ITopicSection topicSection in this.TopicSections)
                topicSection.Revert();
        }

        protected override void OnAfterRevert()
        {
            this.removedTopicSections.Clear();
        }

        public ITopicSection GetTopicSection(string id)
        {
            return this.topicSections.ItemsList.Where(f => f.Id == id).SingleOrDefault();
        }

        public bool CanMoveDown(ITopicSection topicSection)
        {
            return topicSections.CanMoveDown(topicSection);
        }

        public bool CanMoveTo(ITopicSection topicSection, int ordinal)
        {
            return topicSections.CanMoveTo(topicSection, ordinal);
        }

        public bool CanMoveUp(ITopicSection topicSection)
        {
            return topicSections.CanMoveUp(topicSection);
        }

        public virtual void MoveDown(ITopicSection topicSection)
        {
            topicSections.MoveDown(topicSection);
            TopicSectionMovedDown?.Invoke(this, new TopicSectionEventArgs(topicSection));
        }

        public void MoveTo(ITopicSection topicSection, int ordinal)
        {
            topicSections.MoveTo(topicSection, ordinal);
        }

        public virtual void MoveUp(ITopicSection topicSection)
        {
            topicSections.MoveUp(topicSection);
            TopicSectionMovedUp?.Invoke(this, new TopicSectionEventArgs(topicSection));
        }

        public void MoveLast(ITopicSection topicSection)
        {
            topicSections.MoveLast(topicSection);
        }

        public void MoveFirst(ITopicSection topicSection)
        {
            topicSections.MoveFirst(topicSection);
        }

        private void OpenTopicSections()
        {
            List<ITopicSection> topicSections = LoadTopicSections();

            foreach (ITopicSection topicSection in topicSections)
                topicSection.Open();

            this.topicSections.InitializeList(topicSections);
        }

        private void SaveTopicSections()
        {
            foreach (ITopicSection topicSection in this.TopicSections)
                topicSection.Save();
        }

        private void DeleteTopicSections()
        {
            foreach (ITopicSection topicSection in this.topicSections.ItemsList)
                topicSection.Delete();
            
            topicSections.Clear();

            foreach (ITopicSection topicSection in this.removedTopicSections)
                topicSection.Delete();
            
            removedTopicSections.Clear();
        }

        private void DeleteRemovedTopicSections()
        {
            foreach (ITopicSection topicSection in removedTopicSections)
                topicSection.Delete();
            
            removedTopicSections.Clear();
        }
    }
}
