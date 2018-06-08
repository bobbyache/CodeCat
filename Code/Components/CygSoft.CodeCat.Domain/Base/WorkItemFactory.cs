using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Plugin.Infrastructure;

namespace CygSoft.CodeCat.Domain.Base
{
    internal static class WorkItemFactory
    {
        public static IFile Create(IKeywordIndexItem indexItem, string folderPath)
        {
            IFile workItem = new TopicDocument(new DocumentIndexPathGenerator(folderPath, "xml", indexItem.Id),
                    indexItem as TopicKeywordIndexItem);
            
            return workItem;
        }
    }
}
