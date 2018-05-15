using System;

namespace CygSoft.CodeCat.Plugins.UI.Infrastructure.TopicSection
{
    public interface IKeywordIndexItem : ITitledEntity
    {
        string FileTitle { get; }
        string[] Keywords { get; }
        string CommaDelimitedKeywords { get; }
        DateTime DateModified { get; }
        DateTime DateCreated { get; }

        void AddKeywords(string commaDelimitedKeywords);
        bool AllKeywordsFound(string[] keywords);
        bool ValidateRemoveKeywords(string[] keywords);
        void RemoveKeywords(string[] keywords);
        void SetKeywords(string commaDelimitedKeywords);
        
    }
}
