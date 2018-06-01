using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.TopicSections.SearchableSnippet
{
    public interface ISearchableSnippetKeywordIndexItem : IKeywordIndexItem, ICategorizedListItem
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
            Category = string.Empty;
        }

        public override void Deserialize(XElement element)
        {
            base.Deserialize(element);
            Syntax = (string)element.Attribute("Syntax");
            Category = (string)element.Attribute("Category");
            Text = (string)element.Element("Text");
        }

        public override XElement Serialize()
        {
            XElement element = base.Serialize();
            element.Add(new XAttribute("Syntax", Syntax));
            element.Add(new XAttribute("Category", Category));
            element.Add(new XElement("Text", new XCData(Text)));
            return element;
        }
    }
}
