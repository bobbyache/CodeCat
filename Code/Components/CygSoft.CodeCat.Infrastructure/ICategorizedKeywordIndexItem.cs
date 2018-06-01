namespace CygSoft.CodeCat.Infrastructure
{
    public interface ICategorizedKeywordIndexItem : ICategorizedItem
    {
        IKeywordIndexItem IndexItem { get; }
    }
}
