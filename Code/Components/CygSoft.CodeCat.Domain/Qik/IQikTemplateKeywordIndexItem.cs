using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;

namespace CygSoft.CodeCat.Domain.Qik
{
    public interface IQikTemplateKeywordIndexItem : IKeywordIndexItem
    {
        string Syntax { get; set; }
    }
}
