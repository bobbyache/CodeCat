using CygSoft.CodeCat.DocumentManager.Infrastructure;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class CodeDocument : TextDocument, ICodeDocument
    {
        public string Syntax { get; set; }

        internal CodeDocument(string folder, string title, string extension, string syntax) : base(folder, title, extension)
        {
            this.Syntax = syntax;
            this.DocumentType = DocumentFactory.GetDocumentType(TopicSectionType.CodeSnippet);
        }

        internal CodeDocument(string folder, string id, string title, string extension, int ordinal, string description, string syntax)
            : base(folder, id, title, extension, ordinal, description)
        {
            this.Syntax = syntax;
            this.DocumentType = DocumentFactory.GetDocumentType(TopicSectionType.CodeSnippet);
        }
    }
}
