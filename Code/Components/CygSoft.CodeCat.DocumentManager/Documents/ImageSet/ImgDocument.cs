using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Documents.ImageSet
{
    public class ImgDocument :  BaseDocument, IImgDocument
    {
        internal ImgDocument(string folder, string extension)
            : base(new DocumentPathGenerator(folder, extension), "", null)
        {
            this.DocumentType = DocumentFactory.GetDocumentType(DocumentTypeEnum.ImageDocument);
        }

        internal ImgDocument(string folder, string id, string extension, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, extension, id), "", description, ordinal)
        {
            this.DocumentType = DocumentFactory.GetDocumentType(DocumentTypeEnum.ImageDocument);
        }

        protected override IFileVersion NewVersion(DateTime timeStamp, string description)
        {
            return null;
            //throw new NotImplementedException();
        }

        protected override void OpenFile()
        {
            //throw new NotImplementedException();
        }

        protected override void SaveFile()
        {
            //throw new NotImplementedException();
        }

        public DateTime DateCreated
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTime DateModified
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
