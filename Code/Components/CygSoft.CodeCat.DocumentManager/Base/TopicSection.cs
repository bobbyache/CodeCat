using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.IO;

namespace CygSoft.CodeCat.DocumentManager.Base
{
    public abstract class TopicSection : ITopicSection
    {
        public event EventHandler<TopicSectionEventArgs> BeforeDelete;
        public event EventHandler<TopicSectionEventArgs> AfterDelete;
        public event EventHandler<TopicSectionEventArgs> BeforeOpen;
        public event EventHandler<TopicSectionEventArgs> AfterOpen;
        public event EventHandler<TopicSectionEventArgs> BeforeSave;
        public event EventHandler<TopicSectionEventArgs> AfterSave;
        public event EventHandler<TopicSectionEventArgs> BeforeClose;
        public event EventHandler<TopicSectionEventArgs> AfterClose;
        public event EventHandler<TopicSectionEventArgs> BeforeRevert;
        public event EventHandler<TopicSectionEventArgs> AfterRevert;

        protected BaseFilePathGenerator filePathGenerator;

        public string Id { get; private set; }
        public string FilePath { get; protected set; }
        public string FileName { get; private set; }
        public string FileExtension { get; private set; }
        public int Ordinal { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DocumentType { get; set; }
        public virtual string Folder { get { return Path.GetDirectoryName(this.FilePath); } }
        public virtual bool FolderExists { get { return Directory.Exists(Path.GetDirectoryName(this.FilePath)); } }
        public bool Exists { get { return File.Exists(this.FilePath); } }
        public bool Loaded { get; private set; }

        public TopicSection(BaseFilePathGenerator filePathGenerator, string title, string description = null)
        {
            this.Ordinal = -1;
            this.Title = title;
            this.Description = description;

            this.filePathGenerator = filePathGenerator;
            this.Id = filePathGenerator.Id;
            this.FileExtension = filePathGenerator.FileExtension;
            this.FilePath = filePathGenerator.FilePath;
            this.FileName = filePathGenerator.FileName;
        }

        public TopicSection(BaseFilePathGenerator filePathGenerator, string title, string description = null, int ordinal = -1)
        {
            this.Ordinal = -1;
            this.Title = title;
            this.Description = description;

            this.filePathGenerator = filePathGenerator;
            this.Id = filePathGenerator.Id;
            this.FileExtension = filePathGenerator.FileExtension;
            this.FilePath = filePathGenerator.FilePath;
            this.FileName = filePathGenerator.FileName;
        }

        protected virtual void OnOpen() { }
        protected virtual void OnSave() { }
        protected virtual void OnBeforeDelete() { }
        protected virtual void OnBeforeOpen() { BeforeOpen?.Invoke(this, new TopicSectionEventArgs(this)); }
        protected virtual void OnAfterOpen() { AfterOpen?.Invoke(this, new TopicSectionEventArgs(this)); }
        protected virtual void OnBeforeRevert() { BeforeRevert?.Invoke(this, new TopicSectionEventArgs(this)); }
        protected virtual void OnAfterRevert() { AfterRevert?.Invoke(this, new TopicSectionEventArgs(this));  }
        protected virtual void OnBeforeSave() { }
        protected virtual void OnAfterSave() { }
        protected virtual void OnClose() { }

        /// <summary>
        /// Any extra logic that needs to be implemented during a revert should be handled by overriding this method.
        /// </summary>
        protected virtual void OnRevert() { }

        protected virtual void OnAfterDelete()
        {
            this.Ordinal = -1;
        }

        public void Open()
        {
            try
            {
                OnBeforeOpen();
                OnOpen();
                OnAfterOpen();
                this.Loaded = true;

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
                OnBeforeRevert();
                OnRevert();
                OnOpen();
                OnAfterRevert();
                this.Loaded = true;
                
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        protected virtual void OnDelete()
        {
            File.Delete(this.FilePath);
        }

        public void Delete()
        {
            BeforeDelete?.Invoke(this, new TopicSectionEventArgs(this));
            OnBeforeDelete();
            OnDelete();
            OnAfterDelete();

            this.Loaded = false;
            AfterDelete?.Invoke(this, new TopicSectionEventArgs(this));
        }

        public void Save()
        {
            try
            {
                BeforeSave?.Invoke(this, new TopicSectionEventArgs(this));
                OnBeforeSave();
                OnSave();
                OnAfterSave();
                this.Loaded = true;
                AfterSave?.Invoke(this, new TopicSectionEventArgs(this));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void Close()
        {
            BeforeClose?.Invoke(this, new TopicSectionEventArgs(this));
            this.OnClose();
            this.Loaded = false;
            AfterClose?.Invoke(this, new TopicSectionEventArgs(this));
        }
    }
}
