using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Domain.Code;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests
{
    [TestFixture]
    public class CodeFileTests
    {
        [Test]
        public void CodeFile_Loaded_With_FolderPath_Returns_Expected_FilePath()
        {
            Mock<ICodeKeywordIndexItem> indexItem = new Mock<ICodeKeywordIndexItem>();
            DocumentPathGenerator pathGenerator = new DocumentPathGenerator(@"C:\Users\robb\Documents\CodeCat\InfoVest\code", "xml", 
                "c489c7e9-96d9-4eab-b2e1-7ce0ec175fee");

            CodeFile codeFile = new CodeFile(pathGenerator, indexItem.Object);

            Assert.AreEqual(@"C:\Users\robb\Documents\CodeCat\InfoVest\code\c489c7e9-96d9-4eab-b2e1-7ce0ec175fee.xml", codeFile.FilePath);
        }

        [Test]
        public void CodeFile_Save_Calls_OnSave()
        {
            Mock<ICodeKeywordIndexItem> indexItem = new Mock<ICodeKeywordIndexItem>();
            DocumentPathGenerator pathGenerator = new DocumentPathGenerator(@"C:\Users\robb\Documents\CodeCat\InfoVest\code", "xml",
                "c489c7e9-96d9-4eab-b2e1-7ce0ec175fee");

            TestCodeFile codeFile = new TestCodeFile(pathGenerator, indexItem.Object);
            codeFile.Save();

            Assert.IsTrue(codeFile.OnSaveCalled);
        }

        [Test]
        public void CodeFile_Open_Calls_OnOpen()
        {
            Mock<ICodeKeywordIndexItem> indexItem = new Mock<ICodeKeywordIndexItem>();
            DocumentPathGenerator pathGenerator = new DocumentPathGenerator(@"C:\Users\robb\Documents\CodeCat\InfoVest\code", "xml",
                "c489c7e9-96d9-4eab-b2e1-7ce0ec175fee");

            TestCodeFile codeFile = new TestCodeFile(pathGenerator, indexItem.Object);
            codeFile.Open();

            Assert.IsTrue(codeFile.OnOpenCalled);
        }

        [Test]
        public void CodeFile_Revert_Calls_OnRevert()
        {
            Mock<ICodeKeywordIndexItem> indexItem = new Mock<ICodeKeywordIndexItem>();
            DocumentPathGenerator pathGenerator = new DocumentPathGenerator(@"C:\Users\robb\Documents\CodeCat\InfoVest\code", "xml",
                "c489c7e9-96d9-4eab-b2e1-7ce0ec175fee");

            TestCodeFile codeFile = new TestCodeFile(pathGenerator, indexItem.Object);
            codeFile.Revert();

            Assert.IsTrue(codeFile.OnRevertCalled);
        }

        private class TestCodeFile : CodeFile
        {
            public bool OnSaveCalled = false;
            public bool OnOpenCalled = false;
            public bool OnRevertCalled = false;

            public TestCodeFile(BaseFilePathGenerator filePathGenerator, ICodeKeywordIndexItem indexItem) 
                : base(filePathGenerator, indexItem)
            {
            }

            protected override void OnSave()
            {
                OnSaveCalled = true;
            }

            protected override void OnOpen()
            {
                OnOpenCalled = true;
            }

            protected override void OnRevert()
            {
                OnRevertCalled = true;
            }
        }
    }
}
