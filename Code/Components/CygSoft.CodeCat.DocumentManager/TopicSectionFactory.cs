using CygSoft.CodeCat.DocumentManager.TopicSections;
using CygSoft.CodeCat.DocumentManager.TopicSections.FileAttachments;
using CygSoft.CodeCat.DocumentManager.TopicSections.ImagePager;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.TopicSections.VersionedCode;

namespace CygSoft.CodeCat.DocumentManager
{
    public class TopicSectionFactory
    {
        private const string CODE_TOPIC_SECTION = "CODESNIPPET";
        private const string VERSIONED_CODE_TOPIC_SECTION = "VERSIONEDCODE";
        private const string PDF_VIEWER_TOPIC_SECTION = "PDFDOCUMENT";
        private const string QIK_SCRIPT_TOPIC_SECTION = "QIKSCRIPT";
        private const string RTF_EDITOR_TOPIC_SECTION = "RICHTEXT";
        private const string SINGLE_IMAGE_TOPIC_SECTION = "IMAGEDOCUMENT";
        private const string IMAGE_PAGER_TOPIC_SECTION = "IMAGESET";
        private const string WEB_REFERENCES_TOPIC_SECTION = "URLGROUP";
        private const string FILE_ATTACHMENTS_TOPIC_SECTION = "FILEGROUP";

        private class TopicSectionArgs
        {
            public string DocumentType { get; set; }
            public string Folder { get; set; }
            public string Title { get; set; }
            public string Id { get; set; }
            public int Ordinal { get; set; }
            public string Description { get; set; }
            public string Extension {get; set;}
            public string Syntax { get; set; }
            public string FileName { get; set; }
        }

        public static ITopicSection Create(TopicSectionType documentType, string folder, string title, string id = null, int ordinal = 0, string description = null, string extension = null, string syntax = null)
        {
            TopicSectionArgs topicSectionArgs = new TopicSectionArgs { DocumentType = GetDocumentType(documentType), Folder = folder, Title = title, Id = id, Ordinal = ordinal, Description = description, Extension = extension, Syntax = syntax };

            if (topicSectionArgs.DocumentType == CODE_TOPIC_SECTION)
                return CreateCodeTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == VERSIONED_CODE_TOPIC_SECTION)
                return CreateVersionedCodeTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == WEB_REFERENCES_TOPIC_SECTION)
                return CreateWebReferencesTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == QIK_SCRIPT_TOPIC_SECTION)
                return CreateQikScriptTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == PDF_VIEWER_TOPIC_SECTION)
                return CreatePdfViewerTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == SINGLE_IMAGE_TOPIC_SECTION)
                return CreateSingleImageTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == IMAGE_PAGER_TOPIC_SECTION)
                return CreateImagePagerTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == RTF_EDITOR_TOPIC_SECTION)
                return CreateRichTextEditorTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == FILE_ATTACHMENTS_TOPIC_SECTION)
                return CreateFileAttachmentsTopicSection(topicSectionArgs);

            return null;
        }

        public static TopicSectionType GetDocumentType(string documentType)
        {
            switch (documentType)
            {
                case CODE_TOPIC_SECTION:
                    return TopicSectionType.Code;

                case VERSIONED_CODE_TOPIC_SECTION:
                    return TopicSectionType.VersionedCode;

                case QIK_SCRIPT_TOPIC_SECTION:
                    return TopicSectionType.QikScript;

                case WEB_REFERENCES_TOPIC_SECTION:
                    return TopicSectionType.WebReferences;

                case PDF_VIEWER_TOPIC_SECTION:
                    return TopicSectionType.PdfViewer;

                case SINGLE_IMAGE_TOPIC_SECTION:
                    return TopicSectionType.SingleImage;

                case IMAGE_PAGER_TOPIC_SECTION:
                    return TopicSectionType.ImagePager;

                case RTF_EDITOR_TOPIC_SECTION:
                    return TopicSectionType.RtfEditor;

                case FILE_ATTACHMENTS_TOPIC_SECTION:
                    return TopicSectionType.FileAttachments;

                default:
                    return TopicSectionType.Code;
            }
        }

        public static string GetDocumentType(TopicSectionType documentType)
        {
            switch (documentType)
            {
                case TopicSectionType.Code:
                    return CODE_TOPIC_SECTION;

                case TopicSectionType.VersionedCode:
                    return VERSIONED_CODE_TOPIC_SECTION;

                case TopicSectionType.QikScript:
                    return QIK_SCRIPT_TOPIC_SECTION;

                case TopicSectionType.WebReferences:
                    return WEB_REFERENCES_TOPIC_SECTION;

                case TopicSectionType.PdfViewer:
                    return PDF_VIEWER_TOPIC_SECTION;

                case TopicSectionType.SingleImage:
                    return SINGLE_IMAGE_TOPIC_SECTION;

                case TopicSectionType.ImagePager:
                    return IMAGE_PAGER_TOPIC_SECTION;

                case TopicSectionType.RtfEditor:
                    return RTF_EDITOR_TOPIC_SECTION;

                case TopicSectionType.FileAttachments:
                    return FILE_ATTACHMENTS_TOPIC_SECTION;

                default:
                    return CODE_TOPIC_SECTION;
            }
        }

        private static ITopicSection CreateVersionedCodeTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new VersionedCodeTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title);
            else
                return new VersionedCodeTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Ordinal, topicSectionArgs.Description);
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
