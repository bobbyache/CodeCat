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
    public class SearchableSnippetTopicSection : TopicSection, ISearchableSnippetTopicSection
    {
        IKeywordSearchIndex searchIndex = null;
        private SearchableSnippetKeywordSearchIndexRepository repository = new SearchableSnippetKeywordSearchIndexRepository("Snippet_Repository");

        public SearchableSnippetTopicSection(string folder, string title, string extension)
            : base(new DocumentPathGenerator(folder, "searchsnippets"), title, null)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.SearchableSnippet);
        }

        public SearchableSnippetTopicSection(string folder, string id, string title, string extension, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, "searchsnippets", id), title, null, ordinal)
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
    }
}
