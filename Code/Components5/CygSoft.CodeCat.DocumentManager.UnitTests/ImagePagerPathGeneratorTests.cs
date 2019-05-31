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
    public class ImagePagerPathGeneratorTests
    {
        [Test]
        public void FullPathGeneratedCorrectly()
        {
            ImagePagerPathGenerator pathGenerator = new ImagePagerPathGenerator(@"C:\MyRootFolder\DocumentFolder", ".jpg");
            Assert.That(pathGenerator.FileExtension, Is.EqualTo("jpg"));
            Assert.That(HelperFuncs.FileNameIsGuid(pathGenerator.FileName), Is.True);
            Assert.That(Path.GetExtension(pathGenerator.FileName), Is.EqualTo(".jpg"));
            Assert.That(pathGenerator.FolderPath, Is.EqualTo(@"C:\MyRootFolder\DocumentFolder"));
            Assert.That(pathGenerator.FilePath, Is.EqualTo(Path.Combine(pathGenerator.FolderPath, pathGenerator.FileName)));
            Assert.That(HelperFuncs.IdIsGuid(pathGenerator.Id), Is.True);
        }

        [Test]
        public void RelativePathGeneratedCorrectly()
        {
            ImagePagerPathGenerator pathGenerator = new ImagePagerPathGenerator(@"\DocumentFolder", ".jpg");
            Assert.That(pathGenerator.FileExtension, Is.EqualTo("jpg"));
            Assert.That(HelperFuncs.FileNameIsGuid(pathGenerator.FileName), Is.True);
            Assert.That(Path.GetExtension(pathGenerator.FileName), Is.EqualTo(".jpg"));
            Assert.That(pathGenerator.FolderPath, Is.EqualTo(@"\DocumentFolder"));
            Assert.That(pathGenerator.FilePath, Is.EqualTo(Path.Combine(pathGenerator.FolderPath, pathGenerator.FileName)));
            Assert.That(HelperFuncs.IdIsGuid(pathGenerator.Id), Is.True);
        }
    }
}
