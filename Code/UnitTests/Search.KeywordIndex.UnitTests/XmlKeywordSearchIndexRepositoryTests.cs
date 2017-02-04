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
            var fileValidator = new StubFileValidator();
            fileValidator.FileExists = false;
            FilePathValidatorFactory.SetValidator(fileValidator);

            TestXmlKeywordSearchIndexRepository repository = new TestXmlKeywordSearchIndexRepository("RootElement");
            Assert.That(() => repository.OpenIndex("", 2), Throws.InstanceOf(typeof(FileNotFoundException)));
        }

        [Test]
        public void XmlIndexRepository_OpeningIncorrectFormat_ThrowsArgumentException()
        {
            var fileValidator = new StubFileValidator();
            fileValidator.FormatCorrect = false;
            FilePathValidatorFactory.SetValidator(fileValidator);

            TestXmlKeywordSearchIndexRepository repository = new TestXmlKeywordSearchIndexRepository("RootElement");
            Assert.That(() => repository.OpenIndex("", 2), Throws.InstanceOf(typeof(InvalidDataException)));
        }

        [Test]
        public void XmlIndexRepository_OpeningIncorrectVersion_ThrowsVersionException()
        {
            var fileValidator = new StubFileValidator();
            fileValidator.VersionCorrect = false;
            FilePathValidatorFactory.SetValidator(fileValidator);

            TestXmlKeywordSearchIndexRepository repository = new TestXmlKeywordSearchIndexRepository("RootElement");
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

        class StubFileValidator : IKeywordSearchIndexFileValidator
        {
            public bool FileExists = true;
            public bool FormatCorrect = true;
            public bool VersionCorrect = true;

            public bool CorrectFormat(string filePath)
            {
                return FormatCorrect;
            }

            public bool CorrectVersion(string filePath)
            {
                return VersionCorrect;
            }

            public bool Exists(string filePath)
            {
                return FileExists;
            }
        }

        class TestXmlKeywordSearchIndexRepository : XmlKeywordSearchIndexRepository<TestXmlKeywordIndexItem>
        {
            public TestXmlKeywordSearchIndexRepository(string rootElement) : base(rootElement)
            {
            }

            protected override List<TestXmlKeywordIndexItem> LoadIndexItems(string filePath, int currentVersion)
            {
                return new List<TestXmlKeywordIndexItem>();
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
