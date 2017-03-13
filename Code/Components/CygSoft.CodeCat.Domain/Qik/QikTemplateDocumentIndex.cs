using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Documents;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.CodeCat.Domain.Qik
{
    public class QikTemplateDocumentIndex : BaseDocumentIndex
    {
        public IQikScriptDocument ScriptDocument 
        { 
            get 
            {
                return base.DocumentFiles.OfType<QikScriptDocument>().SingleOrDefault();
            } 
        }

        public QikTemplateDocumentIndex(IDocumentIndexRepository repository, DocumentIndexPathGenerator indexPathGenerator)
            : base(repository, indexPathGenerator)
        {
            if (!base.Exists)
            {
                // creating this document for the very first time... so we need to ensure that we create a script document!
                ICodeDocument scriptDoc = DocumentFactory.Create(DocumentTypeEnum.QikScript, this.Folder, "Qik Script", null, 0, null, "qik", "Qik") as ICodeDocument;
                scriptDoc.Ordinal = 1;  // should always be the last item, but is the first over here.
                this.AddDocumentFile(scriptDoc);
            }
        }

        protected override List<ITopicSection> LoadDocumentFiles()
        {
            return base.indexRepository.LoadDocuments();
        }

        protected override void SaveDocumentIndex()
        {
            base.indexRepository.WriteDocuments(base.DocumentFiles.ToList());
        }

        protected override void AfterAddDocumentFile()
        {
            base.topicSections.MoveLast(this.ScriptDocument as ITopicSection);
        }
    }
}
