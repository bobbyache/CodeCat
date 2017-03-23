using System;
using System.Drawing;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.DocumentManager.Infrastructure;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class RtfDocumentControl : UserControl, IDocumentItemControl
    {
        public event EventHandler Modified;

        private IRichTextEditorTopicSection richTextEditorTopicSection;
        private ICodeGroupDocumentSet codeGroupDocumentSet;

        public string Id { get; private set; }

        public string Title
        {
            get { return txtTitle.Text; }
        }

        public int ImageKey { get { return IconRepository.Get(IconRepository.Documents.RTF).Index; } }
        public Icon ImageIcon { get { return IconRepository.Get(IconRepository.Documents.RTF).Icon; } }
        public Image IconImage { get { return IconRepository.Get(IconRepository.Documents.RTF).Image; } }

        public bool IsModified { get; private set; }

        public bool FileExists { get { return false; } }

        public RtfDocumentControl(AppFacade application, ICodeGroupDocumentSet codeGroupDocumentSet, IRichTextEditorTopicSection rtfDocument)
        {
            InitializeComponent();
            this.richTextEditorTopicSection = rtfDocument;
            this.codeGroupDocumentSet = codeGroupDocumentSet;
            Id = rtfDocument.Id;

            ResetFieldValues();
            LoadIfExists();

            codeGroupDocumentSet.BeforeSave += codeGroupDocumentSet_BeforeContentSaved;
            codeGroupDocumentSet.AfterSave += codeGroupDocumentSet_ContentSaved;
            rtfDocument.RequestSaveRtf += rtfDocument_RequestSaveRtf;
            rtfEditor.ContentChanged += rtfEditor_ContentChanged;
        }

        private void rtfEditor_ContentChanged(object sender, EventArgs e)
        {
            if (rtfEditor.Modified && !IsModified)
                SetModified();
        }

        private void rtfDocument_RequestSaveRtf(object sender, EventArgs e)
        {
            rtfEditor.Save(richTextEditorTopicSection.FilePath);
        }

        public void Revert()
        {
            LoadIfExists();
        }

        private void SetModified()
        {
            IsModified = true;
            SetChangeStatus();
            Modified?.Invoke(this, new EventArgs());
        }

        private void LoadIfExists()
        {
            if (richTextEditorTopicSection.Exists)
                rtfEditor.OpenFile(richTextEditorTopicSection.FilePath);
        }

        private void ResetFieldValues()
        {
            txtTitle.Text = richTextEditorTopicSection.Title;
            IsModified = false;
        }

        private void codeGroupDocumentSet_ContentSaved(object sender, TopicEventArgs e)
        {
            IsModified = false;
            SetChangeStatus();
        }

        private void codeGroupDocumentSet_BeforeContentSaved(object sender, TopicEventArgs e)
        {
            richTextEditorTopicSection.Title = txtTitle.Text;
        }

        private void SetChangeStatus()
        {
            lblEditStatus.Text = IsModified ? "Edited" : "Saved";
            lblEditStatus.ForeColor = IsModified ? Color.DarkRed : Color.Black;
        }
    }
}
