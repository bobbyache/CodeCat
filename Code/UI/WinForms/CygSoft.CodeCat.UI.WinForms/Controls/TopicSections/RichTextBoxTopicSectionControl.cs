using System;
using System.Drawing;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Topics;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class RichTextBoxTopicSectionControl : BaseTopicSectionControl
    {
        public override int ImageKey => IconRepository.Get(IconRepository.TopicSections.RTF).Index;
        public override Icon ImageIcon => IconRepository.Get(IconRepository.TopicSections.RTF).Icon;
        public override Image IconImage => IconRepository.Get(IconRepository.TopicSections.RTF).Image;

        public RichTextBoxTopicSectionControl() : this(null, null, null) { }

        public RichTextBoxTopicSectionControl(AppFacade application, ITopicDocument topicDocument, IRichTextEditorTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();

            LoadIfExists();

            topicSection.RequestSaveRtf += RequestSaveRtf;
            rtfEditor.ContentChanged += ContentChanged;
        }

        private void ContentChanged(object sender, EventArgs e)
        {
            if (rtfEditor.Modified)
                Modify();
        }

        private void RequestSaveRtf(object sender, EventArgs e) => rtfEditor.Save(topicSection.FilePath);

        private void LoadIfExists()
        {
            if (topicSection.Exists)
                rtfEditor.OpenFile(topicSection.FilePath);
        }
    }
}