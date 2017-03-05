using CygSoft.CodeCat.Search.KeywordIndex;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.Qik
{
    public class QikTemplateKeywordIndexItem : XmlKeywordIndexItem, IQikTemplateKeywordIndexItem
    {
        public QikTemplateKeywordIndexItem()
        {
            this.Syntax = string.Empty;
        }
        
        public QikTemplateKeywordIndexItem(string title, string syntax, string commaDelimitedKeywords)
                    : base(title, syntax, commaDelimitedKeywords)
        {
            this.Syntax = syntax;
        }

        public string Syntax { get; set; }

        public override void Deserialize(System.Xml.Linq.XElement element)
        {
            base.Deserialize(element);
            this.Syntax = (string)element.Element("Syntax");
        }

        public override System.Xml.Linq.XElement Serialize()
        {
            XElement element = base.Serialize();
            element.Add(new XElement("Syntax", this.Syntax));

            return element;
        }
    }
}
