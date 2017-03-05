using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.IO;

namespace CygSoft.CodeCat.DocumentManager.Base
{
    public abstract class BaseFile : IFile
    {
        public event EventHandler<FileEventArgs> BeforeDelete;
        public event EventHandler<FileEventArgs> AfterDelete;
        public event EventHandler<FileEventArgs> BeforeOpen;
        public event EventHandler<FileEventArgs> AfterOpen;
        public event EventHandler<FileEventArgs> BeforeSave;
        public event EventHandler<FileEventArgs> AfterSave;
        public event EventHandler<FileEventArgs> BeforeClose;
        public event EventHandler<FileEventArgs> AfterClose;
        public event EventHandler<FileEventArgs> BeforeRevert;
        public event EventHandler<FileEventArgs> AfterRevert;

        public string Id { get; private set; }
        public string FilePath { get; protected set; }
        public string FileName { get; private set; }
        public string FileExtension { get; private set; }
        protected BaseFilePathGenerator filePathGenerator;

        public virtual string Folder
        {
            get { return Path.GetDirectoryName(this.FilePath); }
        }

        public bool Exists { get { return File.Exists(this.FilePath); } }
        public bool Loaded { get; private set; }

        protected abstract void OpenFile();
        protected abstract void SaveFile();

        protected virtual void OnBeforeDelete() { }
        protected virtual void OnAfterDelete() { }
        protected virtual void OnBeforeOpen() { }
        protected virtual void OnAfterOpen() { }
        protected virtual void OnBeforeRevert() { }
        protected virtual void OnAfterRevert() { }
        protected virtual void OnBeforeSave() { }
        protected virtual void OnAfterSave() { }
        protected virtual void OnClose() { }

        public BaseFile(BaseFilePathGenerator filePathGenerator)
        {
            this.filePathGenerator = filePathGenerator;
            this.Id = filePathGenerator.Id;
            this.FileExtension = filePathGenerator.FileExtension;
            this.FilePath = filePathGenerator.FilePath;
            this.FileName = filePathGenerator.FileName;
        }

        public void Open()
        {
            try
            {
                BeforeOpen?.Invoke(this, new FileEventArgs(this));
                OnBeforeOpen();
                OpenFile();
                OnAfterOpen();

                this.Loaded = true;
                AfterOpen?.Invoke(this, new FileEventArgs(this));
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
                BeforeRevert?.Invoke(this, new FileEventArgs(this));
                OnBeforeRevert();

                if (File.Exists(this.FilePath))
                    OpenFile();
                OnAfterRevert();

                this.Loaded = true;
                AfterRevert?.Invoke(this, new FileEventArgs(this));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Delete()
        {
            BeforeDelete?.Invoke(this, new FileEventArgs(this));
            OnBeforeDelete();

            if (File.Exists(this.FilePath))
                File.Delete(this.FilePath);

            OnAfterDelete();

            this.Loaded = false;
            AfterDelete?.Invoke(this, new FileEventArgs(this));
        }

        public void Save()
        {
            try
            {
                BeforeSave?.Invoke(this, new FileEventArgs(this));
                OnBeforeSave();
                SaveFile();
                OnAfterSave();
                this.Loaded = true;
                AfterSave?.Invoke(this, new FileEventArgs(this));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void Close()
        {
            BeforeClose?.Invoke(this, new FileEventArgs(this));
            this.OnClose();
            this.Loaded = false;
            AfterClose?.Invoke(this, new FileEventArgs(this));
        }

        private string CleanExtension(string extension)
        {
            if (extension.Length > 0)
            {
                if (extension.StartsWith("."))
                    return extension.Substring(1);
            }

            return extension;
        }
    }
}
