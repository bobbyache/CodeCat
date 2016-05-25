using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Search.KeywordIndex
{
    public class CodeFileKeywordIndexItem : KeywordIndexItem, ICodeFileKeywordIndexItem
    {
        public string Syntax { get; set; }

        public CodeFileKeywordIndexItem() : base()
        {
            this.Syntax = string.Empty;
        }

        public CodeFileKeywordIndexItem(string title, string syntax, string commaDelimitedKeywords) 
            : base(title, commaDelimitedKeywords)
        {
            this.Syntax = syntax;
        }

        public CodeFileKeywordIndexItem(string id, string title, string syntax, DateTime dateCreated, DateTime dateModified, string commaDelimitedKeywords) 
            : base(id, title, dateCreated, dateModified, commaDelimitedKeywords)
        {
            this.Syntax = syntax;
        }
    }
}
