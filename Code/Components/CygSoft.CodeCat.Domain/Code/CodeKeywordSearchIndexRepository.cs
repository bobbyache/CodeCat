using CygSoft.CodeCat.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.Code
{
    public class CodeKeywordSearchIndexRepository :  XmlKeywordSearchIndexRepository<CodeKeywordIndexItem>
    {
        public CodeKeywordSearchIndexRepository(string rootElement) : base(rootElement)
        {

        }

        protected override List<CodeKeywordIndexItem> LoadIndexItems(string fileText, Version expectedVersion)
        {
            base.FileFunctions.CheckVersion(fileText, expectedVersion);

            List<CodeKeywordIndexItem> indexItems = new List<CodeKeywordIndexItem>();

            XElement xElement = XElement.Parse(fileText);
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
