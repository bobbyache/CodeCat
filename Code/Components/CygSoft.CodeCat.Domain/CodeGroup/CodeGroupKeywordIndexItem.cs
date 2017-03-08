using CygSoft.CodeCat.Search.KeywordIndex;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.CodeGroup
{
    public class CodeGroupKeywordIndexItem : XmlKeywordIndexItem, ICodeGroupKeywordIndexItem
    {
        public CodeGroupKeywordIndexItem()
        {
            Syntax = string.Empty;
        }

        public CodeGroupKeywordIndexItem(string title, string syntax, string commaDelimitedKeywords)
                    : base(title, syntax, commaDelimitedKeywords)
        {
            Syntax = syntax;
        }

        public string Syntax { get; set; }

        public override void Deserialize(XElement element)
        {
            base.Deserialize(element);
            Syntax = (string)element.Element("Syntax");
        }

        public override XElement Serialize()
        {
            XElement element = base.Serialize();
            element.Add(new XElement("Syntax", Syntax));

            return element;
        }
    }
}
