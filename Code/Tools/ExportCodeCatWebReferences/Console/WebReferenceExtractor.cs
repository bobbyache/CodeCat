using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ExportCodeCatWebReferences
{
    public class WebReferenceExtractor
    {
        public void Extract(string filePath, string outputPath, bool addDescriptions)
        {
            var outputFile = outputPath ?? Path.Combine(Path.GetDirectoryName(filePath), $"{Path.GetFileNameWithoutExtension(filePath)}.md");

            var xDocument = XDocument.Load(filePath);
            var elements = xDocument.Element("UrlGroup").Elements("Urls").Elements();

            var webReferences = ExtractFromXml(elements);
            var categorizedWebReferences = webReferences.OfType<WebReference>().GroupBy(wr => wr.Category).Select(wr => wr);

            var fileLines = new List<string>();

            foreach (var category in categorizedWebReferences)
            {
                fileLines.Add($"## {category.Key}");
                fileLines.Add("");

                foreach (var webReference in category)
                {
                    fileLines.Add($"[{webReference.Title}]({webReference.Url})");

                    if (addDescriptions)
                    {
                        if (!string.IsNullOrWhiteSpace(webReference.Description))
                            fileLines.Add($"- {webReference.Description}");
                    }
                    fileLines.Add("");
                }
            }

            using (StreamWriter streamWriter = new StreamWriter(outputFile))
            {
                foreach (string fileLine in fileLines)
                {
                    streamWriter.WriteLine(fileLine);
                }
                streamWriter.Flush();
            }
        }

        private List<WebReference> ExtractFromXml(IEnumerable<XElement> elements)
        {
            var webReferences = new List<WebReference>();

            foreach (XElement element in elements)
            {
                var item = new WebReference(
                    (string)element.Attribute("Id"),
                    (string)element.Attribute("Title"),
                    element.Attribute("Category") != null ? (string)element.Attribute("Category") : "Unknown",
                    (string)element.Attribute("Description"),
                    (string)element.Attribute("Url"),
                    DateTime.Parse((string)element.Attribute("Created")),
                    DateTime.Parse((string)element.Attribute("Modified"))
                );

                webReferences.Add(item);
            }
            return webReferences.ToList();
        }
    }
}