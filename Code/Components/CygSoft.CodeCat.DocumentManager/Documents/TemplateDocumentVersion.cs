using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class TemplateDocumentVersion : BaseVersionFile
    {
        public TemplateDocumentVersion(string filePath, DateTime timeStamp, string description)
            : base(filePath, timeStamp, description) 
        {

        }

        protected override void CreateFile()
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Version File Created: {0}", this.FileName));
        }

        protected override void OpenFile()
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Version File Opened: {0}", this.FileName));
        }

        protected override void SaveFile()
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Version File Saved: {0}", this.FileName));
        }
    }
}
