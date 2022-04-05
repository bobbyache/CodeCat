using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Domain.TopicSections.SearchableSnippet;
using CygSoft.CodeCat.Domain.TopicSections.Tasks;
using CygSoft.CodeCat.TaskListing.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Management.Exporters
{
    internal class TaskListExporter : IExporter
    {
        private readonly TopicDocument topicDocument;
        private readonly MarkDown markDown;

        public TaskListExporter(TopicDocument topicDocument, MarkDown markDown)
        {
            this.topicDocument = topicDocument;
            this.markDown = markDown;
        }

        public string Generate()
        {
            StringBuilder builder = new StringBuilder();

            var sections = topicDocument.TopicSections.OfType<TaskListTopicSection>();


            if (sections.Count() > 0)
                builder.AppendLine(markDown.Header1("Tasks"));

            builder.Append(GenerateWebReferences(sections));

            return builder.ToString();
        }

        private string GenerateWebReferences(IEnumerable<TaskListTopicSection> sections)
        {
            var builder = new StringBuilder();

            foreach (var section in sections)
            {
                section.Open();
                builder.AppendLine(markDown.Header2(section.Title));

                var tasks = section.Tasks
                    .GroupBy(wr => wr.Priority.ToString())
                    .Select(wr => wr).OrderBy(g => g.Key);

                foreach (var priority in tasks)
                {
                    builder.AppendLine(markDown.Header3(priority.Key));
                    builder.AppendLine("");

                    foreach (var task in priority)
                    {
                        builder.AppendLine(markDown.Task(task.Title, task.Completed));
                    }
                }
            }

            return builder.ToString();
        }

        private string MapPriority(TaskPriority priority)
        {
            return priority.ToString();
        }
    }
}
