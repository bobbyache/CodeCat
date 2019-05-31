using CygSoft.CodeCat.DocumentManager.Infrastructure;

namespace CygSoft.CodeCat.DocumentManager.TopicSections
{
    public class CodeTopicSection : TextTopicSection, ICodeTopicSection
    {
        public string Syntax { get; set; }

        public CodeTopicSection(IFileRepository fileRepository, string title, string syntax) 
            : base(fileRepository, title)
        {
            this.Syntax = syntax;
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.Code);
        }

        public CodeTopicSection(IFileRepository fileRepository, string title, int ordinal, string description, string syntax)
            : base(fileRepository, title, ordinal, description)
        {
            this.Syntax = syntax;
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.Code);
        }
    }
}
