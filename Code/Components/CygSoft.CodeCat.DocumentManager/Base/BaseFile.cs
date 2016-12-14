using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (BeforeOpen != null)
                    BeforeOpen(this, new FileEventArgs(this));

                OnBeforeOpen();
                OpenFile();
                OnAfterOpen();

                this.Loaded = true;

                if (AfterOpen != null)
                    AfterOpen(this, new FileEventArgs(this));
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
                if (BeforeRevert != null)
                    BeforeRevert(this, new FileEventArgs(this));

                OnBeforeRevert();
                OpenFile();
                OnAfterRevert();

                this.Loaded = true;

                if (AfterRevert != null)
                    AfterRevert(this, new FileEventArgs(this));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Delete()
        {
            if (BeforeDelete != null)
                BeforeDelete(this, new FileEventArgs(this));

            OnBeforeDelete();

            if (File.Exists(this.FilePath))
                File.Delete(this.FilePath);

            OnAfterDelete();

            this.Loaded = false;

            if (AfterDelete != null)
                AfterDelete(this, new FileEventArgs(this));
        }

        public void Save()
        {
            try
            {
                if (BeforeSave != null)
                    BeforeSave(this, new FileEventArgs(this));

                OnBeforeSave();
                SaveFile();
                OnAfterSave();

                this.Loaded = true;

                if (AfterSave != null)
                    AfterSave(this, new FileEventArgs(this));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void Close()
        {
            if (BeforeClose != null)
                BeforeClose(this, new FileEventArgs(this));

            this.OnClose();
            this.Loaded = false;

            if (AfterClose != null)
                AfterClose(this, new FileEventArgs(this));
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
