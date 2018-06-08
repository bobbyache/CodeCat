using CygSoft.CodeCat.Plugin.Infrastructure;
using System;
using System.Collections.Generic;

namespace CygSoft.CodeCat.Infrastructure
{
    public interface IAppFacade
    {
        string CodeSyntaxFolderPath { get; }
        bool Loaded { get; }
        string ProjectCategoryFilePath { get; }
        string ProjectFileExtension { get; }
        string ProjectFileTitle { get; }

        void AddCategory(IItemCategory category, string relativeToCategoryId);
        ICategorizedKeywordIndexItem AddCategoryItem(IKeywordIndexItem indexItem, string categoryId);
        void AddKeywords(IKeywordIndexItem[] indeces, string delimitedKeywordList);
        string AddKeywordsToDelimitedText(string keywords, string addKeywords);
         string[] AllKeywords(IKeywordIndexItem[] indeces);
        string CopyAllKeywords(IKeywordIndexItem[] indeces);
        void Create(string filePath, Version currentVersion);
        IItemCategory CreateCategory();
        ITitledEntity CreateCategoryItem(string id, string title);
        IFile CreateWorkItem(string syntax);
        void DeleteCategoryOrItem(ITitledEntity item);
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
        void MoveCategory(string id, string newParentId);
        void Open(string filePath, Version currentVersion);
        void OpenContextFolder();
        IFile OpenWorkItem(IKeywordIndexItem keywordIndexItem);
        bool RemoveKeywords(IKeywordIndexItem[] indeces, string[] keywords, out IKeywordIndexItem[] invalidIndeces);
        string RemoveKeywordsFromDelimitedText(string keywords, string removeKeywords);
        void RenameCategory(string categoryId, string newTitle);
        void SetLastOpenedIds(IKeywordIndexItem[] keywordIndexItems);
    }
}