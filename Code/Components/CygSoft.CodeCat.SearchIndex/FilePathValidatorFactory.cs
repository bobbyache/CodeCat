using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Search.KeywordIndex
{
    class FilePathValidatorFactory
    {
        private static IKeywordSearchIndexFileValidator fileValidator = null;

        public static IKeywordSearchIndexFileValidator Create()
        {
            if (fileValidator != null)
                return fileValidator;
            return new FilePathValidator();
        }

        internal static void SetValidator(IKeywordSearchIndexFileValidator keywordSearchIndexFileValidator)
        {
            fileValidator = keywordSearchIndexFileValidator;
        }
    }
}
