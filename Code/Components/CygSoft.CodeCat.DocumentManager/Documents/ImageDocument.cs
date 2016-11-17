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
    public class ImageDocument : BaseDocument, IImageDocument
    {
        // YOU MIGHT NOT NEED TO INHERIT FROM BASEDOCUMENT HERE BECAUSE YOU ACTUALLY DON'T WRITE THE 
        // THE DOCUMENT (However some events etc. might be used further upstream.
        // Something to look into when you have time.
        // Perhaps you need to look at IFile which implemented by BaseFile... this contains all the events etc. And core methods and properties.

        internal ImageDocument(string folder, string title, string extension)
            : base(new DocumentPathGenerator(folder, extension), title, null)
        {
            this.DocumentType = DocumentFactory.GetDocumentType(DocumentTypeEnum.ImageDocument);
        }

        internal ImageDocument(string folder, string id, string title, string extension, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, extension, id), title, description, ordinal)
        {
            this.DocumentType = DocumentFactory.GetDocumentType(DocumentTypeEnum.ImageDocument);
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
