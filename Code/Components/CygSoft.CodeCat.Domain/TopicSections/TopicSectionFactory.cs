﻿using CygSoft.CodeCat.DocumentManager.TopicSections;
using CygSoft.CodeCat.Domain.TopicSections.WebReference;
using CygSoft.CodeCat.Infrastructure.TopicSections;

namespace CygSoft.CodeCat.Domain.TopicSections
{
    internal class TopicSectionFactory
    {
        private class TopicSectionArgs
        {
            public string DocumentType { get; set; }
            public string Folder { get; set; }
            public string Title { get; set; }
            public string Id { get; set; }
            public int Ordinal { get; set; }
            public string Description { get; set; }
            public string Extension { get; set; }
            public string Syntax { get; set; }
            public string FileName { get; set; }
        }

        public static ITopicSection Create(TopicSectionType documentType, string folder, string title, string id = null, int ordinal = 0, string description = null, string extension = null, string syntax = null)
        {
            TopicSectionArgs topicSectionArgs = new TopicSectionArgs { DocumentType = SectionTypes.GetDocumentType(documentType), Folder = folder, Title = title, Id = id, Ordinal = ordinal, Description = description, Extension = extension, Syntax = syntax };

            if (topicSectionArgs.DocumentType == SectionTypes.CODE_TOPIC_SECTION)
                return CreateCodeTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == SectionTypes.WEB_REFERENCES_TOPIC_SECTION)
                return CreateWebReferencesTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == SectionTypes.QIK_SCRIPT_TOPIC_SECTION)
                return CreateQikScriptTopicSection(topicSectionArgs);

            return null;
        }

        private static ITopicSection CreateWebReferencesTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new WebReferencesTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title);
            else
                return new WebReferencesTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Ordinal, topicSectionArgs.Description);
        }

        private static ITopicSection CreateCodeTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new CodeTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title, topicSectionArgs.Extension, topicSectionArgs.Syntax);
            else
                return new CodeTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Extension, topicSectionArgs.Ordinal, topicSectionArgs.Description, topicSectionArgs.Syntax);
        }

        private static ITopicSection CreateQikScriptTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new QikScriptTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title, topicSectionArgs.Extension, topicSectionArgs.Syntax);
            else
                return new QikScriptTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Extension, topicSectionArgs.Ordinal, topicSectionArgs.Description, topicSectionArgs.Syntax);
        }
    }
}
