using CygSoft.CodeCat.Infrastructure.TopicSections;

namespace CygSoft.CodeCat.DocumentManager.TopicSections
{
    public class SectionTypes
    {
        public const string CODE_TOPIC_SECTION = "CODESNIPPET";
        public const string VERSIONED_CODE_TOPIC_SECTION = "VERSIONEDCODE";
        public const string PDF_VIEWER_TOPIC_SECTION = "PDFDOCUMENT";
        public const string QIK_SCRIPT_TOPIC_SECTION = "QIKSCRIPT";
        public const string RTF_EDITOR_TOPIC_SECTION = "RICHTEXT";
        public const string SINGLE_IMAGE_TOPIC_SECTION = "IMAGEDOCUMENT";
        public const string IMAGE_PAGER_TOPIC_SECTION = "IMAGESET";
        public const string WEB_REFERENCES_TOPIC_SECTION = "URLGROUP";
        public const string FILE_ATTACHMENTS_TOPIC_SECTION = "FILEGROUP";
        public const string SEARCHABLE_SNIPPET_TOPIC_SECTION = "SEARCHABLESNIPPET";
        public const string SEARCHABLE_EVENT_TOPIC_SECTION = "SEARCHABLEEVENT";
        public const string CODE_TEMPLATE_SECTION = "CODETEMPLATE";
        public const string TASKLIST_SECTION = "TASKLIST";

        public static TopicSectionType GetDocumentType(string documentType)
        {
            switch (documentType)
            {
                case CODE_TOPIC_SECTION:
                    return TopicSectionType.Code;

                case SEARCHABLE_SNIPPET_TOPIC_SECTION:
                    return TopicSectionType.SearchableSnippet;

                case SEARCHABLE_EVENT_TOPIC_SECTION:
                    return TopicSectionType.SearchableEvent;

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

                case CODE_TEMPLATE_SECTION:
                    return TopicSectionType.CodeTemplate;

                case TASKLIST_SECTION:
                    return TopicSectionType.TaskList;

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

                case TopicSectionType.SearchableSnippet:
                    return SEARCHABLE_SNIPPET_TOPIC_SECTION;

                case TopicSectionType.SearchableEvent:
                    return SEARCHABLE_EVENT_TOPIC_SECTION;

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

                case TopicSectionType.CodeTemplate:
                    return CODE_TEMPLATE_SECTION;

                case TopicSectionType.TaskList:
                    return TASKLIST_SECTION;

                default:
                    return CODE_TOPIC_SECTION;
            }
        }
    }
}
