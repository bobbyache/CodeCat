using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class RichTextEditorTopicSection : TopicSection, IRichTextEditorTopicSection
    {
        // YOU MIGHT NOT NEED TO INHERIT FROM BASEDOCUMENT HERE BECAUSE YOU ACTUALLY DON'T WRITE THE 
        // THE DOCUMENT (However some events etc. might be used further upstream.
        // Something to look into when you have time.
        // Perhaps you need to look at IFile which implemented by BaseFile... this contains all the events etc. And core methods and properties.

        public event EventHandler RequestSaveRtf;

        internal RichTextEditorTopicSection(string folder, string title)
            : base(new DocumentPathGenerator(folder, "rtf"), title, null)
        {
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.RtfEditor);
        }

        internal RichTextEditorTopicSection(string folder, string id, string title, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, "rtf", id), title, description, ordinal)
        {
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.RtfEditor);
        }

        protected override void OpenFile()
        {
            // DON'T WANT TO IMPLEMENT THIS BECAUSE THE FILE IS NOT OPENED THE SAME WAY OTHER DOCUMENTS ARE OPENED.
        }

        protected override void SaveFile()
        {
            // Delegate the saving of this file to the caller.
            RequestSaveRtf?.Invoke(this, new EventArgs());
        }
    }
}
