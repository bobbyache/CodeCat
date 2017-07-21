using CygSoft.CodeCat.Category.Infrastructure;
using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Category
{
    public class FileWriter
    {
        public void Create(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                XElement documentElement = new XElement("CategoryHierarchy",
                    new XElement("Categories")
                );

                documentElement.Save(filePath);
            }
        }

        public void AddCategoryItem(string filePath, ITitledEntity categoryItem, string categoryId)
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                return;

            XDocument doc = XDocument.Load(filePath);
            XElement rootElement = doc.Root;

            if (rootElement.Element("Categories").Descendants().Any(r => (string)r.Attribute("ID") == categoryId))
            {
                XElement parentElement = (from el in rootElement.Element("Categories").Descendants()
                                          where (string)el.Attribute("ID") == categoryId
                                          select el).Single();

                parentElement.Add(new XElement("TargetItem",
                                    new XAttribute("ID", categoryItem.Id)
                                ));

                doc.Save(filePath);
            }
        }

        public void AddBlueprintCategory(string filePath, ITitledEntity blueprintCategory, string parentCategoryId)
        {

            XDocument doc = XDocument.Load(filePath);
            XElement rootElement = doc.Root;

            if (string.IsNullOrWhiteSpace(parentCategoryId))
            {
                rootElement.Element("Categories").Add(new XElement("Category",
                                    new XAttribute("ID", blueprintCategory.Id),
                                    new XAttribute("Title", blueprintCategory.Title)
                                ));

                doc.Save(filePath);
            }
            else
            {
                if (rootElement.Element("Categories").Descendants().Any(r => (string)r.Attribute("ID") == parentCategoryId))
                {
                    XElement parentElement = (from el in rootElement.Element("Categories").Descendants()
                                              where (string)el.Attribute("ID") == parentCategoryId
                                              select el).Single();

                    parentElement.Add(new XElement("Category",
                                        new XAttribute("ID", blueprintCategory.Id),
                                        new XAttribute("Title", blueprintCategory.Title)
                                    ));
                    doc.Save(filePath);
                }
            }
        }

        public void MoveBlueprintOrCategory(string filePath, string displacedId, string newParentId)
        {
            XDocument doc = XDocument.Load(filePath);
            XElement rootElement = doc.Root;

            if (!string.IsNullOrWhiteSpace(displacedId) && !string.IsNullOrWhiteSpace(newParentId))
            {
                XElement parentElement = null;
                XElement displacedElement = null;

                if (rootElement.Element("Categories").Descendants().Any(r => (string)r.Attribute("ID") == displacedId))
                {
                    parentElement = (from el in rootElement.Element("Categories").Descendants()
                                     where (string)el.Attribute("ID") == newParentId
                                     select el).Single();

                    displacedElement = (from el in rootElement.Element("Categories").Descendants()
                                        where (string)el.Attribute("ID") == displacedId
                                        select el).Single();

                    displacedElement.Remove();

                    parentElement.Add(displacedElement);

                    doc.Save(filePath);
                }
            }
        }

        public void RenameBlueprintOrCategoryItem(string filePath, string itemId, string newTitle)
        {
            XDocument doc = XDocument.Load(filePath);
            XElement rootElement = doc.Root;

            if (!string.IsNullOrWhiteSpace(itemId))
            {
                if (rootElement.Element("Categories").Descendants().Any(r => (string)r.Attribute("ID") == itemId))
                {
                    XElement element = (from el in rootElement.Element("Categories").Descendants()
                                        where (string)el.Attribute("ID") == itemId
                                        select el).Single();
                    element.Attribute("Title").Value = newTitle;

                    doc.Save(filePath);
                }
            }
        }

        public void RemoveBlueprintOrCategoryItem(string filePath, string itemId)
        {
            XDocument doc = XDocument.Load(filePath);
            XElement rootElement = doc.Root;

            if (!string.IsNullOrWhiteSpace(itemId))
            {
                if (rootElement.Element("Categories").Descendants().Any(r => (string)r.Attribute("ID") == itemId))
                {
                    XElement element = (from el in rootElement.Element("Categories").Descendants()
                                        where (string)el.Attribute("ID") == itemId
                                        select el).Single();
                    element.Remove();

                    doc.Save(filePath);
                }
            }
        }

        private bool FetchRootElement(string filePath, out XElement rootElement)
        {
            rootElement = null;

            if (File.Exists(filePath))
            {
                XDocument doc = XDocument.Load(filePath);
                rootElement = doc.Root;
                return true;
            }
            return false;
        }
    }
}
