using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.FileManagement
{
    public abstract class BaseFile
    {
        protected string filePath;
        public event EventHandler Closed;

        public BaseFile(string filePath)
        {
            this.filePath = filePath;
        }

        public string Path { get { return filePath; } }

        public abstract bool IsOpen { get; }

        protected abstract void OnDelete();
        protected abstract void OnOpen();
        protected abstract void OnClose();

        public void Delete()
        {
            Close();
            OnDelete();
        }

        public void Open()
        {
            if (!this.IsOpen)
            {
                OnOpen();
            }
        }

        public void Close()
        {
            if (this.IsOpen)
            {
                OnClose();
                Closed?.Invoke(this, new EventArgs());
            }
        } 
    }
}
