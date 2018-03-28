using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Domain.Management;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Qik
{
    internal class QikTemplateExporter : IWorkItemExporter
    {
        private readonly string folderPath;
        private readonly IKeywordIndexItem[] indexItems;

        public QikTemplateExporter(string folderPath, IKeywordIndexItem[] indexItems)
        {
            if (string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("Folder must be supplied in order to retrieve the export data.");

            this.folderPath = folderPath;
            this.indexItems = indexItems;
        }

        public IndexExportImportData[] GetExportData()
        {
            List<IndexExportImportData> exportList = new List<IndexExportImportData>();

            foreach (QikTemplateKeywordIndexItem indexItem in indexItems.OfType<QikTemplateKeywordIndexItem>())
            {
                QikTemplateDocumentSet codeFile = new QikTemplateDocumentSet(new DocumentIndexPathGenerator(folderPath, "xml", indexItem.Id), 
                    indexItem as QikTemplateKeywordIndexItem);
                exportList.Add(new IndexExportImportData(indexItem.Id, codeFile.Folder, indexItem.Id, indexItem));
            }
            return exportList.ToArray();
        }
    }
}
