using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
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
