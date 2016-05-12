using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Infrastructure
{
    public interface IKeywordIndexItem
    {
        string Id { get; set; }
        string FileTitle { get; }
        string[] Keywords { get; }
        int NoOfHits { get; set; }
        string CommaDelimitedKeywords { get; }
        string Title { get; set; }
        DateTime DateModified { get; }
        DateTime DateCreated { get; }

        void AddKeywords(string commaDelimitedKeywords);
        bool AllKeywordsFound(string[] keywords);
        void RemoveKeywords(string[] keywords);
        void SetKeywords(string commaDelimitedKeywords);
        
    }
}
