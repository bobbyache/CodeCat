using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Domain.Topics;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests
{
    [TestFixture]
    public class TopicDocumentTests
    {
        [Test]
        public void TopicDocument_WhenInitialized_WithA_DocumentPathGenerator_Displays_Correct_State()
        {
            Mock<ITopicKeywordIndexItem> indexItem = new Mock<ITopicKeywordIndexItem>();
            DocumentIndexPathGenerator pathGenerator = new DocumentIndexPathGenerator(@"C:\Folder", ".ext", "13215");

            TopicDocument topicDocument = new TopicDocument(pathGenerator, indexItem.Object);
            Assert.AreEqual(@"C:\Folder\13215\13215.ext", topicDocument.FilePath);

        }
    }
}
