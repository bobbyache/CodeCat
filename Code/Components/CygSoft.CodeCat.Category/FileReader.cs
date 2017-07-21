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
    public class FileReader
    {
        public string FileName { get; set; }

        public FileReader()
        {
        }

        public FileReader(string fileName)
        {
            this.FileName = fileName;
        }

        public bool GetCategoryById(string id, out ItemCategory category)
        {
            category = null;

            XElement rootElement;
            if (FetchRootElement(out rootElement))
            {
                if (rootElement.Element("Categories").Descendants().Any(r => (string)r.Attribute("ID") == id))
                {
                    category = (from el in rootElement.Element("Categories").Descendants()
                                     where (string)el.Attribute("ID") == id
                                     select new ItemCategory((string)el.Attribute("ID"))
                                     {
                                         Title = (string)el.Attribute("Title")
                                     }).Single();
                    return true;
                }
            }

            return false;
        }

        public bool GetRootCategories(out List<ItemCategory> categories)
        {
            categories = new List<ItemCategory>();

            XElement rootElement;
            if (FetchRootElement(out rootElement))
            {
                if (rootElement.Element("Categories").Elements("Category").Any())
                {
                    categories = (from el in rootElement.Element("Categories").Elements("Category")
                                         select new ItemCategory((string)el.Attribute("ID"))
                                         {
                                             Title = (string)el.Attribute("Title")
                                         }).ToList();
                    return true;
                }
            }

            return false;
        }

        public bool GetChildCategories(string categoryId, out List<IItemCategory> categoryList)
        {
            XElement rootElement;
            categoryList = new List<IItemCategory>();

            if (FetchRootElement(out rootElement))
            {
                if (rootElement.Element("Categories").Descendants().Any(r => (string)r.Attribute("ID") == categoryId))
                {
                    XElement parentElement = (from el in rootElement.Element("Categories").Descendants()
                                              where (string)el.Attribute("ID") == categoryId
                                              select el).Single();


                    if (parentElement.Elements("Category").Any())
                    {
                        categoryList = (from el in parentElement.Elements("Category")
                                             select new ItemCategory((string)el.Attribute("ID"))
                                             {
                                                 Title = (string)el.Attribute("Title")
                                             }).OfType<IItemCategory>().ToList();
                        return true;
                    }
                }
            }

            return false;
        }

        public bool GetChildCategorizedItemsByCategory(string categoryId, out List<ICategorizedItem> categorizedItems)
        {
            XElement rootElement;
            categorizedItems = new List<ICategorizedItem>();

            if (FetchRootElement(out rootElement))
            {
                if (rootElement.Element("Categories").Descendants().Any(r => (string)r.Attribute("ID") == categoryId))
                {
                    XElement parentElement = (from el in rootElement.Element("Categories").Descendants()
                                              where (string)el.Attribute("ID") == categoryId
                                              select el).Single();


                    if (parentElement.Elements("Item").Any())
                    {
                        categorizedItems = (from el in parentElement.Elements("Item")
                                           select new CategorizedItem((string)el.Attribute("ID"))
                                           {
                                               Title = (string)el.Attribute("ID")
                                           }).OfType<ICategorizedItem>().ToList();
                        return true;
                    }
                }
            }

            return false;
        }


        private bool FetchRootElement(out XElement rootElement)
        {
            rootElement = null;

            if (File.Exists(this.FileName))
            {
                XDocument doc = XDocument.Load(this.FileName);
                rootElement = doc.Root;
                return true;
            }
            return false;
        }

    }
}
