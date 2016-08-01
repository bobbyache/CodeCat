using CygSoft.CodeCat.Domain.Base;
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
            : base(new QikKeywordSearchIndexRepository("CodeCat_CodeIndex"), "qik")
        {
            base.FileExtension = "*.xml";
        }

        protected override IPersistableTarget CreateSpecializedTarget(IKeywordIndexItem indexItem)
        {
            return null;
        }

        protected override IPersistableTarget OpenSpecializedTarget(IKeywordIndexItem indexItem)
        {
            return null;
        }
    }
}
