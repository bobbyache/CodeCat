using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CygSoft.CodeCat.ProjectConverter;
using System.Xml;
using System.Xml.Linq;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager;
using UnitTests.Stubs.DocumentManager;
using System.Threading;
using UnitTests.Helpers.DocumentManager;

namespace UnitTests.Tests.DocumentManagement
{
    [TestClass]
    public class DocumentIndex_Tests
    {
        private DocFileSimulator documentSimulator;

        [TestInitialize]
        public void InitializeTests()
        {
            documentSimulator = new DocFileSimulator();
        }

        [TestMethod]
        public void DocumentIndex_CreateDocument()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Id);
            multiDocFile.Create(documentSimulator.DocumentIndex.FilePath);

            Assert.AreEqual(documentSimulator.DocumentIndex.FileName, multiDocFile.FileName);
            Assert.AreEqual(documentSimulator.DocumentIndex.FilePath, multiDocFile.FilePath);
            Assert.AreEqual(documentSimulator.DocumentIndex.Folder, multiDocFile.Folder);
            Assert.AreEqual(0, multiDocFile.DocumentFiles.Length);
            Assert.AreEqual(documentSimulator.DocumentIndex.Id, multiDocFile.Id);
        }

        [TestMethod]
        public void DocumentIndex_OpenDocument()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Id);
            multiDocFile.Open(documentSimulator.DocumentIndex.FilePath);

            Assert.AreEqual(documentSimulator.DocumentIndex.FileName, multiDocFile.FileName);
            Assert.AreEqual(documentSimulator.DocumentIndex.FilePath, multiDocFile.FilePath);
            Assert.AreEqual(documentSimulator.DocumentIndex.Folder, multiDocFile.Folder);
            Assert.AreEqual(3, multiDocFile.DocumentFiles.Length);
            Assert.AreEqual(documentSimulator.DocumentIndex.Id, multiDocFile.Id);
        }


        [TestMethod]
        public void DocumentIndex_DeleteDocument()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Id);
            multiDocFile.Create(documentSimulator.DocumentIndex.FilePath);

            IDocumentFile file_1 = multiDocFile.AddDocumentFile(new StubDocumentFile(documentSimulator.DocumentFile1.Id, "Title"));
            IDocumentFile file_2 = multiDocFile.AddDocumentFile(new StubDocumentFile(documentSimulator.DocumentFile2.Id, "Title"));
            IDocumentFile file_3 = multiDocFile.AddDocumentFile(new StubDocumentFile(documentSimulator.DocumentFile3.Id, "Title"));

            Assert.AreEqual(3, multiDocFile.DocumentFiles.Length);

            multiDocFile.DeleteDocumentFile(file_1.Id);

            Assert.IsFalse(file_1.Loaded);
            Assert.AreEqual(-1, file_1.Ordinal);
            Assert.IsFalse(file_1.HasVersions);

        }


        [TestMethod]
        public void DocumentIndex_Delete()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Id);
            multiDocFile.Create(documentSimulator.DocumentIndex.FilePath);

            multiDocFile.AddDocumentFile(new StubDocumentFile(documentSimulator.DocumentFile1.Id, "Title"));
            multiDocFile.AddDocumentFile(new StubDocumentFile(documentSimulator.DocumentFile2.Id, "Title"));
            multiDocFile.AddDocumentFile(new StubDocumentFile(documentSimulator.DocumentFile3.Id, "Title"));

            Assert.AreEqual(3, multiDocFile.DocumentFiles.Length);

            multiDocFile.Delete();

            Assert.IsFalse(multiDocFile.Loaded);
            Assert.AreEqual(0, multiDocFile.DocumentFiles.Length);
        }

        [TestMethod]
        public void DocumentIndex_LoadWithDocuments()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Id);

            bool loadedOnInstantiate = multiDocFile.Loaded;
            multiDocFile.Open(documentSimulator.DocumentIndex.FilePath);
            bool loadedOnOpen = multiDocFile.Loaded;

            Assert.IsTrue(multiDocFile.DocumentFiles.Count() == 3);

            IDocumentFile documentFile_1 = multiDocFile.DocumentFiles[0];
            Assert.AreEqual(documentSimulator.DocumentFile1.Id, documentFile_1.Id);

            Assert.AreEqual(documentSimulator.DocumentFile1.FilePath, documentFile_1.FilePath);
            Assert.AreEqual(documentSimulator.DocumentFile1.FileName, documentFile_1.FileName);
            Assert.AreEqual(documentSimulator.DocumentIndex.Folder, documentFile_1.Folder);

            Assert.IsFalse(documentFile_1.HasVersions);
            Assert.AreEqual(0, documentFile_1.Versions.Length);
            Assert.IsTrue(documentFile_1.Loaded);

            Assert.IsFalse(loadedOnInstantiate);
            Assert.IsTrue(loadedOnOpen);
        }

        [TestMethod]
        public void DocumentIndex_LoadWithDocuments_Reposition()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Id);
            multiDocFile.Open(documentSimulator.DocumentIndex.FilePath);

            Assert.IsTrue(multiDocFile.DocumentFiles.Count() == 3);

            IDocumentFile documentFile_1 = multiDocFile.DocumentFiles[0];
            Assert.AreEqual(documentSimulator.DocumentFile1.Id, documentFile_1.Id);

            multiDocFile.MoveDown(documentFile_1);

            documentFile_1 = multiDocFile.DocumentFiles[1];
            Assert.AreEqual(documentSimulator.DocumentFile1.Id, documentFile_1.Id);
        }

        [TestMethod]
        public void DocumentIndex_LoadWithDocuments_AddDocuments()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Id);
            multiDocFile.Open(documentSimulator.DocumentIndex.FilePath);

            IDocumentFile documentFile_4 = new StubDocumentFile(documentSimulator.DocumentFile4.Id, "Title");
            multiDocFile.AddDocumentFile(documentFile_4);

            Assert.AreEqual(documentSimulator.DocumentFile4.Id, multiDocFile.DocumentFiles[multiDocFile.DocumentFiles.Length - 1].Id);
            multiDocFile.MoveUp(documentFile_4);
            Assert.AreEqual(documentSimulator.DocumentFile4.Id, multiDocFile.DocumentFiles[multiDocFile.DocumentFiles.Length - 2].Id);
        }


        [TestMethod]
        public void DocumentIndex_Create_And_AddDocuments()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Id);
            multiDocFile.Create(documentSimulator.DocumentIndex.FilePath);

            Assert.AreEqual(0, multiDocFile.DocumentFiles.Length);

            IDocumentFile documentFile_4 = new StubDocumentFile(documentSimulator.DocumentFile4.Id, "Title");
            multiDocFile.AddDocumentFile(documentFile_4);

            Assert.AreEqual(documentSimulator.DocumentFile4.Id, multiDocFile.DocumentFiles[multiDocFile.DocumentFiles.Length - 1].Id);
            multiDocFile.MoveUp(documentFile_4);
            Assert.AreEqual(documentSimulator.DocumentFile4.Id, multiDocFile.DocumentFiles[multiDocFile.DocumentFiles.Length - 1].Id);
            Assert.AreEqual(1, multiDocFile.DocumentFiles.Length);
        }

        [TestMethod]
        public void DocumentIndex_Positioning()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Id);
            multiDocFile.Open(documentSimulator.DocumentIndex.FilePath);

            // because multi-document files could have more than one type of document file, these document
            // files will have to be created elsewhere (think document factory).
            IDocumentFile documentFile_1 = multiDocFile.DocumentFiles[0];
            IDocumentFile documentFile_2 = multiDocFile.DocumentFiles[1];
            IDocumentFile documentFile_3 = multiDocFile.DocumentFiles[2];

            Assert.AreEqual(documentSimulator.DocumentFile1.Id, documentFile_1.Id);
            Assert.AreEqual(documentSimulator.DocumentFile2.Id, documentFile_2.Id);
            Assert.AreEqual(documentSimulator.DocumentFile3.Id, documentFile_3.Id);

            Assert.AreEqual(documentSimulator.DocumentFile1.FilePath, documentFile_1.FilePath);
            Assert.AreEqual(documentSimulator.DocumentFile2.FileName, documentFile_2.FileName);
            Assert.AreEqual(documentSimulator.DocumentIndex.Folder, documentFile_1.Folder);

            Assert.IsFalse(documentFile_1.HasVersions);
            Assert.AreEqual(0, documentFile_1.Versions.Length);
            Assert.IsTrue(documentFile_1.Loaded);

            IDocumentFile documentFile = multiDocFile.GetDocumentFile(documentSimulator.DocumentFile1.Id);

            Assert.AreEqual(1, documentFile.Ordinal);
            Assert.IsFalse(multiDocFile.CanMoveUp(documentFile));
            Assert.IsTrue(multiDocFile.CanMoveDown(documentFile));

            multiDocFile.MoveDown(documentFile);

            Assert.AreEqual(2, documentFile.Ordinal);
            Assert.IsTrue(multiDocFile.CanMoveUp(documentFile));
            Assert.IsTrue(multiDocFile.CanMoveDown(documentFile));

            Assert.AreEqual(documentSimulator.DocumentFile1.Id, multiDocFile.DocumentFiles[1].Id);
        }

        [TestMethod]
        public void MultiDocument_TestEvents()
        {
            bool beforeOpenEventFired = false;
            bool afterOpenEventFired = false;
            ManualResetEvent beforeOpenResetEvent = new ManualResetEvent(false);
            ManualResetEvent afterOpenResetEvent = new ManualResetEvent(false);

            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Id);

            multiDocFile.BeforeOpen += (s, e) =>
            {
                beforeOpenEventFired = true;
                beforeOpenResetEvent.Set();
            };
            multiDocFile.AfterOpen += (s, e) =>
            {
                afterOpenEventFired = true;
                beforeOpenResetEvent.Set();
            };

            beforeOpenResetEvent.WaitOne(50, false);
            afterOpenResetEvent.WaitOne(50, false);
            multiDocFile.Open(documentSimulator.DocumentIndex.FilePath);

            Assert.IsTrue(beforeOpenEventFired);
            Assert.IsTrue(afterOpenEventFired);
        }
    }
}
