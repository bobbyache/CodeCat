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
    public class DocumentPathGeneratorTests
    {
        [Test]
        public void FullPathGeneratedCorrectly()
        {
            DocumentPathGenerator pathGenerator = new DocumentPathGenerator(@"C:\MyRootFolder\DocumentFolder", "index");
            Assert.That(pathGenerator.FileExtension, Is.EqualTo("index"));
            Assert.That(HelperFuncs.FileNameIsGuid(pathGenerator.FileName), Is.True);
            Assert.That(pathGenerator.FolderPath, Is.EqualTo(@"C:\MyRootFolder\DocumentFolder"));
            Assert.That(pathGenerator.FilePath, Is.EqualTo(Path.Combine(pathGenerator.FolderPath, pathGenerator.FileName)));
            Assert.That(pathGenerator.Id, Is.EqualTo(HelperFuncs.GetIdFromFileName(pathGenerator.FilePath)));
        }

        [Test]
        public void RelativePathGeneratedCorrectly()
        {
            DocumentPathGenerator pathGenerator = new DocumentPathGenerator(@"\DocumentFolder", "index");
            Assert.That(pathGenerator.FileExtension, Is.EqualTo("index"));
            Assert.That(HelperFuncs.FileNameIsGuid(pathGenerator.FileName), Is.True);
            Assert.That(pathGenerator.FolderPath, Is.EqualTo(@"\DocumentFolder"));
            Assert.That(pathGenerator.FilePath, Is.EqualTo(Path.Combine(pathGenerator.FolderPath, pathGenerator.FileName)));
            Assert.That(pathGenerator.Id, Is.EqualTo(HelperFuncs.GetIdFromFileName(pathGenerator.FilePath)));
        }


    }
}
