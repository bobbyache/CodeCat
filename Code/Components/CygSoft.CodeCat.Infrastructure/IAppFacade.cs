using System;
using System.Collections.Generic;
using CygSoft.CodeCat.Category.Infrastructure;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Files.Infrastructure;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using CygSoft.CodeCat.Syntax.Infrastructure;
using CygSoft.CodeCat.TaskListing.Infrastructure;

namespace CygSoft.CodeCat.Infrastructure
{
    public interface IAppFacade
    {
        string CodeSyntaxFolderPath { get; }
        ITask[] CurrrentTasks { get; }
        bool Loaded { get; }
        string ProjectCategoryFilePath { get; }
        string ProjectFileExtension { get; }
        string ProjectFileTitle { get; }
        string ProjectTaskFilePath { get; }
        string[] TaskPriorities { get; }

        void AddCategory(IItemCategory category, string relativeToCategoryId);
        ICategorizedKeywordIndexItem AddCategoryItem(IKeywordIndexItem indexItem, string categoryId);
        void AddKeywords(IKeywordIndexItem[] indeces, string delimitedKeywordList);
        string AddKeywordsToDelimitedText(string keywords, string addKeywords);
        void AddTask(ITask task);
        string[] AllKeywords(IKeywordIndexItem[] indeces);
        string CopyAllKeywords(IKeywordIndexItem[] indeces);
        void Create(string filePath, Version currentVersion);
        IItemCategory CreateCategory();
        ITitledEntity CreateCategoryItem(string id, string title);
        ITask CreateTask();
        IFile CreateWorkItem(string syntax, WorkItemType itemType);
        string CurrentTaskInformation();
        void DeleteCategoryOrItem(ITitledEntity item);
        void DeleteTasks(ITask[] tasks);
        void DeleteWorkItem(IKeywordIndexItem keywordIndexItem);
        IKeywordIndexItem[] FindIndeces(string commaDelimitedKeywords);
        List<ITitledEntity> GetChildCategorizedItemsByCategory(string categoryId);
        int GetIndexCount();
        IKeywordIndexItem[] GetLastOpenedIds();
        List<ITitledEntity> GetRootCategories();
        string[] GetSyntaxes();
        string GetSyntaxFile(string syntax);
        ISyntaxFile[] GetSyntaxFileInfo();
        string KeywordArrayToDelimitedText(string[] keywords);
        void LoadTasks();
        void MoveCategory(string id, string newParentId);
        void Open(string filePath, Version currentVersion);
        void OpenContextFolder();
        IFile OpenWorkItem(IKeywordIndexItem keywordIndexItem);
        int PercentageOfTasksCompleted();
        bool RemoveKeywords(IKeywordIndexItem[] indeces, string[] keywords, out IKeywordIndexItem[] invalidIndeces);
        string RemoveKeywordsFromDelimitedText(string keywords, string removeKeywords);
        void RenameCategory(string categoryId, string newTitle);
        void SaveTasks();
        void SetLastOpenedIds(IKeywordIndexItem[] keywordIndexItems);
        TaskPriority TaskPriorityFromText(string text);
    }
}