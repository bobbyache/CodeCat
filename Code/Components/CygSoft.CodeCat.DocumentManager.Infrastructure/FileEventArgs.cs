﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public class FileEventArgs : EventArgs
    {
        public IFile File { get; private set; }

        public FileEventArgs(IFile file)
        {
            this.File = file;
        }
    }

    public interface IFile
    {
        event EventHandler<FileEventArgs> BeforeDelete;
        event EventHandler<FileEventArgs> AfterDelete;

        event EventHandler<FileEventArgs> BeforeOpen;
        event EventHandler<FileEventArgs> AfterOpen;
        event EventHandler<FileEventArgs> BeforeSave;
        event EventHandler<FileEventArgs> AfterSave;
        event EventHandler<FileEventArgs> BeforeClose;
        event EventHandler<FileEventArgs> AfterClose;
        event EventHandler<FileEventArgs> BeforeRevert;
        event EventHandler<FileEventArgs> AfterRevert;

        string FilePath { get; }
        string FileName { get; }
        string FileExtension { get; }
        string Folder { get; }
        bool FolderExists { get; }
        bool Exists { get; }
        bool Loaded { get; }

        void Open();
        void Delete();
        void Save();
        void Close();
        void Revert();
    }
}
