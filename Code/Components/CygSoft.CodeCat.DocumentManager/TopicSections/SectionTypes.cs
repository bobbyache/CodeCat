using CygSoft.CodeCat.Infrastructure.TopicSections;

namespace CygSoft.CodeCat.DocumentManager.TopicSections
{
    public class SectionTypes
    {
        public const string CODE_TOPIC_SECTION = "CODESNIPPET";

        public static TopicSectionType GetDocumentType(string documentType)
        {
            switch (documentType)
            {
                case CODE_TOPIC_SECTION:
                    return TopicSectionType.Code;

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

                default:
                    return CODE_TOPIC_SECTION;
            }
        }
    }
}
