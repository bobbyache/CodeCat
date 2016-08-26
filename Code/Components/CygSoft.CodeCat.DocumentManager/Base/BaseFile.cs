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
        public event EventHandler<FileEventArgs> BeforeCreate;
        public event EventHandler<FileEventArgs> AfterCreate;
        public event EventHandler<FileEventArgs> BeforeSave;
        public event EventHandler<FileEventArgs> AfterSave;

        //public string Id { get; protected set; }
        //public string FilePath { get; protected set; }

        //public string FileName { get { return Path.GetFileName(this.FilePath); } }
        //public string FileExtension { get; private set; }

        public string Id { get; private set; }
        public string FilePath { get; private set; }
        public string FileName { get; private set; }
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

        //public BaseFile(string fileExtension)
        //{
        //    this.Loaded = false;
        //    this.FileExtension = CleanExtension(fileExtension);
        //}

        //public BaseFile(string folder, string id, string extension)
        //{
        //    this.Id = id;
        //    this.FileExtension = CleanExtension(extension);
        //    this.FilePath = Path.Combine(folder, id + "." + CleanExtension(extension));
        //    this.Loaded = false;
        //}

        public BaseFile(BaseFilePathGenerator filePathGenerator)
        {
            this.Id = filePathGenerator.Id;
            this.FileExtension = filePathGenerator.FileExtension;
            this.FilePath = filePathGenerator.FilePath;
            this.FileName = filePathGenerator.FileName;
        }

        /// <summary>
        /// Create a file (in memory). The file is not yet saved to disk. Call the "Save" method to persist the file.
        /// Use the template method "CreateFile" to initialize the file invariants so that it is ready to be persisted.
        /// This overload takes no arguments, therefore it is expected that a valid file path for this file already
        /// exists.
        /// </summary>
        public void Create()
        {
            try
            {
                if (BeforeCreate != null)
                    BeforeCreate(this, new FileEventArgs(this));

                CreateFile();
                this.Loaded = true;

                if (AfterCreate != null)
                    AfterCreate(this, new FileEventArgs(this));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Create a file (in memory). The file is not yet saved to disk. Call the "Save" method to persist the file.
        /// Use the template method "CreateFile" to initialize the file invariants so that it is ready to be persisted.
        /// This overload takes a file path argument, and the invariants (ie. file location
        /// </summary>
        public void Create(string filePath)
        {
            this.FilePath = filePath;
            try
            {
                if (BeforeCreate != null)
                    BeforeCreate(this, new FileEventArgs(this));

                CreateFile();
                this.Loaded = true;

                if (AfterCreate != null)
                    AfterCreate(this, new FileEventArgs(this));
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
                    BeforeOpen(this, new FileEventArgs(this));

                OpenFile();
                this.Loaded = true;

                if (AfterOpen != null)
                    AfterOpen(this, new FileEventArgs(this));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Open()
        {
            try
            {
                if (BeforeOpen != null)
                    BeforeOpen(this, new FileEventArgs(this));

                OpenFile();
                this.Loaded = true;

                if (AfterOpen != null)
                    AfterOpen(this, new FileEventArgs(this));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public virtual void Delete()
        {
            if (BeforeDelete != null)
                BeforeDelete(this, new FileEventArgs(this));

            if (File.Exists(this.FilePath))
                File.Delete(this.FilePath);

            DeleteFile();

            this.Loaded = false;

            if (AfterDelete != null)
                AfterDelete(this, new FileEventArgs(this));
        }

        // can be used to do more cleanup during delete ... ie. version files.
        protected virtual void DeleteFile() { }

        public void Save()
        {
            try
            {
                if (BeforeSave != null)
                    BeforeSave(this, new FileEventArgs(this));

                SaveFile();
                this.Loaded = true;

                if (AfterSave != null)
                    AfterSave(this, new FileEventArgs(this));
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
