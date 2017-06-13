using CygSoft.CodeCat.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.Topics
{
    public class TopicKeywordSearchIndexRepository : XmlKeywordSearchIndexRepository<TopicKeywordIndexItem>
    {
        public TopicKeywordSearchIndexRepository(string rootElement)
            : base(rootElement)
        {

        }

        protected override List<TopicKeywordIndexItem> LoadIndexItems(string fileText, Version expectedVersion)
        {
            base.FileFunctions.CheckVersion(fileText, expectedVersion);

            List<TopicKeywordIndexItem> indexItems = new List<TopicKeywordIndexItem>();

            XElement xElement = XElement.Parse(fileText);
            var items = from h in xElement.Elements("IndexItem")
                        select h;

            foreach (var item in items)
            {
                TopicKeywordIndexItem indexItem = new TopicKeywordIndexItem();
                indexItem.Deserialize(item);
                indexItems.Add(indexItem);
            }

            return indexItems.ToList();
        }
    }
}
