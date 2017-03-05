using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System.IO;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class TextDocumentVersion : BaseVersionFile
    {
        public string Text { get; set; }

        public TextDocumentVersion(VersionPathGenerator versionPathGenerator, string description, string text)
            : base(versionPathGenerator, description) 
        {
            this.Text = null;
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
