using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class UrlGroupDocument : BaseDocument, IUrlGroupDocument
    {
        public string Text { get; set; }

        internal UrlGroupDocument(string folder, string title)
            : base(new DocumentPathGenerator(folder, "urlgrp"), title, null)
        {
            this.Text = null;
            this.DocumentType = DocumentFactory.GetDocumentType(DocumentTypeEnum.UrlGroup);
        }

        internal UrlGroupDocument(string folder, string id, string title, int ordinal, string description) : base(new DocumentPathGenerator(folder, "urlgrp", id), title, description, ordinal)
        {
            this.Text = null;
            this.DocumentType = DocumentFactory.GetDocumentType(DocumentTypeEnum.UrlGroup);
        }

        protected override IFileVersion NewVersion(DateTime timeStamp, string description)
        {
            return new TextDocumentVersion(new VersionPathGenerator(this.FilePath, timeStamp), description, this.Text);
        }

        protected override void OpenFile()
        {
            this.Text = File.ReadAllText(this.FilePath);
        }

        protected override void SaveFile()
        {
            File.WriteAllText(this.FilePath, this.Text);
        }
    }
}
