using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Management;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.CodeCat.Domain.Qik
{
    internal class QikTemplateLibrary : BaseLibrary
    {
        public QikTemplateLibrary()
            : base(new QikTemplateKeywordSearchIndexRepository("CodeCat_QikIndex"), "qik")
        {
            base.FileExtension = "*.xml";
        }

        public override IndexExportImportData[] GetExportData(IKeywordIndexItem[] indexItems)
        {
            IKeywordIndexItem[] foundItems = base.FindIndecesByIds(indexItems.Select(r => r.Id).ToArray());
            IWorkItemExporter exporter = new QikTemplateExporter(this.FolderPath, foundItems);
            return exporter.GetExportData();
        }

        protected override IWorkItem CreateTargetWorkItem(IKeywordIndexItem indexItem)
        {
            QikTemplateKeywordIndexItem qikIndexItem = indexItem as QikTemplateKeywordIndexItem;
            QikTemplateDocumentSet qikFile = new QikTemplateDocumentSet(qikIndexItem, this.FolderPath);

            if (this.workItems == null)
                this.workItems = new Dictionary<string, IWorkItem>();

            this.workItems.Add(qikFile.Id, qikFile);

            return qikFile as IWorkItem;
        }

        protected override IWorkItem OpenTargetWorkItem(IKeywordIndexItem indexItem)
        {
            QikTemplateKeywordIndexItem qikIndexItem = indexItem as QikTemplateKeywordIndexItem;
            IWorkItem workItem;

            if (this.workItems == null)
                this.workItems = new Dictionary<string, IWorkItem>();

            // first check to see if the file exists..
            if (this.workItems.ContainsKey(qikIndexItem.Id))
            {
                workItem = this.workItems[qikIndexItem.Id];
            }

            else
            {
                // retrieve the file and add it to the opened code files.
                workItem = new QikTemplateDocumentSet(qikIndexItem, this.FolderPath);

                if (this.workItems == null)
                    this.workItems = new Dictionary<string, IWorkItem>();

                this.workItems.Add(workItem.Id, workItem);

                workItem.Open();
            }

            return workItem;
        }
    }
}
