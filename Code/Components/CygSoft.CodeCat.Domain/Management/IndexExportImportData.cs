using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Management
{
    public class IndexExportImportData
    {
        public string Id { get; private set; }
        public string Path { get; private set; }
        public string FileOrFolderName { get; private set; }
        public bool IsFile { get; private set; }
        public IKeywordIndexItem KeywordIndexItem { get; private set; }

        public IndexExportImportData(string Id, string path, string fileOrFolderName, IKeywordIndexItem keywordIndexItem, bool isFile = false)
        {
            this.Id = Id;
            this.Path = path;
            this.FileOrFolderName = fileOrFolderName;
            this.IsFile = isFile;
            this.KeywordIndexItem = keywordIndexItem;
        }
    }
}
