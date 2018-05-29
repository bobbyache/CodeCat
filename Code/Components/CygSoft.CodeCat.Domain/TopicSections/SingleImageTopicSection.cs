using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.DocumentManager.TopicSections;

namespace CygSoft.CodeCat.Domain.TopicSections
{
    public class SingleImageTopicSection : TopicSection, ISingleImageTopicSection
    {
        // YOU MIGHT NOT NEED TO INHERIT FROM BASEDOCUMENT HERE BECAUSE YOU ACTUALLY DON'T WRITE THE 
        // THE DOCUMENT (However some events etc. might be used further upstream.
        // Something to look into when you have time.
        // Perhaps you need to look at IFile which implemented by BaseFile... this contains all the events etc. And core methods and properties.

        public SingleImageTopicSection(string folder, string title, string extension)
            : base(new DocumentPathGenerator(folder, extension), title, null)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.SingleImage);
        }

        public SingleImageTopicSection(string folder, string id, string title, string extension, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, extension, id), title, description, ordinal)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.SingleImage);
        }
    }
}
