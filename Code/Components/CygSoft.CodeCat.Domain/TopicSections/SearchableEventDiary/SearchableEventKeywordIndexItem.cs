using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Infrastructure;
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

        public string Category
        {
            get { return EventDateGrouper.GetGroup(DateTime.Now, this.DateCreated); }
            set { throw new InvalidOperationException("Cannot set the category for a diary event."); }
        }

        public SearchableEventKeywordIndexItem()
        {
            Text = string.Empty;
        }

        public override void Deserialize(XElement element)
        {
            base.Deserialize(element);
            Text = (string)element.Element("Text");
        }

        public override XElement Serialize()
        {
            XElement element = base.Serialize();
            element.Add(new XElement("Text", new XCData(Text)));
            return element;
        }
    }

}
