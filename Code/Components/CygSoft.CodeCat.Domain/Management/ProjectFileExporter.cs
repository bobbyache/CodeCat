using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Code.Base;
using CygSoft.CodeCat.Domain.Management.Exporters;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Domain.TopicSections.CodeTemplate;
using CygSoft.CodeCat.Domain.TopicSections.WebReference;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CygSoft.CodeCat.Domain.Management
{
    public class ProjectFileExporter
    {
        private CodeLibrary codeLibrary;
        private QikTemplateLibrary qikLibrary;
        private TopicLibrary codeGroupLibrary;
        private Project project = new Project();

        public ProjectFileExporter()
        {
            this.codeLibrary = new CodeLibrary();
            this.qikLibrary = new QikTemplateLibrary();
            this.codeGroupLibrary = new TopicLibrary();
        }

        public bool Loaded
        {
            get { return this.codeLibrary.Loaded && this.qikLibrary.Loaded && this.codeGroupLibrary.Loaded; }
        }

        public string RootFilePath
        {
            get { return this.project.FilePath; }
        }

        public void Export(string projectPath, string folderPath, string commaDelimitedKeywords)
        {
            var currentVersion = new Version("1.1.0");
            project.Open(projectPath, currentVersion);

            ProcessTopicFiles(projectPath, folderPath, currentVersion, commaDelimitedKeywords);

            //// this.codeLibrary.Open(Path.GetDirectoryName(projectPath), currentVersion);
            //// this.qikLibrary.Open(Path.GetDirectoryName(projectPath), currentVersion);
        }

        private void ProcessTopicFiles(string projectPath, string exportFolderPath, Version currentVersion, string commaDelimitedKeywords)
        {
            this.codeGroupLibrary.Open(Path.GetDirectoryName(projectPath), currentVersion);

            List<IKeywordIndexItem> keywordIndexItems = new List<IKeywordIndexItem>();
            keywordIndexItems.AddRange(this.codeGroupLibrary.FindIndeces(commaDelimitedKeywords));

            var indexItemArray = keywordIndexItems.ToArray();

            List<IndexExportImportData> indexExportData = new List<IndexExportImportData>();
            indexExportData.AddRange(this.codeGroupLibrary.GetExportData(indexItemArray));

            foreach (var export in indexExportData.ToArray())
            {
                var workItem = codeGroupLibrary.GetWorkItem(export.KeywordIndexItem);
                ProcessTopicDocument(workItem as TopicDocument, export, exportFolderPath);
            }
        }

        private void ProcessTopicDocument(TopicDocument topicDocument, IndexExportImportData exportImportData, string exportFolderPath)
        {
            MarkDown markdown = new MarkDown();

            if (topicDocument.Exists)
            {
                StringBuilder builder = new StringBuilder();

                topicDocument.Open();

                builder.AppendLine(markdown.TableOfContents());

                builder.AppendLine(markdown.Footer(topicDocument.Title, topicDocument.FilePath, topicDocument.Syntax,
                    exportImportData.KeywordIndexItem.CommaDelimitedKeywords));

                builder.Append(new FileGroupExporter(topicDocument, markdown).Generate());
                builder.Append(new TaskListExporter(topicDocument, markdown).Generate());
                builder.Append(new SearchableSnippetExporter(topicDocument, markdown).Generate());
                builder.Append(new WebReferencesExporter(topicDocument, markdown).Generate());



                WriteToDisk(CreateFilePath(topicDocument, exportFolderPath), builder.ToString());

                topicDocument.Close();
            }
        }

        private string CreateFilePath(TopicDocument topicDocument, string exportFolderPath)
            => Path.Combine(exportFolderPath, ReplaceInvalidChars(topicDocument.Title) + ".md");

        private string ReplaceInvalidChars(string title) => string.Join("_", title.Split(Path.GetInvalidFileNameChars()));

        private void WriteToDisk(string filePath, string content)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.Write(content);
                streamWriter.Flush();
            }
        }
    }
}
