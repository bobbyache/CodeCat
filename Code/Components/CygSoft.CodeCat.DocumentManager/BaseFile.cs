using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager
{
    public abstract class BaseFile : IFile
    {
        public event EventHandler BeforeDelete;
        public event EventHandler AfterDelete;
        public event EventHandler BeforeOpen;
        public event EventHandler AfterOpen;
        public event EventHandler BeforeCreate;
        public event EventHandler AfterCreate;
        public event EventHandler BeforeSave;
        public event EventHandler AfterSave;

        public virtual string Id { get; protected set; }
        public virtual string FilePath { get; protected set; }
        //public string Content { get; set; }

        public virtual string FileName { get { return Path.GetFileName(this.FilePath); } }
        public string FileExtension { get; private set; }

        public virtual string Folder
        {
            get { return Path.GetDirectoryName(this.FilePath); }
        }

        public bool Exists { get { return File.Exists(this.FilePath); } }
        public bool Loaded { get; private set; }

        protected abstract void CreateFile();
        protected abstract void OpenFile();
        protected abstract void SaveFile();

        public BaseFile(string fileExtension)
        {
            this.Loaded = false;
            this.FileExtension = CleanExtension(fileExtension);
        }

        public void Create(string filePath)
        {
            this.FilePath = filePath;
            try
            {
                if (BeforeCreate != null)
                    BeforeCreate(this, new EventArgs());

                CreateFile();
                this.Loaded = true;

                if (AfterCreate != null)
                    AfterCreate(this, new EventArgs());
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Open(string filePath)
        {
            this.FilePath = filePath;
            try
            {
                if (BeforeOpen != null)
                    BeforeOpen(this, new EventArgs());

                OpenFile();
                this.Loaded = true;

                if (AfterOpen != null)
                    AfterOpen(this, new EventArgs());
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Delete()
        {
            if (BeforeDelete != null)
                BeforeDelete(this, new EventArgs());

            if (File.Exists(this.FilePath))
                File.Delete(this.FilePath);
            this.Loaded = false;

            if (AfterDelete != null)
                AfterDelete(this, new EventArgs());
        }

        public void Save()
        {
            try
            {
                if (BeforeSave != null)
                    BeforeSave(this, new EventArgs());

                OpenFile();
                this.Loaded = true;

                if (AfterSave != null)
                    AfterSave(this, new EventArgs());
            }
            catch (Exception exception)
            {
                throw exception;
            }
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
