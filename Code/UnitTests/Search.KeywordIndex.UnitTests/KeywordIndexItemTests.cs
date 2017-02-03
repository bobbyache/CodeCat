using CygSoft.CodeCat.Search.KeywordIndex;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using NUnit.Framework;
using Search.KeywordIndex.UnitTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Test]
        public void IndexItem_SetKeywords_ResetsKeywords()
        {

            IKeywordIndexItem keywordIndexItem = new TestKeywordIndexItem("4ecac722-8ec5-441c-8e3e-00b192b30453", "Title", DateTime.Now, DateTime.Now, "apple,pear,banana,orange");
            keywordIndexItem.SetKeywords("banana,orange");
            bool found = keywordIndexItem.AllKeywordsFound(new string[] { "BANANA", "Orange"});
            bool pearNotFound = keywordIndexItem.AllKeywordsFound(new string[] { "pear" });
            bool appleNotFound = keywordIndexItem.AllKeywordsFound(new string[] { "apple" });

            Assert.That(found, Is.True);
            Assert.That(pearNotFound, Is.False);
            Assert.That(appleNotFound, Is.False);
        }


        [Test]
        public void IndexItem_ValidateRemoveKeywords_IsInvalidIfAllKeywordsRemoved()
        {
            // better to change from ValidateRemoveKeywords() to IsSearchableAfterRemove()
            // perhaps add property "IsSearchable" or "HasKeywords" or both....
            IKeywordIndexItem keywordIndexItem = new TestKeywordIndexItem("4ecac722-8ec5-441c-8e3e-00b192b30453", "Title", DateTime.Now, DateTime.Now, "");
            keywordIndexItem.SetKeywords("banana,orange");
            bool willResultNonSearchhableIndexItem = !keywordIndexItem.ValidateRemoveKeywords(new string[] { "banana", "orange" });

            Assert.IsTrue(willResultNonSearchhableIndexItem);
        }

        [Test]
        public void IndexItem_ValidateRemoveKeywords_IsValidIfSomeKeywordsRemain()
        {
            // better to change from ValidateRemoveKeywords() to IsSearchableAfterRemove()
            // perhaps add property "IsSearchable" or "HasKeywords" or both....
            IKeywordIndexItem keywordIndexItem = new TestKeywordIndexItem("4ecac722-8ec5-441c-8e3e-00b192b30453", "Title", DateTime.Now, DateTime.Now, "");
            keywordIndexItem.SetKeywords("banana,orange");
            bool willResultNonSearchhableIndexItem = !keywordIndexItem.ValidateRemoveKeywords(new string[] { "banana" });
            Assert.IsFalse(willResultNonSearchhableIndexItem);
        }

        [Test]
        public void IndexItem_AfterAddingIndexItems_ContainsOnlyUniqueKeywords()
        {
            // better to change from ValidateRemoveKeywords() to IsSearchableAfterRemove()
            // perhaps add property "IsSearchable" or "HasKeywords" or both....
            IKeywordIndexItem keywordIndexItem = new TestKeywordIndexItem("4ecac722-8ec5-441c-8e3e-00b192b30453", "Title", DateTime.Now, DateTime.Now, "");
            keywordIndexItem.AddKeywords("banana,orange");
            keywordIndexItem.AddKeywords("banana,ORANGE");
            keywordIndexItem.AddKeywords("apple,BANANA");

            int keywordCount = keywordIndexItem.Keywords.Length;
            Assert.That(keywordCount, Is.EqualTo(3));
        }

    }
}
