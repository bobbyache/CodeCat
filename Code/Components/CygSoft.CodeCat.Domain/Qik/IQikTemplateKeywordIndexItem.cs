using CygSoft.CodeCat.Infrastructure;

namespace CygSoft.CodeCat.Domain.Qik
{
    public interface IQikTemplateKeywordIndexItem : IKeywordIndexItem
    {
        string Syntax { get; set; }
    }
}
