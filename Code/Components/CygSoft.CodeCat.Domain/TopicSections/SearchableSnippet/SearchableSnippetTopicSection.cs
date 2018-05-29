using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.TopicSections;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.CodeCat.Domain.TopicSections.SearchableSnippet
{
    public interface ISearchableSnippetTopicSection : ICodeTopicSection
    {
        ISearchableSnippetKeywordIndexItem[] Find(string commaDelimitedKeywordList);
        ISearchableSnippetKeywordIndexItem NewSnippet(string title);
        string[] Categories { get; }
        string[] Keywords { get; }
        void AddSnippet(ISearchableSnippetKeywordIndexItem snippet);
        void UpdateSnippet(ISearchableSnippetKeywordIndexItem snippet);
        void DeleteSnippet(string id);
        void DeleteSnippets(IEnumerable<ISearchableSnippetKeywordIndexItem> snippets);
    }

    public class SearchableSnippetTopicSection : CodeTopicSection, ISearchableSnippetTopicSection
    {
        IKeywordSearchIndex searchIndex = null;
        private SearchableSnippetKeywordSearchIndexRepository repository = new SearchableSnippetKeywordSearchIndexRepository("Snippet_Repository");

        public SearchableSnippetTopicSection(string folder, string title, string extension, string syntax)
            : base(folder, title, extension, syntax)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.SearchableSnippet);
        }

        public SearchableSnippetTopicSection(string folder, string id, string title, string extension, int ordinal, string description, string syntax)
            : base(folder, id, title, extension, ordinal, null, syntax)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.SearchableSnippet);
        }

        protected override void OnOpen()
        {
            if (!this.Exists)
                repository.CreateIndex(this.FilePath, GetVersion());
            searchIndex = repository.OpenIndex(this.FilePath, GetVersion());
        }

        protected override void OnSave()
        {
            if (this.searchIndex == null)
                searchIndex = repository.CreateIndex(this.FilePath, GetVersion());
            repository.SaveIndex(searchIndex);
        }

        private Version GetVersion()
        {
            return new Version(1, 0);
        }

        public string[] Categories
        {
            get { return Find(string.Empty).Select(r => r.Category).OrderBy(r => r).Distinct().ToArray(); }
        }

        public string[] Keywords
        {
            get
            {
                if (searchIndex != null)
                    return searchIndex.Keywords;
                else
                    return new string[0];
            }
        }

        public void UpdateSnippet(ISearchableSnippetKeywordIndexItem snippet)
        {
            searchIndex.Update(snippet);
        }

        public void AddSnippet(ISearchableSnippetKeywordIndexItem snippet)
        {
            searchIndex.Update(snippet);
        }

        public void DeleteSnippets(IEnumerable<ISearchableSnippetKeywordIndexItem> snippets)
        {
            foreach (ISearchableSnippetKeywordIndexItem snippet in snippets)
                DeleteSnippet(snippet.Id);
        }

        public void DeleteSnippet(string id)
        {
            searchIndex.Remove(id);
        }

        public ISearchableSnippetKeywordIndexItem NewSnippet(string title)
        {
            return new SearchableSnippetKeywordIndexItem() { Title = title, Syntax = this.Syntax };
        }

        public ISearchableSnippetKeywordIndexItem[] Find(string commaDelimitedKeywordList)
        {
            if (searchIndex == null)
                return new SearchableSnippetKeywordIndexItem[0];

            if (commaDelimitedKeywordList.Trim() == string.Empty)
                return searchIndex.All().OfType<ISearchableSnippetKeywordIndexItem>().ToArray();
            else
                return searchIndex.Find(commaDelimitedKeywordList).OfType<ISearchableSnippetKeywordIndexItem>().ToArray();
        }
    }
}
