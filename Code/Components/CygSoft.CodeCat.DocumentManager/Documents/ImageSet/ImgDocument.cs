using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Documents.ImageSet
{
    public class ImgDocument :  BaseDocument, IImgDocument
    {
        private ImagePathGenerator imagePathGenerator;

        public bool IsModified
        {
            // if the modified "temp" file exists, then we know that this image document is
            // in a modified state.
            get { return File.Exists(this.imagePathGenerator.ModifiedFilePath); }
        }

        public string DisplayFilePath
        {
            get
            {
                if (File.Exists(this.imagePathGenerator.ModifiedFilePath))
                    return this.imagePathGenerator.ModifiedFilePath;
                else if (File.Exists(this.FilePath))
                    return this.FilePath;
                else
                    return null;
            }
        }

        public string ModifyFilePath
        {
            get { return this.imagePathGenerator.ModifiedFilePath; }
        }

        internal ImgDocument(ImagePathGenerator imagePathGenerator)
            : base(imagePathGenerator, "", null)
        {
            this.imagePathGenerator = imagePathGenerator;
            this.DocumentType = DocumentFactory.GetDocumentType(DocumentTypeEnum.ImageDocument);
        }

        internal ImgDocument(ImagePathGenerator imagePathGenerator, int ordinal, string description)
            : base(imagePathGenerator, "", description, ordinal)
        {
            this.imagePathGenerator = imagePathGenerator;
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

        // we are saving the file... the image saving component will write to the ModifiedFilePath
        // here, we need to copy the modified file to the saved file and then delete the modified
        // file.
        protected override void SaveFile()
        {
            if (this.IsModified)
            {
                File.Copy(this.imagePathGenerator.ModifiedFilePath, this.FilePath, true);
                DeleteTemporaryFile();
            }
        }

        protected override void OnBeforeRevert()
        {
            DeleteTemporaryFile();
            base.OnBeforeRevert();
        }

        protected override void OnBeforeDelete()
        {
            // Need to delete the temporary file if it exists as well as this file.
            DeleteTemporaryFile();
            base.OnBeforeDelete();
        }

        private void DeleteTemporaryFile()
        {
            if (File.Exists(this.imagePathGenerator.ModifiedFilePath))
                File.Delete(this.imagePathGenerator.ModifiedFilePath);
        }
    }
}
