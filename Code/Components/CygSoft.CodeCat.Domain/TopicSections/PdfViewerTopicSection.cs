using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.DocumentManager.TopicSections;
using System;

namespace CygSoft.CodeCat.Domain.TopicSections
{
    public class PdfViewerTopicSection : TopicSection, IPdfViewerTopicSection
    {
        // YOU MIGHT NOT NEED TO INHERIT FROM BASEDOCUMENT HERE BECAUSE YOU ACTUALLY DON'T WRITE THE 
        // THE DOCUMENT (However some events etc. might be used further upstream.
        // Something to look into when you have time.
        // Perhaps you need to look at IFile which implemented by BaseFile... this contains all the events etc. And core methods and properties.

        public PdfViewerTopicSection(string folder, string title)
            : base(new DocumentPathGenerator(folder, "pdf"), title, null)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.PdfViewer);
        }

        public PdfViewerTopicSection(string folder, string id, string title, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, "pdf", id), title, description, ordinal)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.PdfViewer);
        }
    }
}
