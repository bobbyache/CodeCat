﻿using CygSoft.CodeCat.DocumentManager.PathGenerators;
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
    public class DocumentIndexPathGeneratorTests
    {
        [Test]
        public void FullPathGeneratedCorrectly()
        {
            DocumentIndexPathGenerator pathGenerator = new DocumentIndexPathGenerator(@"C:\MyRootFolder\SubFolder", "index");
            Assert.That(pathGenerator.FileExtension, Is.EqualTo("index"));
            Assert.That(HelperFuncs.FileNameIsGuid(pathGenerator.FileName), Is.True);
            Assert.That(Path.GetExtension(pathGenerator.FileName), Is.EqualTo(".index"));
            Assert.That(pathGenerator.FolderPath, Is.EqualTo(Path.Combine(@"C:\MyRootFolder\SubFolder", pathGenerator.Id)));
            Assert.That(pathGenerator.FilePath, Is.EqualTo(Path.Combine(pathGenerator.FolderPath, pathGenerator.FileName)));
            Assert.That(pathGenerator.Id, Is.EqualTo(HelperFuncs.GetIdFromFileName(pathGenerator.FilePath)));
        }

        [Test]
        public void RelativePathGeneratedCorrectly()
        {
            DocumentIndexPathGenerator pathGenerator = new DocumentIndexPathGenerator(@"\SubFolder", "index");
            Assert.That(pathGenerator.FileExtension, Is.EqualTo("index"));
            Assert.That(HelperFuncs.FileNameIsGuid(pathGenerator.FileName), Is.True);
            Assert.That(Path.GetExtension(pathGenerator.FileName), Is.EqualTo(".index"));
            Assert.That(pathGenerator.FolderPath, Is.EqualTo(Path.Combine(@"\SubFolder", pathGenerator.Id)));
            Assert.That(pathGenerator.FilePath, Is.EqualTo(Path.Combine(pathGenerator.FolderPath, pathGenerator.FileName)));
            Assert.That(pathGenerator.Id, Is.EqualTo(HelperFuncs.GetIdFromFileName(pathGenerator.FilePath)));
        }
    }
}
