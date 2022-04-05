using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Domain.TopicSections.SearchableSnippet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Management.Exporters
{
    internal class SearchableSnippetExporter : IExporter
    {
        private readonly TopicDocument topicDocument;
        private readonly MarkDown markDown;

        public SearchableSnippetExporter(TopicDocument topicDocument, MarkDown markDown)
        {
            this.topicDocument = topicDocument;
            this.markDown = markDown;
        }

        public string Generate()
        {
            StringBuilder builder = new StringBuilder();

            

            var sections = topicDocument.TopicSections.OfType<SearchableSnippetTopicSection>();


            if (sections.Count() > 0)
                builder.AppendLine(markDown.Header1("Code Snippets"));

            builder.Append(GenerateWebReferences(sections));

            return builder.ToString();
        }

        private string GenerateWebReferences(IEnumerable<SearchableSnippetTopicSection> sections)
        {
            var builder = new StringBuilder();
            var searchableSnippets = sections.OfType<SearchableSnippetTopicSection>();

            foreach (var searchableSnippetIndex in searchableSnippets)
            {
                searchableSnippetIndex.Open();

                var categorizedCodeSnippets = searchableSnippetIndex.Find("").OfType<ISearchableSnippetKeywordIndexItem>()
                    .GroupBy(wr => wr.Category)
                    .Select(wr => wr).OrderBy(g => g.Key);

                foreach (var category in categorizedCodeSnippets)
                {
                    builder.AppendLine(markDown.Header3(category.Key));
                    builder.AppendLine("");

                    foreach (var codeSnippet in category)
                    {
                        builder.AppendLine(markDown.TitledCodeBox(codeSnippet.Title, codeSnippet.Text, MapSyntax(codeSnippet.Syntax)));
                    }
                }
            }

            return builder.ToString();
        }

        private string MapSyntax(string sourceId)
        {
            switch (sourceId.ToUpper())
            {
                case "SQLSERVER2K_SQL": 
                    return "sql";
                case "SQLSERVER7_SQL":
                    return "sql";
                case "MySQL_SQL":
                    return "sql";
                case "XD_BASH":
                    return "bash";
                default:
                    return sourceId.ToLower();
            }
        }
    }
}
