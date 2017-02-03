using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CygSoft.CodeCat.Infrastructure;
using Moq;
using CygSoft.CodeCat.Search.KeywordIndex;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;

namespace UnitTests.Search.KeywordIndex
{
    [TestClass]
    public class KeywordSearchIndex_Tests
    {
        [TestMethod]
        public void KeywordSearchIndex_Create()
        {
            IKeywordSearchIndex searchIndex = new KeywordSearchIndex(@"C:\keywords\keyword_index.xml", 2);

            Assert.AreEqual(searchIndex.CurrentVersion, 2);
            Assert.AreEqual(searchIndex.FilePath, @"C:\keywords\keyword_index.xml");
            Assert.AreEqual(searchIndex.FileTitle, "keyword_index.xml");

            //searchIndex.Update(
        }

        [TestMethod]
        public void KeywordSearchIndex_AddItems()
        {
            IKeywordSearchIndex searchIndex = new KeywordSearchIndex(@"C:\keywords\keyword_index.xml", 2);
            
            //KeywordIndexItem keywordIndexItem = new 

            //searchIndex.AddKeywords(
        }
    }
}
