﻿using CygSoft.CodeCat.Qik.LanguageEngine;
using CygSoft.CodeCat.Qik.LanguageEngine.Funcs;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;
using CygSoft.CodeCat.Qik.LanguageEngine.Symbols;
using LanguageEngine.Tests.UnitTests.Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace LanguageEngine.Tests.UnitTests.Functions
{
    [TestFixture]
    [Category("Qik")]
    [Category("Qik.Functions")]
    [Category("Tests.UnitTests")]
    public class AbbreviationFunctionTests
    {
        [Test]
        public void Should_Abbreviate_Empty_String_To_Empty_String()
        {
            var text = "";
            var funcText = $"abbreviate(\"{text}\")";
            var output = TestHelpers.EvaluateCompilerFunction(funcText);
            Assert.AreEqual(null, output);
        }

        [Test]
        public void Should_Abbreviate_White_Space_To_Empty_String()
        {
            var text = " ";
            var funcText = $"abbreviate(\"{text}\")";
            var output = TestHelpers.EvaluateCompilerFunction(funcText);
            Assert.AreEqual(null, output);
        }

        [Test]
        public void Should_Abbreviate__UpperCase_Text_With_Spaces()
        {
            var text = @"Hello World";
            var funcText = $"abbreviate(\"{text}\")";
            var output = TestHelpers.EvaluateCompilerFunction(funcText);
            Assert.AreEqual(@"HW", output);
        }

        [Test]
        public void Should_Abbreviate_LowerCase_Text_With_Spaces()
        {
            var text = @"hello world";
            var funcText = $"abbreviate(\"{text}\")";
            var output = TestHelpers.EvaluateCompilerFunction(funcText);
            Assert.AreEqual(@"hw", output);
        }

        [Test]
        public void Should_Abbreviate_LowerCase_ProperCase_Text_With_No_Spaces()
        {
            var text = @"HelloWorld";
            var funcText = $"abbreviate(\"{text}\")";
            var output = TestHelpers.EvaluateCompilerFunction(funcText);
            Assert.AreEqual(@"HW", output);
        }

        [Test]
        public void Should_Abbreviate_Using_Underscores()
        {
            var text = @"hello_world";
            var funcText = $"abbreviate(\"{text}\")";
            var output = TestHelpers.EvaluateCompilerFunction(funcText);
            Assert.AreEqual(@"hw", output);
        }

        [Test]
        public void Should_Abbreviate_LowerCase_Single_Word_With_LowerCase_Start_Letter()
        {
            var text = @"hello";
            var funcText = $"abbreviate(\"{text}\")";
            var output = TestHelpers.EvaluateCompilerFunction(funcText);
            Assert.AreEqual(@"h", output);
        }


        [Test]
        public void Should_Abbreviate_LowerCase_Single_Word_With_UpperCase_Start_Letter()
        {
            var text = @"World";
            var funcText = $"abbreviate(\"{text}\")";
            var output = TestHelpers.EvaluateCompilerFunction(funcText);
            Assert.AreEqual(@"W", output);
        }
    }
}
