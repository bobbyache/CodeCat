using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.ProjectConverter
{
    internal class ToVersion2
    {
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
                        new XAttribute("File", @"code\_code.xml")
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

        public XDocument ConvertCodeFile(string oldCodeFilePath, XDocument oldCodeIndexDocument)
        {
            XDocument newDocument = null;
            XDocument oldDocument = XDocument.Load(oldCodeFilePath);


            
            XElement oldIndex = oldCodeIndexDocument.Element("CodeCat_CodeIndex").Elements()
                .Where(e => e.Attribute("ID").Value == oldDocument.Element("Snippet").Attribute("ID").Value).SingleOrDefault();

            if (oldIndex != null)
            {
                newDocument = XDocument.Parse(oldDocument.ToString());
                XElement rootElement = newDocument.Element("Snippet");
                XElement oldSyntaxElement = oldIndex.Element("Language");

                if (oldSyntaxElement != null)
                    rootElement.Add(new XAttribute("Syntax", oldIndex.Element("Language").Value));

                newDocument.Declaration = oldDocument.Declaration;
            }
            else
            {
                // The old link was already broken and the old index knows nothing about
                // this code file !!!
                // we'll have to copy it anyway !!!
                newDocument = XDocument.Parse(oldDocument.ToString());
            }

            return newDocument;
        }

        private XElement ConvertIndex(XElement previousVersion)
        {
            XElement newVersion = new XElement(previousVersion.Name);

            // leave out the "Language" attribute...
            newVersion.Add(new XAttribute("ID", previousVersion.Attribute("ID").Value));
            newVersion.Add(new XElement("Title", previousVersion.Element("Title").Value));
            newVersion.Add(new XElement("Syntax", previousVersion.Element("Language").Value));
            newVersion.Add(new XElement("Hits", previousVersion.Element("Hits").Value));
            newVersion.Add(new XElement("DateCreated", previousVersion.Element("DateCreated").Value));
            newVersion.Add(new XElement("DateModified", previousVersion.Element("DateModified").Value));
            newVersion.Add(new XElement("Keywords", previousVersion.Element("Keywords").Value));

            return newVersion;
        }
    }
}
