using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CygSoft.CodeCat.ProjectConverter;
using System.Xml.Linq;

namespace UnitTests.Tests.Converters
{
    [TestClass]
    //[DeploymentItem(@"Files\ProjectFiles\IndexItem V2.xml")]
    //[DeploymentItem(@"Files\ProjectFiles\IndexItem V3.xml")]
    [DeploymentItem(@"Files\ProjectFiles\CodeIndex V2.xml")]
    [DeploymentItem(@"Files\ProjectFiles\CodeIndex V3.xml")]
    [DeploymentItem(@"Files\ProjectFiles\CodeFile V2.xml")]
    [DeploymentItem(@"Files\ProjectFiles\CodeFile V3.xml")]
    [DeploymentItem(@"Files\ProjectFiles\ProjectFile V2.xml")]
    [DeploymentItem(@"Files\ProjectFiles\ProjectFile V3.xml")]
    public class Version2ToVersion3_Tests
    {
        private string codeIndex_v2_Path = "CodeIndex V2.xml";
        private string codeIndex_v3_Path = "CodeIndex V3.xml";
        private string codeFile_v2_Path = "CodeFile V2.xml";
        private string codeFile_v3_Path = "CodeFile V3.xml";
        //private string projectFile_v2_Path = "ProjectFile V2.xml";
        private string projectFile_v3_Path = "ProjectFile V3.xml";


        [TestMethod]
        public void Version2_to_Version3_Create_ProjectFile()
        {
            XDocument expectedDocument = XDocument.Load(projectFile_v3_Path);

            CygSoft.CodeCat.ProjectConverter.ToVersion3 convertToVersion3 = new ToVersion3();
            XDocument document = convertToVersion3.CreateProjectDocument();

            Assert.AreEqual(expectedDocument.ToString(), document.ToString());
            Assert.AreEqual(expectedDocument.Declaration.ToString(), document.Declaration.ToString());
        }

        [TestMethod]
        public void Version2_to_Version3_CreateCodeIndexFile()
        {
            XDocument expectedDocument = XDocument.Load(codeIndex_v3_Path);

            CygSoft.CodeCat.ProjectConverter.ToVersion3 converterVersion3 = new ToVersion3();
            XDocument document = converterVersion3.CreateCodeIndexDocument(codeIndex_v2_Path);

            Assert.AreEqual(expectedDocument.ToString(), document.ToString());
            Assert.AreEqual(expectedDocument.Declaration.ToString(), document.Declaration.ToString());
        }

        [TestMethod]
        public void Version2_to_Version3_Convert_CodeFile()
        {
            XDocument expectedDocument = XDocument.Load(codeFile_v3_Path);
            XDocument oldIndexFile = XDocument.Load(codeIndex_v2_Path);

            CygSoft.CodeCat.ProjectConverter.ToVersion3 converterVersion3 = new ToVersion3();
            XDocument document = converterVersion3.ConvertCodeFile(codeFile_v2_Path, oldIndexFile);

            Assert.AreEqual(expectedDocument.ToString(), document.ToString());
            Assert.AreEqual(expectedDocument.Declaration.ToString(), document.Declaration.ToString());
        }

        //[TestMethod]
        //public void Version2_to_Version3_ConverterProperties()
        //{
        //    Converter converter = new Converter(@"D:\Temporary\CodeCat\TestConversion\OldProject\TESTING.xml");

        //    Assert.AreEqual("2", converter.FromVersion);
        //    Assert.AreEqual("3", converter.ToVersion);

        //    Assert.AreEqual("xml", converter.OldProjectFileExtension);
        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\TESTING.xml", converter.OldProjectFilePath);
        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject", converter.OldProjectFolder);
        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject", converter.OldCodeFolder);
        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject", converter.OldCodeIndexFolder);
        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\TESTING.xml", converter.OldCodeIndexFilePath);

        //    Assert.AreEqual("codecat", converter.NewProjectFileExtension);
        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\TESTING.codecat", converter.NewProjectFilePath);
        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject", converter.NewProjectFolder);
        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\code", converter.NewCodeFolder);
        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\code", converter.NewCodeIndexFolder);
        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\code\_code.xml", converter.NewCodeIndexFilePath);

        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\_temp", converter.TempProjectFolder);
        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\_temp\code", converter.TempCodeFolder);
        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\_temp\code\_code.xml", converter.TempCodeIndexFilePath);
        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\_temp\TESTING.codecat", converter.TempProjectFilePath);


        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\_backup", converter.BackupProjectFolder);
        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\_backup\code", converter.BackupCodeFolder);
        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\_backup\TESTING.xml", converter.BackupCodeIndexFilePath);
        //    Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\_backup\TESTING.xml", converter.BackupProjectFilePath);
        //}
    }
}
