using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class TextDocumentFile : BaseDocumentFile
    {
        public string Text { get; set; }

        // Only create these documents internally.
        internal TextDocumentFile(string id, string title, string description = null, string extension = "txt")
            : base(id, extension, title, description)
        {
            this.Text = null;
        }

        internal TextDocumentFile(string id, string title, int ordinal, string description = null, string extension = "txt")
            : base(id, extension, ordinal, title, description)
        {
            this.Text = null;
        }

        internal TextDocumentFile(string id, string title, string description = null, string text = null, string extension = "txt")
            : base(id, extension, title, description)
        {
            this.Text = text;
        }

        internal TextDocumentFile(string id, string title, int ordinal, string description = null, string text = null, string extension = "txt")
            : base(id, extension, ordinal, title, description)
        {
            this.Text = text;
        }

        protected override IFileVersion NewVersion(DateTime timeStamp, string description)
        {
            return new TextDocumentVersion(this.FilePath, timeStamp, description);
        }

        protected override void CreateFile()
        {
            File.WriteAllText(this.FileName, this.Text);
        }

        protected override void OpenFile()
        {
            this.Text = File.ReadAllText(this.FilePath);
        }

        protected override void SaveFile()
        {
            File.WriteAllText(this.FilePath, this.Text);
        }
    }
}
