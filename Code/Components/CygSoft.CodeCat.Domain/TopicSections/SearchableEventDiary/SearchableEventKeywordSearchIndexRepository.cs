using CygSoft.CodeCat.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.TopicSections.SearchableEventDiary
{
    public class SearchableEventKeywordSearchIndexRepository : XmlKeywordSearchIndexRepository<SearchableEventKeywordIndexItem>
    {
        public SearchableEventKeywordSearchIndexRepository(string rootElement)
            : base(rootElement)
        {

        }

        protected override List<SearchableEventKeywordIndexItem> LoadIndexItems(string fileText, Version expectedVersion)
        {
            base.FileFunctions.CheckVersion(fileText, expectedVersion);

            List<SearchableEventKeywordIndexItem> indexItems = new List<SearchableEventKeywordIndexItem>();

            XElement xElement = XElement.Parse(fileText);
            var items = from h in xElement.Elements("IndexItem")
                        select h;

            foreach (var item in items)
            {
                SearchableEventKeywordIndexItem indexItem = new SearchableEventKeywordIndexItem();
                indexItem.Deserialize(item);
                indexItems.Add(indexItem);
            }

            return indexItems.ToList();
        }

        protected override void CheckFormat(string fileText)
        {
            //TODO: Need to do something about this, research how this works again.
            //base.CheckFormat(fileText);
        }
    }
}
