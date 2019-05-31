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
    public class FilePathGeneratorTests
    {
        [Test]
        public void FullPathGeneratedCorrectly()
        {
            FilePathGenerator pathGenerator = new FilePathGenerator(@"C:\MyRootFolder\DocumentFolder", "file.ext");
            Assert.That(pathGenerator.FileExtension, Is.EqualTo(".ext"));
            Assert.That(pathGenerator.FileName, Is.EqualTo("file.ext"));
            Assert.That(pathGenerator.FolderPath, Is.EqualTo(@"C:\MyRootFolder\DocumentFolder"));
            Assert.That(pathGenerator.FilePath, Is.EqualTo(Path.Combine(pathGenerator.FolderPath, pathGenerator.FileName)));
            Assert.That(HelperFuncs.IdIsGuid(pathGenerator.Id), Is.True);
        }

        [Test]
        public void RelativePathGeneratedCorrectly()
        {
            FilePathGenerator pathGenerator = new FilePathGenerator(@"\DocumentFolder", "file.ext");
            Assert.That(pathGenerator.FileExtension, Is.EqualTo(".ext"));
            Assert.That(pathGenerator.FileName, Is.EqualTo("file.ext"));
            Assert.That(pathGenerator.FolderPath, Is.EqualTo(@"\DocumentFolder"));
            Assert.That(pathGenerator.FilePath, Is.EqualTo(Path.Combine(pathGenerator.FolderPath, pathGenerator.FileName)));
            Assert.That(HelperFuncs.IdIsGuid(pathGenerator.Id), Is.True);
        }
    }
}
