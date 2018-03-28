using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Domain.Topics;
using Moq;
using NUnit.Framework;

namespace Domain.UnitTests
{
    [TestFixture]
    public class TopicDocumentTests
    {
        [Test]
        public void TopicDocument_WhenInitialized_WithA_DocumentPathGenerator_Displays_Correct_FilePath()
        {
            Mock<ITopicKeywordIndexItem> indexItem = new Mock<ITopicKeywordIndexItem>();
            DocumentIndexPathGenerator pathGenerator = new DocumentIndexPathGenerator(@"C:\Folder", ".ext", "13215");

            TopicDocument topicDocument = new TopicDocument(pathGenerator, indexItem.Object);
            Assert.AreEqual(@"C:\Folder\13215\13215.ext", topicDocument.FilePath);
        }

        [Test]
        public void TopicDocument_WhenInitialized_WithA_DocumentPathGenerator_Displays_Correct_FileName()
        {
            Mock<ITopicKeywordIndexItem> indexItem = new Mock<ITopicKeywordIndexItem>();
            DocumentIndexPathGenerator pathGenerator = new DocumentIndexPathGenerator(@"C:\Folder", ".ext", "13215");

            TopicDocument topicDocument = new TopicDocument(pathGenerator, indexItem.Object);
            Assert.AreEqual("13215.ext", topicDocument.FileName);
        }

        [Test]
        public void TopicDocument_WhenInitialized_WithA_DocumentPathGenerator_Displays_Correct_FileExt()
        {
            Mock<ITopicKeywordIndexItem> indexItem = new Mock<ITopicKeywordIndexItem>();
            DocumentIndexPathGenerator pathGenerator = new DocumentIndexPathGenerator(@"C:\Folder", ".ext", "13215");

            TopicDocument topicDocument = new TopicDocument(pathGenerator, indexItem.Object);
            Assert.AreEqual("ext", topicDocument.FileExtension);
        }

        [Test]
        public void TopicDocument_WhenInitialized_WithA_DocumentPathGenerator_Displays_Correct_Folder()
        {
            Mock<ITopicKeywordIndexItem> indexItem = new Mock<ITopicKeywordIndexItem>();
            DocumentIndexPathGenerator pathGenerator = new DocumentIndexPathGenerator(@"C:\Folder", ".ext", "13215");

            TopicDocument topicDocument = new TopicDocument(pathGenerator, indexItem.Object);
            Assert.AreEqual(@"C:\Folder\13215", topicDocument.Folder);
        }
    }
}
