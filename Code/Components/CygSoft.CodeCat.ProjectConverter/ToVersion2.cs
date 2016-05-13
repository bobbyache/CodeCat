using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.ProjectConverter
{
    internal class ToVersion2
    {
        public XElement ConvertIndex(XElement previousVersion)
        {
            XElement newVersion = new XElement(previousVersion.Name);

            // leave out the "Language" attribute...
            newVersion.Add(new XAttribute("ID", previousVersion.Attribute("ID").Value));
            newVersion.Add(new XElement("Title", previousVersion.Element("Title").Value));
            newVersion.Add(new XElement("Hits", previousVersion.Element("Hits").Value));
            newVersion.Add(new XElement("DateCreated", previousVersion.Element("DateCreated").Value));
            newVersion.Add(new XElement("DateModified", previousVersion.Element("DateModified").Value));
            newVersion.Add(new XElement("Keywords", previousVersion.Element("Keywords").Value));

            return newVersion;
        }

        public XDocument ConvertCodeFile(string oldCodeFilePath, XDocument oldCodeIndexDocument)
        {
            XDocument oldDocument = XDocument.Load(oldCodeFilePath);

            XElement oldIndex = oldCodeIndexDocument.Element("CodeCat_CodeIndex").Elements()
                .Where(e => e.Attribute("ID").Value == oldDocument.Element("Snippet").Attribute("ID").Value).SingleOrDefault();


            XDocument newDocument = XDocument.Parse(oldDocument.ToString());

            XElement rootElement = newDocument.Element("Snippet");
            rootElement.Add(new XAttribute("Syntax", oldIndex.Element("Language").Value));

            newDocument.Declaration = oldDocument.Declaration;

            return newDocument;
        }

        public XDocument CreateProjectDocument()
        {
            XElement projectElement = new XElement("CodeCat_Project", new XAttribute("Version", "2"),
                new XElement("CodeLibrary",
                    new XAttribute("Library", "CODE"),
                    new XAttribute("File", "_code.xml")
                ));

            XDocument projectDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("CodeCat_Project", new XAttribute("Version", "2"),
                    new XElement("CodeLibrary",
                        new XAttribute("Library", "CODE"),
                        new XAttribute("File", "_code.xml")
                )));


            return projectDocument;
        }

        public XDocument CreateCodeIndexDocument(string oldDocumentPath)
        {
            XDocument oldDocument = XDocument.Load(oldDocumentPath);
            XDocument newDocument = XDocument.Parse(oldDocument.ToString());

            XElement rootElement = newDocument.Element("CodeCat_CodeIndex");
            rootElement.RemoveNodes();
            rootElement.Add(new XAttribute("Version", "2"));

            newDocument.Declaration = oldDocument.Declaration;

            foreach (XElement element in oldDocument.Element("CodeCat_CodeIndex").Elements())
            {
                XElement newElement = ConvertIndex(element);
                rootElement.Add(newElement);
            }

            return newDocument;
        }
    }
}
