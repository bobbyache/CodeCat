using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Management;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.CodeCat.Domain.Code.Base
{
    internal class CodeLibrary : BaseLibrary
    {
        public CodeLibrary()
            : base(new CodeKeywordSearchIndexRepository("CodeCat_CodeIndex"), "code")
        {
            base.FileExtension = "*.xml";
        }

        public override IndexExportImportData[] GetExportData(IKeywordIndexItem[] indexItems)
        {
            IKeywordIndexItem[] foundItems = base.FindIndecesByIds(indexItems.Select(r => r.Id).ToArray());
            IWorkItemExporter exporter = new CodeExporter(this.FolderPath, foundItems);
            return exporter.GetExportData();
        }

        protected override IWorkItem CreateSpecializedTarget(IKeywordIndexItem indexItem)
        {
            CodeKeywordIndexItem codeIndexItem = indexItem as CodeKeywordIndexItem;
            CodeFile codeFile = new CodeFile(codeIndexItem, this.FolderPath);

            if (this.workItems == null)
                this.workItems = new Dictionary<string, IWorkItem>();

            this.workItems.Add(codeFile.Id, codeFile);

            codeFile.Open();

            return codeFile as IWorkItem;
        }

        protected override IWorkItem OpenSpecializedTarget(IKeywordIndexItem indexItem)
        {
            CodeKeywordIndexItem codeIndexItem = indexItem as CodeKeywordIndexItem;
            IWorkItem workItem;

            if (this.workItems == null)
                this.workItems = new Dictionary<string, IWorkItem>();

            // first check to see if the file exists..
            if (this.workItems.ContainsKey(codeIndexItem.Id))
            {
                workItem = this.workItems[codeIndexItem.Id];
            }

            else
            {
                // retrieve the file and add it to the opened code files.
                workItem = new CodeFile(codeIndexItem, this.FolderPath);

                if (this.workItems == null)
                    this.workItems = new Dictionary<string, IWorkItem>();
                this.workItems.Add(workItem.Id, workItem);

                workItem.Open();
            }

            return workItem;
        }
    }
}
