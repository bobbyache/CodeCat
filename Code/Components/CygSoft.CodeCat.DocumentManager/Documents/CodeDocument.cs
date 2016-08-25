﻿using CygSoft.CodeCat.DocumentManager.Infrastructure;
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

        internal CodeDocument(string id, string title, string extension, string syntax) : base(id, title, extension)
        {
            this.Syntax = syntax;
        }

        internal CodeDocument(string id, string title, string extension, int ordinal, string description, string syntax)
            : base(id, title, extension, ordinal, description)
        {
            this.Syntax = syntax;
        }
    }
}
