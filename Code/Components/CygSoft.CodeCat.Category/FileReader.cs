﻿using CygSoft.CodeCat.Category.Infrastructure;
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

        public bool GetBlueprintCategoryById(string queryCategoryId, out ItemCategory queryCategory)
        {
            queryCategory = null;

            XElement rootElement;
            if (FetchRootElement(out rootElement))
            {
                if (rootElement.Element("Categories").Descendants().Any(r => (string)r.Attribute("ID") == queryCategoryId))
                {
                    queryCategory = (from el in rootElement.Element("Categories").Descendants()
                                     where (string)el.Attribute("ID") == queryCategoryId
                                     select new ItemCategory((string)el.Attribute("ID"))
                                     {
                                         Title = (string)el.Attribute("Title")
                                     }).Single();
                    return true;
                }
            }

            return false;
        }

        public bool GetRootBlueprintCategories(out List<ItemCategory> queryCategoryList)
        {
            queryCategoryList = new List<ItemCategory>();

            XElement rootElement;
            if (FetchRootElement(out rootElement))
            {
                if (rootElement.Element("Categories").Elements("Category").Any())
                {
                    queryCategoryList = (from el in rootElement.Element("Categories").Elements("Category")
                                         select new ItemCategory((string)el.Attribute("ID"))
                                         {
                                             Title = (string)el.Attribute("Title")
                                         }).ToList();
                    return true;
                }
            }

            return false;
        }

        public bool GetChildBlueprintCategories(string parentCategoryId, out List<IItemCategory> queryCategoryList)
        {
            XElement rootElement;
            queryCategoryList = new List<IItemCategory>();

            if (FetchRootElement(out rootElement))
            {
                if (rootElement.Element("Categories").Descendants().Any(r => (string)r.Attribute("ID") == parentCategoryId))
                {
                    XElement parentElement = (from el in rootElement.Element("Categories").Descendants()
                                              where (string)el.Attribute("ID") == parentCategoryId
                                              select el).Single();


                    if (parentElement.Elements("Category").Any())
                    {
                        queryCategoryList = (from el in parentElement.Elements("Category")
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

        public bool GetBlueprintHeadersByCategory(string parentCategoryId, out List<ICategoryItem> queryHeaderList)
        {
            XElement rootElement;
            queryHeaderList = new List<ICategoryItem>();

            if (FetchRootElement(out rootElement))
            {
                if (rootElement.Element("Categories").Descendants().Any(r => (string)r.Attribute("ID") == parentCategoryId))
                {
                    XElement parentElement = (from el in rootElement.Element("Categories").Descendants()
                                              where (string)el.Attribute("ID") == parentCategoryId
                                              select el).Single();


                    if (parentElement.Elements("TargetItem").Any())
                    {
                        queryHeaderList = (from el in parentElement.Elements("TargetItem")
                                           select new CategoryItem((string)el.Attribute("ID"))
                                           {
                                           }).OfType<ICategoryItem>().ToList();
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
