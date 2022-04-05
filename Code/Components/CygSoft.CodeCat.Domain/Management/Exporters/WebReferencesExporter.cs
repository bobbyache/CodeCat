using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Domain.TopicSections.WebReference;

namespace CygSoft.CodeCat.Domain.Management.Exporters
{
    internal class WebReferencesExporter : IExporter
    {
        private readonly TopicDocument topicDocument;
        private readonly MarkDown markDown;

        public WebReferencesExporter(TopicDocument topicDocument, MarkDown markDown)
        {
            this.topicDocument = topicDocument;
            this.markDown = markDown;
        }

        public string Generate()
        {
            StringBuilder builder = new StringBuilder();

            var webReferences = topicDocument.TopicSections.OfType<WebReferencesTopicSection>();

            if (webReferences.Count() > 0)
                builder.AppendLine(markDown.Header1("Web References"));

            builder.Append(GenerateWebReferences(webReferences));

            return builder.ToString();
        }

        private string GenerateWebReferences(IEnumerable<WebReferencesTopicSection> webReferences)
        {
            var categorizedWebReferences = webReferences.SelectMany(wr => wr.WebReferences).OfType<IWebReference>()
                .GroupBy(wr => wr.Category)
                .Select(wr => wr).OrderBy(g => g.Key);

            StringBuilder builder = new StringBuilder();

            foreach (var category in categorizedWebReferences)
            {
                builder.AppendLine(markDown.Header3(category.Key));
                builder.AppendLine("");

                foreach (var webReference in category)
                {
                    builder.AppendLine(markDown.WebReference(webReference.Title, webReference.Url, webReference.Description));
                }
            }

            return builder.ToString();
        }
    }
}
