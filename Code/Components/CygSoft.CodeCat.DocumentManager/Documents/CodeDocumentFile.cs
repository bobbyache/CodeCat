using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class CodeDocumentFile : TextDocumentFile
    {
        public string Syntax { get; set; }

        internal CodeDocumentFile(string id, string title, string description = null, string extension = "cde")
            : base(id, extension, title, description)
        {
            this.Syntax = null;
        }

        internal CodeDocumentFile(string id, string title, int ordinal, string description = null, string extension = "cde")
            : base(id, extension, ordinal, title, description)
        {
            this.Syntax = null;
        }

        internal CodeDocumentFile(string id, string title, string description = null, string text = null, string extension = "cde", string syntax = null)
            : base(id, extension, title, description)
        {
            this.Syntax = syntax;
        }

        internal CodeDocumentFile(string id, string title, int ordinal, string description = null, string text = null, string extension = "cde", string syntax = null)
            : base(id, extension, ordinal, title, description)
        {
            this.Syntax = syntax;
        }
    }
}
