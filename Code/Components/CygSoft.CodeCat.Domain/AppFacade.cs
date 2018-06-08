using CygSoft.CodeCat.Category;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex;
using CygSoft.CodeCat.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CygSoft.CodeCat.Domain
{
    public class AppFacade : IAppFacade
    {
        private SyntaxRepository syntaxRepository;
        private TopicLibrary topicLibrary;

        private Project project = new Project();
        private CategoryHierarchy categoryHierarchy = new CategoryHierarchy();

        public AppFacade(string syntaxFilePath)
        {
            this.syntaxRepository = new SyntaxRepository(syntaxFilePath);
            this.topicLibrary = new TopicLibrary();
        }

        public string CodeSyntaxFolderPath
        {
            get { return this.syntaxRepository.FilePath; }
        }

        public string ProjectFileExtension
        {
            get { return this.project.ProjectFileExtension; }
        }

        public string ProjectFileTitle
        {
            get { return this.project.FileTitle; }
        }

        public string ProjectTaskFilePath
        {
            get
            {
                if (project != null)
                    return project.TaskFilePath;
                return null;
            }
        }

        public string ProjectCategoryFilePath
        {
            get
            {
                if (project != null)
                    return project.CategoryFilePath;
                return null;
            }
        }

        public bool Loaded
        {
            get { return this.topicLibrary.Loaded; }
        }

        public int GetIndexCount()
        {
            return this.topicLibrary.IndexCount;
        }

        public void OpenContextFolder()
        {
            FileSys.OpenFolder(this.project.FolderPath);
        }

        public void Open(string filePath, Version currentVersion)
        {
            project.Open(filePath, currentVersion);
            this.categoryHierarchy.LoadProject(project.CategoryFilePath);

            this.topicLibrary.Open(Path.GetDirectoryName(filePath), currentVersion);
        }

        public void Create(string filePath, Version currentVersion)
        {
            project.Create(filePath, currentVersion);
            this.categoryHierarchy.CreateProject(project.CategoryFilePath);
            this.topicLibrary.Create(Path.GetDirectoryName(filePath), currentVersion);
        }

        public IKeywordIndexItem[] GetLastOpenedIds()
        {
            List<IKeywordIndexItem> lastOpenedItems = new List<IKeywordIndexItem>();
            lastOpenedItems.AddRange(this.topicLibrary.GetLastOpenedIds());
            return lastOpenedItems.ToArray();
        }

        public void SetLastOpenedIds(IKeywordIndexItem[] keywordIndexItems)
        {
            // filter by type rather than just send in a bunch of ids.
            this.topicLibrary.SetLastOpenedIds(keywordIndexItems.OfType<TopicKeywordIndexItem>().ToArray());
        }

        public string[] GetSyntaxes()
        {
            return this.syntaxRepository.Languages;
        }

        public ISyntaxFile[] GetSyntaxFileInfo()
        {
            return this.syntaxRepository.SyntaxFiles;
        }

        public string GetSyntaxFile(string syntax)
        {
            return this.syntaxRepository[syntax].FilePath;
        }

        public IKeywordIndexItem[] FindIndeces(string commaDelimitedKeywords)
        {
            List<IKeywordIndexItem> keywordIndexItems = new List<IKeywordIndexItem>();

            keywordIndexItems.AddRange(this.topicLibrary.FindIndeces(commaDelimitedKeywords));

            return keywordIndexItems.ToArray();
        }

        public string[] AllKeywords(IKeywordIndexItem[] indeces)
        {
            KeyPhrases keyPhrases = new KeyPhrases();
            foreach (IKeywordIndexItem index in indeces)
            {
                keyPhrases.AddKeyPhrases(index.CommaDelimitedKeywords);
            }
            return keyPhrases.Phrases;
        }

        public string CopyAllKeywords(IKeywordIndexItem[] indeces)
        {
            KeyPhrases keyPhrases = new KeyPhrases();
            foreach (IKeywordIndexItem index in indeces)
            {
                keyPhrases.AddKeyPhrases(index.CommaDelimitedKeywords);
            }
            return keyPhrases.DelimitKeyPhraseList();
        }

        public void AddKeywords(IKeywordIndexItem[] indeces, string delimitedKeywordList)
        {
            TopicKeywordIndexItem[] codeGroupIndeces = indeces.OfType<TopicKeywordIndexItem>().ToArray();
            this.topicLibrary.AddKeywords(codeGroupIndeces, delimitedKeywordList);
        }

        public bool RemoveKeywords(IKeywordIndexItem[] indeces, string[] keywords, out IKeywordIndexItem[] invalidIndeces)
        {
            TopicKeywordIndexItem[] codeGroupIndeces = indeces.OfType<TopicKeywordIndexItem>().ToArray();

            IKeywordIndexItem[] invalidCodeGroupIndeces;

            bool canRemoveCodeGroupKeywords = this.topicLibrary.CanRemoveKeywords(codeGroupIndeces, keywords, out invalidCodeGroupIndeces);

            List<IKeywordIndexItem> allInvalidIndeces = new List<IKeywordIndexItem>();
            allInvalidIndeces.AddRange(invalidCodeGroupIndeces);

            invalidIndeces = allInvalidIndeces.ToArray();

            if (canRemoveCodeGroupKeywords)
            {
                this.topicLibrary.RemoveKeywords(codeGroupIndeces, keywords);
                return true;
            }
            else
            {
                return false;
            }
        }

        public string KeywordArrayToDelimitedText(string[] keywords)
        {
            KeyPhrases keyPhrases = new KeyPhrases();
            keyPhrases.AddKeyPhrases(keywords);

            return keyPhrases.DelimitKeyPhraseList();
        }

        public string RemoveKeywordsFromDelimitedText(string keywords, string removeKeywords)
        {
            KeyPhrases keyPhrases = new KeyPhrases();
            keyPhrases.AddKeyPhrases(keywords);
            keyPhrases.RemovePhrases(removeKeywords);

            return keyPhrases.DelimitKeyPhraseList();
        }

        public string AddKeywordsToDelimitedText(string keywords, string addKeywords)
        {
            KeyPhrases keyPhrases = new KeyPhrases();
            keyPhrases.AddKeyPhrases(keywords);
            keyPhrases.AddKeyPhrases(addKeywords);

            return keyPhrases.DelimitKeyPhraseList();
        }



        public IFile OpenWorkItem(IKeywordIndexItem keywordIndexItem)
        {
            return this.topicLibrary.GetWorkItem(keywordIndexItem);
        }

        public IFile CreateWorkItem(string syntax)
        {
            return this.topicLibrary.CreateWorkItem(new TopicKeywordIndexItem("New Topic",
                syntax, string.Empty));
        }

        public void DeleteWorkItem(IKeywordIndexItem keywordIndexItem)
        {
            IFile workItem = this.topicLibrary.GetWorkItem(keywordIndexItem);
            workItem.Delete();
        }

        public IItemCategory CreateCategory()
        {
            IItemCategory category = new ItemCategory();
            category.Title = "New Category";
            return category;
        }

        public ITitledEntity CreateCategoryItem(string id, string title)
        {
            ITitledEntity entity = new CategorizedItem(id);
            entity.Title = title;

            return entity;
        }

        public void AddCategory(IItemCategory category, string relativeToCategoryId)
        {
            categoryHierarchy.AddBlueprintCategory(category, relativeToCategoryId);
        }

        public List<ITitledEntity> GetRootCategories()
        {
            return categoryHierarchy.GetRootBlueprintCategories();
        }

        public void RenameCategory(string categoryId, string newTitle)
        {
            categoryHierarchy.RenameBlueprintOrCategoryItem(categoryId, newTitle);
        }

        public List<ITitledEntity> GetChildCategorizedItemsByCategory(string categoryId)
        {
            List<ITitledEntity> children = new List<ITitledEntity>();


            List<IItemCategory> categories = categoryHierarchy.GetChildCategories(categoryId);
            List<ICategorizedItem> categoryItems = categoryHierarchy.GetChildCategorizedItemsByCategory(categoryId);

            children.AddRange(categories);

            if (categoryItems != null && categoryItems.Count() > 0)
            {
                var catItems = categoryHierarchy.GetChildCategorizedItemsByCategory(categoryId);
                string[] itemIds = catItems.Select(r => r.ItemId).ToArray();

                List<IKeywordIndexItem> indexItems = new List<IKeywordIndexItem>();
                indexItems.AddRange(topicLibrary.FindByIds(itemIds));

                List<ICategorizedKeywordIndexItem> categoryIndexItems = new List<ICategorizedKeywordIndexItem>();


                categoryIndexItems = (from c in catItems
                                      join p in indexItems on c.ItemId equals p.Id
                                      select new CategorizedKeywordIndexItem(c.Id, p as IKeywordIndexItem))
                               .OfType<ICategorizedKeywordIndexItem>()
                               .ToList();

                children.AddRange(categoryIndexItems);
            }
            return children;
        }

        public void MoveCategory(string id, string newParentId)
        {
            categoryHierarchy.MoveCategory(id, newParentId);
        }

        public ICategorizedKeywordIndexItem AddCategoryItem(IKeywordIndexItem indexItem, string categoryId)
        {
            CategorizedItem categorizedItem = new CategorizedItem(indexItem);
            ICategorizedKeywordIndexItem categorizedIndexItem = new CategorizedKeywordIndexItem(categorizedItem.Id, indexItem);

            categoryHierarchy.AddCategorizedItem(categorizedIndexItem, categoryId);
            return categorizedIndexItem;
        }

        public void DeleteCategoryOrItem(ITitledEntity item)
        {
            if (item is IKeywordIndexItem)
                categoryHierarchy.RemoveItemOrCategory(new CategorizedItem(item).InstanceId);
            else
                categoryHierarchy.RemoveItemOrCategory(item.Id);
        }
    }
}
