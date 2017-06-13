using CygSoft.CodeCat.Domain.Topics;
using System;
using System.IO;

namespace CygSoft.CodeCat.Domain.Management
{
    internal class CodeGroupImporter
    {
        public void Import(string sourceIndexFilePath, Version currentVersion, IndexExportImportData[] exportData)
        {
            TopicLibrary codeGroupLibrary = new TopicLibrary();
            codeGroupLibrary.Open(Path.GetDirectoryName(sourceIndexFilePath), currentVersion);
        }
    }
}
