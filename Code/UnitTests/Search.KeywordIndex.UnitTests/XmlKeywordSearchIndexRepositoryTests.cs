using CygSoft.CodeCat.Search.KeywordIndex;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System.IO;
using Moq;
using Search.KeywordIndex.UnitTests.Helpers;

namespace Search.KeywordIndex.UnitTests
{
    [TestFixture]
    class XmlKeywordSearchIndexRepositoryTests
    {
        // Replacing the ExpectedExceptionAttribute with Throws.InstanceOf
        // http://jamesnewkirk.typepad.com/posts/2008/06/replacing-expec.html

        [Test]
        public void XmlIndexRepository_OpeningNonExistingFile_ThrowsFileNotFoundException()
        {
            TestXmlKeywordSearchIndexRepository repository = new TestXmlKeywordSearchIndexRepository("RootElement");
            repository.IsExistingFile = false; // simulate that the index file is not found at the path expected.

            Assert.That(() => repository.OpenIndex("", 2), Throws.InstanceOf(typeof(FileNotFoundException)),
                "Stub method simulated that an Index file was not found at the path specified. Repository should have thrown a FileNotFoundException.");
        }

        [Test]
        public void XmlIndexRepository_OpeningIncorrectFormat_ThrowsArgumentException()
        {
            TestXmlKeywordSearchIndexRepository repository = new TestXmlKeywordSearchIndexRepository("RootElement");
            repository.IsCorrectFormat = false; // simulate that the index file format is incorrect

            Assert.That(() => repository.OpenIndex("", 2), Throws.InstanceOf(typeof(InvalidDataException)),
                "Stub method simulated that an Index file format was invalid but a InvalidDataException was not thrown.");
        }

        [Test]
        public void XmlIndexRepository_OpeningIncorrectVersion_ThrowsVersionException()
        {
            TestXmlKeywordSearchIndexRepository repository = new TestXmlKeywordSearchIndexRepository("RootElement");
            repository.IsCorrectVersion = false; // simulate that the index file version is incorrect

            Assert.That(() => repository.OpenIndex("", 2), Throws.InstanceOf(typeof(InvalidFileIndexVersionException)));
        }

        [Test]
        public void XmlIndexRepository_CloneIndex_ClonesCorrectly()
        {
            var indexItems = new IKeywordIndexItem[]
            {
                new TestKeywordIndexItem("Item 1", "green,blue"),
                new TestKeywordIndexItem("Item 1", "green,red"),
                new TestKeywordIndexItem("Item 1", "yellow,gray")
            };
            var stubKeywordSearchIndex = new Mock<IKeywordSearchIndex>();
            stubKeywordSearchIndex.Setup(m => m.All()).Returns(indexItems);
            stubKeywordSearchIndex.Setup(m => m.CurrentVersion).Returns(2);

            var keywordSearchIndex = new KeywordSearchIndex("C:File.xml", 2);
 
            TestXmlKeywordSearchIndexRepository repository = new TestXmlKeywordSearchIndexRepository("RootElement");
            IKeywordSearchIndex newSearchIndex = repository.CloneIndex(stubKeywordSearchIndex.Object, @"C:\hello_world.txt");

            Assert.That(newSearchIndex, Is.Not.SameAs(stubKeywordSearchIndex.Object));
            Assert.That(newSearchIndex.ItemCount, Is.EqualTo(3));
            Assert.That(newSearchIndex.Contains(indexItems[0]), Is.True);
            Assert.That(newSearchIndex.Contains(indexItems[1]), Is.True);
            Assert.That(newSearchIndex.Contains(indexItems[2]), Is.True);
        }

        class StubKeywordSearchIndexFile : IKeywordSearchIndexFile
        {
            public bool IsCorrectFormat = true;
            public bool IsCorrectVersion = true;
            public bool IsExistingFile = true;

            public string FilePath { get; private set; }

            public string Text { get { return ""; } }

            public bool CorrectFormat { get { return IsCorrectFormat; } }

            public bool CorrectVersion { get { return IsCorrectVersion; } }

            public bool FileExists {  get { return IsExistingFile; } } 

            public void Save(string fileText) {  }

            public string Open()
            {
                return string.Empty;
            }

            public void SaveAs(string fileText, string filePath)
            {
                FilePath = filePath;
            }
        }

        class TestXmlKeywordSearchIndexRepository : XmlKeywordSearchIndexRepository<TestXmlKeywordIndexItem>
        {

            public bool IsExistingFile = true;
            public bool IsCorrectFormat = true;
            public bool IsCorrectVersion = true;

            public TestXmlKeywordSearchIndexRepository(string rootElement) : base(rootElement)
            {
            }

            protected override List<TestXmlKeywordIndexItem> LoadIndexItems(string filePath, int currentVersion)
            {
                return new List<TestXmlKeywordIndexItem>();
            }

            protected override IKeywordSearchIndexFile NewIndexFile(string filePath)
            {
                StubKeywordSearchIndexFile stubFile = new StubKeywordSearchIndexFile();
                stubFile.IsCorrectFormat = IsCorrectFormat;
                stubFile.IsCorrectVersion = IsCorrectVersion;
                stubFile.IsExistingFile = IsExistingFile;

                return stubFile;
            }
        }

        class TestXmlKeywordIndexItem : XmlKeywordIndexItem
        {
            public TestXmlKeywordIndexItem() : base()
            {
            }

            public TestXmlKeywordIndexItem(string id, string title, string syntax, DateTime dateCreated, DateTime dateModified, string commaDelimitedKeywords)
            : base(id, title, syntax, dateCreated, dateModified, commaDelimitedKeywords)
            {
            }

            public TestXmlKeywordIndexItem(string title, string syntax, string commaDelimitedKeywords)
            : base(title, syntax, commaDelimitedKeywords)
            {
            }
        }
    }
}
