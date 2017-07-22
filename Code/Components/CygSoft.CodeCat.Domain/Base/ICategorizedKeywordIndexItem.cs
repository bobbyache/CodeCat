using CygSoft.CodeCat.Category.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CygSoft.CodeCat.Infrastructure;

namespace CygSoft.CodeCat.Domain.Base
{
    public interface ICategorizedKeywordIndexItem : ICategorizedItem
    {
        IKeywordIndexItem IndexItem { get; }
    }
}
