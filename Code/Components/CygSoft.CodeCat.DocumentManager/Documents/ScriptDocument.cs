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
    public class ScriptDocument : BaseVersionableFile
    {
        public string Text { get; set; }

        public ScriptDocument() : base("qik")
        {
        }

        protected override void CreateFile()
        {
            File.WriteAllText(this.FileName, this.Text);
        }

        protected override void OpenFile()
        {
            this.Text = File.ReadAllText(this.FilePath);
        }

        protected override void DeleteFile()
        {
            base.DeleteFile();
        }

        protected override void SaveFile()
        {
            File.WriteAllText(this.FileName, this.Text);
        }

        protected override IFileVersion NewVersion(DateTime timeStamp, string description)
        {
            return new ScriptDocumentVersion(this.FilePath, timeStamp, description);
        }
    }
}
