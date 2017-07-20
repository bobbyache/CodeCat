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

        public bool GetBlueprintCategoryById(string queryCategoryId, out BlueprintCategory queryCategory)
        {
            queryCategory = null;

            XElement rootElement;
            if (FetchRootElement(out rootElement))
            {
                if (rootElement.Element("BlueprintCategories").Descendants().Any(r => (string)r.Attribute("ID") == queryCategoryId))
                {
                    queryCategory = (from el in rootElement.Element("BlueprintCategories").Descendants()
                                     where (string)el.Attribute("ID") == queryCategoryId
                                     select new BlueprintCategory((string)el.Attribute("ID"))
                                     {
                                         Title = (string)el.Attribute("Title")
                                     }).Single();
                    return true;
                }
            }

            return false;
        }

        public bool GetRootBlueprintCategories(out List<BlueprintCategory> queryCategoryList)
        {
            queryCategoryList = new List<BlueprintCategory>();

            XElement rootElement;
            if (FetchRootElement(out rootElement))
            {
                if (rootElement.Element("BlueprintCategories").Elements("BlueprintCategory").Any())
                {
                    queryCategoryList = (from el in rootElement.Element("BlueprintCategories").Elements("BlueprintCategory")
                                         select new BlueprintCategory((string)el.Attribute("ID"))
                                         {
                                             Title = (string)el.Attribute("Title")
                                         }).ToList();
                    return true;
                }
            }

            return false;
        }

        public bool GetChildBlueprintCategories(string parentCategoryId, out List<BlueprintCategory> queryCategoryList)
        {
            XElement rootElement;
            queryCategoryList = new List<BlueprintCategory>();

            if (FetchRootElement(out rootElement))
            {
                if (rootElement.Element("BlueprintCategories").Descendants().Any(r => (string)r.Attribute("ID") == parentCategoryId))
                {
                    XElement parentElement = (from el in rootElement.Element("BlueprintCategories").Descendants()
                                              where (string)el.Attribute("ID") == parentCategoryId
                                              select el).Single();


                    if (parentElement.Elements("BlueprintCategory").Any())
                    {
                        queryCategoryList = (from el in parentElement.Elements("BlueprintCategory")
                                             select new BlueprintCategory((string)el.Attribute("ID"))
                                             {
                                                 Title = (string)el.Attribute("Title")
                                             }).ToList();
                        return true;
                    }
                }
            }

            return false;
        }

        public bool GetBlueprintHeadersByCategory(string parentCategoryId, out List<IBlueprintCategory> queryHeaderList)
        {
            XElement rootElement;
            queryHeaderList = new List<IBlueprintCategory>();

            if (FetchRootElement(out rootElement))
            {
                if (rootElement.Element("BlueprintCategories").Descendants().Any(r => (string)r.Attribute("ID") == parentCategoryId))
                {
                    XElement parentElement = (from el in rootElement.Element("BlueprintCategories").Descendants()
                                              where (string)el.Attribute("ID") == parentCategoryId
                                              select el).Single();


                    if (parentElement.Elements("BlueprintHeader").Any())
                    {
                        queryHeaderList = (from el in parentElement.Elements("BlueprintHeader")
                                           select new BlueprintCategory((string)el.Attribute("ID"))
                                           {
                                               Title = (string)el.Attribute("Title")
                                           }).OfType<IBlueprintCategory>().ToList();
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
