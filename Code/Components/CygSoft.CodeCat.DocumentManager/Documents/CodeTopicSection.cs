using CygSoft.CodeCat.DocumentManager.Infrastructure;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class CodeTopicSection : TextTopicSection, ICodeTopicSection
    {
        public string Syntax { get; set; }

        internal CodeTopicSection(string folder, string title, string extension, string syntax) : base(folder, title, extension)
        {
            this.Syntax = syntax;
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.Code);
        }

        internal CodeTopicSection(string folder, string id, string title, string extension, int ordinal, string description, string syntax)
            : base(folder, id, title, extension, ordinal, description)
        {
            this.Syntax = syntax;
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.Code);
        }
    }
}
