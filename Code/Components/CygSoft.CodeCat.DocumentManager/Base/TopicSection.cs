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

        public string Id {  get { return filePathGenerator.Id; } }
        public string FilePath {  get { return filePathGenerator.FilePath; } }
        public string FileName {  get { return filePathGenerator.FileName; } }
        public string FileExtension {  get { return filePathGenerator.FileExtension; } }
        public int Ordinal { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DocumentType { get; set; }
        public virtual string Folder { get { return Path.GetDirectoryName(FilePath); } }
        public virtual bool FolderExists { get { return Directory.Exists(Path.GetDirectoryName(FilePath)); } }
        public bool Exists { get { return File.Exists(FilePath); } }
        public bool Loaded { get; private set; }

        public TopicSection(BaseFilePathGenerator filePathGenerator, string title, string description = null)
        {
            Ordinal = -1;
            Title = title;
            Description = description;
            this.filePathGenerator = filePathGenerator;
        }

        public TopicSection(BaseFilePathGenerator filePathGenerator, string title, string description = null, int ordinal = -1)
        {
            Ordinal = -1;
            Title = title;
            Description = description;
            this.filePathGenerator = filePathGenerator;
        }

        protected virtual void OnOpen() { }
        protected virtual void OnSave() { }
        protected virtual void OnBeforeDelete() { BeforeDelete?.Invoke(this, new TopicSectionEventArgs(this)); }

        protected virtual void OnAfterDelete()
        {
            Ordinal = -1;
            AfterDelete?.Invoke(this, new TopicSectionEventArgs(this));
        }

        protected virtual void OnBeforeOpen() { BeforeOpen?.Invoke(this, new TopicSectionEventArgs(this)); }
        protected virtual void OnAfterOpen() { AfterOpen?.Invoke(this, new TopicSectionEventArgs(this)); }
        protected virtual void OnBeforeRevert() { BeforeRevert?.Invoke(this, new TopicSectionEventArgs(this)); }
        protected virtual void OnAfterRevert() { AfterRevert?.Invoke(this, new TopicSectionEventArgs(this));  }
        protected virtual void OnBeforeSave() { BeforeSave?.Invoke(this, new TopicSectionEventArgs(this)); }
        protected virtual void OnAfterSave() { AfterSave?.Invoke(this, new TopicSectionEventArgs(this)); }
        protected virtual void OnClose() { }
        protected virtual void OnBeforeClose() { BeforeClose?.Invoke(this, new TopicSectionEventArgs(this)); }
        protected virtual void OnAfterClose() { AfterClose?.Invoke(this, new TopicSectionEventArgs(this)); }

        /// <summary>
        /// Any extra logic that needs to be implemented during a revert should be handled by overriding this method.
        /// </summary>
        protected virtual void OnRevert() { }

        public void Open()
        {
            try
            {
                OnBeforeOpen();
                OnOpen();
                OnAfterOpen();
                Loaded = true;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Revert()
        {
            try
            {
                OnBeforeRevert();
                OnRevert();
                if (Exists)
                    OnOpen();
                OnAfterRevert();
                Loaded = true;
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected virtual void OnDelete()
        {
            File.Delete(FilePath);
        }

        public void Delete()
        {
            try
            {
                OnBeforeDelete();
                OnDelete();
                OnAfterDelete();
                Loaded = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Save()
        {
            try
            {
                OnBeforeSave();
                OnSave();
                OnAfterSave();
                Loaded = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Close()
        {
            try
            {
                OnBeforeClose();
                OnClose();
                OnAfterClose();
                Loaded = false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
