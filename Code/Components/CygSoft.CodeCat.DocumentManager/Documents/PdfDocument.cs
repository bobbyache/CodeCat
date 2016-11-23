﻿using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class PdfDocument : BaseDocument, IPdfDocument
    {
        // YOU MIGHT NOT NEED TO INHERIT FROM BASEDOCUMENT HERE BECAUSE YOU ACTUALLY DON'T WRITE THE 
        // THE DOCUMENT (However some events etc. might be used further upstream.
        // Something to look into when you have time.
        // Perhaps you need to look at IFile which implemented by BaseFile... this contains all the events etc. And core methods and properties.

        internal PdfDocument(string folder, string title)
            : base(new DocumentPathGenerator(folder, "pdf"), title, null)
        {
            this.DocumentType = DocumentFactory.GetDocumentType(DocumentTypeEnum.PdfDocument);
        }

        internal PdfDocument(string folder, string id, string title, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, "pdf", id), title, description, ordinal)
        {
            this.DocumentType = DocumentFactory.GetDocumentType(DocumentTypeEnum.PdfDocument);
        }

        protected override IFileVersion NewVersion(DateTime timeStamp, string description)
        {
            //throw new NotImplementedException();
            return null;
        }

        protected override void OpenFile()
        {
            // DON'T WANT TO IMPLEMENT THIS BECAUSE THE FILE IS NOT OPENED THE SAME WAY OTHER DOCUMENTS ARE
            // OPENED.
        }

        protected override void SaveFile()
        {
            // DON'T WANT TO IMPLEMENT THIS BECAUSE THE FILE IS NOT SAVED THE SAME WAY OTHER DOCUMENTS ARE
            // SAVED.
        }
    }
}