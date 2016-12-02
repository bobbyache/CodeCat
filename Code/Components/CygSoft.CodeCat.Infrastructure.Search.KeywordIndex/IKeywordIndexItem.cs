using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Search.KeywordIndex.Infrastructure
{
    public interface IKeywordIndexItem
    {
        string Id { get; set; }
        string FileTitle { get; }
        string[] Keywords { get; }
        string CommaDelimitedKeywords { get; }
        string Title { get; set; }
        DateTime DateModified { get; }
        DateTime DateCreated { get; }

        //string Syntax { get; set; }

        void AddKeywords(string commaDelimitedKeywords);
        bool AllKeywordsFound(string[] keywords);
        bool ValidateRemoveKeywords(string[] keywords);
        void RemoveKeywords(string[] keywords);
        void SetKeywords(string commaDelimitedKeywords);
        
    }
}
