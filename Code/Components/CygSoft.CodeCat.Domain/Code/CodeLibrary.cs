﻿using CygSoft.CodeCat.Domain.Base;
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

        protected override IPersistableTarget CreateSpecializedTarget(IKeywordIndexItem indexItem)
        {
            CodeKeywordIndexItem codeIndexItem = indexItem as CodeKeywordIndexItem;
            CodeFile codeFile = new CodeFile(codeIndexItem, this.FolderPath);

            if (this.openFiles == null)
                this.openFiles = new Dictionary<string, IPersistableTarget>();

            this.openFiles.Add(codeFile.Id, codeFile);

            codeFile.Open();

            return codeFile as IPersistableTarget;
        }

        protected override IPersistableTarget OpenSpecializedTarget(IKeywordIndexItem indexItem)
        {
            CodeKeywordIndexItem codeIndexItem = indexItem as CodeKeywordIndexItem;
            IPersistableTarget persistableFile;

            if (this.openFiles == null)
                this.openFiles = new Dictionary<string, IPersistableTarget>();

            // first check to see if the file exists..
            if (this.openFiles.ContainsKey(codeIndexItem.Id))
            {
                persistableFile = this.openFiles[codeIndexItem.Id];
            }

            else
            {
                // retrieve the file and add it to the opened code files.
                persistableFile = new CodeFile(codeIndexItem, this.FolderPath);

                if (this.openFiles == null)
                    this.openFiles = new Dictionary<string, IPersistableTarget>();
                this.openFiles.Add(persistableFile.Id, persistableFile);

                persistableFile.Open();
            }

            return persistableFile;
        }
    }
}
