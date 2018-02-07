using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Microsoft.Win32.SafeHandles;
using CygSoft.CodeCat.FileManagement;

namespace FileManagement_Tests
{
    [TestClass]
    public class TextFileTests
    {
        [TestMethod]
        [TestCategory("FileHandling")]
        [DeploymentItem(@"Files\TestFile_01.txt")]
        public void Test_That_Tests_Can_Read_TestFile()
        {
            string text = System.IO.File.ReadAllText("TestFile_01.txt");
            Assert.AreEqual("Test File", text);
        }

        [TestMethod]
        [TestCategory("FileHandling")]
        [DeploymentItem(@"Files\TestFile_02.txt")]
        [ExpectedException(typeof(System.ObjectDisposedException))]
        public void Test_That_Disposing_TextFile_Disposes_Underlying_FileStream()
        {
            FileStream fileStream;

            using (TestDisposableTextFile textFile = new TestDisposableTextFile("TestFile_02.txt"))
            {
                textFile.Open();
                fileStream = textFile.UnderlyingStream;
            }
            SafeFileHandle ptr = fileStream.SafeFileHandle;
            
        }

        [TestMethod]
        [TestCategory("FileHandling")]
        [DeploymentItem(@"Files\TestFile_03.txt")]
        public void Read_Text_From_TextFile_Ensure_FileOpen_After_Read()
        {
            TextFile textFile = new TextFile("TestFile_03.txt");
            textFile.Open();
            string text = textFile.Text;

            Assert.IsTrue(textFile.IsOpen);
            Assert.AreEqual("Test File", text);
        }

        [TestMethod]
        [TestCategory("FileHandling")]
        [DeploymentItem(@"Files\TestFile_04.txt")]
        public void TextFile_When_Disposed_Raises_Closed_Event()
        {
            bool called = false;

            using (TestDisposableTextFile textFile = new TestDisposableTextFile("TestFile_04.txt"))
            {
                textFile.Closed += (s, e) => called = true;
                textFile.Open();
                textFile.Close();
            }
            Assert.IsTrue(called);
        }

        [TestMethod]
        [TestCategory("FileHandling")]
        [DeploymentItem(@"Files\TestFile_05.txt")]
        public void TextFile_When_Closed_Has_State_Of_IsOpen_False()
        {
            using (TestDisposableTextFile textFile = new TestDisposableTextFile("TestFile_05.txt"))
            {
                textFile.Open();
                textFile.Close();
                Assert.IsFalse(textFile.IsOpen);
            }
        }
    }



    internal class TestDisposableTextFile : TextFile
    {
        public FileStream UnderlyingStream
        {
            get { return base.fileStream; }
        }

        public TestDisposableTextFile(string filePath) : base(filePath)
        {

        }
    }




}
