using System;
using System.Drawing;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Topics;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class RtfDocumentControl : UserControl, ITopicSectionBaseControl
    {
        public event EventHandler Modified;

        private IRichTextEditorTopicSection topicSection;
        private ITopicDocument topicDocument;

        public string Id { get; private set; }

        public string Title
        {
            get { return txtTitle.Text; }
        }

        public int ImageKey { get { return IconRepository.Get(IconRepository.TopicSections.RTF).Index; } }
        public Icon ImageIcon { get { return IconRepository.Get(IconRepository.TopicSections.RTF).Icon; } }
        public Image IconImage { get { return IconRepository.Get(IconRepository.TopicSections.RTF).Image; } }

        public bool IsModified { get; private set; }

        public bool FileExists { get { return false; } }

        public RtfDocumentControl(AppFacade application, ITopicDocument topicDocument, IRichTextEditorTopicSection topicSection)
        {
            InitializeComponent();
            this.topicSection = topicSection;
            this.topicDocument = topicDocument;
            Id = topicSection.Id;

            ResetFieldValues();
            LoadIfExists();

            txtTitle.TextChanged += (s, e) => SetModified();
            topicDocument.BeforeSave += codeGroupDocumentSet_BeforeContentSaved;
            topicDocument.AfterSave += codeGroupDocumentSet_ContentSaved;
            topicSection.RequestSaveRtf += rtfDocument_RequestSaveRtf;
            rtfEditor.ContentChanged += rtfEditor_ContentChanged;
        }

        private void rtfEditor_ContentChanged(object sender, EventArgs e)
        {
            if (rtfEditor.Modified)
                SetModified();
        }

        private void rtfDocument_RequestSaveRtf(object sender, EventArgs e)
        {
            rtfEditor.Save(topicSection.FilePath);
        }

        public void Revert()
        {
            LoadIfExists();
        }

        private void SetModified()
        {
            if (!IsModified)
            {
                IsModified = true;
                SetChangeStatus();
                Modified?.Invoke(this, new EventArgs());
            }
        }

        private void LoadIfExists()
        {
            if (topicSection.Exists)
                rtfEditor.OpenFile(topicSection.FilePath);
        }

        private void ResetFieldValues()
        {
            txtTitle.Text = topicSection.Title;
            IsModified = false;
        }

        private void codeGroupDocumentSet_ContentSaved(object sender, TopicEventArgs e)
        {
            IsModified = false;
            SetChangeStatus();
        }

        private void codeGroupDocumentSet_BeforeContentSaved(object sender, TopicEventArgs e)
        {
            topicSection.Title = txtTitle.Text;
        }

        private void SetChangeStatus()
        {
            lblEditStatus.Text = IsModified ? "Edited" : "Saved";
            lblEditStatus.ForeColor = IsModified ? Color.DarkRed : Color.Black;
        }
    }
}
