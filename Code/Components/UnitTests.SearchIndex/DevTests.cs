using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex;
using Moq;

namespace UnitTests.SearchIndex
{
    [TestClass]
    public class DevTests
    {
        [TestMethod]
        public void KeywordSearchIndex_Create()
        {
            IKeywordSearchIndex searchIndex = new KeywordSearchIndex(@"C:\keywords\keyword_index.xml", 2);

            Assert.AreEqual(searchIndex.CurrentVersion, 2);
            Assert.AreEqual(searchIndex.FilePath, @"C:\keywords\keyword_index.xml");
            Assert.AreEqual(searchIndex.FileTitle, "keyword_index.xml");
        }

        [TestMethod]
        public void KeywordSearchIndex_AddItems()
        {
            //IKeywordSearchIndex searchIndex = new KeywordSearchIndex(@"C:\keywords\keyword_index.xml", 2);
            //searchIndex.AddKeywords(
        }
    }
}
