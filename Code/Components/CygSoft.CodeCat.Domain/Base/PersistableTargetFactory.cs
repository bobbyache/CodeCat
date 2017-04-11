using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;

namespace CygSoft.CodeCat.Domain.Base
{
    public class PersistableTargetFactory
    {
        private static ICodeGroupDocumentSet persistableTarget;

        public static IPersistableTarget Create(ICodeGroupKeywordIndexItem indexItem, string folderPath)
        {
            if (persistableTarget != null)
                return persistableTarget;

            return new CodeGroupDocumentSet(indexItem, folderPath);
        }

        internal static void SetIndexItem(ICodeGroupDocumentSet codeGroupDocumentSet)
        {
            persistableTarget = codeGroupDocumentSet;
        }
    }
}
