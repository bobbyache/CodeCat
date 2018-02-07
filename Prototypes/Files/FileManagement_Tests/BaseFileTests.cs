using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CygSoft.CodeCat.FileManagement;

namespace FileManagement_Tests
{
    [TestClass]
    public class BaseFileTests
    {
        [TestMethod]
        [TestCategory("FileHandling")]
        public void BaseFile_When_Created_With_Path_SuccessfullyStores_Path()
        {
            StubBaseFile textFile = new StubBaseFile(@"C:\path\file.txt");
            Assert.AreEqual(@"C:\path\file.txt", textFile.Path, "File Path has not been set via the constructor.");
        }

        [TestMethod]
        [TestCategory("FileHandling")]
        public void BaseFile_When_Created_Is_Not_Open()
        {
            StubBaseFile textFile = new StubBaseFile(@"C:\path\file.txt");
            Assert.IsFalse(textFile.IsOpen);
        }

        [TestMethod]
        [TestCategory("FileHandling")]
        public void BaseFile_When_Deleting_Fires_Closed_Event()
        {
            bool called = false;

            StubBaseFile textFile = new StubBaseFile(@"C:\path\file.txt");
            textFile.Closed += (s, e) => called = true;
            textFile.Open();
            textFile.Delete();

            Assert.IsTrue(called);
        }

        [TestMethod]
        [TestCategory("FileHandling")]
        public void TextFile_When_Deleted_Is_Not_Open()
        {
            StubBaseFile textFile = new StubBaseFile(@"C:\path\file.txt");
            textFile.Open();
            textFile.Delete();
            Assert.IsFalse(textFile.IsOpen);
        }

        [TestMethod]
        [TestCategory("FileHandling")]
        public void TextFile_When_Opened_IsOpen()
        {
            StubBaseFile textFile = new StubBaseFile(@"C:\path\file.txt");
            textFile.Open();
            Assert.IsTrue(textFile.IsOpen);
        }

    }

    internal class StubBaseFile : BaseFile
    {
        public StubBaseFile(string filePath) : base(filePath) { }

        protected override void OnClose()
        {
        }

        protected override void OnDelete()
        {
        }

        protected override void OnOpen()
        {
        }
    }

}
