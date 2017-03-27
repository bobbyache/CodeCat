using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.TopicSections.ImagePager;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.Linq;

namespace CygSoft.CodeCat.DocumentManager.TopicSections.ImagePager
{
    public class ImagePagerTopicSection : TopicSection, IImagePagerTopicSection
    {
        public event EventHandler ImageAdded;
        public event EventHandler ImageRemoved;
        public event EventHandler ImageMovedUp;
        public event EventHandler ImageMovedDown;

        private ImagePagerPathGenerator imagePagerPathGenerator;
        private IDocumentIndexRepository repository;
        private ImagePager documentIndex;

        public int ImageCount { get { return this.documentIndex.TopicSections.Count(); } }

        public IPagerImage FirstImage
        {
            get { return this.documentIndex.FirstTopicSection as IPagerImage; }
        }


        internal ImagePagerTopicSection(string folder, string title)
            : base(new DocumentPathGenerator(folder, "imgset"), title, null)
        {
            imagePagerPathGenerator = new ImagePagerPathGenerator(folder, "imgset", this.Id);
            repository = new ImageSetIndexXmlRepository(imagePagerPathGenerator);
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.ImagePager);
            this.documentIndex = new ImagePager(repository, imagePagerPathGenerator);
        }

        internal ImagePagerTopicSection(string folder, string id, string title, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, "imgset", id), title, description, ordinal)
        {
            imagePagerPathGenerator = new ImagePagerPathGenerator(folder, "imgset", this.Id);
            repository = new ImageSetIndexXmlRepository(imagePagerPathGenerator);
            this.DocumentType = TopicSectionFactory.GetDocumentType(TopicSectionType.ImagePager);
            this.documentIndex = new ImagePager(repository, imagePagerPathGenerator);
        }

        protected override void OnRevert()
        {
            this.documentIndex.Revert();
            base.OnRevert();
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

        public bool IsLastImage(IPagerImage pagerImage)
        {
            return this.documentIndex.LastTopicSection.Ordinal <= pagerImage.Ordinal;
        }

        public IPagerImage NextImage(IPagerImage pagerImage)
        {
            if (this.documentIndex.LastTopicSection.Ordinal > pagerImage.Ordinal)
                return this.documentIndex.TopicSections.Where(img => img.Ordinal == pagerImage.Ordinal + 1).OfType<IPagerImage>().SingleOrDefault();
            else
                return pagerImage;
        }

        public bool IsFirstImage(IPagerImage pagerImage)
        {
            return this.documentIndex.FirstTopicSection.Ordinal >= pagerImage.Ordinal;
        }

        public IPagerImage PreviousImage(IPagerImage pagerImage)
        {
            if (this.documentIndex.FirstTopicSection.Ordinal <= pagerImage.Ordinal)
                return this.documentIndex.TopicSections.Where(img => img.Ordinal == pagerImage.Ordinal - 1).OfType<IPagerImage>().SingleOrDefault();
            else
                return pagerImage;
        }

        protected override void OnOpen()
        {
            this.documentIndex.Open();
            base.OnOpen();
        }

        protected override void OnSave()
        {
            this.documentIndex.Save();
            base.OnSave();
        }

        public IPagerImage Add()
        {
            IPagerImage pagerImage = this.Add("Blank Image", "png");
            return pagerImage;
        }

        public IPagerImage Add(string description, string extension)
        {
            ImagePathGenerator imagePathGenerator = new ImagePathGenerator(this.Folder, extension);
            PagerImage pagerImage = new PagerImage(imagePathGenerator);
            pagerImage.Description = description;
            this.documentIndex.AddTopicSection(pagerImage);

            ImageAdded?.Invoke(this, new EventArgs());

            return pagerImage;
        }


        public void Remove(IPagerImage pagerImage)
        {
            this.documentIndex.RemoveTopicSection(pagerImage.Id);
            ImageRemoved?.Invoke(this, new EventArgs());
        }

        public bool CanMovePrevious(IPagerImage pagerImage)
        {
            return this.documentIndex.CanMoveUp(pagerImage);
        }

        public bool CanMoveNext(IPagerImage pagerImage)
        {
            return this.documentIndex.CanMoveDown(pagerImage);
        }

        public virtual void MovePrevious(IPagerImage pagerImage)
        {
            this.documentIndex.MoveUp(pagerImage);
            ImageMovedUp?.Invoke(this, new EventArgs());
        }

        public virtual void MoveNext(IPagerImage pagerImage)
        {
            this.documentIndex.MoveDown(pagerImage);
            ImageMovedDown?.Invoke(this, new EventArgs());
        }

    }
}
