using CygSoft.CodeCat.Search.KeywordIndex;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Search.KeywordIndex.UnitTests
{
    [TestFixture]
    class KeywordSearchIndexTests
    {
        [Test]
        public void KeywordSearchIndex_Create()
        {
            IKeywordSearchIndex searchIndex = new KeywordSearchIndex(@"C:\keywords\keyword_index.xml", 2);

            Assert.AreEqual(searchIndex.CurrentVersion, 2);
            Assert.AreEqual(searchIndex.FilePath, @"C:\keywords\keyword_index.xml");
            Assert.AreEqual(searchIndex.FileTitle, "keyword_index.xml");
        }

        //[Test]
        //public void KeywordSearchIndex_WhenAddingKeywordsToIndexItems_ReturnsTrueOnSubsequentSearchForOneOfThoseKeywords()
        //{
            

        //    // ### ARRANGE ###
        //    var stubKeywordIndexItem = new Mock<IKeywordIndexItem>();
        //    stubKeywordIndexItem.Setup(ki => ki.CommaDelimitedKeywords).Returns("test,testing,tested");
        //    stubKeywordIndexItem.Setup(ki => ki.Keywords).Returns(new string[] { "test", "testing", "tested" });
        //    // currently have to mock as uppercase "TEST", should rather perform this case conversion in the "AllKeywordsFound()" method.
        //    stubKeywordIndexItem.Setup(ki => ki.AllKeywordsFound(new string[] { "TEST" })).Returns(true);
        //    IKeywordSearchIndex searchIndex = new KeywordSearchIndex(@"C:\keywords\keyword_index.xml", 2, new List<IKeywordIndexItem> { stubKeywordIndexItem.Object });

        //    // ### ACT ###
        //    /*
        //     * This method of adding keywords to items within the search index does not feel right.
        //     *                  searchIndex.AddKeywordsTo(string[] indexIds, string delimitedKeywords)
        //     * ... is probably a better idea.
        //     */
        //    searchIndex.AddKeywords(new IKeywordIndexItem[] { stubKeywordIndexItem.Object }, @"test,testing,tested");
        //    IKeywordIndexItem[] items = searchIndex.Find("TEST");

        //    // ### ASSERT ###
        //    // Only finding a single item because we've added it into the index above....

        //    Assert.IsNotNull(items, "A single item should be found.");
        //    Assert.IsTrue(items.Length == 1);
        //}

        //[Test]
        //public void KeywordSearchIndex_WhenRemovingKeywordsFromIndexItems_ReturnsTrueOnSubsequentSearchForOneOfThoseKeywords()
        //{
        //    // ### ARRANGE ###
        //    var stubKeywordIndexItem = new Mock<IKeywordIndexItem>();
        //    stubKeywordIndexItem.Setup(ki => ki.CommaDelimitedKeywords).Returns("test,testing,tested");
        //    stubKeywordIndexItem.Setup(ki => ki.Keywords).Returns(new string[] { "test", "testing", "tested" });
        //    // currently have to mock as uppercase "TEST", should rather perform this case conversion in the "AllKeywordsFound()" method.
        //    stubKeywordIndexItem.Setup(ki => ki.AllKeywordsFound(new string[] { "TEST" })).Returns(true);
        //    IKeywordSearchIndex searchIndex = new KeywordSearchIndex(@"C:\keywords\keyword_index.xml", 2, new List<IKeywordIndexItem> { stubKeywordIndexItem.Object });

        //    // ### ACT ###
        //    /*
        //     * This method of adding keywords to items within the search index does not feel right.
        //     *                  searchIndex.AddKeywordsTo(string[] indexIds, string delimitedKeywords)
        //     * ... is probably a better idea.
        //     */
        //    searchIndex.AddKeywords(new IKeywordIndexItem[] { stubKeywordIndexItem.Object }, @"test,testing,tested");
        //    IKeywordIndexItem[] items = searchIndex.Find("TEST");

        //    // ### ASSERT ###
        //    // Only finding a single item because we've added it into the index above....

        //    Assert.IsNotNull(items, "A single item should be found.");
        //    Assert.IsTrue(items.Length == 1);
        //}

    }
}
