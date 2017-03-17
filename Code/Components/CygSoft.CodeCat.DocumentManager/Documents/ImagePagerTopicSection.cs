using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Documents.ImageSet;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Linq;

namespace CygSoft.CodeCat.DocumentManager.Documents
{
    public class ImagePagerTopicSection : TopicSection, IImagePagerTopicSection
    {
        public event EventHandler ImageAdded;
        public event EventHandler ImageRemoved;
        public event EventHandler ImageMovedUp;
        public event EventHandler ImageMovedDown;

        private ImageSetIndexPathGenerator indexPathGenerator;
        private IDocumentIndexRepository repository;
        private ImageSetIndex documentIndex;

        public int ImageCount { get { return this.documentIndex.TopicSections.Count(); } }

        public IImagePagerImageTopicSection FirstImage
        {
            get { return this.documentIndex.FirstTopicSection as IImagePagerImageTopicSection; }
        }


        internal ImagePagerTopicSection(string folder, string title)
            : base(new DocumentPathGenerator(folder, "imgset"), title, null)
        {
            indexPathGenerator = new ImageSetIndexPathGenerator(folder, "imgset", this.Id);
            repository = new ImageSetIndexXmlRepository(indexPathGenerator);
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.ImageSet);
            this.documentIndex = new ImageSetIndex(repository, indexPathGenerator);
        }

        internal ImagePagerTopicSection(string folder, string id, string title, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, "imgset", id), title, description, ordinal)
        {
            indexPathGenerator = new ImageSetIndexPathGenerator(folder, "imgset", this.Id);
            repository = new ImageSetIndexXmlRepository(indexPathGenerator);
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.ImageSet);
            this.documentIndex = new ImageSetIndex(repository, indexPathGenerator);
        }

        protected override void OnBeforeRevert()
        {
            this.documentIndex.Revert();
            base.OnBeforeRevert();
        }

        protected override void OnAfterDelete()
        {
            var imageFiles = this.documentIndex.TopicSections;
            foreach (var imageFile in imageFiles)
            {
                if (imageFile.Exists)
                    imageFile.Delete();
            }

            base.OnAfterDelete();
        }

        public bool IsLastImage(IImagePagerImageTopicSection imagePagerImageTopicSection)
        {
            return this.documentIndex.LastTopicSection.Ordinal <= imagePagerImageTopicSection.Ordinal;
        }

        public IImagePagerImageTopicSection NextImage(IImagePagerImageTopicSection imagePagerImageTopicSection)
        {
            if (this.documentIndex.LastTopicSection.Ordinal > imagePagerImageTopicSection.Ordinal)
                return this.documentIndex.TopicSections.Where(img => img.Ordinal == imagePagerImageTopicSection.Ordinal + 1).OfType<IImagePagerImageTopicSection>().SingleOrDefault();
            else
                return imagePagerImageTopicSection;
        }

        public bool IsFirstImage(IImagePagerImageTopicSection imagePagerImageTopicSection)
        {
            return this.documentIndex.FirstTopicSection.Ordinal >= imagePagerImageTopicSection.Ordinal;
        }

        public IImagePagerImageTopicSection PreviousImage(IImagePagerImageTopicSection imagePagerImageTopicSection)
        {
            if (this.documentIndex.FirstTopicSection.Ordinal <= imagePagerImageTopicSection.Ordinal)
                return this.documentIndex.TopicSections.Where(img => img.Ordinal == imagePagerImageTopicSection.Ordinal - 1).OfType<IImagePagerImageTopicSection>().SingleOrDefault();
            else
                return imagePagerImageTopicSection;
        }

        protected override void OpenFile()
        {
            this.documentIndex.Open();
        }

        protected override void SaveFile()
        {
            this.documentIndex.Save();
        }

        public IImagePagerImageTopicSection Add()
        {
            IImagePagerImageTopicSection imagePagerImageTopicSection = this.Add("Blank Image", "png");
            return imagePagerImageTopicSection;
        }

        public IImagePagerImageTopicSection Add(string description, string extension)
        {
            ImagePathGenerator imagePathGenerator = new ImagePathGenerator(this.Folder, extension);
            ImagePagerImageTopicSection imagePagerImageTopicSection = new ImagePagerImageTopicSection(imagePathGenerator);
            imagePagerImageTopicSection.Description = description;
            this.documentIndex.AddTopicSection(imagePagerImageTopicSection);

            ImageAdded?.Invoke(this, new EventArgs());

            return imagePagerImageTopicSection;
        }


        public void Remove(IImagePagerImageTopicSection imagePagerImageTopicSection)
        {
            this.documentIndex.RemoveTopicSection(imagePagerImageTopicSection.Id);
            ImageRemoved?.Invoke(this, new EventArgs());
        }

        public bool CanMovePrevious(IImagePagerImageTopicSection imagePagerImageTopicSection)
        {
            return this.documentIndex.CanMoveUp(imagePagerImageTopicSection);
        }

        public bool CanMoveNext(IImagePagerImageTopicSection imagePagerImageTopicSection)
        {
            return this.documentIndex.CanMoveDown(imagePagerImageTopicSection);
        }

        public virtual void MovePrevious(IImagePagerImageTopicSection imagePagerImageTopicSection)
        {
            this.documentIndex.MoveUp(imagePagerImageTopicSection);
            ImageMovedUp?.Invoke(this, new EventArgs());
        }

        public virtual void MoveNext(IImagePagerImageTopicSection imagePagerImageTopicSection)
        {
            this.documentIndex.MoveDown(imagePagerImageTopicSection);
            ImageMovedDown?.Invoke(this, new EventArgs());
        }

    }
}
