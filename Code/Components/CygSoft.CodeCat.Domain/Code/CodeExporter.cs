using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Domain.Management;
using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.CodeCat.Domain.Code
{
    internal class CodeExporter : IWorkItemExporter
    {
        private readonly string folderPath;
        private readonly IKeywordIndexItem[] indexItems;

        public CodeExporter(string folderPath, IKeywordIndexItem[] indexItems)
        {
            if (string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("Folder must be supplied in order to retrieve the export data.");

            this.folderPath = folderPath;
            this.indexItems = indexItems;
        }

        public IndexExportImportData[] GetExportData()
        {
            List<IndexExportImportData> exportList = new List<IndexExportImportData>();

            foreach (CodeKeywordIndexItem indexItem in indexItems.OfType<CodeKeywordIndexItem>())
            {
                CodeFile codeFile = new CodeFile(new DocumentPathGenerator(folderPath, "xml", indexItem.Id), 
                    indexItem as CodeKeywordIndexItem);
                exportList.Add(new IndexExportImportData(indexItem.Id, codeFile.FilePath, indexItem.FileTitle, indexItem, true));
            }

            return exportList.ToArray();
        }
    }
}
