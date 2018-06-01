﻿using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using CygSoft.CodeCat.UI.Resources;
using CygSoft.CodeCat.UI.WinForms.TopicSectionBase;
using System;
using System.Drawing;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class RichTextBoxTopicSectionControl : BaseTopicSectionControl
    {
        public override int ImageKey { get { return imageResources.Get(ImageResources.TopicSections.RTF).Index; } }
        public override Icon ImageIcon { get { return imageResources.Get(ImageResources.TopicSections.RTF).Icon; } }
        public override Image IconImage { get { return imageResources.Get(ImageResources.TopicSections.RTF).Image; } }

        public RichTextBoxTopicSectionControl(IAppFacade application, IImageResources imageResources, ITopicDocument topicDocument, IRichTextEditorTopicSection topicSection)
            : base(application, imageResources, topicDocument, topicSection)
        {
            InitializeComponent();

            LoadIfExists();

            topicSection.RequestSaveRtf += rtfDocument_RequestSaveRtf;
            rtfEditor.ContentChanged += rtfEditor_ContentChanged;
        }

        private void rtfEditor_ContentChanged(object sender, EventArgs e)
        {
            if (rtfEditor.Modified)
                Modify();
        }

        private void rtfDocument_RequestSaveRtf(object sender, EventArgs e)
        {
            rtfEditor.Save(topicSection.FilePath);
        }

        private void LoadIfExists()
        {
            if (topicSection.Exists)
                rtfEditor.OpenFile(topicSection.FilePath);
        }
    }
}