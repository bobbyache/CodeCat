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

        internal TopicLibrary(IKeywordSearchIndexRepository keywordSearchIndexRepository, string subFolder) 
            : base(keywordSearchIndexRepository, subFolder)
        {
            base.FileExtension = "*.xml";
        }

        public override IndexExportImportData[] GetExportData(IKeywordIndexItem[] indexItems)
        {
            IKeywordIndexItem[] foundItems = base.FindIndecesByIds(indexItems.Select(r => r.Id).ToArray());
            IWorkItemExporter exporter = new TopicExporter(this.FolderPath, foundItems);
            return exporter.GetExportData();
        }
    }
}
