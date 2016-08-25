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
    public class TextDocument : BaseDocument, ITextDocument
    {
        public string Text { get; set; }

        // Only create these documents internally.
        internal TextDocument(string id, string title, string extension) : base(id, extension, title, null)
        {
            this.Text = null;
        }

        internal TextDocument(string id, string title, string extension, int ordinal, string description) : base(id, extension, ordinal, title, description)
        {
            this.Text = null;
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
