using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.CodeCat.DocumentManager.Documents.ImageSet
{
    internal class ImageSetIndex : BaseDocumentIndex
    {
        public ImageSetIndex(IDocumentIndexRepository repository, ImageSetIndexPathGenerator indexPathGenerator)
            : base(repository, indexPathGenerator)
        {
        }

        protected override List<DocumentManager.Infrastructure.ITopicSection> LoadDocumentFiles()
        {
            return base.indexRepository.LoadDocuments();
        }

        protected override void SaveDocumentIndex()
        {
            base.indexRepository.WriteDocuments(base.DocumentFiles.ToList());
        }
    }
}
