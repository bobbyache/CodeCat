using CygSoft.CodeCat.Category.Infrastructure;
using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<ITitledEntity> GetBlueprintCategoryChildren(string parentCategoryId)
        {
            try
            {
                List<ITitledEntity> childList;

                // this method retrieves categories and queries at a certain level, probably has to be renamed
                // and possibly moved to another level of abstraction, perhaps.
                projectFile.GetChildItems(parentCategoryId, out childList);

                return childList;
            }
            catch (Exception ex)
            {
                // you'll want to handle this by routing to a log file.
                throw ex;
            }
        }

        public void AddBlueprintCategory(IBlueprintCategory blueprintCategory, string blueprintCategoryId)
        {
            projectFile.AddBlueprintCategory(blueprintCategory, blueprintCategoryId);
        }

        public void MoveBlueprintOrCategory(string displacedId, string newParentId)
        {
            projectFile.MoveBlueprintOrCategory(displacedId, newParentId);
        }

        public void RemoveBlueprintCategory(BlueprintCategory blueprintCategory)
        {
            projectFile.RemoveBlueprintOrCategoryItem(blueprintCategory.Id);
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

        public void AddBlueprint(ITitledEntity blueprintHeader, string blueprintCategoryId)
        {
            projectFile.AddBlueprintHeader(blueprintHeader, blueprintCategoryId);
        }
    }
}
