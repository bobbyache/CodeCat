using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Domain.Management;
using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.CodeCat.Domain.Topics
{
    internal class TopicExporter : IWorkItemExporter
    {
        private readonly string folderPath;
        private readonly IKeywordIndexItem[] indexItems;

        public TopicExporter(string folderPath, IKeywordIndexItem[] indexItems)
        {
            if (string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("Folder must be supplied in order to retrieve the export data.");

            this.folderPath = folderPath;
            this.indexItems = indexItems;
        }

        public IndexExportImportData[] GetExportData()
        {
            List<IndexExportImportData> exportList = new List<IndexExportImportData>();

            foreach (TopicKeywordIndexItem indexItem in indexItems.OfType<TopicKeywordIndexItem>())
            {
                TopicDocument codeFile = new TopicDocument(new DocumentIndexPathGenerator(folderPath, "xml", indexItem.Id), indexItem as TopicKeywordIndexItem);
                exportList.Add(new IndexExportImportData(indexItem.Id, codeFile.Folder, indexItem.Id, indexItem));
            }
            return exportList.ToArray();
        }
    }
}
