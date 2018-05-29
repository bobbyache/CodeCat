using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.DocumentManager.TopicSections;
using CygSoft.CodeCat.Infrastructure.TopicSections;
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


        public IDisposable Document { get; set; }

        protected override void OnBeforeDelete()
        {
            // *** Important fix: Need to have the file handled of the viewer on the UI closed 
            //pdfViewer1.Disposed += (s, e) => ((PdfViewer)pdfViewer1).Document.Dispose();
            //topicDocument.BeforeDelete += (s, e) => pdfViewer1.Dispose();
            Document.Dispose();
            base.OnBeforeDelete();
        }
    }
}
