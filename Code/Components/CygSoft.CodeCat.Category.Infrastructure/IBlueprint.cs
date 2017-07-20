using CygSoft.CodeCat.Infrastructure;

namespace CygSoft.CodeCat.Category.Infrastructure
{
    public interface IBlueprint : ITitledEntity
    {
        string Id { get; set; }
    }
}