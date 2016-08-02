using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.Qik.FileManager
{
    internal class QikFile
    {
        public string FilePath { get; private set; }
        public string Text { get; set; }
        public string FileName { get { return Path.GetFileName(this.FilePath); } }

        public QikFile(string filePath)
        {
            this.FilePath = filePath;
        }

        public void Load()
        {
            this.Text = File.ReadAllText(this.FilePath);
        }

        public void Save()
        {
            File.WriteAllText(this.FilePath, this.Text);
        }
    }
}
