using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.TopicSections.SearchableSnippet
{
    public class SearchableSnippetTopicSection : TopicSection, ISearchableSnippetTopicSection
    {
        public SearchableSnippetTopicSection(string folder, string title, string extension) 
            : base(new DocumentPathGenerator(folder, "xml"), title, null)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.SearchableSnippet);
        }

        public SearchableSnippetTopicSection(string folder, string id, string title, string extension, int ordinal, string description)
            : base(new DocumentIndexPathGenerator(folder, "xml", id), title, null, ordinal)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.SearchableSnippet);
        }
    }
}
