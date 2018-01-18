using LanguageEngine.Tests.UnitTests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qik.LanguageEngine.UnitTests.Functions
{
    [TestFixture]
    [Category("Qik")]
    [Category("Qik.Functions")]
    [Category("Tests.UnitTests")]
    public class GuidFunctionTests
    {
        [Test]
        public void GuidFunction_Returns_NewGuid()
        {
            string funcText = $"guid(\"\")";
            string output = TestHelpers.EvaluateCompilerFunction(funcText);
            Guid guid = new Guid(output);
        }
    }
}
