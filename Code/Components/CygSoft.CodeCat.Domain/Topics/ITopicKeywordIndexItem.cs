using CygSoft.CodeCat.Infrastructure;

namespace CygSoft.CodeCat.Domain.Topics
{
    public interface ITopicKeywordIndexItem : IKeywordIndexItem
    {
        string Syntax { get; set; }
    }
}
