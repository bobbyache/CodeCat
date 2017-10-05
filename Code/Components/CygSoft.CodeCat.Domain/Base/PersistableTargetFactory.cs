using CygSoft.CodeCat.Domain.Topics;

namespace CygSoft.CodeCat.Domain.Base
{
    public class PersistableTargetFactory
    {
        private static ITopicDocument persistableTarget;

        public static IWorkItem Create(ITopicKeywordIndexItem indexItem, string folderPath)
        {
            if (persistableTarget != null)
                return persistableTarget;

            return new TopicDocument(indexItem, folderPath);
        }

        internal static void SetIndexItem(ITopicDocument topicDocument)
        {
            persistableTarget = topicDocument;
        }
    }
}
