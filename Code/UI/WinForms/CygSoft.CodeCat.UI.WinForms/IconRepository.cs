﻿using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using CygSoft.CodeCat.Syntax.Infrastructure;
using CygSoft.CodeCat.UI.Resources;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static CygSoft.CodeCat.UI.Resources.ImageResources;

namespace CygSoft.CodeCat.UI.WinForms
{
    public class IconRepository : IIconRepository
    {
        private IImageResources imageResources;
        public IImageResources ImageResources
        {
            get
            {
                if (imageResources == null)
                    imageResources = new ImageResources();
                return imageResources;
            }
        }

        public ImageList ImageList { get { return imageResources.ImageList; } }

        public Icon QikGroupIcon { get { return imageResources.QikGroupIcon; } }
        public Icon CodeGroupIcon { get { return imageResources.CodeGroupIcon; } }
        public Icon FileGroupIcon { get { return imageResources.FileGroupIcon; } }

        public IconRepository()
        {
            imageResources = new ImageResources();
        }

        public IImageOutput GetKeywordIndexItemImage(IKeywordIndexItem item)
        {
            string imageKey = null;

            if (item is ICodeKeywordIndexItem)
                imageKey = (item as ICodeKeywordIndexItem).Syntax;

            else if (item is IQikTemplateKeywordIndexItem)
                imageKey = TopicSections.QikGroup;

            else if (item is ITopicKeywordIndexItem)
                imageKey = TopicSections.CodeGroup;

            return imageResources.GetKeywordIndexItemImage(imageKey);
        }

        public IImageOutput Get(string key, bool isFileExtensionKey = false)
        {
            return imageResources.Get(key, isFileExtensionKey);
        }

        public void AddCategoryInfo()
        {
            imageResources.AddCategoryInfo();
        }

        public void AddDocuments()
        {
            imageResources.AddDocuments();
        }

        public void AddSyntaxes(ISyntaxFile[] syntaxFiles)
        {
            imageResources.AddSyntaxes(syntaxFiles);
        }

        public void AddFileExtensions(IEnumerable<string> fileExtensions)
        {
            imageResources.AddFileExtensions(fileExtensions);
        }
    }
}
