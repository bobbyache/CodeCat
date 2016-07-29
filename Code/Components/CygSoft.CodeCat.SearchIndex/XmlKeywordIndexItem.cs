using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Search.KeywordIndex
{
    public class XmlKeywordIndexItem : KeywordIndexItem
    {

        public XmlKeywordIndexItem() : base()
        {
        }

        public XmlKeywordIndexItem(string id, string title, string syntax, DateTime dateCreated, DateTime dateModified, string commaDelimitedKeywords)
            : base(id, title, syntax, dateCreated, dateModified, commaDelimitedKeywords)
        {
        }

        public XmlKeywordIndexItem(string title, string syntax, string commaDelimitedKeywords)
            : base(title, syntax, commaDelimitedKeywords)
        {
        }

        public void Deserialize(XElement element)
        {
            this.Id = (string)element.Attribute("ID");
            this.Title = (string)element.Element("Title");
            this.Syntax = (string)element.Element("Syntax");
            this.DateCreated = (DateTime)element.Element("DateCreated");
            this.DateModified = (DateTime)element.Element("DateModified");
            this.SetKeywords((string)element.Element("Keywords"));
        }

        public XElement Serialize()
        {
            XElement element = new XElement("IndexItem",
                    new XAttribute("ID", this.Id),
                    new XElement("Title", this.Title),
                    new XElement("Syntax", this.Syntax),
                    new XElement("DateCreated", this.DateCreated),
                    new XElement("DateModified", this.DateModified),
                    new XElement("Keywords", this.CommaDelimitedKeywords)
                    );
            return element;
        }
    }
}
