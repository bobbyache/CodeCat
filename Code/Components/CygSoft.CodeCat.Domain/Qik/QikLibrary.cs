using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using CygSoft.CodeCat.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Qik
{
    internal class QikLibrary : BaseLibrary
    {
        public QikLibrary()
            : base(new QikKeywordSearchIndexRepository("CodeCat_CodeIndex"), "qik")
        {
            base.FileExtension = "*.xml";
        }

        public override IPersistableFile CreateFile(string title, string syntax)
        {
            CodeFile codeFile = new CodeFile(new QikKeywordIndexItem(), this.FolderPath);
            codeFile.Title = title;
            codeFile.Syntax = syntax;

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
