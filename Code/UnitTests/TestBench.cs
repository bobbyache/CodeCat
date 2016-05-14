using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CygSoft.CodeCat.ProjectConverter;
using System.Xml;
using System.Xml.Linq;

namespace UnitTestFile
{
    [TestClass]
    [DeploymentItem(@"Files\ProjectFiles\IndexItem V1.xml")]
    [DeploymentItem(@"Files\ProjectFiles\IndexItem V2.xml")]
    [DeploymentItem(@"Files\ProjectFiles\CodeIndex V1.xml")]
    [DeploymentItem(@"Files\ProjectFiles\CodeIndex V2.xml")]
    [DeploymentItem(@"Files\ProjectFiles\CodeFile V1.xml")]
    [DeploymentItem(@"Files\ProjectFiles\CodeFile V2.xml")]
    [DeploymentItem(@"Files\ProjectFiles\ProjectFile V2.xml")]
    public class TestBench
    {
        private string indexItem_v1_Path = "IndexItem V1.xml";
        private string indexItem_v2_Path = "IndexItem V2.xml";
        private string codeIndex_v1_Path = "CodeIndex V1.xml";
        private string codeIndex_v2_Path = "CodeIndex V2.xml";
        private string codeFile_v1_Path = "CodeFile V1.xml";
        private string codeFile_v2_Path = "CodeFile V2.xml";
        private string projectFile_v2_Path = "ProjectFile V2.xml";

        [TestInitialize]
        public void TestInitialize()
        {
        }


        ///*
        // * Used this to test the whole import procedure.
        // * It's not really a test but it is somewhere to start
        // * If experience problems here, will need to set up new
        // * tests on "Converter" as it isn't currently covered!
        // * */
        //[TestMethod]
        //public void Version1_to_Version2_Convert_Project()
        //{
        //    Converter converter = new Converter();
        //    converter.Convert(@"D:\Temporary\CodeCat\TestConversion\TESTINGPROJECTFILE.xml", "1", "2");
        //}

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


        /*
         * Relegated to private, so not really testable. If needs to be tested,
         * must change the method scope from private to public for the duration
         * of the tests, don't forget to change it back!
         * */

        //[TestMethod]
        //public void Version1_to_Version2_Convert_IndexItem()
        //{
        //    XElement oldElement = XElement.Load(indexItem_v1_Path);
        //    XElement compareElement = XElement.Load(indexItem_v2_Path);

        //    CygSoft.CodeCat.ProjectConverter.ToVersion2 convertToVersion2 = new ToVersion2();
        //    XElement newElement = convertToVersion2.ConvertIndex(oldElement);

        //    Assert.AreEqual(compareElement.ToString(), newElement.ToString());

        //}
    }
}
