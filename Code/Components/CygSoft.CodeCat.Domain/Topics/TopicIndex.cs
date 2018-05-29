using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.CodeCat.Domain.Topics
{
    public class TopicIndex : Topic
    {
        public TopicIndex(IDocumentIndexRepository repository, DocumentIndexPathGenerator indexPathGenerator)
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
