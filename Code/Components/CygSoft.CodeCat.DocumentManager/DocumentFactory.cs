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

        public static ITextDocument CreateTextDocument(string id, string title, string description = null, string extension = null)
        {
            return new TextDocument(id, title, description, extension);
        }

        public static ITextDocument CreateTextDocument(string id, string title, string description = null, string text = null, string extension = null)
        {
            return new TextDocument(id, title, description, text, extension);
        }

        public static ITextDocument CreateTextDocument(string id, string title, int ordinal, string description = null, string extension = null)
        {
            return new TextDocument(id, title, ordinal, description, extension);
        }

        public static ITextDocument CreateTextDocument(string id, string title, int ordinal, string description = null, string text = null, string extension = null)
        {
            return new TextDocument(id, title, ordinal, description, text, extension);
        }

        public static ICodeDocument CreateCodeDocument(string id, string title, string description = null, string extension = null)
        {
            return new CodeDocument(id, title, description, extension);
        }

        public static ICodeDocument CreateCodeDocument(string id, string title, string description = null, string text = null, string extension = null)
        {
            return new CodeDocument(id, title, description, text, extension);
        }

        public static ICodeDocument CreateCodeDocument(string id, string title, int ordinal, string description = null, string extension = null, string syntax = null)
        {
            return new CodeDocument(id, title, ordinal, description, extension, syntax);
        }

        public static ICodeDocument CreateCodeDocument(string id, string title, int ordinal, string description = null, string text = null, string extension = null, string syntax = null)
        {
            return new CodeDocument(id, title, ordinal, description, text, extension, syntax);
        }
    }
}
