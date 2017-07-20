using CygSoft.CodeCat.Category.Infrastructure;
using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Category
{
    public class ProjFile
    {
        public string FileName { get; private set; }

        private FileReader blueprintFileReader = new FileReader();
        private FileWriter fileWriter = new FileWriter();

        public void Load(string fileName)
        {
            this.FileName = fileName;
            blueprintFileReader.FileName = fileName;
        }

        public void Create(string fileName)
        {
            //if (!File.Exists(fileName))
            //    File.Create(fileName);
            fileWriter.Create(fileName);
        }

        // this method retrieves categories and queries at a certain level, probably has to be renamed
        // and possibly moved to another level of abstraction, perhaps.
        public bool GetChildItems(string parentCategoryId, out List<IItemCategory> itemList)
        {
            itemList = new List<IItemCategory>();
            List<ItemCategory> blueprintCategories;
            List<IItemCategory> blueprintHeaders;

            if (GetChildBlueprintCategories(parentCategoryId, out blueprintCategories))
            {
                itemList.AddRange(blueprintCategories.ToArray());
            }
            if (GetBlueprintHeadersByCategory(parentCategoryId, out blueprintHeaders))
            {
                itemList.AddRange(blueprintHeaders);
            }
            return false;
        }

        public bool GetBlueprintHeadersByCategory(string parentCategoryId, out List<IItemCategory> queryHeaderList)
        {
            return blueprintFileReader.GetBlueprintHeadersByCategory(parentCategoryId, out queryHeaderList);
        }

        public bool GetBlueprintCategoryById(string blueprintCategoryId, out ItemCategory blueprintCategory)
        {
            return blueprintFileReader.GetBlueprintCategoryById(blueprintCategoryId, out blueprintCategory);
        }

        public bool GetRootBlueprintCategories(out List<ItemCategory> blueprintCategoryList)
        {
            return blueprintFileReader.GetRootBlueprintCategories(out blueprintCategoryList);
        }

        public bool GetRootBlueprintCategories(out List<ITitledEntity> abstractBlueprintCategoryList)
        {
            abstractBlueprintCategoryList = new List<ITitledEntity>();
            List<ItemCategory> blueprintCategories;

            if (GetRootBlueprintCategories(out blueprintCategories))
            {
                abstractBlueprintCategoryList.AddRange(blueprintCategories.ToArray());
            }
            return false;
        }

        public bool GetChildBlueprintCategories(string parentCategoryId, out List<ItemCategory> blueprintCategoryList)
        {
            return blueprintFileReader.GetChildBlueprintCategories(parentCategoryId, out blueprintCategoryList);
        }

        public void AddBlueprintCategory(IItemCategory blueprintCategory, string parentId)
        {
            fileWriter.AddBlueprintCategory(this.FileName, blueprintCategory, parentId);
        }

        public void AddBlueprintHeader(ITitledEntity blueprintHeader, string parentId)
        {
            fileWriter.AddBlueprintHeader(this.FileName, blueprintHeader, parentId);
        }

        public void MoveBlueprintOrCategory(string displacedId, string newParentId)
        {
            fileWriter.MoveBlueprintOrCategory(this.FileName, displacedId, newParentId);
        }

        public void RenameBlueprintOrCategoryItem(string itemId, string newTitle)
        {
            fileWriter.RenameBlueprintOrCategoryItem(this.FileName, itemId, newTitle);
        }

        public void RemoveBlueprintOrCategoryItem(string itemId)
        {
            fileWriter.RemoveBlueprintOrCategoryItem(this.FileName, itemId);
        }

    }
}
