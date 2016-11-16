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
    public partial class UrlGroupControl : UserControl, IDocumentItemControl
    {
        private IUrlGroupDocument urlDocument;
        private ICodeGroupDocumentGroup codeGroupFile;

        public UrlGroupControl(AppFacade application, ICodeGroupDocumentGroup codeGroupFile, IUrlGroupDocument urlDocument)
        {
            InitializeComponent();

            this.urlDocument = urlDocument;
            this.codeGroupFile = codeGroupFile;

            ResetFieldValues();
            //RegisterDataFieldEvents();
            RegisterFileEvents();
        }

        public event EventHandler Modified;

        public string Id { get; private set; }

        public string Title
        {
            get { return this.txtTitle.Text; }
        }

        public int ImageKey { get { return 0; } }

        public Icon ImageIcon { get { return IconRepository.GetIcon("TEXT"); } }

        public Image IconImage { get { return IconRepository.GetImage("TEXT"); } }

        public bool IsModified { get; private set; }

        public bool FileExists { get { return false; } }

        public void Revert()
        {
        }


        private void RegisterFileEvents()
        {
            codeGroupFile.BeforeSave += codeGroupFile_BeforeContentSaved;
            codeGroupFile.AfterSave += codeGroupFile_ContentSaved;
        }

        private void ResetFieldValues()
        {
            txtTitle.Text = urlDocument.Title;
            this.IsModified = false;
        }

        private void codeGroupFile_ContentSaved(object sender, FileEventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void codeGroupFile_BeforeContentSaved(object sender, FileEventArgs e)
        {
            this.urlDocument.Title = txtTitle.Text;
        }

        private void SetChangeStatus()
        {
            lblEditStatus.Text = this.IsModified ? "Edited" : "Saved";
            lblEditStatus.ForeColor = this.IsModified ? Color.DarkRed : Color.Black;
        }
    }
}
