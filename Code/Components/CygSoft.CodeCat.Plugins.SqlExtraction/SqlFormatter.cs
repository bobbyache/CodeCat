using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoorMansTSqlFormatterLib.Formatters;
using PoorMansTSqlFormatterLib.Interfaces;
using PoorMansTSqlFormatterLib.Tokenizers;
using PoorMansTSqlFormatterLib.Parsers;

namespace CygSoft.CodeCat.Plugins.SqlExtraction
{
    // http://poorsql.com/
    // http://architectshack.com/PoorMansTSqlFormatter.ashx
    // https://github.com/TaoK/PoorMansTSqlFormatter
    public class SqlFormatter
    {
        private TSqlStandardFormatterOptions currentOptions;
        private ISqlTokenizer _tokenizer;
        private ISqlTokenParser _parser;
        private ISqlTreeFormatter _formatter;

        public TSqlStandardFormatterOptions CurrentOptions
        {
            get
            {
                if (currentOptions == null)
                    currentOptions = GetDefaultOptions();
                return currentOptions;
            }
        }

        public string Format(string rawSql, TSqlStandardFormatterOptions formatOptions)
        {
            _tokenizer = new TSqlStandardTokenizer();
            _parser = new TSqlStandardParser();
            _formatter = new TSqlStandardFormatter(formatOptions);
            var tokenizedSql = _tokenizer.TokenizeSQL(rawSql);
            var parsedSql = _parser.ParseSQL(tokenizedSql);
            var output = _formatter.FormatSQLTree(parsedSql);
            
            return output.Trim();
        }
        private TSqlStandardFormatterOptions GetDefaultOptions()
        {
            return new TSqlStandardFormatterOptions
            {
                IndentString = "\t",
                SpacesPerTab = 4,
                MaxLineWidth = 999,
                ExpandCommaLists = true,
                TrailingCommas = true,
                SpaceAfterExpandedComma = false,
                ExpandBooleanExpressions = true,
                ExpandCaseStatements = true,
                ExpandBetweenConditions = true,
                ExpandInLists = true,
                BreakJoinOnSections = false,
                UppercaseKeywords = true,
                HTMLColoring = false,
                KeywordStandardization = false,
                NewStatementLineBreaks = 2,
                NewClauseLineBreaks = 1
            };
        }

    }
}
