using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.CodeGroup
{
    public interface ICodeGroupKeywordIndexItem : IKeywordIndexItem
    {
        string Syntax { get; set; }
    }
}
