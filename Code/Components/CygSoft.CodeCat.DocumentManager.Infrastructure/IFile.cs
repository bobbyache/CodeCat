using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IFile
    {
        event EventHandler<FileEventArgs> BeforeDelete;
        event EventHandler<FileEventArgs> AfterDelete;
        event EventHandler<FileEventArgs> BeforeOpen;
        event EventHandler<FileEventArgs> AfterOpen;
        event EventHandler<FileEventArgs> BeforeCreate;
        event EventHandler<FileEventArgs> AfterCreate;
        event EventHandler<FileEventArgs> BeforeSave;
        event EventHandler<FileEventArgs> AfterSave;

        string Id { get; }
        string FilePath { get; }
        //string Content { get; set; } Do not set any file content. This is the job of specialized file classes.
        string FileName { get; }
        string FileExtension { get; }
        string Folder { get; }
        bool Exists { get; }
        bool Loaded { get; }

        void Create();
        void Open();
        void Create(string filePath);
        void Open(string filePath);
        void Delete();
        void Save();
    }
}
