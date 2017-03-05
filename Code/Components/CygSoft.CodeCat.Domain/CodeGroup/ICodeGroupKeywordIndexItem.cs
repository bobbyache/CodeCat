using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;

namespace CygSoft.CodeCat.Domain.CodeGroup
{
    public interface ICodeGroupKeywordIndexItem : IKeywordIndexItem
    {
        string Syntax { get; set; }
    }
}
