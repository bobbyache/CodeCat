using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using CygSoft.CodeCat.UI.WinForms.TopicSectionBase;
using System;
using System.Drawing;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class RichTextBoxTopicSectionControl : BaseTopicSectionControl
    {
        public override int ImageKey { get { return iconRepository.Get(IconRepository.TopicSections.RTF).Index; } }
        public override Icon ImageIcon { get { return iconRepository.Get(IconRepository.TopicSections.RTF).Icon; } }
        public override Image IconImage { get { return iconRepository.Get(IconRepository.TopicSections.RTF).Image; } }

        public RichTextBoxTopicSectionControl()
            : this(null, null, null, null, null)
        {

        }

        public RichTextBoxTopicSectionControl(IAppFacade application, IImageResources imageResources, IIconRepository iconRepository, ITopicDocument topicDocument, IRichTextEditorTopicSection topicSection)
            : base(application, imageResources, iconRepository, topicDocument, topicSection)
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