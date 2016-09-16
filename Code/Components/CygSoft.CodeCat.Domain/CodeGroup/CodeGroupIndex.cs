using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.CodeGroup
{
    public class CodeGroupIndex : BaseDocumentIndex
    {
        public CodeGroupIndex(IDocumentIndexRepository repository, DocumentIndexPathGenerator indexPathGenerator)
            : base(repository, indexPathGenerator)
        {
        }

        protected override List<DocumentManager.Infrastructure.IDocument> LoadDocumentFiles()
        {
            return base.indexRepository.LoadDocuments();
        }

        protected override void SaveDocumentIndex()
        {
            base.indexRepository.WriteDocuments(base.DocumentFiles.ToList());
        }
    }
}
