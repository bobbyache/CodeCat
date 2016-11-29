using CygSoft.CodeCat.Domain.CodeGroup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Management
{
    internal class CodeGroupImporter
    {
        public void Import(string sourceIndexFilePath, int currentVersion, IndexExportImportData[] exportData)
        {
            CodeGroupLibrary codeGroupLibrary = new CodeGroupLibrary();
            codeGroupLibrary.Open(Path.GetDirectoryName(sourceIndexFilePath), currentVersion);
        }
    }
}
