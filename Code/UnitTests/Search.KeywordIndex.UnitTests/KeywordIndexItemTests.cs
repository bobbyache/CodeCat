using CygSoft.CodeCat.Search.KeywordIndex;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
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
    class KeywordIndexItemTests
    {
        [Test]
        public void IndexItem_OnInitializedWithoutKeywords_ReturnsFalseForAllSearches()
        {
            IKeywordIndexItem keywordIndexItem = new TestKeywordIndexItem("4ecac722-8ec5-441c-8e3e-00b192b30453", "Title", DateTime.Now, DateTime.Now, "");

            bool foundSingle = keywordIndexItem.AllKeywordsFound(new string[] { "single" });
            bool emptyStringFound = keywordIndexItem.AllKeywordsFound(new string[] { "" });

            Assert.That(foundSingle, Is.False);
            Assert.That(emptyStringFound, Is.False);
        }

        [Test]
        public void IndexItem_OnInitializedWithKeywords_ReturnsFalseIfNotFound()
        {
            IKeywordIndexItem keywordIndexItem = new TestKeywordIndexItem("4ecac722-8ec5-441c-8e3e-00b192b30453", "Title", DateTime.Now, DateTime.Now, "test,tested");

            bool foundSingle = keywordIndexItem.AllKeywordsFound(new string[] { "testing" });
            bool emptyStringFound = keywordIndexItem.AllKeywordsFound(new string[] { "tester", "testing" });

            Assert.That(foundSingle, Is.False);
            Assert.That(emptyStringFound, Is.False);
        }

        [Test]
        public void IndexItem_OnInitializedWithKeywords_ReturnsTrueIfFound()
        {
            IKeywordIndexItem keywordIndexItem = new TestKeywordIndexItem("4ecac722-8ec5-441c-8e3e-00b192b30453", "Title", DateTime.Now, DateTime.Now, "test,tested");

            bool foundSingle = keywordIndexItem.AllKeywordsFound(new string[] { "test" });
            bool foundAll = keywordIndexItem.AllKeywordsFound(new string[] { "test", "tested" });

            Assert.That(foundSingle, Is.True);
            Assert.That(foundAll, Is.True);
        }

        [Test]
        public void IndexItem_OnInitializedWithKeywords_ReturnsFalseIfSomeFound()
        {
            IKeywordIndexItem keywordIndexItem = new TestKeywordIndexItem("4ecac722-8ec5-441c-8e3e-00b192b30453", "Title", DateTime.Now, DateTime.Now, "test,tested");
            bool found = keywordIndexItem.AllKeywordsFound(new string[] { "test", "testing" });
            Assert.That(found, Is.False);
        }

        [Test]
        public void IndexItem_AllKeywordsFound_ReturnsTrueIfExistRegardlessOfCase()
        {
            IKeywordIndexItem keywordIndexItem = new TestKeywordIndexItem("4ecac722-8ec5-441c-8e3e-00b192b30453", "Title", DateTime.Now, DateTime.Now, "test,tested");
            bool found = keywordIndexItem.AllKeywordsFound(new string[] { "TEST", "tested" });
            Assert.That(found, Is.True);
        }

        [Test]
        public void IndexItem_AddKeywordsAndSubsequentSearch_ReturnsTrueIfExistRegardlessOfCase()
        {
            
            IKeywordIndexItem keywordIndexItem = new TestKeywordIndexItem("4ecac722-8ec5-441c-8e3e-00b192b30453", "Title", DateTime.Now, DateTime.Now, "apple,pear");
            keywordIndexItem.AddKeywords("banana,orange");
            bool found = keywordIndexItem.AllKeywordsFound(new string[] { "apple", "BANANA", "Orange", "pear" });
            Assert.That(found, Is.True);
        }

        [Test]
        public void IndexItem_RemoveKeywordsAndSubsequentSearch_ReturnsFalseIfSomeDoNotExist()
        {

            IKeywordIndexItem keywordIndexItem = new TestKeywordIndexItem("4ecac722-8ec5-441c-8e3e-00b192b30453", "Title", DateTime.Now, DateTime.Now, "apple,pear,banana,orange");
            keywordIndexItem.RemoveKeywords(new string[] { "banana", "orange" });
            bool found = keywordIndexItem.AllKeywordsFound(new string[] { "apple", "BANANA", "Orange", "pear" });
            Assert.That(found, Is.False);
        }

        public class TestKeywordIndexItem : KeywordIndexItem
        {
            public TestKeywordIndexItem() : base()
            {
            }

            public TestKeywordIndexItem(string title, string commaDelimitedKeywords) : base(title, commaDelimitedKeywords)
            {

            }

            public TestKeywordIndexItem(string id, string title, DateTime dateCreated, DateTime dateModified, string commaDelimitedKeywords) : base(id, title, dateCreated, dateModified, commaDelimitedKeywords)
            {

            }
        }
    }
}
