using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UnitTestFile
{
    [TestClass]
    [DeploymentItem(@"Files\ProjectFiles\CodeIndex V1.xml")]
    [DeploymentItem(@"Files\ProjectFiles\CodeIndex V2.xml")]
    public class TestBench
    {
        //[TestMethod]
        //public void MyTest()
        //{
        //    //Assert.IsTrue(File.Exists(@"helloworld.txt"));

        //    string version1_Path = "CodeIndex V1.xml";
        //    string version2_Path = "CodeIndex V2.xml";

        //    using (StreamReader version1Reader = new StreamReader(version1_Path))
        //    {
        //        string text = version1Reader.ReadToEnd();
        //    }


        //    using (StreamReader version2Reader = new StreamReader(version2_Path))
        //    {
        //        string text = version2Reader.ReadToEnd();
        //    }
        //}

        [TestMethod]
        public void ConvertIndexItem_Version1_to_Version2()
        {

        }
    }
}
