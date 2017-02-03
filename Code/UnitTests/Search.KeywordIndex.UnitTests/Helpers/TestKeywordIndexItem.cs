using CygSoft.CodeCat.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search.KeywordIndex.UnitTests.Helpers
{
    public class TestKeywordIndexItem : KeywordIndexItem
    {
        public TestKeywordIndexItem() : base()
        {
        }

        public TestKeywordIndexItem(string title, string commaDelimitedKeywords) : base(title, commaDelimitedKeywords)
        {

        }

        public TestKeywordIndexItem(string id, string title, DateTime dateCreated, DateTime dateModified, string commaDelimitedKeywords) : base(id, title, dateCreated, dateModified, commaDelimitedKeywords)
        {

        }
    }
}
