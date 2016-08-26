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
    public class Document_Tests
    {
        private DocFileSimulator documentSimulator;

        [TestInitialize]
        public void InitializeTests()
        {
            documentSimulator = new DocFileSimulator();
        }

        [TestMethod]
        public void DocumentFile_Create()
        {
            IDocument documentFile = new StubDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentFile1.Id, "Title");
            documentFile.Create(documentSimulator.DocumentFile1.FilePath);

            Assert.AreEqual(documentSimulator.DocumentIndex.Folder, documentFile.Folder);
            Assert.AreEqual(documentSimulator.DocumentFile1.FilePath, documentFile.FilePath);
            Assert.AreEqual(documentSimulator.DocumentFile1.FileName, documentFile.FileName);
            Assert.AreEqual(documentSimulator.DocumentFile1.Id, documentFile.Id);
        }

        [TestMethod]
        public void DocumentFile_FileVersions_Create()
        {
            IDocument documentFile = new StubDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentFile1.Id, "Title");
            documentFile.Open(documentSimulator.DocumentFile1.FilePath);
            IFileVersion fileVersion_1 = documentFile.CreateVersion("Snapshot 1");
            Assert.AreEqual("Snapshot 1", fileVersion_1.Description);
            Assert.IsNotNull(fileVersion_1.TimeTaken);
            Assert.AreEqual(fileVersion_1.Id.Substring(0, 36), documentFile.Id);
            Assert.AreEqual(documentFile.Folder, fileVersion_1.Folder);
            Assert.AreEqual(documentFile.FileExtension, fileVersion_1.FileExtension);
            Assert.AreEqual(Path.Combine(documentFile.Folder, fileVersion_1.FileName), fileVersion_1.FilePath);
        }

        [TestMethod]
        public void DocumentFile_FileVersions_FileVersionName()
        {
            IDocument documentFile = new StubDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentFile1.Id, "Title");
            documentFile.Open(documentSimulator.DocumentFile1.FilePath);

            int initialVersionCount = documentFile.Versions.Length;

            documentFile.CreateVersion("Snapshot 1");
            int versionCount_1 = documentFile.Versions.Length;

            IFileVersion fileVersion_1 = documentFile.Versions[0];

            //string fileVersion_1_ExpectedId = VersionFileHelper.CreateId(documentFile.FilePath, fileVersion_1.TimeTaken);
            //string fileVersion_1_ExpectedFileName = VersionFileHelper.CreateFileName(documentFile.FilePath, fileVersion_1.TimeTaken);
            //string fileVersion_1_ExpectedFilePath = VersionFileHelper.CreateFilePath(documentFile.FilePath, fileVersion_1.TimeTaken);

            Thread.Sleep(5);
            documentFile.CreateVersion("Snapshot 2");
            int versionCount_2 = documentFile.Versions.Length;

            IFileVersion fileVersion_2 = documentFile.Versions[1];

            //string fileVersion_2_ExpectedId = VersionFileHelper.CreateId(documentFile.FilePath, fileVersion_2.TimeTaken);
            //string fileVersion_2_ExpectedFileName = VersionFileHelper.CreateFileName(documentFile.FilePath, fileVersion_2.TimeTaken);
            //string fileVersion_2_ExpectedFilePath = VersionFileHelper.CreateFilePath(documentFile.FilePath, fileVersion_2.TimeTaken);

            Assert.AreEqual(0, initialVersionCount);
            Assert.AreEqual(1, versionCount_1);
            Assert.AreEqual(2, versionCount_2);

            //Assert.AreEqual(fileVersion_1_ExpectedId, fileVersion_1.Id);
            //Assert.AreEqual(fileVersion_1_ExpectedFileName, fileVersion_1.FileName);
            //Assert.AreEqual(fileVersion_1_ExpectedFilePath, fileVersion_1.FilePath);

            //Assert.AreEqual(fileVersion_2_ExpectedId, fileVersion_2.Id);
            //Assert.AreEqual(fileVersion_2_ExpectedFileName, fileVersion_2.FileName);
            //Assert.AreEqual(fileVersion_2_ExpectedFilePath, fileVersion_2.FilePath);

            Assert.AreEqual(fileVersion_2.Id, documentFile.LatestVersion().Id);
            Assert.IsTrue(documentFile.HasVersion(fileVersion_1.Id));
            Assert.AreSame(fileVersion_1, documentFile.GetVersion(fileVersion_1.Id));
        }

        [TestMethod]
        public void DocumentFile_FileVersions_Delete()
        {
            IDocument documentFile = new StubDocumentFile(documentSimulator.DocumentIndex.Folder, documentSimulator.DocumentFile1.Id, "Title");
            documentFile.Open(documentSimulator.DocumentFile1.FilePath);

            // You'll never be creating versions as fast as this.
            documentFile.CreateVersion("Snapshot 1");
            Thread.Sleep(5);
            documentFile.CreateVersion("Snapshot 2");
            Thread.Sleep(5);
            documentFile.CreateVersion("Snapshot 3");
            Thread.Sleep(5);
            documentFile.CreateVersion("Snapshot 4");
            Thread.Sleep(5);

            int initialVersionCount = documentFile.Versions.Length;

            IFileVersion version1 = documentFile.Versions[0];
            //IFileVersion version2 = documentFile.Versions[1];
            //IFileVersion version3 = documentFile.Versions[2];
            //IFileVersion version4 = documentFile.Versions[3];

            documentFile.DeleteVersion(version1.Id);

            int afterVersionCount = documentFile.Versions.Length;

            Assert.AreEqual(4, initialVersionCount);
            Assert.IsFalse(version1.Loaded);
            Assert.AreEqual(3, afterVersionCount);

        }
    }
}
