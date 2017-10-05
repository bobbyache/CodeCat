using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Management;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.CodeCat.Domain.Topics
{
    internal class TopicLibrary : BaseLibrary
    {
        public TopicLibrary()
            : base(new TopicKeywordSearchIndexRepository("CodeCat_CodeGroupIndex"), "codegroup")
        {
            base.FileExtension = "*.xml";
        }

        internal TopicLibrary(IKeywordSearchIndexRepository keywordSearchIndexRepository, string subFolder) : base(keywordSearchIndexRepository, subFolder)
        {
            base.FileExtension = "*.xml";
        }

        public override IndexExportImportData[] GetExportData(IKeywordIndexItem[] indexItems)
        {
            IKeywordIndexItem[] foundItems = base.FindIndecesByIds(indexItems.Select(r => r.Id).ToArray());
            IWorkItemExporter exporter = new TopicExporter(this.FolderPath, foundItems);
            return exporter.GetExportData();
        }

        protected override IWorkItem CreateSpecializedTarget(IKeywordIndexItem indexItem)
        {
            TopicKeywordIndexItem codeGroupIndexItem = indexItem as TopicKeywordIndexItem;
            TopicDocument codeGroupFile = new TopicDocument(codeGroupIndexItem, this.FolderPath);

            if (this.workItems == null)
                this.workItems = new Dictionary<string, IWorkItem>();

            this.workItems.Add(codeGroupFile.Id, codeGroupFile);

            return codeGroupFile as IWorkItem;
        }

        protected override IWorkItem OpenSpecializedTarget(IKeywordIndexItem indexItem)
        {
            TopicKeywordIndexItem codeGroupIndexItem = indexItem as TopicKeywordIndexItem;
            IWorkItem workItem;

            if (this.workItems == null)
                this.workItems = new Dictionary<string, IWorkItem>();

            // first check to see if the file exists..
            if (this.workItems.ContainsKey(codeGroupIndexItem.Id))
            {
                workItem = this.workItems[codeGroupIndexItem.Id];
            }
            else
            {
                // retrieve the file and add it to the opened code files.
                workItem = PersistableTargetFactory.Create(codeGroupIndexItem, this.FolderPath);

                if (this.workItems == null)
                    this.workItems = new Dictionary<string, IWorkItem>();
                this.workItems.Add(workItem.Id, workItem);

                workItem.Open();
            }

            return workItem;
        }
    }
}
