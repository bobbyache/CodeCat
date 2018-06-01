using CygSoft.CodeCat.DocumentManager.TopicSections;
using CygSoft.CodeCat.Infrastructure.TopicSections;

namespace CygSoft.CodeCat.Domain.TopicSections
{
    public class QikScriptTopicSection : CodeTopicSection, IQikScriptTopicSection
    {
        public QikScriptTopicSection(string folder, string title, string extension, string syntax) : base(folder, title, extension, syntax)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.QikScript);
        }

        public QikScriptTopicSection(string folder, string id, string title, string extension, int ordinal, string description, string syntax)
            : base(folder, id, title, extension, ordinal, description, syntax)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.QikScript);
        }
    }
}
