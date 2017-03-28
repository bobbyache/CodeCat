using CygSoft.CodeCat.Search.KeywordIndex;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using Moq;
using NUnit.Framework;
using Search.KeywordIndex.UnitTests.Helpers;
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
            IKeywordSearchIndex searchIndex = new KeywordSearchIndex(@"C:\keywords\keyword_index.xml", new Version(2,0));

            Assert.AreEqual(searchIndex.CurrentVersion.ToString(), new Version(2, 0).ToString());
            Assert.AreEqual(searchIndex.FilePath, @"C:\keywords\keyword_index.xml");
            Assert.AreEqual(searchIndex.FileTitle, "keyword_index.xml");
        }

        [Test]
        public void KeywordSearchIndex_AddKeywords_SetsDateModified()
        {
            IKeywordSearchIndex searchIndex = new KeywordSearchIndex(@"C:\keywords\keyword_index.xml", new Version(2, 0));
            var keywordSearchIndexItem = new TestKeywordIndexItem();
            searchIndex.AddKeywords(new IKeywordIndexItem[] { keywordSearchIndexItem }, @"test,testing,tested");

        }

        [Test]
        public void KeywordSearchIndex_WhenAddingKeywordsToIndexItems_ReturnsTrueOnSubsequentSearchForOneOfThoseKeywords()
        {
            var keywordSearchIndexItem = new TestKeywordIndexItem();
            IKeywordSearchIndex searchIndex = new KeywordSearchIndex("", new Version(2, 0), new List<IKeywordIndexItem> { keywordSearchIndexItem });
            searchIndex.AddKeywords(new IKeywordIndexItem[] { keywordSearchIndexItem }, @"test,testing,tested");
            IKeywordIndexItem[] items = searchIndex.Find("TEST");

            Assert.IsNotNull(items, "A single item should be found.");
            Assert.IsTrue(items.Length == 1);
        }

        [Test]
        public void KeywordSearchIndex_AfterRemovingKeywordsFromIndexItems_ReturnsFalseOnSubsequentSearchForThoseKeywords()
        {
            var keywordSearchIndexItem = new TestKeywordIndexItem();
            var searchIndex = new KeywordSearchIndex("", new Version(2, 0), new List<IKeywordIndexItem> { keywordSearchIndexItem });
            searchIndex.AddKeywords(new IKeywordIndexItem[] { keywordSearchIndexItem }, @"test,testing,tested");

            searchIndex.RemoveKeywords(new IKeywordIndexItem[] { keywordSearchIndexItem }, new string[] { "test", "testing", "tested" });
            var items = searchIndex.Find("TEST");

            Assert.IsNotNull(items, "A single item should be found.");
            Assert.That(items.Length, Is.Zero);
        }

        [Test]
        public void KeywordSearchIndex_AfterAddingKeywordIndeces_ContainsIndeces()
        {
            var keywordSearchIndexItem = new TestKeywordIndexItem();
            var searchIndex = new KeywordSearchIndex("", new Version(2, 0), new List<IKeywordIndexItem> { keywordSearchIndexItem });
            searchIndex.AddKeywords(new IKeywordIndexItem[] { keywordSearchIndexItem }, @"test,testing,tested");

            Assert.That(searchIndex.Contains(keywordSearchIndexItem), Is.True);
            Assert.That(searchIndex.Contains(keywordSearchIndexItem.Id), Is.True);
        }

        [Test]
        public void KeywordSearchIndex_AfterAddingItems_AllReturnsIndexItemCount()
        {
            List<IKeywordIndexItem> indexItems = (new List<TestKeywordIndexItem> {
                new TestKeywordIndexItem("Title 1", "test,testing,tested"),
                new TestKeywordIndexItem("Title 2", "red,black"),
                new TestKeywordIndexItem("Title 3", "apple,pear")
            }).OfType<IKeywordIndexItem>().ToList();

            var searchIndex = new KeywordSearchIndex("", new Version(2, 0), indexItems);

            int numItemsInIndex = searchIndex.All().Length;

            Assert.That(numItemsInIndex, Is.EqualTo(3));
        }

        [Test]
        public void KeywordSearchIndex_AfterAddingItems_AllKeywordsReturnsUniqueKeywords()
        {
            List<IKeywordIndexItem> indexItems = (new List<TestKeywordIndexItem> {
                new TestKeywordIndexItem("Title 1", "test,testing,tested"),
                new TestKeywordIndexItem("Title 2", "test,testing"),
                new TestKeywordIndexItem("Title 3", "TESTING,TESTED")
            }).OfType<IKeywordIndexItem>().ToList();

            var searchIndex = new KeywordSearchIndex("", new Version(2, 0), indexItems);

            string[] allKeywords = searchIndex.AllKeywords(searchIndex.All());

            Assert.That(allKeywords.Length, Is.EqualTo(3));
        }
    }
}
