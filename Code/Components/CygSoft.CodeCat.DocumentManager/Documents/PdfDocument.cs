using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class PdfDocument : BaseDocument, IPdfDocument
    {
        protected override IFileVersion NewVersion(DateTime timeStamp, string description)
        {
            //throw new NotImplementedException();
            return null;
        }

        protected override void OpenFile()
        {
            //throw new NotImplementedException();
        }

        protected override void SaveFile()
        {
            //throw new NotImplementedException();
        }
    }
}
