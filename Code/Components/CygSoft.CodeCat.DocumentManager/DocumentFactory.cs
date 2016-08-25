using CygSoft.CodeCat.DocumentManager.Documents;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager
{
    public class DocumentFactory
    {
        public static ITextDocument CreateTextDocument(string title, string extension)
        {
            string id = Guid.NewGuid().ToString();
            return new TextDocument(id, title, extension);
        }

        public static ITextDocument CreateTextDocument(string id, string title, string extension, int ordinal, string description)
        {
            return new TextDocument(id, title, extension, ordinal, description);
        }

        public static ICodeDocument CreateCodeDocument(string title, string extension, string syntax)
        {
            string id = Guid.NewGuid().ToString();
            return new CodeDocument(id, title, extension, syntax);
        }

        public static ICodeDocument CreateCodeDocument(string id, string title, string extension, string syntax, int ordinal, string description)
        {
            return new CodeDocument(id, title, extension, ordinal, description, syntax);
        }

        public static ICodeDocument CreateQikScriptDocument(string title, string extension, string syntax)
        {
            string id = Guid.NewGuid().ToString();
            return new QikScriptDocument(id, title, extension, syntax);
        }

        public static ICodeDocument CreateQikScriptDocument(string id, string title, string extension, string syntax, int ordinal, string description)
        {
            return new QikScriptDocument(id, title, extension, ordinal, description, syntax);
        }
    }
}
