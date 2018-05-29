using CygSoft.CodeCat.Infrastructure;

namespace CygSoft.CodeCat.Category.Infrastructure
{
    public interface ICategorizedItem : ITitledEntity
    {
        string ItemId { get; }
    }
}