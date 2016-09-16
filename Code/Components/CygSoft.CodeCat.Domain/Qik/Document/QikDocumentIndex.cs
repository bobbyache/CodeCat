using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Documents;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.Qik.Document
{
    public class QikDocumentIndex : BaseDocumentIndex
    {
        public IQikScriptDocument ScriptDocument 
        { 
            get 
            {
                return base.DocumentFiles.OfType<QikScriptDocument>().SingleOrDefault();
            } 
        }

        public QikDocumentIndex(IDocumentIndexRepository repository, DocumentIndexPathGenerator indexPathGenerator)
            : base(repository, indexPathGenerator)
        {
            if (!base.Exists)
            {
                // creating this document for the very first time... so we need to ensure that we create a script document!
                ICodeDocument scriptDoc = DocumentFactory.CreateQikScriptDocument(this.Folder, "Qik Script", "qik", "Qik");
                scriptDoc.Ordinal = 1;  // should always be the last item, but is the first over here.
                this.AddDocumentFile(scriptDoc);
            }
        }

        protected override List<DocumentManager.Infrastructure.IDocument> LoadDocumentFiles()
        {
            return base.indexRepository.LoadDocuments();
        }

        protected override void SaveDocumentIndex()
        {
            base.indexRepository.WriteDocuments(base.DocumentFiles.ToList());
        }

        protected override void AfterAddDocumentFile()
        {
            base.documentFiles.MoveLast(this.ScriptDocument as IDocument);
        }
    }
}
