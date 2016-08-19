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
        public void MultiDocumentFile_CreateDocument()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile("d33b59bd-54af-4f0b-967f-64084847b678");
            multiDocFile.Create(Path.Combine(@"H:\ParentFolder\", "d33b59bd-54af-4f0b-967f-64084847b678", "document_index.xml"));

            Assert.AreEqual("document_index.xml", multiDocFile.FileName);
            Assert.AreEqual(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\document_index.xml", multiDocFile.FilePath);
            Assert.AreEqual(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678", multiDocFile.Folder);
            Assert.AreEqual(0, multiDocFile.DocumentFiles.Length);
            Assert.AreEqual("d33b59bd-54af-4f0b-967f-64084847b678", multiDocFile.Id);
        }

        [TestMethod]
        public void MultiDocumentFile_OpenDocument()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile("d33b59bd-54af-4f0b-967f-64084847b678");
            multiDocFile.Open(Path.Combine(@"H:\ParentFolder\", "d33b59bd-54af-4f0b-967f-64084847b678", "document_index.xml"));

            Assert.AreEqual("document_index.xml", multiDocFile.FileName);
            Assert.AreEqual(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\document_index.xml", multiDocFile.FilePath);
            Assert.AreEqual(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678", multiDocFile.Folder);
            Assert.AreEqual(0, multiDocFile.DocumentFiles.Length);
            Assert.AreEqual("d33b59bd-54af-4f0b-967f-64084847b678", multiDocFile.Id);
        }

        [TestMethod]
        public void MultiDocumentFile_Positioning()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile("d33b59bd-54af-4f0b-967f-64084847b678");
            

            bool loadedOnInstantiate = multiDocFile.Loaded;
            multiDocFile.Open(Path.Combine(@"H:\ParentFolder\", "d33b59bd-54af-4f0b-967f-64084847b678", "document_index.xml"));
            bool loadedOnOpen = multiDocFile.Loaded;

            Assert.IsFalse(loadedOnInstantiate);
            Assert.IsTrue(loadedOnOpen);

            // because multi-document files could have more than one type of document file, these document
            // files will have to be created elsewhere (think document factory).
            IDocumentFile documentFile_1 = new StubDocumentFile("11334214-ca43-406b-9cae-f986c3c63332");
            IDocumentFile documentFile_2 = new StubDocumentFile("f562810b-a1f7-4cf8-b370-dbaf87ff8759");
            IDocumentFile documentFile_3 = new StubDocumentFile("37334412-5735-4c34-b88c-0be7dcb742a9");

            multiDocFile.AddDocumentFile(documentFile_1);
            multiDocFile.AddDocumentFile(documentFile_2);
            multiDocFile.AddDocumentFile(documentFile_3);

            Assert.AreEqual("11334214-ca43-406b-9cae-f986c3c63332", multiDocFile.DocumentFiles[0].Id);
            Assert.AreEqual("f562810b-a1f7-4cf8-b370-dbaf87ff8759", multiDocFile.DocumentFiles[1].Id);
            Assert.AreEqual("37334412-5735-4c34-b88c-0be7dcb742a9", multiDocFile.DocumentFiles[2].Id);

            Assert.AreEqual(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\11334214-ca43-406b-9cae-f986c3c63332.txt", documentFile_1.FilePath);
            Assert.AreEqual("11334214-ca43-406b-9cae-f986c3c63332.txt", documentFile_1.FileName);
            Assert.AreEqual(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678", documentFile_1.Folder);
            Assert.AreEqual(null, documentFile_1.Content);
            Assert.IsFalse(documentFile_1.HasVersions);
            Assert.AreEqual(0, documentFile_1.Versions.Length);
            Assert.IsTrue(documentFile_1.Loaded);

            IDocumentFile documentFile = multiDocFile.GetDocumentFile("11334214-ca43-406b-9cae-f986c3c63332");

            Assert.AreEqual(1, documentFile.Ordinal);
            Assert.IsFalse(multiDocFile.CanMoveUp(documentFile));
            Assert.IsTrue(multiDocFile.CanMoveDown(documentFile));

            multiDocFile.MoveDown(documentFile);

            Assert.AreEqual(2, documentFile.Ordinal);
            Assert.IsTrue(multiDocFile.CanMoveUp(documentFile));
            Assert.IsTrue(multiDocFile.CanMoveDown(documentFile));

            Assert.AreEqual("11334214-ca43-406b-9cae-f986c3c63332", multiDocFile.DocumentFiles[1].Id);
        }

        [TestMethod]
        public void DocumentFile_Create()
        {
            IDocumentFile documentFile = new StubDocumentFile("11334214-ca43-406b-9cae-f986c3c63332");
            documentFile.Create(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\11334214-ca43-406b-9cae-f986c3c63332.txt");

            Assert.AreEqual(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678", documentFile.Folder);
            Assert.AreEqual(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\11334214-ca43-406b-9cae-f986c3c63332.txt", documentFile.FilePath);
            Assert.AreEqual("11334214-ca43-406b-9cae-f986c3c63332.txt", documentFile.FileName);
            Assert.AreEqual("11334214-ca43-406b-9cae-f986c3c63332", documentFile.Id);
        }

        [TestMethod]
        public void DocumentFile_Snapshots()
        {
            IDocumentFile documentFile = new StubDocumentFile(@"11334214-ca43-406b-9cae-f986c3c63332");
            documentFile.Open(@"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\11334214-ca43-406b-9cae-f986c3c63332.txt");
            documentFile.Content = "Code Sample V1";
            Assert.AreEqual(0, documentFile.Versions.Count());

            documentFile.CreateVersion("Snapshot 1");
            Assert.AreEqual(1, documentFile.Versions.Count());

            IFileVersion fileVersion_1 = documentFile.Versions[0];
            Assert.AreEqual("Code Sample V1", fileVersion_1.Content);
            
            string fileVersion_1_ExpectedId = VersionFileHelper.CreateId(documentFile.FilePath, fileVersion_1.TimeTaken);
            string fileVersion_1_ExpectedFileName = VersionFileHelper.CreateFileName(documentFile.FilePath, fileVersion_1.TimeTaken);
            string fileVersion_1_ExpectedFilePath = VersionFileHelper.CreateFilePath(documentFile.FilePath, fileVersion_1.TimeTaken);

            Assert.AreEqual(fileVersion_1_ExpectedId, fileVersion_1.Id);
            Assert.AreEqual(fileVersion_1_ExpectedFileName, fileVersion_1.FileName);
            Assert.AreEqual(fileVersion_1_ExpectedFilePath, fileVersion_1.FilePath);

            documentFile.Content = "Code Sample V2";
            documentFile.CreateVersion("Snapshot 2");
            Assert.AreEqual(2, documentFile.Versions.Count());

            IFileVersion fileVersion_2 = documentFile.Versions[1];
            Assert.AreEqual("Code Sample V2", fileVersion_2.Content);

            string fileVersion_2_ExpectedId = VersionFileHelper.CreateId(documentFile.FilePath, fileVersion_2.TimeTaken);
            string fileVersion_2_ExpectedFileName = VersionFileHelper.CreateFileName(documentFile.FilePath, fileVersion_2.TimeTaken);
            string fileVersion_2_ExpectedFilePath = VersionFileHelper.CreateFilePath(documentFile.FilePath, fileVersion_2.TimeTaken);

            Assert.AreEqual(fileVersion_2_ExpectedId, fileVersion_2.Id);
            Assert.AreEqual(fileVersion_2_ExpectedFileName, fileVersion_2.FileName);
            Assert.AreEqual(fileVersion_2_ExpectedFilePath, fileVersion_2.FilePath);
        }
    }
}
