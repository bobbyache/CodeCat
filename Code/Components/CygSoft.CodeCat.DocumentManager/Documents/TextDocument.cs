using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.IO;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public abstract class TextDocument : BaseDocument, ITextDocument
    {
        public string Text { get; set; }

        // Only create these documents internally.
        internal TextDocument(string folder, string title, string extension) : base(new DocumentPathGenerator(folder, extension), title, null)
        {
            this.Text = null;
        }

        internal TextDocument(string folder, string id, string title, string extension, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, extension, id), title, description, ordinal)
        {
            this.Text = null;
        }

        protected override IFileVersion NewVersion(DateTime timeStamp, string description)
        {
            return new TextDocumentVersion(new VersionPathGenerator(this.FilePath, timeStamp), description, this.Text);
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
