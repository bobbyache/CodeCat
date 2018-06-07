using CygSoft.CodeCat.Infrastructure.TopicSections;

namespace CygSoft.CodeCat.DocumentManager.TopicSections
{
    public class SectionTypes
    {
        public const string CODE_TOPIC_SECTION = "CODESNIPPET";
        public const string QIK_SCRIPT_TOPIC_SECTION = "QIKSCRIPT";
        public const string WEB_REFERENCES_TOPIC_SECTION = "URLGROUP";

        public static TopicSectionType GetDocumentType(string documentType)
        {
            switch (documentType)
            {
                case CODE_TOPIC_SECTION:
                    return TopicSectionType.Code;

                case QIK_SCRIPT_TOPIC_SECTION:
                    return TopicSectionType.QikScript;

                case WEB_REFERENCES_TOPIC_SECTION:
                    return TopicSectionType.WebReferences;

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

                case TopicSectionType.QikScript:
                    return QIK_SCRIPT_TOPIC_SECTION;

                case TopicSectionType.WebReferences:
                    return WEB_REFERENCES_TOPIC_SECTION;

                default:
                    return CODE_TOPIC_SECTION;
            }
        }
    }
}
