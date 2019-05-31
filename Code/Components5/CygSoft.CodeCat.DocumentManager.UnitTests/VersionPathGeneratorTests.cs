using CygSoft.CodeCat.DocumentManager.PathGenerators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.UnitTests
{
    [TestFixture]
    public class VersionPathGeneratorTests
    {
        [Test]
        public void FullPathGeneratedCorrectly()
        {
            VersionPathGenerator pathGenerator = new VersionPathGenerator(@"C:\MyRootFolder\DocumentFolder\File.jpg", DateTime.Parse("2019-04-16 10:22:12"));
            Assert.That(pathGenerator.FileExtension, Is.EqualTo("jpg"));
            Assert.That(pathGenerator.FileName, Is.EqualTo("File_2019-04-16_10-22-12-0.jpg"));
            Assert.That(Path.GetExtension(pathGenerator.FileName), Is.EqualTo(".jpg"));
            Assert.That(pathGenerator.FolderPath, Is.EqualTo(@"C:\MyRootFolder\DocumentFolder"));
            Assert.That(pathGenerator.FilePath, Is.EqualTo(Path.Combine(pathGenerator.FolderPath, pathGenerator.FileName)));
            Assert.That(pathGenerator.Id, Is.EqualTo("File_2019-04-16_10-22-12-0"));
        }

        [Test]
        public void RelativePathGeneratedCorrectly()
        {
            VersionPathGenerator pathGenerator = new VersionPathGenerator(@"\DocumentFolder\File.jpg", DateTime.Parse("2019-04-16 10:22:12"));
            Assert.That(pathGenerator.FileExtension, Is.EqualTo("jpg"));
            Assert.That(pathGenerator.FileName, Is.EqualTo("File_2019-04-16_10-22-12-0.jpg"));
            Assert.That(Path.GetExtension(pathGenerator.FileName), Is.EqualTo(".jpg"));
            Assert.That(pathGenerator.FolderPath, Is.EqualTo(@"\DocumentFolder"));
            Assert.That(pathGenerator.FilePath, Is.EqualTo(Path.Combine(pathGenerator.FolderPath, pathGenerator.FileName)));
            Assert.That(pathGenerator.Id, Is.EqualTo("File_2019-04-16_10-22-12-0"));
        }
    }
}
