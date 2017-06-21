using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.DocumentManager.TopicSections;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.TopicSections.SearchableSnippet
{
    public interface ISearchableSnippetTopicSection : ICodeTopicSection
    {
        ISearchableSnippetKeywordIndexItem[] SnippetList();
        ISearchableSnippetKeywordIndexItem NewSnippet(string title);
        void AddSnippet(ISearchableSnippetKeywordIndexItem snippet);
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

        private List<ISearchableSnippetKeywordIndexItem> items = new List<ISearchableSnippetKeywordIndexItem>()
        {
            new SearchableSnippetKeywordIndexItem() { Title = "Text 1", Text = "Text 1", Syntax = "TEXT" },
            new SearchableSnippetKeywordIndexItem() { Title = "Text 2", Text = "Text 2", Syntax = "TEXT" },
            new SearchableSnippetKeywordIndexItem() { Title = "Text 3", Text = "Text 3", Syntax = "HTML" },
            new SearchableSnippetKeywordIndexItem() { Title = "Text 4", Text = "Text 4", Syntax = "TEXT" }
        };

        public ISearchableSnippetKeywordIndexItem[] SnippetList()
        {
            return items.ToArray();
        }

        public void AddSnippet(ISearchableSnippetKeywordIndexItem snippet)
        {
            items.Add(snippet);
        }

        public void DeleteSnippets(IEnumerable<ISearchableSnippetKeywordIndexItem> snippets)
        {
            foreach (ISearchableSnippetKeywordIndexItem snippet in snippets)
                DeleteSnippet(snippet.Id);
        }

        public void DeleteSnippet(string id)
        {
            var foundItem = items.Where(i => i.Id == id).SingleOrDefault();

            if (foundItem != null)
                items.Remove(foundItem);
        }

        public ISearchableSnippetKeywordIndexItem NewSnippet(string title)
        {
            return new SearchableSnippetKeywordIndexItem() { Title = title, Syntax = this.Syntax };
        }
    }
}
