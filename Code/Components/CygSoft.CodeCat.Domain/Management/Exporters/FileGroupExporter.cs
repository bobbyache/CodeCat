using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Domain.TopicSections.FileAttachment;
using CygSoft.CodeCat.Domain.TopicSections.SearchableSnippet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CygSoft.CodeCat.Domain.Management.Exporters
{
    internal class FileGroupExporter : IExporter
    {
        private readonly TopicDocument topicDocument;
        private readonly MarkDown markDown;

        public FileGroupExporter(TopicDocument topicDocument, MarkDown markDown)
        {
            this.topicDocument = topicDocument;
            this.markDown = markDown;
        }

        public string Generate()
        {
            StringBuilder builder = new StringBuilder();

            var sections = topicDocument.TopicSections.OfType<FileAttachmentsTopicSection>();


            if (sections.Count() > 0)
                builder.AppendLine(markDown.Header1("File Attachments"));

            builder.Append(GenerateWebReferences(sections));

            return builder.ToString();
        }

        private string GenerateWebReferences(IEnumerable<FileAttachmentsTopicSection> sections)
        {
            var builder = new StringBuilder();

            foreach (var section in sections)
            {
                section.Open();

                var categorizedItems = section.Items.OfType<IFileAttachment>()
                    .GroupBy(wr => wr.Category)
                    .Select(wr => wr).OrderBy(g => g.Key);

                foreach (var category in categorizedItems)
                {
                    builder.AppendLine(markDown.Header3(category.Key));
                    builder.AppendLine("");

                    foreach (var codeSnippet in category)
                    {
                        builder.AppendLine(markDown.TitledCodeBox(codeSnippet.Title, codeSnippet.FilePath, codeSnippet.Description));
                    }
                }
            }

            return builder.ToString();
        }
    }
}
