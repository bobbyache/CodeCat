using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CygSoft.CodeCat.DocumentManager.Base
{
    public abstract class Topic : ITopic
    {
        public event EventHandler<TopicSectionEventArgs> TopicSectionAdded;
        public event EventHandler<TopicSectionEventArgs> TopicSectionRemoved;
        public event EventHandler<TopicSectionEventArgs> TopicSectionMovedUp;
        public event EventHandler<TopicSectionEventArgs> TopicSectionMovedDown;

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

        public string Id { get; private set; }
        public string FilePath { get; protected set; }
        public string FileName { get; private set; }
        public string FileExtension { get; private set; }
        protected BaseFilePathGenerator filePathGenerator;

        public virtual string Folder
        {
            get { return Path.GetDirectoryName(this.FilePath); }
        }

        public virtual bool FolderExists
        {
            get { return Directory.Exists(Path.GetDirectoryName(this.FilePath)); }
        }

        public bool Exists { get { return File.Exists(this.FilePath); } }
        public bool Loaded { get; private set; }

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

        public Topic(IDocumentIndexRepository indexRepository, BaseFilePathGenerator filePathGenerator)
        {
            this.indexRepository = indexRepository;
            this.filePathGenerator = filePathGenerator;
            this.Id = filePathGenerator.Id;
            this.FileExtension = filePathGenerator.FileExtension;
            this.FilePath = filePathGenerator.FilePath;
            this.FileName = filePathGenerator.FileName;
        }

        protected virtual void OnBeforeDelete() { }
        protected virtual void OnBeforeOpen() { }
        protected virtual void OnAfterOpen() { }
        protected virtual void OnBeforeSave() { }
        protected virtual void OnAfterSave() { }
        protected virtual void OnClose() { }

        protected virtual void OpenFile()
        {
            try
            {
                this.OpenTopicSections();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        protected virtual void SaveFile()
        {
            try
            {
                // save the document index (abstract, must be implemented).
                SaveDocumentIndex();
                // saves all the physical document files.
                SaveTopicSections();
                DeleteRemovedTopicSections();
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }

        public bool TopicSectionExists(string id)
        {
            return this.topicSections.ItemsList.Exists(r => r.Id == id);

        }

        // IDocumentFile could be of a different type, so it needs to be created
        // elsewhere such as a IDocumentFile factory.
        public ITopicSection AddTopicSection(ITopicSection topicSection)
        {
            try
            {
                this.topicSections.Insert(topicSection);
                AfterAddTopicSection();
                TopicSectionAdded?.Invoke(this, new TopicSectionEventArgs(topicSection));

                return topicSection;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        protected virtual void AfterAddTopicSection() { }

        public void RemoveTopicSection(string id)
        {
            try
            {
                ITopicSection topicSection = this.topicSections.ItemsList.Where(f => f.Id == id).SingleOrDefault();
                removedTopicSections.Add(topicSection);
                topicSections.Remove(topicSection);
                topicSection.Ordinal = -1;

                TopicSectionRemoved?.Invoke(this, new TopicSectionEventArgs(topicSection));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        protected virtual void OnAfterDelete()
        {
            DeleteTopicSections();
            if (Directory.Exists(this.Folder))
                Directory.Delete(this.Folder);
        }

        protected virtual void OnBeforeRevert()
        {
            foreach (ITopicSection topicSection in this.TopicSections)
                topicSection.Revert();
        }

        protected virtual void OnAfterRevert()
        {
            this.removedTopicSections.Clear();
        }

        public ITopicSection GetTopicSection(string id)
        {
            try
            {
                return this.topicSections.ItemsList.Where(f => f.Id == id).SingleOrDefault();
            }
            catch (Exception exception)
            {
                throw exception;
            }
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

        public void Open()
        {
            try
            {
                BeforeOpen?.Invoke(this, new TopicEventArgs(this));
                OnBeforeOpen();
                OpenFile();
                OnAfterOpen();

                this.Loaded = true;
                AfterOpen?.Invoke(this, new TopicEventArgs(this));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Revert()
        {
            try
            {
                BeforeRevert?.Invoke(this, new TopicEventArgs(this));
                OnBeforeRevert();

                if (File.Exists(this.FilePath))
                    OpenFile();
                OnAfterRevert();

                this.Loaded = true;
                AfterRevert?.Invoke(this, new TopicEventArgs(this));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Delete()
        {
            BeforeDelete?.Invoke(this, new TopicEventArgs(this));
            OnBeforeDelete();

            if (File.Exists(this.FilePath))
                File.Delete(this.FilePath);

            OnAfterDelete();

            this.Loaded = false;
            AfterDelete?.Invoke(this, new TopicEventArgs(this));
        }

        public void Save()
        {
            try
            {
                BeforeSave?.Invoke(this, new TopicEventArgs(this));
                OnBeforeSave();
                SaveFile();
                OnAfterSave();
                this.Loaded = true;
                AfterSave?.Invoke(this, new TopicEventArgs(this));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void Close()
        {
            BeforeClose?.Invoke(this, new TopicEventArgs(this));
            this.OnClose();
            this.Loaded = false;
            AfterClose?.Invoke(this, new TopicEventArgs(this));
        }

        private void OpenTopicSections()
        {
            try
            {
                List<ITopicSection> topicSections = LoadTopicSections();

                foreach (ITopicSection topicSection in topicSections)
                    topicSection.Open();

                this.topicSections.InitializeList(topicSections);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SaveTopicSections()
        {
            foreach (ITopicSection topicSection in this.TopicSections)
                topicSection.Save();
        }

        private void DeleteTopicSections()
        {
            try
            {
                foreach (ITopicSection topicSection in this.topicSections.ItemsList)
                {
                    topicSection.Delete();
                }
                topicSections.Clear();

                foreach (ITopicSection topicSection in this.removedTopicSections)
                {
                    topicSection.Delete();
                }
                removedTopicSections.Clear();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void DeleteRemovedTopicSections()
        {
            foreach (ITopicSection topicSection in removedTopicSections)
            {
                topicSection.Delete();
            }
            removedTopicSections.Clear();
        }
    }
}
