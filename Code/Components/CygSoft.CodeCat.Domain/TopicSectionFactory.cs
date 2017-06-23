using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.TopicSections;
using CygSoft.CodeCat.DocumentManager.TopicSections.VersionedCode;
using CygSoft.CodeCat.Domain.TopicSections.FileAttachment;
using CygSoft.CodeCat.Domain.TopicSections.ImagePager;
using CygSoft.CodeCat.Domain.TopicSections.SearchableSnippet;

namespace CygSoft.CodeCat.Domain
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

            if (topicSectionArgs.DocumentType == SectionTypes.SEARCHABLE_SNIPPET_TOPIC_SECTION)
                return CreateSearchableSnippetTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == SectionTypes.VERSIONED_CODE_TOPIC_SECTION)
                return CreateVersionedCodeTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == SectionTypes.WEB_REFERENCES_TOPIC_SECTION)
                return CreateWebReferencesTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == SectionTypes.QIK_SCRIPT_TOPIC_SECTION)
                return CreateQikScriptTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == SectionTypes.PDF_VIEWER_TOPIC_SECTION)
                return CreatePdfViewerTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == SectionTypes.SINGLE_IMAGE_TOPIC_SECTION)
                return CreateSingleImageTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == SectionTypes.IMAGE_PAGER_TOPIC_SECTION)
                return CreateImagePagerTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == SectionTypes.RTF_EDITOR_TOPIC_SECTION)
                return CreateRichTextEditorTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == SectionTypes.FILE_ATTACHMENTS_TOPIC_SECTION)
                return CreateFileAttachmentsTopicSection(topicSectionArgs);

            return null;
        }

        private static ITopicSection CreateSearchableSnippetTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new SearchableSnippetTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title, topicSectionArgs.Extension, topicSectionArgs.Syntax);
            else
                return new SearchableSnippetTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Extension, topicSectionArgs.Ordinal, topicSectionArgs.Description, topicSectionArgs.Syntax);
        }

        private static ITopicSection CreateVersionedCodeTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new VersionedCodeTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title, topicSectionArgs.Extension, topicSectionArgs.Syntax);
            else
                return new VersionedCodeTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Extension, topicSectionArgs.Ordinal, topicSectionArgs.Description, topicSectionArgs.Syntax);
        }

        private static ITopicSection CreateFileAttachmentsTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new FileAttachmentsTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title);
            else
                return new FileAttachmentsTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Ordinal, topicSectionArgs.Description);
        }

        private static ITopicSection CreateImagePagerTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new ImagePagerTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title);
            else
                return new ImagePagerTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Ordinal, topicSectionArgs.Description);
        }

        private static ITopicSection CreateSingleImageTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new SingleImageTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title, topicSectionArgs.Extension);
            else
                return new SingleImageTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Extension, topicSectionArgs.Ordinal, topicSectionArgs.Description);
        }

        private static ITopicSection CreateRichTextEditorTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new RichTextEditorTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title);
            else
                return new RichTextEditorTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Ordinal, topicSectionArgs.Description);
        }

        private static ITopicSection CreatePdfViewerTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new PdfViewerTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title);
            else
                return new PdfViewerTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Ordinal, topicSectionArgs.Description);
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
