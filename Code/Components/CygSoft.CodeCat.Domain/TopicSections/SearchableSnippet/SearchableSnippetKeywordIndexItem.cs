using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.TopicSections.SearchableSnippet
{
    public interface ISearchableSnippetKeywordIndexItem : IKeywordIndexItem, ICategorizedItem
    {
        string Syntax { get; set; }
        string Text { get; set; }
    }

    public class SearchableSnippetKeywordIndexItem : XmlKeywordIndexItem, ISearchableSnippetKeywordIndexItem
    {
        public string Syntax { get; set; }
        public string Text { get; set; }

        public string Category { get; set; }

        public SearchableSnippetKeywordIndexItem()
        {
            Syntax = string.Empty;
            Text = string.Empty;
        }

        public override void Deserialize(XElement element)
        {
            base.Deserialize(element);
            Syntax = (string)element.Element("Syntax");
            Text = (string)element.Element("Text");
        }

        public override XElement Serialize()
        {
            XElement element = base.Serialize();
            element.Add(new XElement("Syntax", Syntax));
            element.Add(new XElement("Text", new XCData(Text)));
            return element;
        }
    }
}
