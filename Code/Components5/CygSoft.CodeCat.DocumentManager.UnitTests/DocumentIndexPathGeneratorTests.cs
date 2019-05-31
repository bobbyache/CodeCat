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
    public class DocumentIndexPathGeneratorTests
    {
        //[Test]
        //public void RelativeDocumentIndexPathGenerator_Instantiate_WithRelativeFolder_No_Id_Returns_Correct_FolderPath()
        //{
        //    RelativeDocumentIndexPathGenerator generator = new RelativeDocumentIndexPathGenerator(@"C:\basefolder", @"relative\folder", "txt");
        //    Assert.AreEqual(@"C:\basefolder\relative\folder", generator.FolderPath);
        //}

        //[Test]
        //public void RelativeDocumentIndexPathGenerator_Instantiate_WithRelativeFolder_With_Id_Returns_Correct_FilePath()
        //{
        //    RelativeDocumentIndexPathGenerator generator = new RelativeDocumentIndexPathGenerator(@"C:\basefolder", @"relative\folder", "txt", "0001-0001");
        //    Assert.AreEqual(@"C:\basefolder\relative\folder\0001-0001.txt", generator.FolderPath);
        //}
    }

    public class RelativeDocumentIndexPathGenerator : DocumentIndexPathGenerator
    {
        public RelativeDocumentIndexPathGenerator(string baseFolder, string relativeFolder, string extension)
            : base(Path.Combine(baseFolder, relativeFolder), extension)
        {

        }

        public RelativeDocumentIndexPathGenerator(string baseFolder, string relativeFolder, string extension, string id)
            : base(Path.Combine(baseFolder, relativeFolder), extension, id)
        {
        }
    }
}
