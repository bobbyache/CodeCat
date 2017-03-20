using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Drawing;
using System.IO;

namespace CygSoft.CodeCat.DocumentManager.Documents.ImageSet
{
    public class ImagePagerImageTopicSection :  TopicSection, IImagePagerImageTopicSection
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

        internal ImagePagerImageTopicSection(ImagePathGenerator imagePathGenerator)
            : base(imagePathGenerator, "", null)
        {
            this.imagePathGenerator = imagePathGenerator;
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.SingleImage);
        }

        internal ImagePagerImageTopicSection(ImagePathGenerator imagePathGenerator, int ordinal, string description)
            : base(imagePathGenerator, "", description, ordinal)
        {
            this.imagePathGenerator = imagePathGenerator;
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.SingleImage);
        }

        //protected override IFileVersion NewVersion(DateTime timeStamp, string description)
        //{
        //    return null;
        //}

        protected override void OpenFile()
        {
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


        public Bitmap GetDisplayImage()
        {
            if (File.Exists(this.DisplayFilePath))
            {
                using (FileStream stream = new FileStream(this.DisplayFilePath, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    var memoryStream = new MemoryStream(reader.ReadBytes((int)stream.Length));
                    return new Bitmap(memoryStream);
                }
            }
            else
            {
                // this unfortunately creates a gradient rectangle:
                // http://stackoverflow.com/questions/12502365/how-to-create-1024x1024-rgb-bitmap-image-of-white
                //Bitmap bitmap = new Bitmap(1, 1);
                //bitmap.SetPixel(0, 0, Color.DarkGray);
                //Bitmap result = new Bitmap(bitmap, 800, 800);
                //return result;
                int width = 600, height = 400;

                Bitmap bmp = new Bitmap(width, height);
                using (Graphics graph = Graphics.FromImage(bmp))
                {
                    Rectangle ImageSize = new Rectangle(0, 0, width, height);
                    graph.FillRectangle(Brushes.DarkGray, ImageSize);
                }
                return bmp;
            }
        }

        public void SetImage(string fromFile)
        {
            File.Copy(fromFile, this.imagePathGenerator.ModifiedFilePath, true);
        }


        public void SetImage(Image image)
        {
            Image theImage = image.Clone() as Image;
            image.Save((this.imagePathGenerator.ModifiedFilePath));
            theImage.Dispose();
        }
    }
}
