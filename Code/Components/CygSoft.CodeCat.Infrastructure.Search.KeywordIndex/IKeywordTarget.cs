using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Infrastructure.Search.KeywordIndex
{
    public interface IKeywordTarget
    {
        IKeywordIndexItem IndexItem { get; }
    }
}
