using CygSoft.CodeCat.Domain.CodeGroup;
using System.IO;

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
