using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CygSoft.CodeCat.ProjectConverter;
using System.Xml;
using System.Xml.Linq;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager;
using UnitTests.Stubs.DocumentManager;
using System.Threading;
using UnitTests.Helpers.DocumentManager;

namespace UnitTestFile
{
    /* ===============================================================================================================================================
     * NEXT STEPS
     * ===============================================================================================================================================
     * 
     * You've been testing the BaseMultiDocument file.
     * 
     * You need to test and implement versioning of the BaseVersionable file (this is where you'll add your functionality).
     * You need to test that all the events work. Use the event unit tests in your code cat file "unit  testing with events".
     * 
     * You need to look at extensions. How are you going to keep the files in the index of these mult-document files. Store file name instead of
     * id? If so, you might need to look at how you instantiate the document classes ie. with an ID or a file name. But you're inheriting from a base
     * file with a single constructor?
     * 
     * You've already set up a "Documents" folder in the DocumentManager with the Template Doc stuff for Qik! You can start implementing here once
     * you're happy enough with your unit tests....
     * 
     * You should NOT have a content property since different file types will display text, images, etc. So that will be specialized.
     * */




    [TestClass]
    public class TestBench
    {
        private DocFileSimulator documentSimulator;

        [TestInitialize]
        public void InitializeTests()
        {
            documentSimulator = new DocFileSimulator();
        }

    }
}
