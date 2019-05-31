using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;

namespace CygSoft.CodeCat.DocumentManager.Base
{
    public abstract class BaseFile : IFile
    {
        public virtual string Folder
        {
            get { return fileRepository.GetDirectory(); }
        }

        public virtual bool FolderExists
        {
            get { return fileRepository.DirectoryExists(); }
        }

        public virtual bool Exists { get { return fileRepository.FileExists(); } }
        public virtual bool Loaded { get; private set; }

        public virtual string FilePath { get; protected set; }
        public virtual string FileName { get; private set; }
        public virtual string FileExtension { get; private set; }

        public event EventHandler<FileEventArgs> AfterClose;
        public event EventHandler<FileEventArgs> AfterDelete;
        public event EventHandler<FileEventArgs> AfterOpen;
        public event EventHandler<FileEventArgs> AfterRevert;
        public event EventHandler<FileEventArgs> AfterSave;
        public event EventHandler<FileEventArgs> BeforeClose;
        public event EventHandler<FileEventArgs> BeforeDelete;
        public event EventHandler<FileEventArgs> BeforeOpen;
        public event EventHandler<FileEventArgs> BeforeRevert;
        public event EventHandler<FileEventArgs> BeforeSave;

        protected BaseFilePathGenerator filePathGenerator;
        protected IFileRepository fileRepository;

        public BaseFile(IFileRepository fileRepository, BaseFilePathGenerator filePathGenerator)
        {
            this.FileName = filePathGenerator.FileName;
            this.FileExtension = filePathGenerator.FileExtension;
            this.FilePath = filePathGenerator.FilePath;
            this.filePathGenerator = filePathGenerator;
            this.fileRepository = fileRepository;
        }

        protected virtual void OnBeforeDelete() { }
        protected virtual void OnBeforeOpen() { }
        protected virtual void OnAfterOpen() { }
        protected virtual void OnBeforeSave() { }
        protected virtual void OnAfterSave() { }
        protected virtual void OnClose() { }
        protected virtual void OnOpen() { }
        protected virtual void OnSave() { }
        protected virtual void OnBeforeRevert() { }
        protected virtual void OnAfterRevert() { }
        protected virtual void OnAfterDelete() { }

        protected virtual void OnRevert()
        {
            if (fileRepository.FileExists())
                OnOpen();
        }

        protected virtual void SaveFile() { }

        protected virtual void OnDelete()
        {
            if (fileRepository.FileExists())
                fileRepository.Delete();
        }

        public void Open()
        {
            BeforeOpen?.Invoke(this, new FileEventArgs(this));
            OnBeforeOpen();
            OnOpen();
            OnAfterOpen();
            this.Loaded = true;
            AfterOpen?.Invoke(this, new FileEventArgs(this));
        }

        public void Delete()
        {
            BeforeDelete?.Invoke(this, new FileEventArgs(this));
            OnBeforeDelete();

            OnDelete();
            OnAfterDelete();

            this.Loaded = false;
            AfterDelete?.Invoke(this, new FileEventArgs(this));
        }

        public void Save()
        {
            BeforeSave?.Invoke(this, new FileEventArgs(this));
            OnBeforeSave();
            SaveFile();
            OnSave();
            OnAfterSave();
            this.Loaded = true;
            AfterSave?.Invoke(this, new FileEventArgs(this));
        }
        public void Close()
        {
            BeforeClose?.Invoke(this, new FileEventArgs(this));
            this.OnClose();
            this.Loaded = false;
            AfterClose?.Invoke(this, new FileEventArgs(this));
        }

        public void Revert()
        {
            BeforeRevert?.Invoke(this, new FileEventArgs(this));
            OnBeforeRevert();
            OnRevert();
            OnAfterRevert();

            this.Loaded = true;
            AfterRevert?.Invoke(this, new FileEventArgs(this));
        }
    }
}
