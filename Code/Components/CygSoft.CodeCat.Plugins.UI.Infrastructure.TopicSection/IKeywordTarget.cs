namespace CygSoft.CodeCat.Plugins.UI.Infrastructure.TopicSection
{
    public interface IKeywordTarget
    {
        string CommaDelimitedKeywords { get; set; }
        IKeywordIndexItem IndexItem { get; }
    }
}
