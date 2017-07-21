using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Code.Base;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.TaskListing.Infrastructure;
using CygSoft.CodeCat.TaskListing;
using CygSoft.CodeCat.Syntax.Infrastructure;
using CygSoft.CodeCat.Syntax;
using CygSoft.CodeCat.Category;
using CygSoft.CodeCat.Category.Infrastructure;

namespace CygSoft.CodeCat.Domain
{
    public class AppFacade
    {
        private SyntaxRepository syntaxRepository;
        private CodeLibrary codeLibrary;
        private QikTemplateLibrary qikLibrary;
        private TopicLibrary topicLibrary;

        private Project project = new Project();
        private TaskList taskList;
        private CategoryHierarchy categoryHierarchy = new CategoryHierarchy();

        public AppFacade(string syntaxFilePath)
        {
            this.syntaxRepository = new SyntaxRepository(syntaxFilePath);
            this.codeLibrary = new CodeLibrary();
            this.qikLibrary = new QikTemplateLibrary();
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
            get { return this.codeLibrary.Loaded && this.qikLibrary.Loaded && this.topicLibrary.Loaded; }
        }

        public int GetIndexCount()
        {
            return this.codeLibrary.IndexCount + this.qikLibrary.IndexCount + this.topicLibrary.IndexCount;
        }

        public void OpenContextFolder()
        {
            // rather make open project folder.
            this.codeLibrary.OpenProjectFolder();
        }

        public void Open(string filePath, Version currentVersion)
        {
            project.Open(filePath, currentVersion);
            this.taskList = new TaskList(project.TaskFilePath);
            this.categoryHierarchy.LoadProject(project.CategoryFilePath);

            this.codeLibrary.Open(Path.GetDirectoryName(filePath), currentVersion);
            this.qikLibrary.Open(Path.GetDirectoryName(filePath), currentVersion);
            this.topicLibrary.Open(Path.GetDirectoryName(filePath), currentVersion);
        }

        public void Create(string filePath, Version currentVersion)
        {
            project.Create(filePath, currentVersion);
            this.taskList = new TaskList(project.TaskFilePath);
            this.categoryHierarchy.CreateProject(project.CategoryFilePath);
            this.codeLibrary.Create(Path.GetDirectoryName(filePath), currentVersion);
            this.qikLibrary.Create(Path.GetDirectoryName(filePath), currentVersion);
            this.topicLibrary.Create(Path.GetDirectoryName(filePath), currentVersion);
        }

        public IKeywordIndexItem[] GetLastOpenedIds()
        {
            List<IKeywordIndexItem> lastOpenedItems = new List<IKeywordIndexItem>();
            lastOpenedItems.AddRange(this.codeLibrary.GetLastOpenedIds());
            lastOpenedItems.AddRange(this.qikLibrary.GetLastOpenedIds());
            lastOpenedItems.AddRange(this.topicLibrary.GetLastOpenedIds());
            return lastOpenedItems.ToArray();
        }

        public void SetLastOpenedIds(IKeywordIndexItem[] keywordIndexItems)
        {
            // filter by type rather than just send in a bunch of ids.
            this.codeLibrary.SetLastOpenedIds(keywordIndexItems.OfType<CodeKeywordIndexItem>().ToArray());
            this.qikLibrary.SetLastOpenedIds(keywordIndexItems.OfType<QikTemplateKeywordIndexItem>().ToArray());
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

            keywordIndexItems.AddRange(this.codeLibrary.FindIndeces(commaDelimitedKeywords));
            keywordIndexItems.AddRange(this.qikLibrary.FindIndeces(commaDelimitedKeywords));
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
            CodeKeywordIndexItem[] codeIndeces = indeces.OfType<CodeKeywordIndexItem>().ToArray();
            QikTemplateKeywordIndexItem[] qikIndeces = indeces.OfType<QikTemplateKeywordIndexItem>().ToArray();
            TopicKeywordIndexItem[] codeGroupIndeces = indeces.OfType<TopicKeywordIndexItem>().ToArray();

            this.codeLibrary.AddKeywords(codeIndeces, delimitedKeywordList);
            this.qikLibrary.AddKeywords(qikIndeces, delimitedKeywordList);
            this.topicLibrary.AddKeywords(codeGroupIndeces, delimitedKeywordList);
        }

        public bool RemoveKeywords(IKeywordIndexItem[] indeces, string[] keywords, out IKeywordIndexItem[] invalidIndeces)
        {
            CodeKeywordIndexItem[] codeIndeces = indeces.OfType<CodeKeywordIndexItem>().ToArray();
            QikTemplateKeywordIndexItem[] qikIndeces = indeces.OfType<QikTemplateKeywordIndexItem>().ToArray();
            TopicKeywordIndexItem[] codeGroupIndeces = indeces.OfType<TopicKeywordIndexItem>().ToArray();

            IKeywordIndexItem[] invalidCodeIndeces;
            IKeywordIndexItem[] invalidQikIndeces;
            IKeywordIndexItem[] invalidCodeGroupIndeces;

            bool canRemoveCodeKeywords = codeLibrary.CanRemoveKeywords(codeIndeces, keywords, out invalidCodeIndeces);
            bool canRemoveQikKeywords = this.qikLibrary.CanRemoveKeywords(qikIndeces, keywords, out invalidQikIndeces);
            bool canRemoveCodeGroupKeywords = this.topicLibrary.CanRemoveKeywords(codeGroupIndeces, keywords, out invalidCodeGroupIndeces);

            List<IKeywordIndexItem> allInvalidIndeces = new List<IKeywordIndexItem>();
            allInvalidIndeces.AddRange(invalidCodeIndeces);
            allInvalidIndeces.AddRange(invalidQikIndeces);
            allInvalidIndeces.AddRange(invalidCodeGroupIndeces);

            invalidIndeces = allInvalidIndeces.ToArray();

            if (canRemoveCodeKeywords && canRemoveQikKeywords && canRemoveCodeGroupKeywords)
            {
                this.codeLibrary.RemoveKeywords(codeIndeces, keywords);
                this.qikLibrary.RemoveKeywords(qikIndeces, keywords);
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

        public CodeFile OpenCodeFileTarget(IKeywordIndexItem keywordIndexItem)
        {
            return this.codeLibrary.OpenTarget(keywordIndexItem) as CodeFile;
        }

        public CodeFile CreateCodeSnippet(string syntax)
        {
            CodeFile codeFile = this.codeLibrary.CreateTarget(new CodeKeywordIndexItem("New Snippet", syntax,
                string.Empty)) as CodeFile;
            return codeFile;
        }

        public IQikTemplateDocumentSet CreateQikDocumentGroup(string syntax)
        {
            IQikTemplateDocumentSet qikFile = this.qikLibrary.CreateTarget(new QikTemplateKeywordIndexItem("New Qik Template",
                syntax, string.Empty)) as QikTemplateDocumentSet;
            return qikFile;
        }

        public IQikTemplateDocumentSet OpenQikDocumentGroup(IKeywordIndexItem keywordIndexItem)
        {
            return this.qikLibrary.OpenTarget(keywordIndexItem) as IQikTemplateDocumentSet;
        }

        public ITopicDocument CreateTopicDocument(string syntax)
        {
            ITopicDocument topicDocument = this.topicLibrary.CreateTarget(new TopicKeywordIndexItem("New Group Snippet",
                syntax, string.Empty)) as ITopicDocument;
            return topicDocument;
        }

        public ITopicDocument OpenTopicDocument(IKeywordIndexItem keywordIndexItem)
        {
            return this.topicLibrary.OpenTarget(keywordIndexItem) as ITopicDocument;
        }

        public ITask CreateTask()
        {
            return TaskList.CreateTask();
        }

        public void AddTask(ITask task)
        {
            taskList.AddTask(task);
        }

        public void DeleteTasks(ITask[] tasks)
        {
            taskList.DeleteTasks(tasks);
        }

        public void LoadTasks()
        {
            taskList.Load();
        }

        public ITask[] CurrrentTasks
        {
            get { return taskList.Tasks; }
        }

        public string[] TaskPriorities
        {
            get { return TaskList.Categories; }
        }

        public TaskPriority TaskPriorityFromText(string text)
        {
            return TaskList.PriorityFromText(text);
        }

        public void SaveTasks()
        {
            taskList.Save();
        }


        public string CurrentTaskInformation()
        {
            return $"{taskList.NoOfCompletedTasks}/{taskList.NoOfTasks} ({Math.Round(taskList.PercentageOfTasksCompleted, 1)}%) Tasks Completed";
        }

        public int PercentageOfTasksCompleted()
        {
            return (int)Math.Round(taskList.PercentageOfTasksCompleted);
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
                string[] itemIds = categoryHierarchy.GetChildCategorizedItemsByCategory(categoryId).Select(r => r.ItemId).ToArray();

                List<IKeywordIndexItem> indexItems = new List<IKeywordIndexItem>();
                indexItems.AddRange(codeLibrary.FindByIds(itemIds));
                indexItems.AddRange(qikLibrary.FindByIds(itemIds));
                indexItems.AddRange(topicLibrary.FindByIds(itemIds));

                children.AddRange(indexItems);
            }
            return children;
        }

        public void MoveCategory(string id, string newParentId)
        {
            categoryHierarchy.MoveCategory(id, newParentId);
        }

        public void AddCategoryItem(IKeywordIndexItem indexItem, string categoryId)
        {
            CategorizedItem categorizedItem = new CategorizedItem(indexItem);
            categoryHierarchy.AddCategorizedItem(categorizedItem, categoryId);
        }
    }
}
