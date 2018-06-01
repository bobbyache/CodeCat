using CygSoft.CodeCat.Infrastructure;

namespace CygSoft.CodeCat.Infrastructure
{
    public interface ICategorizedItem : ITitledEntity
    {
        string ItemId { get; }
    }
}