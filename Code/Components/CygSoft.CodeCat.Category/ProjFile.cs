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

        private FileReader fileReader = new FileReader();
        private FileWriter fileWriter = new FileWriter();

        public void Load(string fileName)
        {
            this.FileName = fileName;
            fileReader.FileName = fileName;
        }

        public void Create(string fileName)
        {
            //if (!File.Exists(fileName))
            //    File.Create(fileName);
            fileWriter.Create(fileName);
        }

        public List<IItemCategory> GetChildCategories(string parentCategoryId)
        {
            List<IItemCategory> itemList = new List<IItemCategory>();
            List<IItemCategory> blueprintHeaders;

            if (GetChildBlueprintCategories(parentCategoryId, out blueprintHeaders))
            {
                itemList.AddRange(blueprintHeaders.OfType<IItemCategory>());
            }
            return itemList;
        }

        public List<ICategorizedItem> GetChildCategorizedItemsByCategory(string parentCategoryId)
        {
            List<ICategorizedItem> categorizedItems;

            if (fileReader.GetChildCategorizedItemsByCategory(parentCategoryId, out categorizedItems))
                return categorizedItems;
            return null;
        }


        public bool GetBlueprintCategoryById(string blueprintCategoryId, out ItemCategory blueprintCategory)
        {
            return fileReader.GetCategoryById(blueprintCategoryId, out blueprintCategory);
        }

        public bool GetRootBlueprintCategories(out List<ItemCategory> blueprintCategoryList)
        {
            return fileReader.GetRootCategories(out blueprintCategoryList);
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

        public bool GetChildBlueprintCategories(string parentCategoryId, out List<IItemCategory> blueprintCategoryList)
        {
            return fileReader.GetChildCategories(parentCategoryId, out blueprintCategoryList);
        }

        public void AddBlueprintCategory(IItemCategory blueprintCategory, string parentId)
        {
            fileWriter.AddCategory(this.FileName, blueprintCategory, parentId);
        }

        public void AddCategorizedItem(ICategorizedItem categorizedItem, string categoryId)
        {
            fileWriter.AddCategorizedItem(this.FileName, categorizedItem, categoryId);
        }

        public void MoveCategory(string displacedId, string newParentId)
        {
            fileWriter.MoveCategory(this.FileName, displacedId, newParentId);
        }

        public void RenameBlueprintOrCategoryItem(string itemId, string newTitle)
        {
            fileWriter.RenameTargetItemOrCategoryItem(this.FileName, itemId, newTitle);
        }

        public void RemoveBlueprintOrCategoryItem(string itemId)
        {
            fileWriter.RemoveTargetItemOrCategory(this.FileName, itemId);
        }

    }
}
