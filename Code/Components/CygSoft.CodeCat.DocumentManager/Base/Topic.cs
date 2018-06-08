using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Plugin.Infrastructure;
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

        protected PositionableList<IPluginControl> topicSections = new PositionableList<IPluginControl>();
        private List<IPluginControl> removedTopicSections = new List<IPluginControl>();
        protected IDocumentIndexRepository indexRepository;

        public IPluginControl[] TopicSections
        {
            get { return topicSections.ItemsList.ToArray(); }
        }

        public IPluginControl FirstTopicSection
        {
            get { return topicSections.FirstItem; }
        }

        public IPluginControl LastTopicSection
        {
            get { return topicSections.LastItem; }
        }

        protected abstract List<IPluginControl> LoadTopicSections();
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
        public IPluginControl AddTopicSection(IPluginControl topicSection)
        {
            this.topicSections.Insert(topicSection);
            AfterAddTopicSection();
            TopicSectionAdded?.Invoke(this, new TopicSectionEventArgs(topicSection));

            return topicSection;
        }

        protected virtual void AfterAddTopicSection() { }

        public void RemoveTopicSection(string id)
        {
            IPluginControl topicSection = this.topicSections.ItemsList.Where(f => f.Id == id).SingleOrDefault();
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
            foreach (IPluginControl topicSection in this.TopicSections)
                topicSection.Revert();
        }

        protected override void OnAfterRevert()
        {
            this.removedTopicSections.Clear();
        }

        public IPluginControl GetTopicSection(string id)
        {
            return this.topicSections.ItemsList.Where(f => f.Id == id).SingleOrDefault();
        }

        public bool CanMoveDown(IPluginControl topicSection)
        {
            return topicSections.CanMoveDown(topicSection);
        }

        public bool CanMoveTo(IPluginControl topicSection, int ordinal)
        {
            return topicSections.CanMoveTo(topicSection, ordinal);
        }

        public bool CanMoveUp(IPluginControl topicSection)
        {
            return topicSections.CanMoveUp(topicSection);
        }

        public virtual void MoveDown(IPluginControl topicSection)
        {
            topicSections.MoveDown(topicSection);
            TopicSectionMovedDown?.Invoke(this, new TopicSectionEventArgs(topicSection));
        }

        public void MoveTo(IPluginControl topicSection, int ordinal)
        {
            topicSections.MoveTo(topicSection, ordinal);
        }

        public virtual void MoveUp(IPluginControl topicSection)
        {
            topicSections.MoveUp(topicSection);
            TopicSectionMovedUp?.Invoke(this, new TopicSectionEventArgs(topicSection));
        }

        public void MoveLast(IPluginControl topicSection)
        {
            topicSections.MoveLast(topicSection);
        }

        public void MoveFirst(IPluginControl topicSection)
        {
            topicSections.MoveFirst(topicSection);
        }

        private void OpenTopicSections()
        {
            List<IPluginControl> topicSections = LoadTopicSections();

            foreach (IPluginControl topicSection in topicSections)
                topicSection.Open();

            this.topicSections.InitializeList(topicSections);
        }

        private void SaveTopicSections()
        {
            foreach (IPluginControl topicSection in this.TopicSections)
                topicSection.Save();
        }

        private void DeleteTopicSections()
        {
            foreach (IPluginControl topicSection in this.topicSections.ItemsList)
                topicSection.Delete();
            
            topicSections.Clear();

            foreach (IPluginControl topicSection in this.removedTopicSections)
                topicSection.Delete();
            
            removedTopicSections.Clear();
        }

        private void DeleteRemovedTopicSections()
        {
            foreach (IPluginControl topicSection in removedTopicSections)
                topicSection.Delete();
            
            removedTopicSections.Clear();
        }
    }
}
