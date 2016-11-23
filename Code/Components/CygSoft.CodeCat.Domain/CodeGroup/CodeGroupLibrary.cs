﻿using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.CodeGroup
{
    internal class CodeGroupLibrary : BaseLibrary
    {
        public CodeGroupLibrary()
            : base(new CodeGroupKeywordSearchIndexRepository("CodeCat_CodeGroupIndex"), "codegroup")
        {
            base.FileExtension = "*.xml";
        }

        protected override IPersistableTarget CreateSpecializedTarget(IKeywordIndexItem indexItem)
        {
            CodeGroupKeywordIndexItem codeGroupIndexItem = indexItem as CodeGroupKeywordIndexItem;
            CodeGroupDocumentGroup codeGroupFile = new CodeGroupDocumentGroup(codeGroupIndexItem, this.FolderPath);

            if (this.openFiles == null)
                this.openFiles = new Dictionary<string, IPersistableTarget>();

            this.openFiles.Add(codeGroupFile.Id, codeGroupFile);

            return codeGroupFile as IPersistableTarget;
        }

        protected override IPersistableTarget OpenSpecializedTarget(IKeywordIndexItem indexItem)
        {
            CodeGroupKeywordIndexItem codeGroupIndexItem = indexItem as CodeGroupKeywordIndexItem;
            IPersistableTarget persistableFile;

            if (this.openFiles == null)
                this.openFiles = new Dictionary<string, IPersistableTarget>();

            // first check to see if the file exists..
            if (this.openFiles.ContainsKey(codeGroupIndexItem.Id))
            {
                persistableFile = this.openFiles[codeGroupIndexItem.Id];
            }

            else
            {
                // retrieve the file and add it to the opened code files.
                persistableFile = new CodeGroupDocumentGroup(codeGroupIndexItem, this.FolderPath);

                if (this.openFiles == null)
                    this.openFiles = new Dictionary<string, IPersistableTarget>();
                this.openFiles.Add(persistableFile.Id, persistableFile);

                persistableFile.Open();
            }

            return persistableFile;
        }
    }
}