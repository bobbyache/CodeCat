using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Code.Base
{
    internal class CodeLibrary : BaseLibrary
    {
        private SyntaxRepository syntaxRepository = null;

        public string SyntaxFolderPath { get; set; }

        public CodeLibrary()
            : base(new CodeFileKeywordIndexRepository())
        {
            base.FileExtension = "*.xml";
        }

        public SyntaxFile[] GetSyntaxFiles()
        {
            if (!string.IsNullOrEmpty(this.SyntaxFolderPath))
            {
                if (this.syntaxRepository == null)
                    this.syntaxRepository = new SyntaxRepository(this.SyntaxFolderPath);
                return syntaxRepository.SyntaxFiles;
            }
            return new SyntaxFile[0];
        }

        public string[] GetLanguages()
        {
            if (!string.IsNullOrEmpty(this.SyntaxFolderPath))
            {
                if (this.syntaxRepository == null)
                    this.syntaxRepository = new SyntaxRepository(this.SyntaxFolderPath);
                return syntaxRepository.Languages;
            }
            return new string[0];
        }

        public string GetSyntaxFile(string syntax)
        {
            string fpath = string.Empty;

            if (!string.IsNullOrEmpty(syntax) && !string.IsNullOrEmpty(this.SyntaxFolderPath))
            {
                if (this.syntaxRepository == null)
                    this.syntaxRepository = new SyntaxRepository(this.SyntaxFolderPath);
                fpath = this.syntaxRepository[syntax].FilePath;
            }
            return fpath;
        }

        protected override IPersistableFile CreateFile(IKeywordIndexItem indexItem)
        {
            CodeFile codeFile = new CodeFile(indexItem, this.LibraryFolderPath);
            return codeFile;
        }
    }
}
