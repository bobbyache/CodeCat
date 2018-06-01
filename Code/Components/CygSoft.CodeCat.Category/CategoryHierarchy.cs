using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;

namespace CygSoft.CodeCat.Category
{
    public class CategoryHierarchy
    {
        private ProjFile projectFile = new ProjFile();

        public bool FileLoaded { get; private set; }
        public string ProjectFileTitle { get; private set; }
        public string ProjectFilePath { get; private set; }

        public void LoadProject(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    CreateProject(filePath);
                else
                {
                    this.ProjectFilePath = filePath;
                    projectFile.Load(this.ProjectFilePath);
                    this.FileLoaded = true;
                }
            }
            catch (Exception ex)
            {
                // you'll want to handle this by routing to a log file.
                throw ex;
            }
        }

        public void CreateProject(string filePath)
        {
            try
            {
                this.ProjectFilePath = filePath;
                projectFile.Create(this.ProjectFilePath);
                projectFile.Load(this.ProjectFilePath);
                this.FileLoaded = true;
            }
            catch (Exception ex)
            {
                // you'll want to handle this by routing to a log file.
                throw ex;
            }
        }

        public List<ITitledEntity> GetRootBlueprintCategories()
        {
            try
            {
                List<ITitledEntity> rootBlueprintCategoryList;
                projectFile.GetRootBlueprintCategories(out rootBlueprintCategoryList);
                return rootBlueprintCategoryList;
            }
            catch (Exception ex)
            {
                // you'll want to handle this by routing to a log file.
                throw ex;
            }
        }

        public List<ICategorizedItem> GetChildCategorizedItemsByCategory(string categoryId)
        {
            return projectFile.GetChildCategorizedItemsByCategory(categoryId);
        }

        public List<IItemCategory> GetChildCategories(string parentCategoryId)
        {
            return projectFile.GetChildCategories(parentCategoryId);
        }

        public void AddBlueprintCategory(IItemCategory blueprintCategory, string blueprintCategoryId)
        {
            projectFile.AddBlueprintCategory(blueprintCategory, blueprintCategoryId);
        }

        public void MoveCategory(string displacedId, string newParentId)
        {
            projectFile.MoveCategory(displacedId, newParentId);
        }

        public void RemoveItemOrCategory(string id)
        {
            projectFile.RemoveItemOrCategory(id);
        }

        public void RenameBlueprintOrCategoryItem(string itemId, string newTitle)
        {
            projectFile.RenameBlueprintOrCategoryItem(itemId, newTitle);
        }

        //public void RemoveBlueprint(BlueprintHeader blueprintHeader)
        //{
        //    //projectFile.RemoveBlueprintOrCategoryItem(blueprintHeader.Id);
        //    //blueprintFile.DeleteBlueprintFile(blueprintHeader);
        //}

        public void AddCategorizedItem(ICategorizedItem categorizedItem, string categoryId)
        {
            projectFile.AddCategorizedItem(categorizedItem, categoryId);
        }
    }
}
