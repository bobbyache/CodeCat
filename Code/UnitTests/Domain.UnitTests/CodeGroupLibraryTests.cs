using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.Search.KeywordIndex;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using Moq;
using NUnit.Framework;
using System;

namespace Domain.UnitTests
{
    [TestFixture]
    public class CodeGroupLibraryTests
    {
        [Test]
        public void CodeGroupLibrary_CreateTarget_ReturnsNonNullTarget()
        {
            Mock<IKeywordSearchIndexRepository> stubSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
            IKeywordSearchIndex keywordSearchIndex = new KeywordSearchIndex(@"C:\parent_folder\_code.xml", new Version("4.0.1"));
            stubSearchIndexRepository
                .Setup(stub => stub.OpenIndex(It.IsAny<string>(), It.IsAny<Version>()))
                .Returns(keywordSearchIndex);
            CodeGroupLibrary codeGroupLibrary = new CodeGroupLibrary(stubSearchIndexRepository.Object, "TestFolder");
            CodeGroupKeywordIndexItem keywordIndexItem = new CodeGroupKeywordIndexItem("Code Group Index Item", "C#", "testing,tested,test");

            codeGroupLibrary.Open(@"C:\parent_folder", new Version("4.0.1"));
            IPersistableTarget persistableTarget = codeGroupLibrary.CreateTarget(keywordIndexItem);

            Assert.That(persistableTarget, Is.Not.Null);
        }

        //[Test]
        //public void CodeGroupLibrary_OpenTarget_ReturnsExistingTarget()
        //{
        //    Mock<IKeywordSearchIndexRepository> stubSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
        //    IKeywordSearchIndex keywordSearchIndex = new KeywordSearchIndex(@"C:\parent_folder\_code.xml", new Version("4.0.1"));
        //    stubSearchIndexRepository
        //        .Setup(stub => stub.OpenIndex(It.IsAny<string>(), It.IsAny<Version>()))
        //        .Returns(keywordSearchIndex);
        //    CodeGroupLibrary codeGroupLibrary = new CodeGroupLibrary(stubSearchIndexRepository.Object, "TestFolder");
        //    CodeGroupKeywordIndexItem keywordIndexItem = new CodeGroupKeywordIndexItem("Code Group Index Item", "C#", "testing,tested,test");

        //    codeGroupLibrary.Open(@"C:\parent_folder", new Version("4.0.1"));
        //    IPersistableTarget persistableTarget = codeGroupLibrary.OpenTarget(keywordIndexItem);

        //    Assert.That(persistableTarget, Is.Not.Null);
        //}

        [Test]
        public void CodeGroupLibrary_WhenInitialized_IsNotLoaded()
        {
            // Arrange
            Mock<IKeywordSearchIndexRepository> stubSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
            CodeGroupLibrary codeGroupLibrary = new CodeGroupLibrary(stubSearchIndexRepository.Object, "TestFolder");

            // Assert
            Assert.That(codeGroupLibrary.Loaded, Is.False);
        }

        [Test]
        public void CodeGroupLibrary_WhenOpened_WithRepository_IsLoaded()
        {
            // Arrange
            Mock<IKeywordSearchIndexRepository> stubSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
            IKeywordSearchIndex keywordSearchIndex = new KeywordSearchIndex(@"C:\parent_folder\_code.xml", new Version("4.0.1"));
            stubSearchIndexRepository
                .Setup(stub => stub.OpenIndex(It.IsAny<string>(), It.IsAny<Version>()))
                .Returns(keywordSearchIndex);
            CodeGroupLibrary codeGroupLibrary = new CodeGroupLibrary(stubSearchIndexRepository.Object, "TestFolder");

            // Act
            codeGroupLibrary.Open(@"C:\parent_folder", new Version("4.0.1"));

            // Assert
            Assert.That(codeGroupLibrary.Loaded, Is.True);
        }

        [Test]
        public void CodeGroupLibrary_WhenOpened_HasCorrectFileTitle()
        {
            // Arrange
            Mock<IKeywordSearchIndexRepository> stubSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
            IKeywordSearchIndex keywordSearchIndex = new KeywordSearchIndex(@"C:\parent_folder\_code.xml", new Version("4.0.1"));
            stubSearchIndexRepository
                .Setup(stub => stub.OpenIndex(It.IsAny<string>(), It.IsAny<Version>()))
                .Returns(keywordSearchIndex);
            CodeGroupLibrary codeGroupLibrary = new CodeGroupLibrary(stubSearchIndexRepository.Object, "TestFolder");

            // Act
            codeGroupLibrary.Open(@"C:\parent_folder", new Version("4.0.1"));

            // Assert
            Assert.That(codeGroupLibrary.FileTitle, Is.EqualTo("_code.xml"));
        }

        [Test]
        public void CodeGroupLibrary_WhenCreated_CallsCreateIndexOnKeywordSearchIndexRepository()
        {
            // Arrange
            Mock<IKeywordSearchIndexRepository> mockSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
            mockSearchIndexRepository.Setup(mock => mock.CreateIndex(It.IsAny<string>(), It.IsAny<Version>()));
            CodeGroupLibrary codeGroupLibrary = new CodeGroupLibrary(mockSearchIndexRepository.Object, "TestFolder");

            // Act
            codeGroupLibrary.Create(@"C:\parent_folder", new Version("4.0.1"));

            // Assert
            mockSearchIndexRepository.Verify(mock => mock.CreateIndex(It.IsAny<string>(), It.IsAny<Version>()), Times.Once, 
                "IKeywordSearchIndexRepository.CreateIndex should have been called once while executing this method.");
        }

        [Test]
        public void CodeGroupLibrary_WhenOpened_CallsOpenIndexOnKeywordSearchIndexRepository()
        {
            // Arrange
            Mock<IKeywordSearchIndexRepository> mockSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
            mockSearchIndexRepository.Setup(mock => mock.OpenIndex(It.IsAny<string>(), It.IsAny<Version>()));
            CodeGroupLibrary codeGroupLibrary = new CodeGroupLibrary(mockSearchIndexRepository.Object, "TestFolder");

            // Act
            codeGroupLibrary.Open(@"C:\parentFolder", new Version("1.0.0"));

            // Assert
            mockSearchIndexRepository.Verify(mock => mock.OpenIndex(It.IsAny<string>(), It.IsAny<Version>()), Times.Once,
                "IKeywordSearchIndexRepository.OpenIndex should have been called once while executing this method.");
            //mockSearchIndexRepository.VerifyAll();
        }

        [Test]
        public void CodeGroupLibrary_WhenSaved_CallsSaveIndexOnKeywordSearchIndexRepository()
        {
            // Arrange
            Mock<IKeywordSearchIndexRepository> mockSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
            mockSearchIndexRepository.Setup(mock => mock.SaveIndex(It.IsAny<IKeywordSearchIndex>()));
            CodeGroupLibrary codeGroupLibrary = new CodeGroupLibrary(mockSearchIndexRepository.Object, "TestFolder");

            // Act
            codeGroupLibrary.Save();

            // Assert
            mockSearchIndexRepository.Verify(mock => mock.SaveIndex(It.IsAny<IKeywordSearchIndex>()), Times.Once,
                "IKeywordSearchIndexRepository.Save should have been called once while executing this method.");
        }

        [Test]
        public void CodeGroupLibrary_WhenSavedAs_CallsSaveIndexAsOnKeywordSearchIndexRepository()
        {
            // Arrange
            Mock<IKeywordSearchIndexRepository> mockSearchIndexRepository = new Mock<IKeywordSearchIndexRepository>();
            mockSearchIndexRepository.Setup(mock => mock.SaveIndexAs(It.IsAny<IKeywordSearchIndex>(), It.IsAny<string>()));
            CodeGroupLibrary codeGroupLibrary = new CodeGroupLibrary(mockSearchIndexRepository.Object, "TestFolder");

            // Act
            codeGroupLibrary.SaveAs("C:\test_file_path\testfile.xml");

            // Assert
            mockSearchIndexRepository.Verify(mock => mock.SaveIndexAs(It.IsAny<IKeywordSearchIndex>(), It.IsAny<string>()), Times.Once,
                "IKeywordSearchIndexRepository.SaveIndexAs should have been called once while executing this method.");
        }

        //public class TestKeywordIndexItem : KeywordIndexItem
        //{
        //    public TestKeywordIndexItem() : base()
        //    {
        //    }

        //    public TestKeywordIndexItem(string title, string commaDelimitedKeywords) :
        //        base(title, commaDelimitedKeywords)
        //    {

        //    }

        //    public TestKeywordIndexItem(string id, string title, DateTime dateCreated, DateTime dateModified, string commaDelimitedKeywords) :
        //        base(id, title, dateCreated, dateModified, commaDelimitedKeywords)
        //    {

        //    }
        //}
    }
}
