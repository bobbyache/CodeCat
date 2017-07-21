using CygSoft.CodeCat.Infrastructure;

namespace CygSoft.CodeCat.Category.Infrastructure
{
    public interface ICategorizedItem : ITitledEntity
    {
        string InstanceId { get; }
        string ItemId { get; }
    }
}