using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.FileManagement
{
    public class TextFile : BaseFile, IDisposable
    {
        protected FileStream fileStream;

        public string Text
        {
            get
            {
                using (StreamReader reader = new StreamReader(fileStream, System.Text.Encoding.Unicode, true,
                    (int)fileStream.Length, true))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public override bool IsOpen
        {
            get
            {
                return (fileStream != null && fileStream.SafeFileHandle != null);
            }
        }

        public TextFile(string filePath) : base(filePath) { }

        protected override void OnOpen()
        {
            if (fileStream == null || fileStream.SafeFileHandle == null)
            {
                fileStream = new FileStream(this.filePath, FileMode.Open, FileAccess.ReadWrite);
            }
        }

        public void Dispose()
        {
            base.Close();
            this.fileStream.Dispose();
        }

        protected override void OnClose()
        {
            this.fileStream.Close();
        }

        protected override void OnDelete()
        {
            this.Dispose();
        }
    }
}
