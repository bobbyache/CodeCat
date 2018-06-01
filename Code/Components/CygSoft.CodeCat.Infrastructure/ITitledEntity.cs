namespace CygSoft.CodeCat.Infrastructure
{
    public interface ITitledEntity
    {
        string Id { get; }
        string Title { get; set; }
    }
}
