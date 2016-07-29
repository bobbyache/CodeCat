using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
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
        public CodeLibrary()
            : base(new CodeKeywordSearchIndexRepository("CodeCat_CodeIndex"), "code")
        {
            base.FileExtension = "*.xml";
        }

        public override IPersistableFile CreateFile(string title, string syntax)
        {
            CodeFile codeFile = new CodeFile(new CodeKeywordIndexItem(), this.FolderPath);
            codeFile.Title = title;
            codeFile.Syntax = syntax;

            if (this.openFiles == null)
                this.openFiles = new Dictionary<string, IPersistableFile>();

            this.openFiles.Add(codeFile.Id, codeFile);
            codeFile.ContentSaved += File_ContentSaved;

            return codeFile as IPersistableFile;
        }

        public override IPersistableFile OpenFile(IKeywordIndexItem indexItem)
        {
            IPersistableFile persistableFile;

            if (this.openFiles == null)
                this.openFiles = new Dictionary<string, IPersistableFile>();

            // first check to see if the file exists..
            if (this.openFiles.ContainsKey(indexItem.Id))
            {
                persistableFile = this.openFiles[indexItem.Id];
            }

            else
            {
                // retrieve the file and add it to the opened code files.
                persistableFile = new CodeFile(indexItem, this.FolderPath);

                if (this.openFiles == null)
                    this.openFiles = new Dictionary<string, IPersistableFile>();
                this.openFiles.Add(persistableFile.Id, persistableFile);

                persistableFile.ContentSaved += File_ContentSaved;
            }

            return persistableFile;
        }
    }
}
