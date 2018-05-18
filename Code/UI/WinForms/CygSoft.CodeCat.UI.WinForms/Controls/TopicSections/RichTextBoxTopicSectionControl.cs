using System;
using System.Drawing;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.UI.WinForms.Controls.TopicSections;
using CygSoft.CodeCat.UI.Resources.Infrastructure;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class RichTextBoxTopicSectionControl : BaseTopicSectionControl
    {
        public override int ImageKey { get { return IconRepository.Get(IconRepository.TopicSections.RTF).Index; } }
        public override Icon ImageIcon { get { return IconRepository.Get(IconRepository.TopicSections.RTF).Icon; } }
        public override Image IconImage { get { return IconRepository.Get(IconRepository.TopicSections.RTF).Image; } }

        public RichTextBoxTopicSectionControl()
            : this(null, null, null, null)
        {

        }

        public RichTextBoxTopicSectionControl(AppFacade application, IImageResources imageResources, ITopicDocument topicDocument, IRichTextEditorTopicSection topicSection)
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