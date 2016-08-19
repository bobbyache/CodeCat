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
        private string multiDocId = "d33b59bd-54af-4f0b-967f-64084847b678";
        private string multDocFileName = "document_index.xml";
        private string multiDocFilePath = @"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\document_index.xml";
        
        private string multiDocFolder = @"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678";

        private string documentFile_1_FilePath = @"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\f562810b-a1f7-4cf8-b370-dbaf87ff8759.txt";
        private string documentFile_1_FileName = "f562810b-a1f7-4cf8-b370-dbaf87ff8759.txt";
        private string documentFile_1_Id = "f562810b-a1f7-4cf8-b370-dbaf87ff8759";

        private string documentFile_2_FilePath = @"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\11334214-ca43-406b-9cae-f986c3c63332.txt";
        private string documentFile_2_FileName = "11334214-ca43-406b-9cae-f986c3c63332.txt";
        private string documentFile_2_Id = "11334214-ca43-406b-9cae-f986c3c63332";

        private string documentFile_3_FilePath = @"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\37c1dba5-9da3-4222-af34-43f98c674d82.txt";
        private string documentFile_3_FileName = "37c1dba5-9da3-4222-af34-43f98c674d82.txt";
        private string documentFile_3_Id = "37c1dba5-9da3-4222-af34-43f98c674d82";

        private string documentFile_4_FilePath = @"H:\ParentFolder\d33b59bd-54af-4f0b-967f-64084847b678\0f964889-a3ab-43d6-94a2-76e60cb9aae8.txt";
        private string documentFile_4_FileName = "0f964889-a3ab-43d6-94a2-76e60cb9aae8.txt";
        private string documentFile_4_Id = "0f964889-a3ab-43d6-94a2-76e60cb9aae8";


        [TestMethod]
        public void MultiDocumentFile_CreateDocument()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile(multiDocId);
            multiDocFile.Create(multiDocFilePath);

            Assert.AreEqual(multDocFileName, multiDocFile.FileName);
            Assert.AreEqual(multiDocFilePath, multiDocFile.FilePath);
            Assert.AreEqual(multiDocFolder, multiDocFile.Folder);
            Assert.AreEqual(0, multiDocFile.DocumentFiles.Length);
            Assert.AreEqual(multiDocId, multiDocFile.Id);
        }

        [TestMethod]
        public void MultiDocumentFile_OpenDocument()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile(multiDocId);
            multiDocFile.Open(multiDocFilePath);

            Assert.AreEqual(multDocFileName, multiDocFile.FileName);
            Assert.AreEqual(multiDocFilePath, multiDocFile.FilePath);
            Assert.AreEqual(multiDocFolder, multiDocFile.Folder);
            Assert.AreEqual(3, multiDocFile.DocumentFiles.Length);
            Assert.AreEqual(multiDocId, multiDocFile.Id);
        }


        [TestMethod]
        public void MultiDocumentFile_LoadWithDocuments()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile(multiDocId);

            bool loadedOnInstantiate = multiDocFile.Loaded;
            multiDocFile.Open(multiDocFilePath);
            bool loadedOnOpen = multiDocFile.Loaded;

            Assert.IsTrue(multiDocFile.DocumentFiles.Count() == 3);

            IDocumentFile documentFile_1 = multiDocFile.DocumentFiles[0];
            Assert.AreEqual(documentFile_1_Id, documentFile_1.Id);

            Assert.AreEqual(documentFile_1_FilePath, documentFile_1.FilePath);
            Assert.AreEqual(documentFile_1_FileName, documentFile_1.FileName);
            Assert.AreEqual(multiDocFolder, documentFile_1.Folder);
            Assert.AreEqual(null, documentFile_1.Content);
            Assert.IsFalse(documentFile_1.HasVersions);
            Assert.AreEqual(0, documentFile_1.Versions.Length);
            Assert.IsTrue(documentFile_1.Loaded);

            Assert.IsFalse(loadedOnInstantiate);
            Assert.IsTrue(loadedOnOpen);
        }

        [TestMethod]
        public void MultiDocumentFile_LoadWithDocuments_Reposition()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile(multiDocId);
            multiDocFile.Open(multiDocFilePath);

            Assert.IsTrue(multiDocFile.DocumentFiles.Count() == 3);

            IDocumentFile documentFile_1 = multiDocFile.DocumentFiles[0];
            Assert.AreEqual(documentFile_1_Id, documentFile_1.Id);

            multiDocFile.MoveDown(documentFile_1);

            documentFile_1 = multiDocFile.DocumentFiles[1];
            Assert.AreEqual(documentFile_1_Id, documentFile_1.Id);
        }

        [TestMethod]
        public void MultiDocumentFile_LoadWithDocuments_AddDocuments()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile(multiDocId);
            multiDocFile.Open(multiDocFilePath);

            IDocumentFile documentFile_4 = new StubDocumentFile(documentFile_4_Id);
            multiDocFile.AddDocumentFile(documentFile_4);

            Assert.AreEqual(documentFile_4_Id, multiDocFile.DocumentFiles[multiDocFile.DocumentFiles.Length - 1].Id);
            multiDocFile.MoveUp(documentFile_4);
            Assert.AreEqual(documentFile_4_Id, multiDocFile.DocumentFiles[multiDocFile.DocumentFiles.Length - 2].Id);
        }


        [TestMethod]
        public void MultiDocumentFile_Create_And_AddDocuments()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile(multiDocId);
            multiDocFile.Create(multiDocFilePath);

            Assert.AreEqual(0, multiDocFile.DocumentFiles.Length);

            IDocumentFile documentFile_4 = new StubDocumentFile(documentFile_4_Id);
            multiDocFile.AddDocumentFile(documentFile_4);

            Assert.AreEqual(documentFile_4_Id, multiDocFile.DocumentFiles[multiDocFile.DocumentFiles.Length - 1].Id);
            multiDocFile.MoveUp(documentFile_4);
            Assert.AreEqual(documentFile_4_Id, multiDocFile.DocumentFiles[multiDocFile.DocumentFiles.Length - 1].Id);
            Assert.AreEqual(1, multiDocFile.DocumentFiles.Length);
        }

        [TestMethod]
        public void MultiDocumentFile_Positioning()
        {
            IMultiDocumentFile multiDocFile = new StubMultiDocumentFile(multiDocId);
            multiDocFile.Open(multiDocFilePath);

            // because multi-document files could have more than one type of document file, these document
            // files will have to be created elsewhere (think document factory).
            IDocumentFile documentFile_1 = multiDocFile.DocumentFiles[0];
            IDocumentFile documentFile_2 = multiDocFile.DocumentFiles[1];
            IDocumentFile documentFile_3 = multiDocFile.DocumentFiles[2];

            Assert.AreEqual(documentFile_1_Id, documentFile_1.Id);
            Assert.AreEqual(documentFile_2_Id, documentFile_2.Id);
            Assert.AreEqual(documentFile_3_Id, documentFile_3.Id);

            Assert.AreEqual(documentFile_1_FilePath, documentFile_1.FilePath);
            Assert.AreEqual(documentFile_2_FileName, documentFile_2.FileName);
            Assert.AreEqual(multiDocFolder, documentFile_1.Folder);
            Assert.AreEqual(null, documentFile_1.Content);
            Assert.IsFalse(documentFile_1.HasVersions);
            Assert.AreEqual(0, documentFile_1.Versions.Length);
            Assert.IsTrue(documentFile_1.Loaded);

            IDocumentFile documentFile = multiDocFile.GetDocumentFile(documentFile_1_Id);

            Assert.AreEqual(1, documentFile.Ordinal);
            Assert.IsFalse(multiDocFile.CanMoveUp(documentFile));
            Assert.IsTrue(multiDocFile.CanMoveDown(documentFile));

            multiDocFile.MoveDown(documentFile);

            Assert.AreEqual(2, documentFile.Ordinal);
            Assert.IsTrue(multiDocFile.CanMoveUp(documentFile));
            Assert.IsTrue(multiDocFile.CanMoveDown(documentFile));

            Assert.AreEqual(documentFile_1_Id, multiDocFile.DocumentFiles[1].Id);
        }

        [TestMethod]
        public void DocumentFile_Create()
        {
            IDocumentFile documentFile = new StubDocumentFile(documentFile_1_Id);
            documentFile.Create(documentFile_1_FilePath);

            Assert.AreEqual(multiDocFolder, documentFile.Folder);
            Assert.AreEqual(documentFile_1_FilePath, documentFile.FilePath);
            Assert.AreEqual(documentFile_1_FileName, documentFile.FileName);
            Assert.AreEqual(documentFile_1_Id, documentFile.Id);
        }

        [TestMethod]
        public void DocumentFile_Snapshots()
        {
            IDocumentFile documentFile = new StubDocumentFile(@"11334214-ca43-406b-9cae-f986c3c63332");
            documentFile.Open(documentFile_1_FilePath);
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
