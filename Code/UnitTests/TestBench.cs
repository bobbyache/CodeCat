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

        [TestMethod]
        public void DocumentFactory_CreateTextDocument()
        {
            ICodeDocument document1 = DocumentFactory.CreateCodeDocument(documentSimulator.DocumentFile1.Id, "Document 1", "Document 1 Description", "txt1");
            Assert.AreEqual(documentSimulator.DocumentFile1.Id, document1.Id);
            Assert.AreEqual("Document 1", document1.Title);
            Assert.AreEqual(-1, document1.Ordinal);
            Assert.AreEqual("Document 1 Description", document1.Description);
            Assert.AreEqual(null, document1.Text);
            Assert.AreEqual(null, document1.Syntax);
            Assert.AreEqual("txt1", document1.FileExtension);
            Assert.AreEqual(false, document1.Loaded);

            ICodeDocument document2 = DocumentFactory.CreateCodeDocument(documentSimulator.DocumentFile1.Id, "Document 2", "Document 2 Description", "Document 2 Text", "txt2");
            Assert.AreEqual(documentSimulator.DocumentFile1.Id, document2.Id);
            Assert.AreEqual("Document 2", document2.Title);
            Assert.AreEqual(-1, document2.Ordinal);
            Assert.AreEqual("Document 2 Description", document2.Description);
            Assert.AreEqual("Document 2 Text", document2.Text);
            Assert.AreEqual(null, document1.Syntax);
            Assert.AreEqual("txt2", document2.FileExtension);
            Assert.AreEqual(false, document2.Loaded);

            ICodeDocument document3 = DocumentFactory.CreateCodeDocument(documentSimulator.DocumentFile1.Id, "Document 3", 2, "Document 3 Description", "txt1", "C#");
            Assert.AreEqual(documentSimulator.DocumentFile1.Id, document3.Id);
            Assert.AreEqual("Document 3", document3.Title);
            Assert.AreEqual(2, document3.Ordinal);
            Assert.AreEqual("Document 3 Description", document3.Description);
            Assert.AreEqual("Document 3 Text", document3.Text);
            Assert.AreEqual("C#", document3.Syntax);
            Assert.AreEqual("txt1", document3.FileExtension);
            Assert.AreEqual(false, document3.Loaded);

            ICodeDocument document4 = DocumentFactory.CreateCodeDocument(documentSimulator.DocumentFile1.Id, "Document 4", 3, "Document 4 Description", "Document 4 Text", "txt2", "C#");
            Assert.AreEqual(documentSimulator.DocumentFile1.Id, document4.Id);
            Assert.AreEqual("Document 4", document4.Title);
            Assert.AreEqual(3, document4.Ordinal);
            Assert.AreEqual("Document 4 Description", document4.Description);
            Assert.AreEqual("Document 4 Text", document4.Text);
            Assert.AreEqual("C#", document4.Syntax);
            Assert.AreEqual("txt2", document4.FileExtension);
            Assert.AreEqual(false, document4.Loaded);
        }
    }
}
