﻿using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.CodeCat.Domain.TopicSections.ImagePager
{
    internal class ImagePager : Topic
    {
        public ImagePager(IDocumentIndexRepository repository, ImagePagerPathGenerator imagePagerPathGenerator)
            : base(repository, imagePagerPathGenerator)
        {
        }

        protected override List<ITopicSection> LoadTopicSections()
        {
            return base.indexRepository.LoadDocuments();
        }

        protected override void SaveDocumentIndex()
        {
            base.indexRepository.WriteDocuments(base.TopicSections.ToList());
        }
    }
}
