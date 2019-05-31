using CygSoft.CodeCat.DocumentManager.Infrastructure;

namespace CygSoft.CodeCat.DocumentManager.TopicSections
{
    public class CodeTopicSection : TextTopicSection, ICodeTopicSection
    {
        public string Syntax { get; set; }

        public CodeTopicSection(IFileRepository fileRepository, string folder, string title, string extension, string syntax) : base(fileRepository, folder, title, extension)
        {
            this.Syntax = syntax;
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.Code);
        }

        public CodeTopicSection(IFileRepository fileRepository, string folder, string id, string title, string extension, int ordinal, string description, string syntax)
            : base(fileRepository, folder, id, title, extension, ordinal, description)
        {
            this.Syntax = syntax;
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.Code);
        }
    }
}
