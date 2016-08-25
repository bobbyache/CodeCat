using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class QikScriptDocument : CodeDocument
    {
        internal QikScriptDocument(string id, string title, string extension, string syntax) : base(id, title, extension, syntax)
        {
        }

        internal QikScriptDocument(string id, string title, string extension, int ordinal, string description, string syntax)
            : base(id, title, extension, ordinal, description, syntax)
        {
        }
    }
}
