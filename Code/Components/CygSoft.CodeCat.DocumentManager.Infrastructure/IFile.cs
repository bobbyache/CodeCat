using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IFile
    {
        event EventHandler BeforeDelete;
        event EventHandler AfterDelete;
        event EventHandler BeforeOpen;
        event EventHandler AfterOpen;
        event EventHandler BeforeCreate;
        event EventHandler AfterCreate;
        event EventHandler BeforeSave;
        event EventHandler AfterSave;

        string Id { get; }
        string FilePath { get; }
        //string Content { get; set; } Do not set any file content. This is the job of specialized file classes.
        string FileName { get; }
        string FileExtension { get; }
        string Folder { get; }
        bool Exists { get; }
        bool Loaded { get; }

        void Create(string filePath);
        void Open(string filePath);
        void Delete();
        void Save();
    }
}
