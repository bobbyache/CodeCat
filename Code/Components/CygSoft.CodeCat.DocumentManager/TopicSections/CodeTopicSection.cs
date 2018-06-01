using CygSoft.CodeCat.Infrastructure.TopicSections;

namespace CygSoft.CodeCat.DocumentManager.TopicSections
{
    public class CodeTopicSection : TextTopicSection, ICodeTopicSection
    {
        public string Syntax { get; set; }

        public CodeTopicSection(string folder, string title, string extension, string syntax) : base(folder, title, extension)
        {
            this.Syntax = syntax;
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.Code);
        }

        public CodeTopicSection(string folder, string id, string title, string extension, int ordinal, string description, string syntax)
            : base(folder, id, title, extension, ordinal, description)
        {
            this.Syntax = syntax;
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.Code);
        }
    }
}
