using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class RichTextDocument : BaseDocument, IRichTextDocument
    {
        // YOU MIGHT NOT NEED TO INHERIT FROM BASEDOCUMENT HERE BECAUSE YOU ACTUALLY DON'T WRITE THE 
        // THE DOCUMENT (However some events etc. might be used further upstream.
        // Something to look into when you have time.
        // Perhaps you need to look at IFile which implemented by BaseFile... this contains all the events etc. And core methods and properties.

        public event EventHandler RequestSaveRtf;

        internal RichTextDocument(string folder, string title)
            : base(new DocumentPathGenerator(folder, "rtf"), title, null)
        {
            this.DocumentType = DocumentFactory.GetDocumentType(DocumentTypeEnum.RichTextDocument);
        }

        internal RichTextDocument(string folder, string id, string title, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, "rtf", id), title, description, ordinal)
        {
            this.DocumentType = DocumentFactory.GetDocumentType(DocumentTypeEnum.RichTextDocument);
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
            // Delegate the saving of this file to the caller.
            if (RequestSaveRtf != null)
                RequestSaveRtf(this, new EventArgs());
        }



        //private string NewDocumentRtf()
        //{
        //    string rtfText
        //    {\rtf1\ansi\ansicpg1252\deff0\deflang7177{\fonttbl{\f0\fnil\fcharset0 Calibri;}}
        //    {\*\generator Msftedit 5.41.21.2510;}\viewkind4\uc1\pard\sa200\sl276\slmult1\lang9\f0\fs22\par
        //    }
        //}
    }
}
