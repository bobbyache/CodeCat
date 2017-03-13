using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.IO;

namespace CygSoft.CodeCat.DocumentManager.Base
{
    public abstract class BaseDocument : ITopicSection
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

        public BaseDocument(BaseFilePathGenerator filePathGenerator, string title, string description = null)
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

        public BaseDocument(BaseFilePathGenerator filePathGenerator, string title, string description = null, int ordinal = -1)
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

        protected abstract void OpenFile();
        protected abstract void SaveFile();
        protected virtual void OnBeforeDelete() { }
        protected virtual void OnBeforeOpen() { }
        protected virtual void OnAfterOpen() { }
        protected virtual void OnBeforeRevert() { }
        protected virtual void OnAfterRevert() { }
        protected virtual void OnBeforeSave() { }
        protected virtual void OnAfterSave() { }
        protected virtual void OnClose() { }

        protected virtual void OnAfterDelete()
        {
            this.Ordinal = -1;
        }

        public void Open()
        {
            try
            {
                BeforeOpen?.Invoke(this, new TopicSectionEventArgs(this));
                OnBeforeOpen();
                OpenFile();
                OnAfterOpen();

                this.Loaded = true;
                AfterOpen?.Invoke(this, new TopicSectionEventArgs(this));
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
                BeforeRevert?.Invoke(this, new TopicSectionEventArgs(this));
                OnBeforeRevert();

                if (File.Exists(this.FilePath))
                    OpenFile();
                OnAfterRevert();

                this.Loaded = true;
                AfterRevert?.Invoke(this, new TopicSectionEventArgs(this));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Delete()
        {
            BeforeDelete?.Invoke(this, new TopicSectionEventArgs(this));
            OnBeforeDelete();

            if (File.Exists(this.FilePath))
                File.Delete(this.FilePath);

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
                SaveFile();
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
