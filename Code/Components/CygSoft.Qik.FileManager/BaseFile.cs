using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.Qik.FileManager
{
    internal class BaseFile
    {
        public string Id { get { return this.FileName; } }
        public string FilePath { get; private set; }
        public string Text { get; set; }
        public string FileName { get { return Path.GetFileName(this.FilePath); } }
        public string FolderPath
        {
            get { return Path.GetDirectoryName(this.FilePath); }
        }

        public bool Exists
        {
            get { return File.Exists(this.FilePath); }
        }

        public BaseFile(string filePath)
        {
            this.FilePath = filePath;
        }

        public void Open()
        {
            this.Text = File.ReadAllText(this.FilePath);
        }

        public void Save()
        {
            File.WriteAllText(this.FilePath, this.Text);
        }

        public void Delete()
        {
            File.Delete(this.FilePath);
        }
    }
}
