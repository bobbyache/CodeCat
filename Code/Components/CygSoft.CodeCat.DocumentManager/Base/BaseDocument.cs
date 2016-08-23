using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Base
{
    public abstract class BaseDocument : BaseVersionableFile, IDocument
    {
        public int Ordinal { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public BaseDocument(string id, string fileExtension, string title, string description = null) : base(fileExtension)
        {
            base.Id = id;
            this.Ordinal = -1;
            this.Title = title;
            this.Description = description;
        }

        public BaseDocument(string id, string fileExtension, int ordinal, string title, string description = null)
            : base(fileExtension)
        {
            base.Id = id;
            this.Ordinal = ordinal;
            this.Title = title;
            this.Description = description;
        }

        protected override void DeleteFile()
        {
            base.DeleteFile();
            this.Ordinal = -1;
        }
    }
}
