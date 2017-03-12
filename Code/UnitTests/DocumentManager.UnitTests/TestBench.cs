using CygSoft.CodeCat.DocumentManager;
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
        [Test]
        public void CodeDocument_Test()
        {
            CodeDocument codeDocument = DocumentFactory.Create(CygSoft.CodeCat.DocumentManager.Infrastructure.DocumentTypeEnum.CodeSnippet);
            //codeDocument.Open()

            Assert.Fail("This is a test method that must fail.");
        }
    }
}
