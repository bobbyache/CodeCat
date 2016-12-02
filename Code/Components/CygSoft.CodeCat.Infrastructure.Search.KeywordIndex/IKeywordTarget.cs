using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Search.KeywordIndex.Infrastructure
{
    public interface IKeywordTarget
    {
        string CommaDelimitedKeywords { get; set; }
        IKeywordIndexItem IndexItem { get; }
    }
}
