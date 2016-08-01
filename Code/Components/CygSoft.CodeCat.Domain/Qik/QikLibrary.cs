using CygSoft.CodeCat.Domain.Base;
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
            : base(new QikKeywordSearchIndexRepository("CodeCat_QikIndex"), "qik")
        {
            base.FileExtension = "*.xml";
        }

        protected override IPersistableTarget CreateSpecializedTarget(IKeywordIndexItem indexItem)
        {
            QikKeywordIndexItem qikIndexItem = indexItem as QikKeywordIndexItem;
            QikFile qikFile = new QikFile(qikIndexItem, this.FolderPath);

            if (this.openFiles == null)
                this.openFiles = new Dictionary<string, IPersistableTarget>();

            this.openFiles.Add(qikFile.Id, qikFile);

            qikFile.Open();

            return qikFile as IPersistableTarget;
        }

        protected override IPersistableTarget OpenSpecializedTarget(IKeywordIndexItem indexItem)
        {
            QikKeywordIndexItem qikIndexItem = indexItem as QikKeywordIndexItem;
            IPersistableTarget persistableFile;

            if (this.openFiles == null)
                this.openFiles = new Dictionary<string, IPersistableTarget>();

            // first check to see if the file exists..
            if (this.openFiles.ContainsKey(qikIndexItem.Id))
            {
                persistableFile = this.openFiles[qikIndexItem.Id];
            }

            else
            {
                // retrieve the file and add it to the opened code files.
                persistableFile = new QikFile(qikIndexItem, this.FolderPath);

                if (this.openFiles == null)
                    this.openFiles = new Dictionary<string, IPersistableTarget>();
                this.openFiles.Add(persistableFile.Id, persistableFile);

                persistableFile.Open();
            }

            return persistableFile;
        }
    }
}
