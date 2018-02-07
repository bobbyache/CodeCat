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
        [DeploymentItem(@"Files\TestFile.txt")]
        public void Test_That_Tests_Can_Read_TestFile()
        {
            string text = System.IO.File.ReadAllText("TestFile.txt");
            Assert.AreEqual("Test File", text);
        }

        [TestMethod]
        [DeploymentItem(@"Files\TestFile.txt")]
        [ExpectedException(typeof(System.ObjectDisposedException))]
        public void Test_That_Disposing_TextFile_Disposes_Underlying_FileStream()
        {
            FileStream fileStream;
            using (TestDisposableTextFile textFile = new TestDisposableTextFile("TestFile.txt"))
            {
                textFile.Open();
                fileStream = textFile.UnderlyingStream;
            }
            SafeFileHandle ptr = fileStream.SafeFileHandle;
        }

        [TestMethod]
        [DeploymentItem(@"Files\TestFile.txt")]
        public void Read_Text_From_TextFile_Ensure_FileOpen_After_Read()
        {
            TextFile textFile = new TextFile("TestFile.txt");
            textFile.Open();
            string text = textFile.Text;

            Assert.IsTrue(textFile.IsOpen);
            Assert.AreEqual("Test File", text);
        }

        [TestMethod]
        public void TextFile_When_Created_With_Path_SuccessfullyStores_Path()
        {
            StubBaseFile textFile = new StubBaseFile(@"C:\path\file.txt");
            Assert.AreEqual(@"C:\path\file.txt", textFile.Path, "File Path has not been set via the constructor.");
        }

        [TestMethod]
        [DeploymentItem(@"Files\TestFile.txt")]
        public void TextFile_When_Disposed_Raises_Closed_Event()
        {
            bool called = false;

            using (TestDisposableTextFile textFile = new TestDisposableTextFile("TestFile.txt"))
            {
                textFile.Open();
                textFile.Closed += (s, e) => called = true;
                textFile.Close();
            }
            Assert.IsTrue(called);
        }

        [TestMethod]
        public void TextFile_When_Deleting_Closes_OpenFile()
        {
            bool called = false;

            StubBaseFile textFile = new StubBaseFile(@"C:\path\file.txt");
            textFile.Closed += (s, e) => called = true;

            textFile.Delete();

            Assert.IsTrue(called);
        }

        [TestMethod]
        public void TextFile_When_Opened_Is_Open()
        {
            StubBaseFile textFile = new StubBaseFile(@"C:\path\file.txt");
            textFile.Open();
            Assert.IsTrue(textFile.IsOpen);
        }

        [TestMethod]
        public void TextFile_When_Created_Is_Not_Open()
        {
            StubBaseFile textFile = new StubBaseFile(@"C:\path\file.txt");
            Assert.IsFalse(textFile.IsOpen);
        }

        [TestMethod]
        public void TextFile_When_Deleted_Is_Not_Open()
        {
            StubBaseFile textFile = new StubBaseFile(@"C:\path\file.txt");
            textFile.Open();
            textFile.Delete();
            Assert.IsFalse(textFile.IsOpen);
        }
    }

    internal class StubBaseFile : BaseFile
    {
        private bool isOpen = false;
        public StubBaseFile(string filePath) : base(filePath) { }

        public override bool IsOpen
        {
            get
            {
                return isOpen;
            }
        }

        protected override void OnDelete()
        {
            isOpen = false;
        }

        protected override void OnClose()
        {
            isOpen = false;
        }

        protected override void OnOpen()
        {
            isOpen = true;
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
