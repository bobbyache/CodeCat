using CygSoft.CodeCat.Search.KeywordIndex;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.Qik
{
    public class QikTemplateKeywordSearchIndexRepository : XmlKeywordSearchIndexRepository<QikTemplateKeywordIndexItem>
    {
        public QikTemplateKeywordSearchIndexRepository(string rootElement)
            : base(rootElement)
        {

        }

        protected override List<QikTemplateKeywordIndexItem> LoadIndexItems(string fileText, int expectedVersion)
        {
            XElement xElement = XElement.Parse(fileText);
            CheckVersion(xElement, expectedVersion);

            List<QikTemplateKeywordIndexItem> indexItems = new List<QikTemplateKeywordIndexItem>();

            var items = from h in xElement.Elements("IndexItem")
                        select h;

            foreach (var item in items)
            {
                QikTemplateKeywordIndexItem indexItem = new QikTemplateKeywordIndexItem();
                indexItem.Deserialize(item);
                indexItems.Add(indexItem);
            }

            return indexItems.ToList();
        }
    }
}
