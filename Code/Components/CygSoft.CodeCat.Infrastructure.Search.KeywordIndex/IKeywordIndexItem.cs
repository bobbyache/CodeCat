using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Search.KeywordIndex.Infrastructure
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
