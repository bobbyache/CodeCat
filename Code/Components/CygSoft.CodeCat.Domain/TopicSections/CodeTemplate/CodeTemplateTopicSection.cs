using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.DocumentManager.TopicSections;
using CygSoft.CodeCat.Infrastructure.TopicSections;

namespace CygSoft.CodeCat.Domain.TopicSections.CodeTemplate
{
    public class CodeTemplateTopicSection : TopicSection, ICodeTemplateTopicSection
    {
        public CodeTemplateTopicSection(string folder, string title)
            : base(new DocumentPathGenerator(folder, "filgrp"), title, null)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.CodeTemplate);
        }

        public CodeTemplateTopicSection(string folder, string id, string title, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, "filgrp", id), title, description, ordinal)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.CodeTemplate);
        }
    }
}
