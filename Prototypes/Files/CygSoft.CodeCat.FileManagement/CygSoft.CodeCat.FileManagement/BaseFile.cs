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

        public bool IsOpen { get; private set; }

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
                IsOpen = true;
            }
        }

        public void Close()
        {
            if (this.IsOpen)
            {
                OnClose();
                IsOpen = false;
                Closed?.Invoke(this, new EventArgs());
            }
        } 
    }
}
