using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.TopicSections.SearchableEventDiary
{
    public interface ISearchableEventKeywordIndexItem : IKeywordIndexItem, ICategorizedItem
    {
        string Text { get; set; }
    }

    public class SearchableEventKeywordIndexItem : XmlKeywordIndexItem, ISearchableEventKeywordIndexItem
    {
        public string Text { get; set; }

        public string Category { get; set; }

        public SearchableEventKeywordIndexItem()
        {
            Text = string.Empty;
            Category = string.Empty;
        }

        public override void Deserialize(XElement element)
        {
            base.Deserialize(element);
            Category = (string)element.Attribute("Category");
            Text = (string)element.Element("Text");
        }

        public override XElement Serialize()
        {
            XElement element = base.Serialize();
            element.Add(new XAttribute("Category", Category));
            element.Add(new XElement("Text", new XCData(Text)));
            return element;
        }
    }

}
