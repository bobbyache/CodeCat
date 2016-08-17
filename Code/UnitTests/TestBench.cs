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

namespace UnitTestFile
{
    [TestClass]
    public class TestBench
    {
        [TestMethod]
        public void DocManager_CreateDocument()
        {
            IDocManager docManager = new StubDocManager();
            docManager.Create(@"H:\ParentFolder\", "d33b59bd-54af-4f0b-967f-64084847b678");

            Assert.AreEqual("document_index.xml", docManager.IndexFileTitle);
            Assert.AreEqual(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\document_index.xml", docManager.IndexFilePath);
            Assert.AreEqual(@"H:\ParentFolder\", docManager.ParentFolder);
            Assert.AreEqual(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678", docManager.Folder);
            Assert.AreEqual(0, docManager.DocumentFiles.Length);
            Assert.AreEqual("d33b59bd-54af-4f0b-967f-64084847b678", docManager.Id);
        }

        [TestMethod]
        public void DocManager_OpenDocument()
        {
            IDocManager docManager = new StubDocManager();
            docManager.Open(@"H:\ParentFolder\", "d33b59bd-54af-4f0b-967f-64084847b678");

            Assert.AreEqual("document_index.xml", docManager.IndexFileTitle);
            Assert.AreEqual(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\document_index.xml", docManager.IndexFilePath);
            Assert.AreEqual(@"H:\ParentFolder\", docManager.ParentFolder);
            Assert.AreEqual(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678", docManager.Folder);
            Assert.AreEqual(0, docManager.DocumentFiles.Length);
            Assert.AreEqual("d33b59bd-54af-4f0b-967f-64084847b678", docManager.Id);
        }

        [TestMethod]
        public void DocumentFile_Create()
        {
            IDocumentFile documentFile = new StubDocumentFile(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\11334214-ca43-406b-9cae-f986c3c63332.txt");

            Assert.AreEqual(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678", documentFile.Folder);
            Assert.AreEqual(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\11334214-ca43-406b-9cae-f986c3c63332.txt", documentFile.FilePath);
            Assert.AreEqual("11334214-ca43-406b-9cae-f986c3c63332.txt", documentFile.FileName);
            Assert.AreEqual("d33b59bd-54af-4f0b-967f-64084847b678.txt", documentFile.Id);
        }

        [TestMethod]
        public void DocManager_Positioning()
        {
            IDocManager docManager = new StubDocManager();

            bool loadedOnInstantiate = docManager.Loaded;

            docManager.Open(@"H:\ParentFolder\", "d33b59bd-54af-4f0b-967f-64084847b678");
            bool loadedOnOpen = docManager.Loaded;

            Assert.IsFalse(loadedOnInstantiate);
            Assert.IsTrue(loadedOnOpen);

            docManager.CreateDocumentFile(new StubDocumentFile(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\11334214-ca43-406b-9cae-f986c3c63332.txt"));
            docManager.CreateDocumentFile(new StubDocumentFile(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\f562810b-a1f7-4cf8-b370-dbaf87ff8759.txt"));
            docManager.CreateDocumentFile(new StubDocumentFile(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\37334412-5735-4c34-b88c-0be7dcb742a9.txt"));

            Assert.AreEqual("11334214-ca43-406b-9cae-f986c3c63332.txt", docManager.DocumentFiles[0].Id);
            Assert.AreEqual("f562810b-a1f7-4cf8-b370-dbaf87ff8759.txt", docManager.DocumentFiles[1].Id);
            Assert.AreEqual("37334412-5735-4c34-b88c-0be7dcb742a9.txt", docManager.DocumentFiles[2].Id);

            IPositionableDocManager posDocManager = docManager as IPositionableDocManager;
            IDocumentFile documentFile = docManager.GetDocumentFile("11334214-ca43-406b-9cae-f986c3c63332.txt");

            Assert.AreEqual(1, documentFile.Ordinal);
            Assert.IsFalse(posDocManager.CanMoveUp(documentFile));
            Assert.IsTrue(posDocManager.CanMoveDown(documentFile));

            posDocManager.MoveDown(documentFile);

            Assert.AreEqual(2, documentFile.Ordinal);
            Assert.IsTrue(posDocManager.CanMoveUp(documentFile));
            Assert.IsTrue(posDocManager.CanMoveDown(documentFile));

            Assert.AreEqual("11334214-ca43-406b-9cae-f986c3c63332.txt", docManager.DocumentFiles[1].Id);
        }
    }
}
