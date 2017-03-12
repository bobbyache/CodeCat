using CygSoft.CodeCat.DocumentManager;
using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Documents;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.UnitTests
{
    [TestFixture]
    public class TestBench
    {
        //[Test]
        //public void CodeDocument_Open_FiresOnBeforeOpen()
        //{
        //    string title = "File Title";
        //    string description = "Description";
        //    string syntax = "C#";

        //    bool called = false;

        //    CodeDocument codeDocument = new CodeDocument(title, description, syntax, new StubFilePathGenerator());
        //    codeDocument.BeforeOpen += (s, e) => called = true;
        //    codeDocument.Open();

        //    Assert.That(called, Is.True);
        //}


        //public class StubFilePathGenerator : IBaseFilePathGenerator
        //{
        //    public bool FileExists {  get { return true;  } }

        //    public string FileExtension {  get { return "ext"; } }

        //    public string FileName {  get { return "file.ext"; } }

        //    public string FilePath {  get { return @"C:\my_path\file.ext"; } }

        //    public string FolderPath {  get { return @"C:\my_path"; } }

        //    public string Id {  get { return "4323432"; } }
        //}
    }
}
