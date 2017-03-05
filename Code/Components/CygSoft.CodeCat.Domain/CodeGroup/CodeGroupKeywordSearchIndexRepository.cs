using CygSoft.CodeCat.Search.KeywordIndex;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.CodeGroup
{
    public class CodeGroupKeywordSearchIndexRepository : XmlKeywordSearchIndexRepository<CodeGroupKeywordIndexItem>
    {
        public CodeGroupKeywordSearchIndexRepository(string rootElement)
            : base(rootElement)
        {

        }

        protected override List<CodeGroupKeywordIndexItem> LoadIndexItems(string filePath, int currentVersion)
        {
            XElement xElement = XElement.Load(filePath);
            CheckVersion(xElement, currentVersion);

            List<CodeGroupKeywordIndexItem> indexItems = new List<CodeGroupKeywordIndexItem>();

            var items = from h in xElement.Elements("IndexItem")
                        select h;

            foreach (var item in items)
            {
                CodeGroupKeywordIndexItem indexItem = new CodeGroupKeywordIndexItem();
                indexItem.Deserialize(item);
                indexItems.Add(indexItem);
            }

            return indexItems.ToList();
        }
    }
}
