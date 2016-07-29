using CygSoft.CodeCat.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.Qik
{
    public class QikKeywordSearchIndexRepository : KeywordSearchIndexRepository<QikKeywordIndexItem>
    {
        protected override List<QikKeywordIndexItem> LoadIndexItems(string filePath, int currentVersion)
        {
            XElement xElement = XElement.Load(filePath);
            CheckVersion(xElement, currentVersion);

            List<QikKeywordIndexItem> indexItems = new List<QikKeywordIndexItem>();

            var items = from h in xElement.Elements("IndexItem")
                        select h;

            foreach (var item in items)
            {
                QikKeywordIndexItem indexItem = new QikKeywordIndexItem();
                indexItem.Deserialize(item);
                indexItems.Add(indexItem);
            }

            return indexItems.ToList();
        }
    }
}
