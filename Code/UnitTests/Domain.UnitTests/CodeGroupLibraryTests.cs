using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex;
using Moq;
using NUnit.Framework;
using System;

namespace Domain.UnitTests
{
    [TestFixture]
    [Category("TopicLibrary")]
    [Category("Tests.UnitTests")]
    public class CodeGroupLibraryTests
    {
        [Test]
        public void TopicLibrary_CreateTarget_ReturnsNonNullTarget()
        {
            Mock<IKeywordSearchIndexRepository> stubSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
            IKeywordSearchIndex keywordSearchIndex = new KeywordSearchIndex(@"C:\parent_folder\_code.xml", new Version("4.0.1"));
            stubSearchIndexRepository
                .Setup(stub => stub.OpenIndex(It.IsAny<string>(), It.IsAny<Version>()))
                .Returns(keywordSearchIndex);
            TopicLibrary topicLibrary = new TopicLibrary(stubSearchIndexRepository.Object, "TestFolder");
            TopicKeywordIndexItem keywordIndexItem = new TopicKeywordIndexItem("Code Group Index Item", "C#", "testing,tested,test");

            topicLibrary.Open(@"C:\parent_folder", new Version("4.0.1"));
            IFile workItem = topicLibrary.CreateWorkItem(keywordIndexItem);

            Assert.That(workItem, Is.Not.Null);
        }

        [Test]
        public void TopicLibrary_OpenWithCodeKeywordIndex_Returns_CodeFile()
        {
            IKeywordIndexItem indexItem = new CodeKeywordIndexItem();
            IFile workItem = WorkItemFactory.Create(indexItem, @"C:\TestFolder\");

            Assert.That(workItem, Is.Not.Null);
            Assert.That(workItem, Is.TypeOf(typeof(CodeFile)));
        }

        [Test]
        public void TopicLibrary_OpenWithQikTemplateKeywordIndex_Returns_QikTemplate()
        {
            IKeywordIndexItem indexItem = new QikTemplateKeywordIndexItem();
            IFile workItem = WorkItemFactory.Create(indexItem, @"C:\TestFolder\");

            Assert.That(workItem, Is.Not.Null);
            Assert.That(workItem, Is.TypeOf(typeof(QikTemplateDocumentSet)));
        }

        [Test]
        public void TopicLibrary_OpenWithTopicKeywordIndex_Returns_Topic()
        {
            IKeywordIndexItem indexItem = new TopicKeywordIndexItem();
            IFile workItem = WorkItemFactory.Create(indexItem, @"C:\TestFolder\");

            Assert.That(workItem, Is.Not.Null);
            Assert.That(workItem, Is.TypeOf(typeof(TopicDocument)));
        }

        [Test]
        public void TopicLibrary_WhenInitialized_IsNotLoaded()
        {
            // Arrange
            Mock<IKeywordSearchIndexRepository> stubSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
            TopicLibrary codeGroupLibrary = new TopicLibrary(stubSearchIndexRepository.Object, "TestFolder");

            // Assert
            Assert.That(codeGroupLibrary.Loaded, Is.False);
        }

        private Mock<IKeywordSearchIndexRepository> GetStubKeywordSearchIndexRepository()
        {
            Mock<IKeywordSearchIndexRepository> stubSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
            IKeywordSearchIndex keywordSearchIndex = new KeywordSearchIndex(@"C:\parent_folder\_code.xml", new Version("4.0.1"));
            stubSearchIndexRepository
                .Setup(stub => stub.OpenIndex(It.IsAny<string>(), It.IsAny<Version>()))
                .Returns(keywordSearchIndex);

            return stubSearchIndexRepository;
        }

        [Test]
        public void TopicLibrary_WhenOpened_WithRepository_IsLoaded()
        {
            // Arrange
            Mock<IKeywordSearchIndexRepository> stubSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
            IKeywordSearchIndex keywordSearchIndex = new KeywordSearchIndex(@"C:\parent_folder\_code.xml", new Version("4.0.1"));
            stubSearchIndexRepository
                .Setup(stub => stub.OpenIndex(It.IsAny<string>(), It.IsAny<Version>()))
                .Returns(keywordSearchIndex);
            TopicLibrary codeGroupLibrary = new TopicLibrary(stubSearchIndexRepository.Object, "TestFolder");

            // Act
            codeGroupLibrary.Open(@"C:\parent_folder", new Version("4.0.1"));

            // Assert
            Assert.That(codeGroupLibrary.Loaded, Is.True);
        }

        [Test]
        public void TopicLibrary_WhenOpened_HasCorrectFileTitle()
        {
            // Arrange
            Mock<IKeywordSearchIndexRepository> stubSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
            IKeywordSearchIndex keywordSearchIndex = new KeywordSearchIndex(@"C:\parent_folder\_code.xml", new Version("4.0.1"));
            stubSearchIndexRepository
                .Setup(stub => stub.OpenIndex(It.IsAny<string>(), It.IsAny<Version>()))
                .Returns(keywordSearchIndex);
            TopicLibrary codeGroupLibrary = new TopicLibrary(stubSearchIndexRepository.Object, "TestFolder");

            // Act
            codeGroupLibrary.Open(@"C:\parent_folder", new Version("4.0.1"));

            // Assert
            Assert.That(codeGroupLibrary.FileTitle, Is.EqualTo("_code.xml"));
        }

        [Test]
        public void TopicLibrary_WhenCreated_CallsCreateIndexOnKeywordSearchIndexRepository()
        {
            // Arrange
            Mock<IKeywordSearchIndexRepository> mockSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
            mockSearchIndexRepository.Setup(mock => mock.CreateIndex(It.IsAny<string>(), It.IsAny<Version>()));
            TopicLibrary codeGroupLibrary = new TopicLibrary(mockSearchIndexRepository.Object, "TestFolder");

            // Act
            codeGroupLibrary.Create(@"C:\parent_folder", new Version("4.0.1"));

            // Assert
            mockSearchIndexRepository.Verify(mock => mock.CreateIndex(It.IsAny<string>(), It.IsAny<Version>()), Times.Once, 
                "IKeywordSearchIndexRepository.CreateIndex should have been called once while executing this method.");
        }

        [Test]
        public void TopicLibrary_WhenOpened_CallsOpenIndexOnKeywordSearchIndexRepository()
        {
            // Arrange
            Mock<IKeywordSearchIndexRepository> mockSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
            mockSearchIndexRepository.Setup(mock => mock.OpenIndex(It.IsAny<string>(), It.IsAny<Version>()));
            TopicLibrary codeGroupLibrary = new TopicLibrary(mockSearchIndexRepository.Object, "TestFolder");

            // Act
            codeGroupLibrary.Open(@"C:\parentFolder", new Version("1.0.0"));

            // Assert
            mockSearchIndexRepository.Verify(mock => mock.OpenIndex(It.IsAny<string>(), It.IsAny<Version>()), Times.Once,
                "IKeywordSearchIndexRepository.OpenIndex should have been called once while executing this method.");
            //mockSearchIndexRepository.VerifyAll();
        }

        [Test]
        public void TopicLibrary_WhenSaved_CallsSaveIndexOnKeywordSearchIndexRepository()
        {
            // Arrange
            Mock<IKeywordSearchIndexRepository> mockSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
            mockSearchIndexRepository.Setup(mock => mock.SaveIndex(It.IsAny<IKeywordSearchIndex>()));
            TopicLibrary codeGroupLibrary = new TopicLibrary(mockSearchIndexRepository.Object, "TestFolder");

            // Act
            codeGroupLibrary.Save();

            // Assert
            mockSearchIndexRepository.Verify(mock => mock.SaveIndex(It.IsAny<IKeywordSearchIndex>()), Times.Once,
                "IKeywordSearchIndexRepository.Save should have been called once while executing this method.");
        }

        [Test]
        public void TopicLibrary_WhenSavedAs_CallsSaveIndexAsOnKeywordSearchIndexRepository()
        {
            // Arrange
            Mock<IKeywordSearchIndexRepository> mockSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
            mockSearchIndexRepository.Setup(mock => mock.SaveIndexAs(It.IsAny<IKeywordSearchIndex>(), It.IsAny<string>()));
            TopicLibrary codeGroupLibrary = new TopicLibrary(mockSearchIndexRepository.Object, "TestFolder");

            // Act
            codeGroupLibrary.SaveAs("C:\test_file_path\testfile.xml");

            // Assert
            mockSearchIndexRepository.Verify(mock => mock.SaveIndexAs(It.IsAny<IKeywordSearchIndex>(), It.IsAny<string>()), Times.Once,
                "IKeywordSearchIndexRepository.SaveIndexAs should have been called once while executing this method.");
        }
    }
}
