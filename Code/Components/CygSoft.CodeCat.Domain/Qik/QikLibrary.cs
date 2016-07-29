using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using CygSoft.CodeCat.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Qik
{
    internal class QikLibrary : BaseLibrary
    {
        public QikLibrary()
            : base(new QikKeywordSearchIndexRepository(), "qik")
        {
            base.FileExtension = "*.xml";
        }

        protected override IPersistableFile CreateFile(IKeywordIndexItem indexItem)
        {
            CodeFile codeFile = new CodeFile(indexItem, this.FolderPath);
            return codeFile;
        }

        public override IPersistableFile CreateFile()
        {
            return GetFile(new QikKeywordIndexItem());
        }
    }
}
