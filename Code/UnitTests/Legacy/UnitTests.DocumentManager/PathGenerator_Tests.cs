using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CygSoft.CodeCat.DocumentManager.PathGenerators;

namespace UnitTests.DocumentManager
{
    [TestClass]
    public class PathGenerator_Tests
    {
        [TestMethod]
        public void VersionPathGenerator_Test()
        {
            DateTime dateTime = DateTime.Now;
            string sourceFilePath = @"C:\directory\project\item\d33b59bd-54af-4f0b-967f-64084847b678.txt";
            VersionPathGenerator pathGenerator = new VersionPathGenerator(sourceFilePath, dateTime);

            Assert.IsNotNull(pathGenerator.FileExtension);
            Assert.IsNotNull(pathGenerator.FileName);
            Assert.IsNotNull(pathGenerator.FilePath);
            Assert.IsNotNull(pathGenerator.Id);
            Assert.IsNotNull(pathGenerator.TimeStamp);
            //Assert.IsNotNull(pathGenerator.SourceFileName);
            //Assert.IsNotNull(pathGenerator.SourceId);
            //Assert.IsNotNull(pathGenerator.SourceFilePath);
        }
    }
}
