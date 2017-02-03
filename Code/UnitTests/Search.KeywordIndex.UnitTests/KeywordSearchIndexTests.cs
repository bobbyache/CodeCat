using CygSoft.CodeCat.Search.KeywordIndex;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 4ecac722-8ec5-441c-8e3e-00b192b30453
 * 2d4421df-2b88-470f-b9e8-55af9ccb760d
 * 792858b3-8f68-4047-b7b5-7306c4cd774b
 * 53aacd78-2cf1-48ea-8762-3c8fa8528374
 * f08d4945-625b-4fe2-a8a4-4cedee596ef6
 * dabeb058-7dff-4c74-b2d2-4e5cde75837e
 * */

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
