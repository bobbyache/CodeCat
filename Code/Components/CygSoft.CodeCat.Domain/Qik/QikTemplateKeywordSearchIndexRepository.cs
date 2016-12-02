using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.Qik
{
    public class QikTemplateKeywordSearchIndexRepository : XmlKeywordSearchIndexRepository<QikTemplateKeywordIndexItem>
    {
        public QikTemplateKeywordSearchIndexRepository(string rootElement)
            : base(rootElement)
        {

        }

        protected override List<QikTemplateKeywordIndexItem> LoadIndexItems(string filePath, int currentVersion)
        {
            XElement xElement = XElement.Load(filePath);
            CheckVersion(xElement, currentVersion);

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
