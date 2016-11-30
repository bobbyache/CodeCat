using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Management;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.CodeGroup
{
    internal class CodeGroupLibrary : BaseLibrary
    {
        public CodeGroupLibrary()
            : base(new CodeGroupKeywordSearchIndexRepository("CodeCat_CodeGroupIndex"), "codegroup")
        {
            base.FileExtension = "*.xml";
        }

        public override IndexExportImportData[] GetExportData(IKeywordIndexItem[] indexItems)
        {
            List<IndexExportImportData> exportList = new List<IndexExportImportData>();
            IKeywordIndexItem[] foundItems = base.FindIndecesByIds(indexItems.Select(r => r.Id).ToArray());

            foreach (CodeGroupKeywordIndexItem indexItem in foundItems.OfType<CodeGroupKeywordIndexItem>())
            {
                CodeGroupDocumentSet codeFile = new CodeGroupDocumentSet(indexItem as CodeGroupKeywordIndexItem, this.FolderPath);
                exportList.Add(new IndexExportImportData(indexItem.Id, codeFile.Folder, indexItem.Id, indexItem));
            }
            return exportList.ToArray();
        }

        protected override IPersistableTarget CreateSpecializedTarget(IKeywordIndexItem indexItem)
        {
            CodeGroupKeywordIndexItem codeGroupIndexItem = indexItem as CodeGroupKeywordIndexItem;
            CodeGroupDocumentSet codeGroupFile = new CodeGroupDocumentSet(codeGroupIndexItem, this.FolderPath);

            if (this.openFiles == null)
                this.openFiles = new Dictionary<string, IPersistableTarget>();

            this.openFiles.Add(codeGroupFile.Id, codeGroupFile);

            return codeGroupFile as IPersistableTarget;
        }

        protected override IPersistableTarget OpenSpecializedTarget(IKeywordIndexItem indexItem)
        {
            CodeGroupKeywordIndexItem codeGroupIndexItem = indexItem as CodeGroupKeywordIndexItem;
            IPersistableTarget persistableFile;

            if (this.openFiles == null)
                this.openFiles = new Dictionary<string, IPersistableTarget>();

            // first check to see if the file exists..
            if (this.openFiles.ContainsKey(codeGroupIndexItem.Id))
            {
                persistableFile = this.openFiles[codeGroupIndexItem.Id];
            }

            else
            {
                // retrieve the file and add it to the opened code files.
                persistableFile = new CodeGroupDocumentSet(codeGroupIndexItem, this.FolderPath);

                if (this.openFiles == null)
                    this.openFiles = new Dictionary<string, IPersistableTarget>();
                this.openFiles.Add(persistableFile.Id, persistableFile);

                persistableFile.Open();
            }

            return persistableFile;
        }
    }
}
