using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class CodeDocument : TextDocument, ICodeDocument
    {
        public string Syntax { get; set; }

        internal CodeDocument(string id, string title, string description = null, string extension = "cde")
            : base(id, title, description, extension)
        {
            this.Syntax = null;
        }

        internal CodeDocument(string id, string title, string description = null, string text = null, string extension = "cde", string syntax = null)
            : base(id, title, description, text, extension)
        {
            this.Syntax = syntax;

        }
        internal CodeDocument(string id, string title, int ordinal, string description = null, string extension = "cde")
            : base(id, title, ordinal, description, extension)
        {
            this.Syntax = null;
        }

        internal CodeDocument(string id, string title, int ordinal, string description = null, string text = null, string extension = "cde", string syntax = null)
            : base(id, title, ordinal, description, extension)
        {
            this.Syntax = syntax;
        }
    }
}
