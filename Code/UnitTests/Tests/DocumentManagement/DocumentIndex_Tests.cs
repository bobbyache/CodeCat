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
            IDocumentIndex multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentIndex.Id);

            Assert.AreEqual(0, multiDocFile.DocumentFiles.Length);
            Assert.AreEqual(documentSimulator.DocumentIndex.Id, multiDocFile.Id);
        }

        [TestMethod]
        public void DocumentIndex_OpenDocument()
        {
            IDocumentIndex multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentIndex.Id);
            multiDocFile.Open();

            Assert.AreEqual(3, multiDocFile.DocumentFiles.Length);
            Assert.AreEqual(documentSimulator.DocumentIndex.Id, multiDocFile.Id);
        }


        [TestMethod]
        public void DocumentIndex_DeleteDocument()
        {
            IDocumentIndex multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentIndex.Id);

            IDocument file_1 = multiDocFile.AddDocumentFile(new StubDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentFile1.Id, "Title"));
            IDocument file_2 = multiDocFile.AddDocumentFile(new StubDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentFile2.Id, "Title"));
            IDocument file_3 = multiDocFile.AddDocumentFile(new StubDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentFile3.Id, "Title"));

            Assert.AreEqual(3, multiDocFile.DocumentFiles.Length);

            multiDocFile.RemoveDocumentFile(file_1.Id);

            Assert.AreEqual(-1, file_1.Ordinal);
            Assert.IsFalse(file_1.HasVersions);

        }


        [TestMethod]
        public void DocumentIndex_Delete()
        {
            IDocumentIndex multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentIndex.Id);

            multiDocFile.AddDocumentFile(new StubDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentFile1.Id, "Title"));
            multiDocFile.AddDocumentFile(new StubDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentFile1.Id, "Title"));
            multiDocFile.AddDocumentFile(new StubDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentFile1.Id, "Title"));

            Assert.AreEqual(3, multiDocFile.DocumentFiles.Length);

            multiDocFile.Delete();

            Assert.IsFalse(multiDocFile.Loaded);
            Assert.AreEqual(0, multiDocFile.DocumentFiles.Length);
        }

        [TestMethod]
        public void DocumentIndex_LoadWithDocuments()
        {
            IDocumentIndex multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentIndex.Id);

            bool loadedOnInstantiate = multiDocFile.Loaded;
            multiDocFile.Open();
            //multiDocFile.Open(documentSimulator.DocumentIndex.FilePath);
            bool loadedOnOpen = multiDocFile.Loaded;

            Assert.IsTrue(multiDocFile.DocumentFiles.Count() == 3);

            //IDocument documentFile_1 = multiDocFile.DocumentFiles[0];
            //Assert.AreEqual(documentSimulator.DocumentFile1.Id, documentFile_1.Id);

            //Assert.AreEqual(documentSimulator.DocumentFile1.FilePath, documentFile_1.FilePath);
            //Assert.AreEqual(documentSimulator.DocumentFile1.FileName, documentFile_1.FileName);
            //Assert.AreEqual(documentSimulator.DocumentIndex.Folder, documentFile_1.Folder);

            //Assert.IsFalse(documentFile_1.HasVersions);
            //Assert.AreEqual(0, documentFile_1.Versions.Length);
            //Assert.IsTrue(documentFile_1.Loaded);

            Assert.IsFalse(loadedOnInstantiate);
            Assert.IsTrue(loadedOnOpen);
        }

        [TestMethod]
        public void DocumentIndex_LoadWithDocuments_Reposition()
        {
            IDocumentIndex multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentIndex.Id);
            multiDocFile.Open();
            //multiDocFile.Open(documentSimulator.DocumentIndex.FilePath);

            Assert.IsTrue(multiDocFile.DocumentFiles.Count() == 3);

            IDocument documentFile = multiDocFile.DocumentFiles[0];

            multiDocFile.MoveDown(documentFile);

            Assert.AreEqual(multiDocFile.DocumentFiles[1].Id, documentFile.Id);
        }

        [TestMethod]
        public void DocumentIndex_LoadWithDocuments_AddDocuments()
        {
            IDocumentIndex multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentIndex.Id);
            multiDocFile.Open();
            //multiDocFile.Open(documentSimulator.DocumentIndex.FilePath);

            IDocument documentFile = new StubDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentFile1.Id, "Title");
            multiDocFile.AddDocumentFile(documentFile);

            Assert.AreEqual(documentFile.Id, multiDocFile.DocumentFiles[multiDocFile.DocumentFiles.Length - 1].Id);
            multiDocFile.MoveUp(documentFile);
            Assert.AreEqual(documentFile.Id, multiDocFile.DocumentFiles[multiDocFile.DocumentFiles.Length - 2].Id);
        }


        [TestMethod]
        public void DocumentIndex_Create_And_AddDocuments()
        {
            IDocumentIndex multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentIndex.Id);

            Assert.AreEqual(0, multiDocFile.DocumentFiles.Length);

            IDocument documentFile_4 = new StubDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentFile1.Id, "Title");
            multiDocFile.AddDocumentFile(documentFile_4);

            Assert.AreEqual(documentSimulator.DocumentFile1.Id, multiDocFile.DocumentFiles[multiDocFile.DocumentFiles.Length - 1].Id);
            multiDocFile.MoveUp(documentFile_4);
            Assert.AreEqual(documentSimulator.DocumentFile1.Id, multiDocFile.DocumentFiles[multiDocFile.DocumentFiles.Length - 1].Id);
            Assert.AreEqual(1, multiDocFile.DocumentFiles.Length);
        }

        [TestMethod]
        public void DocumentIndex_Positioning()
        {
            IDocumentIndex multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentIndex.Id);
            multiDocFile.Open();
            //multiDocFile.Open(documentSimulator.DocumentIndex.FilePath);

            // because multi-document files could have more than one type of document file, these document
            // files will have to be created elsewhere (think document factory).
            IDocument documentFile_1 = multiDocFile.DocumentFiles[0];
            IDocument documentFile_2 = multiDocFile.DocumentFiles[1];
            IDocument documentFile_3 = multiDocFile.DocumentFiles[2];

            Assert.IsFalse(documentFile_1.HasVersions);
            Assert.AreEqual(0, documentFile_1.Versions.Length);
            Assert.IsTrue(documentFile_1.Loaded);

            IDocument documentFile = multiDocFile.GetDocumentFile(documentFile_1.Id);

            Assert.AreEqual(1, documentFile.Ordinal);
            Assert.IsFalse(multiDocFile.CanMoveUp(documentFile));
            Assert.IsTrue(multiDocFile.CanMoveDown(documentFile));

            multiDocFile.MoveDown(documentFile);

            Assert.AreEqual(2, documentFile.Ordinal);
            Assert.IsTrue(multiDocFile.CanMoveUp(documentFile));
            Assert.IsTrue(multiDocFile.CanMoveDown(documentFile));

            Assert.AreEqual(documentFile.Id, multiDocFile.DocumentFiles[1].Id);
        }

        [TestMethod]
        public void MultiDocument_TestEvents()
        {
            bool beforeOpenEventFired = false;
            bool afterOpenEventFired = false;
            ManualResetEvent beforeOpenResetEvent = new ManualResetEvent(false);
            ManualResetEvent afterOpenResetEvent = new ManualResetEvent(false);

            IDocumentIndex multiDocFile = new StubMultiDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentIndex.Id);

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
            //multiDocFile.Open(documentSimulator.DocumentIndex.FilePath);
            multiDocFile.Open();

            Assert.IsTrue(beforeOpenEventFired);
            Assert.IsTrue(afterOpenEventFired);
        }
    }
}
