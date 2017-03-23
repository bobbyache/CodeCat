using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.TopicSections;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.CodeCat.Domain.Qik
{
    public class QikTemplateDocumentIndex : BaseDocumentIndex
    {
        public IQikScriptTopicSection QikScriptSection 
        { 
            get 
            {
                return base.TopicSections.OfType<QikScriptTopicSection>().SingleOrDefault();
            } 
        }

        public QikTemplateDocumentIndex(IDocumentIndexRepository repository, DocumentIndexPathGenerator indexPathGenerator)
            : base(repository, indexPathGenerator)
        {
            if (!base.Exists)
            {
                // creating this document for the very first time... so we need to ensure that we create a script document!
                ICodeTopicSection scriptSection = TopicSectionFactory.Create(TopicSectionType.QikScript, this.Folder, "Qik Script", null, 0, null, "qik", "Qik") as ICodeTopicSection;
                scriptSection.Ordinal = 1;  // should always be the last item, but is the first over here.
                this.AddTopicSection(scriptSection);
            }
        }

        protected override List<ITopicSection> LoadTopicSections()
        {
            return base.indexRepository.LoadDocuments();
        }

        protected override void SaveDocumentIndex()
        {
            base.indexRepository.WriteDocuments(base.TopicSections.ToList());
        }

        protected override void AfterAddTopicSection()
        {
            base.topicSections.MoveLast(this.QikScriptSection as ITopicSection);
        }
    }
}
