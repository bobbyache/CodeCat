using CygSoft.CodeCat.Plugin.Infrastructure;
using System;
using System.Windows.Forms;

namespace TestPlugins.PluginA
{
    public partial class TestControl : UserControl, IPluginControl
    {
        public TestControl()
        {
            InitializeComponent();
        }

        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string DocumentType
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool Exists
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string FileExtension
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string FileName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string FilePath
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Folder
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool FolderExists
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool Loaded
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Ordinal
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Title
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

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

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public void Revert()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
