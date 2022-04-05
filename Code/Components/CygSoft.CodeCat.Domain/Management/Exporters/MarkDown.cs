using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Management.Exporters
{
    public class MarkDown
    {
        public string Header1(string text) => $"# {text}";
        public string Header2(string text) => $"## {text}";
        public string Header3(string text) => $"### {text}";
        public string Header4(string text) => $"#### {text}";
        public string WebReference(string title, string url, string description = null)
        {
            StringBuilder builder = new StringBuilder();

            if (url.StartsWith("http://www.plantuml.com/plantuml/png"))
            {
                builder.AppendLine($"![{title}]({url})");

                if (!string.IsNullOrWhiteSpace(description))
                    builder.AppendLine($"- {description}");

                return builder.ToString();
            }
            else
            {
                builder.AppendLine($"[{title}]({url})");

                if (!string.IsNullOrWhiteSpace(description))
                    builder.AppendLine($"- {description}");

                return builder.ToString();
            }
        }

        public string Footer(string title, string filePath, string syntax, string commaDelimitedKeywords)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(Header1("Source Info"));
            builder.AppendLine(BulletPoint(title));
            builder.AppendLine(BulletPoint(filePath));
            builder.AppendLine(BulletPoint(commaDelimitedKeywords));

            return builder.ToString();
        }

        public string BulletPoint(string text)
        {
            return $"- {text}";
        }

        public string Task(string title, bool completed)
        {
            if (completed)
                return $"- [X] {title}";
            else
                return $"- [ ] {title}";
        }

        public string TableOfContents()
        {
            return "* Outline\n{:toc}\n";
        }

        public string FileAttachment(string title, string path, string description)
        {
            string desc = string.IsNullOrWhiteSpace(description) ? "\n" + description : string.Empty;
            return $" - {title} - {path}\n{desc}";
        }
        
        public string TitledCodeBox(string title, string code, string language) => $"**{title}**\n```{language}\n{code}\n```";
    }
}
