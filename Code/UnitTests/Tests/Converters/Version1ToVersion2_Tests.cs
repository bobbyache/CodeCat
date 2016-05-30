using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CygSoft.CodeCat.ProjectConverter;
using System.Xml.Linq;

namespace UnitTests.Tests.Converters
{
    [TestClass]
    [DeploymentItem(@"Files\ProjectFiles\IndexItem V1.xml")]
    [DeploymentItem(@"Files\ProjectFiles\IndexItem V2.xml")]
    [DeploymentItem(@"Files\ProjectFiles\CodeIndex V1.xml")]
    [DeploymentItem(@"Files\ProjectFiles\CodeIndex V2.xml")]
    [DeploymentItem(@"Files\ProjectFiles\CodeFile V1.xml")]
    [DeploymentItem(@"Files\ProjectFiles\CodeFile V2.xml")]
    [DeploymentItem(@"Files\ProjectFiles\ProjectFile V2.xml")]
    public class Version1ToVersion2_Tests
    {

        private string codeIndex_v1_Path = "CodeIndex V1.xml";
        private string codeIndex_v2_Path = "CodeIndex V2.xml";
        private string codeFile_v1_Path = "CodeFile V1.xml";
        private string codeFile_v2_Path = "CodeFile V2.xml";
        private string projectFile_v2_Path = "ProjectFile V2.xml";

        /////*
        //// * Used this to test the whole import procedure.
        //// * It's not really a test but it is somewhere to start
        //// * If experience problems here, will need to set up new
        //// * tests on "Converter" as it isn't currently covered!
        //// * */
        //[TestMethod]
        //public void Version1_to_Version2_Convert_Project()
        //{
        //    ToVersion2Converter converter = new ToVersion2Converter(@"D:\Temporary\CodeCat\TestRepo III\TEST_ALL_HITCOUNT.codecat");
        //    converter.Convert();
        //}


        [TestMethod]
        public void Version1_to_Version2_ConverterProperties()
        {
            ToVersion2Converter converter = new ToVersion2Converter(@"D:\Temporary\CodeCat\TestConversion\OldProject\TESTING.xml");

            Assert.AreEqual("1", converter.FromVersion);
            Assert.AreEqual("2", converter.ToVersion);

            Assert.AreEqual("xml", converter.OldProjectFileExtension);
            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\TESTING.xml", converter.OldProjectFilePath);
            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject", converter.OldProjectFolder);
            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject", converter.OldCodeFolder);
            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject", converter.OldCodeIndexFolder);
            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\TESTING.xml", converter.OldCodeIndexFilePath);

            Assert.AreEqual("codecat", converter.NewProjectFileExtension);
            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\TESTING.codecat", converter.NewProjectFilePath);
            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject", converter.NewProjectFolder);
            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\code", converter.NewCodeFolder);
            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\code", converter.NewCodeIndexFolder);
            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\code\_code.xml", converter.NewCodeIndexFilePath);

            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\_temp", converter.TempProjectFolder);
            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\_temp\code", converter.TempCodeFolder);
            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\_temp\code\_code.xml", converter.TempCodeIndexFilePath);
            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\_temp\TESTING.codecat", converter.TempProjectFilePath);


            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\_backup", converter.BackupProjectFolder);
            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\_backup\code", converter.BackupCodeFolder);
            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\_backup\TESTING.xml", converter.BackupCodeIndexFilePath);
            Assert.AreEqual(@"D:\Temporary\CodeCat\TestConversion\OldProject\_backup\TESTING.xml", converter.BackupProjectFilePath);
        }

        [TestMethod]
        public void Version1_to_Version2_Create_ProjectFile()
        {
            XDocument expectedDocument = XDocument.Load(projectFile_v2_Path);

            CygSoft.CodeCat.ProjectConverter.ToVersion2 convertToVersion2 = new ToVersion2();
            XDocument document = convertToVersion2.CreateProjectDocument();

            Assert.AreEqual(expectedDocument.ToString(), document.ToString());
            Assert.AreEqual(expectedDocument.Declaration.ToString(), document.Declaration.ToString());
        }

        [TestMethod]
        public void Version1_to_Version2_CreateCodeIndexFile()
        {
            XDocument expectedDocument = XDocument.Load(codeIndex_v2_Path);

            CygSoft.CodeCat.ProjectConverter.ToVersion2 converterVersion2 = new ToVersion2();
            XDocument document = converterVersion2.CreateCodeIndexDocument(codeIndex_v1_Path);

            Assert.AreEqual(expectedDocument.ToString(), document.ToString());
            Assert.AreEqual(expectedDocument.Declaration.ToString(), document.Declaration.ToString());
        }

        [TestMethod]
        public void Version1_to_Version2_Convert_CodeFile()
        {
            XDocument expectedDocument = XDocument.Load(codeFile_v2_Path);
            XDocument oldIndexFile = XDocument.Load(codeIndex_v1_Path);

            CygSoft.CodeCat.ProjectConverter.ToVersion2 converterVersion2 = new ToVersion2();
            XDocument document = converterVersion2.ConvertCodeFile(codeFile_v1_Path, oldIndexFile);

            Assert.AreEqual(expectedDocument.ToString(), document.ToString());
            Assert.AreEqual(expectedDocument.Declaration.ToString(), document.Declaration.ToString());
        }
    }
}
