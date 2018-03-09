using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Domain.Qik;
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
    public class QikTemplateDocumentSetTests
    {
        [Test]
        public void QikTemplateDocumentSet_WhenInitialized_WithA_DocumentPathGenerator_Displays_Correct_FilePath()
        {
            Mock<IQikTemplateKeywordIndexItem> indexItem = new Mock<IQikTemplateKeywordIndexItem>();
            DocumentIndexPathGenerator pathGenerator = new DocumentIndexPathGenerator(@"C:\Folder", ".ext", "13215");

            QikTemplateDocumentSet topicDocument = new QikTemplateDocumentSet(pathGenerator, indexItem.Object);
            Assert.AreEqual(@"C:\Folder\13215\13215.ext", topicDocument.FilePath);
        }

        [Test]
        public void QikTemplateDocumentSet_WhenInitialized_WithA_DocumentPathGenerator_Displays_Correct_FileName()
        {
            Mock<IQikTemplateKeywordIndexItem> indexItem = new Mock<IQikTemplateKeywordIndexItem>();
            DocumentIndexPathGenerator pathGenerator = new DocumentIndexPathGenerator(@"C:\Folder", ".ext", "13215");

            QikTemplateDocumentSet topicDocument = new QikTemplateDocumentSet(pathGenerator, indexItem.Object);
            Assert.AreEqual("13215.ext", topicDocument.FileName);
        }

        [Test]
        public void QikTemplateDocumentSet_WhenInitialized_WithA_DocumentPathGenerator_Displays_Correct_FileExt()
        {
            Mock<IQikTemplateKeywordIndexItem> indexItem = new Mock<IQikTemplateKeywordIndexItem>();
            DocumentIndexPathGenerator pathGenerator = new DocumentIndexPathGenerator(@"C:\Folder", ".ext", "13215");

            QikTemplateDocumentSet topicDocument = new QikTemplateDocumentSet(pathGenerator, indexItem.Object);
            Assert.AreEqual("ext", topicDocument.FileExtension);
        }

        [Test]
        public void QikTemplateDocumentSet_WhenInitialized_WithA_DocumentPathGenerator_Displays_Correct_Folder()
        {
            Mock<IQikTemplateKeywordIndexItem> indexItem = new Mock<IQikTemplateKeywordIndexItem>();
            DocumentIndexPathGenerator pathGenerator = new DocumentIndexPathGenerator(@"C:\Folder", ".ext", "13215");

            QikTemplateDocumentSet topicDocument = new QikTemplateDocumentSet(pathGenerator, indexItem.Object);
            Assert.AreEqual(@"C:\Folder\13215", topicDocument.Folder);
        }
    }
}
