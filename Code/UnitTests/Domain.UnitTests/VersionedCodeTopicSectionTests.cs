using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.TopicSections.VersionedCode;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests
{
    [TestFixture]
    [Category("VersionedCode")]
    public class VersionedCodeTopicSectionTests
    {
        [Test]
        public void VersionedCodeTopicSection_Instantiate_Path1_Has_Correct_State()
        {
            VersionedCodeTopicSection versionedCode = new VersionedCodeTopicSection(@"C:\folder", "Title", ".ext", "C#");
            Assert.IsNotNull(versionedCode.Id);
            Assert.AreNotEqual("", versionedCode.Id);
            Assert.AreEqual(@"C:\folder", versionedCode.Folder);
            Assert.AreEqual("Title", versionedCode.Title);
            Assert.AreEqual("ext", versionedCode.FileExtension);
        }

        [Test]
        public void VersionedCodeTopicSection_Instantiate_Path2_Has_Correct_State()
        {
            VersionedCodeTopicSection versionedCode = new VersionedCodeTopicSection(@"C:\folder", "2345", "Title", ".ext", 1, "Description", "C#");
            Assert.IsNotNull(versionedCode.Id);
            Assert.AreNotEqual("", versionedCode.Id);
            Assert.AreEqual(@"C:\folder", versionedCode.Folder);
            Assert.AreEqual("Title", versionedCode.Title);
            Assert.AreEqual("ext", versionedCode.FileExtension);
            Assert.AreEqual("Description", versionedCode.Description);
            Assert.AreEqual("C#", versionedCode.Syntax);
        }

        [Test]
        public void VersionedCodeTopicSection_Open_CallsOnOpen()
        {
            TestVersionedCodeTopicSection versionedCode = new TestVersionedCodeTopicSection();
            versionedCode.Open();

            Assert.IsTrue(versionedCode.IsOpened);
            versionedCode.Open();
        }


        [Test]
        public void VersionedCodeTopicSection_Add_TwoVersions_Has_Available_Version()
        {
            TestVersionedCodeTopicSection versionedCode = new TestVersionedCodeTopicSection();
            versionedCode.Open();
            versionedCode.CreateVersion("Version 1");

            IFileVersion fileVersion = versionedCode.CreateVersion("Version 2");
            string id = fileVersion.Id;

            Assert.AreEqual(2, versionedCode.Versions.Length);
            Assert.IsTrue(versionedCode.HasVersion(fileVersion.Id));
        }
        
        [Test]
        public void VersionedCodeTopicSection_Add_TwoVersions_RemoveOne_Has_CorrectOne_Remaining()
        {
            TestVersionedCodeTopicSection versionedCode = new TestVersionedCodeTopicSection();
            versionedCode.Open();
            IFileVersion fileVersion1 = versionedCode.CreateVersion(DateTime.Parse("2018-03-12 13:05:23"), "Version 1");
            IFileVersion fileVersion2 = versionedCode.CreateVersion(DateTime.Parse("2018-03-12 13:05:24"), "Version 2");

            versionedCode.DeleteVersion(fileVersion1.Id);

            Assert.AreEqual(1, versionedCode.Versions.Length);
            Assert.IsFalse(versionedCode.HasVersion(fileVersion1.Id));
            Assert.IsTrue(versionedCode.HasVersion(fileVersion2.Id));
        }

        [Test]
        public void VersionedCodeTopicSection_Add_SingleVersion_Has_Available_Version()
        {
            TestVersionedCodeTopicSection versionedCode = new TestVersionedCodeTopicSection();
            versionedCode.Open();
            IFileVersion fileVersion = versionedCode.CreateVersion("My First Version");
            string id = fileVersion.Id;

            Assert.AreEqual(1, versionedCode.Versions.Length);
            Assert.IsTrue(versionedCode.HasVersion(fileVersion.Id));
        }

        [Test]
        public void VersionedCodeTopicSection_Add_TwoVersions_GetLatestVersion_Returns_CorrectVersion()
        {
            TestVersionedCodeTopicSection versionedCode = new TestVersionedCodeTopicSection();
            versionedCode.Open();
            IFileVersion fileVersion1 = versionedCode.CreateVersion(DateTime.Parse("2018-03-12 13:05:23"), "Version 1");
            IFileVersion fileVersion2 = versionedCode.CreateVersion(DateTime.Parse("2018-03-12 13:05:24"), "Version 2");

            string latestVersionId = fileVersion2.Id;

            IFileVersion latestVersion = versionedCode.LatestVersion();

            Assert.AreEqual(latestVersionId, latestVersion.Id);
        }

        public class TestVersionedCodeTopicSection : VersionedCodeTopicSection
        {
            public bool IsOpened = false;
            public TestVersionedCodeTopicSection() : base(new TestVersionedFileRepository(), @"C:\folder", "Title", ".ext", "C#") { }

            protected override void OnOpen()
            {
                this.Text = "public static void Main(string[] args) {}";
                IsOpened = true;
            }

            protected override void CreateVersionFile(IFileVersion fileVersion)
            {
                // don't do anything, only want to test that the versions are managed...
                // ... not really interested in persistence.
            }

            protected override void DeleteVersionFile(IFileVersion fileVersion)
            {
                // don't do anything, only want to test that the versions are managed...
                // ... not really interested in persistence.
            }
        }

        public class TestVersionedFileRepository : IVersionedFileRepository
        {
            public string FilePath { get; set; }

            public bool HasVersionFile
            {
                get { return true; }
            }

            public List<IFileVersion> LoadVersions()
            {
                return null;
            }

            public void WriteVersions(List<IFileVersion> fileVersions)
            {
                //throw new NotImplementedException();
            }
        }
    }
}
