using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class TemplateDocument : BaseDocumentFile
    {
        // Only create these documents internally.
        internal TemplateDocument(string id) : base(id, "txt")
        {
        }

        protected override IFileVersion NewVersion(DateTime timeStamp, string description)
        {
            return new TemplateDocumentVersion(this.FilePath, timeStamp, description);
        }

        protected override void CreateFile()
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Test Document File Created: {0}", this.FileName));
        }

        protected override void OpenFile()
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Test Document File Opened: {0}", this.FileName));
        }

        protected override void SaveFile()
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Test Document File Saved: {0}", this.FileName));
        }
    }
}
