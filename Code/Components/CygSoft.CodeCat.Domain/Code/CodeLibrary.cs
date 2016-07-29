using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using CygSoft.CodeCat.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Code.Base
{
    internal class CodeLibrary : BaseLibrary
    {
        public CodeLibrary()
            : base(new KeywordSearchIndexRepository(), "code")
        {
            base.FileExtension = "*.xml";
        }

        protected override IPersistableFile CreateFile(IKeywordIndexItem indexItem)
        {
            CodeFile codeFile = new CodeFile(indexItem, this.FolderPath);
            return codeFile;
        }
    }
}
