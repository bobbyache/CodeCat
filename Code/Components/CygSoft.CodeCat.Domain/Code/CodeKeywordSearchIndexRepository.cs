using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.Code
{
    public class CodeKeywordSearchIndexRepository :  XmlKeywordSearchIndexRepository<CodeKeywordIndexItem>
    {
        public CodeKeywordSearchIndexRepository(string rootElement) : base(rootElement)
        {

        }

        protected override List<CodeKeywordIndexItem> LoadIndexItems(string filePath, int currentVersion)
        {
            XElement xElement = XElement.Load(filePath);
            CheckVersion(xElement, currentVersion);

            List<CodeKeywordIndexItem> indexItems = new List<CodeKeywordIndexItem>();

            var items = from h in xElement.Elements("IndexItem")
                        select h;

            foreach (var item in items)
            {
                CodeKeywordIndexItem indexItem = new CodeKeywordIndexItem();
                indexItem.Deserialize(item);
                indexItems.Add(indexItem);
            }

            return indexItems.ToList();
        }
    }
}
