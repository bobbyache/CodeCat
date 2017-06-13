using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;

namespace CygSoft.CodeCat.Domain.Topics
{
    public interface ITopicKeywordIndexItem : IKeywordIndexItem
    {
        string Syntax { get; set; }
    }
}
