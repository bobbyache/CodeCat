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
            List<IndexExportImportData> exportList = new List<IndexExportImportData>();
            IKeywordIndexItem[] foundItems = base.FindIndecesByIds(indexItems.Select(r => r.Id).ToArray());

            foreach (TopicKeywordIndexItem indexItem in foundItems.OfType<TopicKeywordIndexItem>())
            {
                TopicDocument codeFile = new TopicDocument(indexItem as TopicKeywordIndexItem, this.FolderPath);
                exportList.Add(new IndexExportImportData(indexItem.Id, codeFile.Folder, indexItem.Id, indexItem));
            }
            return exportList.ToArray();
        }

        protected override IPersistableTarget CreateSpecializedTarget(IKeywordIndexItem indexItem)
        {
            TopicKeywordIndexItem codeGroupIndexItem = indexItem as TopicKeywordIndexItem;
            TopicDocument codeGroupFile = new TopicDocument(codeGroupIndexItem, this.FolderPath);

            if (this.openFiles == null)
                this.openFiles = new Dictionary<string, IPersistableTarget>();

            this.openFiles.Add(codeGroupFile.Id, codeGroupFile);

            return codeGroupFile as IPersistableTarget;
        }

        protected override IPersistableTarget OpenSpecializedTarget(IKeywordIndexItem indexItem)
        {
            TopicKeywordIndexItem codeGroupIndexItem = indexItem as TopicKeywordIndexItem;
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
                persistableFile = PersistableTargetFactory.Create(codeGroupIndexItem, this.FolderPath);

                if (this.openFiles == null)
                    this.openFiles = new Dictionary<string, IPersistableTarget>();
                this.openFiles.Add(persistableFile.Id, persistableFile);

                persistableFile.Open();
            }

            return persistableFile;
        }
    }
}
