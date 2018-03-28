using CygSoft.CodeCat.DocumentManager.PathGenerators;
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
    public class VersionPathGeneratorTests
    {
        [Test]
        public void VersionPathGenerator_Instantiate_With_File_And_Date_Returns_Applicable_Version_FileName()
        {
            VersionPathGenerator versionPathGenerator = new VersionPathGenerator(@"C:\folder\file.txt", DateTime.Parse("2015-03-22 22:10:12"));
            Assert.AreEqual("file_2015-03-22_22-10-12-0.txt", versionPathGenerator.FileName);
        }

        [Test]
        public void VersionPathGenerator_Instantiate_With_File_And_Date_Returns_Applicable_Version_FilePath()
        {
            VersionPathGenerator versionPathGenerator = new VersionPathGenerator(@"C:\folder\file.txt", DateTime.Parse("2015-03-22 22:10:12"));
            Assert.AreEqual(@"C:\folder\file_2015-03-22_22-10-12-0.txt", versionPathGenerator.FilePath);
        }

        [Test]
        public void VersionPathGenerator_Instantiate_With_File_And_Date_Returns_Applicable_Version_Extension()
        {
            VersionPathGenerator versionPathGenerator = new VersionPathGenerator(@"C:\folder\file.txt", DateTime.Parse("2015-03-22 22:10:12"));
            Assert.AreEqual("txt", versionPathGenerator.FileExtension);
        }

        [Test]
        public void VersionPathGenerator_Instantiate_With_File_And_Date_Returns_Applicable_Version_FolderPath()
        {
            VersionPathGenerator versionPathGenerator = new VersionPathGenerator(@"C:\folder\file.txt", DateTime.Parse("2015-03-22 22:10:12"));
            Assert.AreEqual(@"C:\folder", versionPathGenerator.FolderPath);
        }

        [Test]
        public void VersionPathGenerator_Instantiate_With_File_And_Date_Returns_Applicable_Version_Id()
        {
            VersionPathGenerator versionPathGenerator = new VersionPathGenerator(@"C:\folder\file.txt", DateTime.Parse("2015-03-22 22:10:12"));
            Assert.AreEqual("file_2015-03-22_22-10-12-0", versionPathGenerator.Id);
        }

        [Test]
        public void VersionPathGenerator_Instantiate_With_File_And_Date_Returns_Applicable_Version_TimeStamp()
        {
            DateTime timeStamp = DateTime.Parse("2015-03-22 22:10:12");
            VersionPathGenerator versionPathGenerator = new VersionPathGenerator(@"C:\folder\file.txt", timeStamp);
            Assert.AreEqual(timeStamp, versionPathGenerator.TimeStamp);
        }
    }
}
