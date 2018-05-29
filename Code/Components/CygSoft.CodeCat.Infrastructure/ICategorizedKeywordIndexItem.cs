using CygSoft.CodeCat.Category.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;

namespace CygSoft.CodeCat.Domain.Base
{
    public interface ICategorizedKeywordIndexItem : ICategorizedItem
    {
        IKeywordIndexItem IndexItem { get; }
    }
}
