using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.DocumentManager.Infrastructure;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class RtfDocumentControl : UserControl, IDocumentItemControl
    {
        public event EventHandler Modified;

        private IRichTextDocument rtfDocument;
        private ICodeGroupDocumentSet codeGroupDocumentSet;

        public string Id { get; private set; }

        public string Title
        {
            get { return txtTitle.Text; }
        }

        public int ImageKey { get { return IconRepository.ImageKeyFor(IconRepository.RTF); } }
        public Icon ImageIcon { get { return IconRepository.GetIcon(IconRepository.RTF); } }
        public Image IconImage { get { return IconRepository.GetImage(IconRepository.RTF); } }

        public bool IsModified { get; private set; }

        public bool FileExists { get { return false; } }

        public RtfDocumentControl(AppFacade application, ICodeGroupDocumentSet codeGroupDocumentSet, IRichTextDocument rtfDocument)
        {
            InitializeComponent();
            this.rtfDocument = rtfDocument;
            this.codeGroupDocumentSet = codeGroupDocumentSet;
            this.Id = rtfDocument.Id;

            ResetFieldValues();
            LoadIfExists();

            codeGroupDocumentSet.BeforeSave += codeGroupDocumentSet_BeforeContentSaved;
            codeGroupDocumentSet.AfterSave += codeGroupDocumentSet_ContentSaved;
            rtfDocument.RequestSaveRtf += rtfDocument_RequestSaveRtf;
            rtfEditor.ContentChanged += rtfEditor_ContentChanged;
        }

        private void rtfEditor_ContentChanged(object sender, EventArgs e)
        {
            if (rtfEditor.Modified && !this.IsModified)
            {
                SetModified();
            }
        }

        private void rtfDocument_RequestSaveRtf(object sender, EventArgs e)
        {
            //Console.WriteLine("Saving");
            rtfEditor.Save(rtfDocument.FilePath);
        }

        public void Revert()
        {
            LoadIfExists();
        }

        private void SetModified()
        {
            this.IsModified = true;
            SetChangeStatus();

            if (this.Modified != null)
                this.Modified(this, new EventArgs());
        }

        private void LoadIfExists()
        {
            if (this.rtfDocument.Exists)
                rtfEditor.OpenFile(this.rtfDocument.FilePath);
        }

        private void ResetFieldValues()
        {
            txtTitle.Text = rtfDocument.Title;
            this.IsModified = false;
        }

        private void codeGroupDocumentSet_ContentSaved(object sender, FileEventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void codeGroupDocumentSet_BeforeContentSaved(object sender, FileEventArgs e)
        {
            this.rtfDocument.Title = txtTitle.Text;
        }

        private void SetChangeStatus()
        {
            lblEditStatus.Text = this.IsModified ? "Edited" : "Saved";
            lblEditStatus.ForeColor = this.IsModified ? Color.DarkRed : Color.Black;
        }
    }
}
