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
        public static ITextDocument CreateTextDocument(string folder, string title, string extension)
        {
            return new TextDocument(folder, title, extension);
        }

        public static ITextDocument CreateTextDocument(string folder, string id, string title, string extension, int ordinal, string description)
        {
            return new TextDocument(folder, id, title, extension, ordinal, description);
        }

        public static ICodeDocument CreateCodeDocument(string folder, string title, string extension, string syntax)
        {
            return new CodeDocument(folder, title, extension, syntax);
        }

        public static ICodeDocument CreateCodeDocument(string folder, string id, string title, string extension, string syntax, int ordinal, string description)
        {
            return new CodeDocument(folder, id, title, extension, ordinal, description, syntax);
        }

        public static ICodeDocument CreateQikScriptDocument(string folder, string title, string extension, string syntax)
        {
            return new QikScriptDocument(folder, title, extension, syntax);
        }

        public static ICodeDocument CreateQikScriptDocument(string folder, string id, string title, string extension, string syntax, int ordinal, string description)
        {
            return new QikScriptDocument(folder, id, title, extension, ordinal, description, syntax);
        }
    }
}
