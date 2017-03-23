using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.CodeCat.Domain.CodeGroup
{
    public class CodeGroupIndex : Topic
    {
        public CodeGroupIndex(IDocumentIndexRepository repository, DocumentIndexPathGenerator indexPathGenerator)
            : base(repository, indexPathGenerator)
        {
        }

        protected override List<ITopicSection> LoadTopicSections()
        {
            return base.indexRepository.LoadDocuments();
        }

        protected override void SaveDocumentIndex()
        {
            base.indexRepository.WriteDocuments(base.TopicSections.ToList());
        }
    }
}
