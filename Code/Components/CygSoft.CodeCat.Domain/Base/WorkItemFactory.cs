using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Files.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;

namespace CygSoft.CodeCat.Domain.Base
{
    internal static class WorkItemFactory
    {
        public static IFile Create(IKeywordIndexItem indexItem, string folderPath)
        {
            IFile workItem = null;
            
            if (indexItem is CodeKeywordIndexItem)
                workItem = new CodeFile(new DocumentPathGenerator(folderPath, "xml", indexItem.Id), 
                    indexItem as CodeKeywordIndexItem);

            else if (indexItem is QikTemplateKeywordIndexItem)
                workItem = new QikTemplateDocumentSet(new DocumentIndexPathGenerator(folderPath, "xml", indexItem.Id), 
                    indexItem as QikTemplateKeywordIndexItem);

            else if (indexItem is TopicKeywordIndexItem)
                workItem = new TopicDocument(new DocumentIndexPathGenerator(folderPath, "xml", indexItem.Id),  
                    indexItem as TopicKeywordIndexItem);

            return workItem;
        }
    }
}
