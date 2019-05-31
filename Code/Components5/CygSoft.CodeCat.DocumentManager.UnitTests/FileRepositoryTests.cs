using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System.IO;

namespace DocumentManager.UnitTests
{
    [TestFixture]
    public class FileRepositoryTests
    {
        [Test]
        public void FileRepositoryStateCorrectAfterPathGeneratorInjection()
        {
            var pathGenerator = new DocumentIndexPathGenerator(@"C:\RootFolder\SubFolder", ".txt");
            var fileRepository = new FileRepository(pathGenerator);

            Assert.That(fileRepository.Extension, Is.EqualTo("txt"));
            Assert.That(HelperFuncs.FileNameIsGuid(fileRepository.FileName), Is.True);
            Assert.That(Path.GetExtension(fileRepository.FileName), Is.EqualTo(".txt"));
            Assert.That(fileRepository.FilePath, Is.EqualTo(Path.Combine(fileRepository.GetDirectory(), fileRepository.FileName)));
            Assert.That(fileRepository.FileTitle, Is.EqualTo(HelperFuncs.GetIdFromFileName(fileRepository.FileName)));
            Assert.That(fileRepository.Loaded, Is.EqualTo(false));
        }
    }
}
