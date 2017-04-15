using CygSoft.CodeCat.Qik.LanguageEngine;
using CygSoft.CodeCat.Qik.LanguageEngine.Funcs;
using CygSoft.CodeCat.Qik.LanguageEngine.Scope;
using CygSoft.CodeCat.Qik.LanguageEngine.Symbols;
using NUnit.Framework;
using System.Collections.Generic;

namespace LanguageEngine.Tests.UnitTests
{
    [TestFixture]
    public class TestBench
    {
        [Test]
        public void Test_LanguageEngine_Tests_UnitTests()
        {
            Assert.IsTrue(true);
        }


        [Test]
        public void BatchCompiler_Generates_AutoInput()
        {
            BatchCompiler batchCompiler = new BatchCompiler();
            batchCompiler.CreateFieldInput("@Column1", "Column 1", "Description for Column 1");
            batchCompiler.CreateFieldInput("@Column2", "Column 1", "Description for Column 1");

            batchCompiler.Input("@Column1", "COL 1");
            batchCompiler.Input("@Column2", "COL 2");

            Generator generator = new Generator();
            string output = generator.Generate(batchCompiler, "@{Column1} is the first column, @{Column2} is the second column.");

            Assert.AreEqual("COL 1 is the first column, COL 2 is the second column.", output);

            Assert.AreEqual("Column 1", batchCompiler.GetSymbolInfo("@Column1").Title);
            Assert.AreEqual("Description for Column 1", batchCompiler.GetSymbolInfo("@Column1").Description);
        }

        [Test]
        public void Placeholder_NotAvailable_When_IsPlaceholder_False()
        {
            GlobalTable globalTable = new GlobalTable();

            List<BaseFunction> functionArguments = new List<BaseFunction>();
            functionArguments.Add(new TextFunction(new FuncInfo("stub", 1, 1), globalTable, "dd/MM/yyyy"));

            CurrentDateFunction currentDateFunction = new CurrentDateFunction(new FuncInfo("stub", 1, 1), globalTable, functionArguments);
            UpperCaseFunction upperCaseFunction = new UpperCaseFunction(new FuncInfo("stub", 1, 1), globalTable, functionArguments);


            ExpressionSymbol expressionSymbol1 = new ExpressionSymbol(new ErrorReport(), "@currentDate", "Current Date", "Description", false, true, currentDateFunction);
            ExpressionSymbol expressionSymbol2 = new ExpressionSymbol(new ErrorReport(), "@camelCase", "Camel Cased", "Description", true, true, upperCaseFunction);

            globalTable.AddSymbol(expressionSymbol1);
            globalTable.AddSymbol(expressionSymbol2);

            Assert.AreEqual(1, globalTable.Placeholders.Length);
            Assert.AreEqual("@{camelCase}", globalTable.Placeholders[0]);
            Assert.AreEqual("DD/MM/YYYY", globalTable.GetValueOfSymbol("@camelCase"));
        }

        [Test]
        public void Create_GlobalTable_TestInput()
        {
            GlobalTable globalTable = new GlobalTable();

            TextInputSymbol textInputSymbol = new TextInputSymbol(new ErrorReport(), "@authorName", "Author Name", "Description", "Rob Blake", true);

            List<BaseFunction> functionArguments = new List<BaseFunction> { new VariableFunction(new FuncInfo("stub", 1, 1), globalTable, "@authorName") };
            ExpressionSymbol expressionSymbol = new ExpressionSymbol(new ErrorReport(), "@upperAuthorName", "Upper Author Name", "Description", true, true, new UpperCaseFunction(new FuncInfo("stub", 1, 1), globalTable, functionArguments));

            globalTable.AddSymbol(textInputSymbol);
            globalTable.AddSymbol(expressionSymbol);

            string textOutput_A = globalTable.GetValueOfSymbol("@authorName");
            string exprOutput_A = globalTable.GetValueOfSymbol("@upperAuthorName");

            globalTable.Input("@authorName", "John Doe");

            string textOutput_B = globalTable.GetValueOfSymbol("@authorName");
            string exprOutput_B = globalTable.GetValueOfSymbol("@upperAuthorName");

            Assert.AreEqual("Rob Blake", textOutput_A);
            Assert.AreEqual("ROB BLAKE", exprOutput_A);

            Assert.AreEqual("John Doe", textOutput_B);
            Assert.AreEqual("JOHN DOE", exprOutput_B);
        }
    }
}
