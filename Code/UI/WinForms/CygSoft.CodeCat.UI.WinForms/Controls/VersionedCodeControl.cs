using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.CodeGroup;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class VersionedCodeControl : UserControl, IDocumentItemControl
    {
        private AppFacade application;
        private ICodeGroupDocumentSet codeGroupDocumentSet;
        private IVersionedCodeTopicSection versionedCodeTopicSection;

        public VersionedCodeControl(AppFacade application, ICodeGroupDocumentSet codeGroupDocumentSet, IVersionedCodeTopicSection versionedCodeTopicSection)
        {
            InitializeComponent();

            tabControl.Alignment = TabAlignment.Left;

            this.application = application;
            this.codeGroupDocumentSet = codeGroupDocumentSet;
            this.versionedCodeTopicSection = versionedCodeTopicSection;
            this.Id = versionedCodeTopicSection.Id;

            syntaxDocument.SyntaxFile = ConfigSettings.QikTemplateSyntaxFile;

            SetDefaultFont();
            InitializeSyntaxList();

            txtTitle.TextChanged += (s, e) => SetModified();
        }

        public string Id { get; private set; }
        public string Title { get { return ""; } }
        public int ImageKey { get { return IconRepository.Get(IconRepository.Documents.CodeFile).Index; } }
        public Icon ImageIcon { get { return IconRepository.Get(IconRepository.Documents.CodeFile).Icon; } }
        public Image IconImage { get { return IconRepository.Get(IconRepository.Documents.CodeFile).Image; } }
        public bool IsModified { get; private set; }
        public bool FileExists { get { return false; } }


        public event EventHandler Modified;

        public void Revert()
        {
            throw new NotImplementedException();
        }

        private void InitializeSyntaxList()
        {
            cboSyntax.Items.Clear();
            cboSyntax.Items.AddRange(application.GetSyntaxes());
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

        private void SetChangeStatus()
        {
            lblEditStatus.Text = IsModified ? "Edited" : "Saved";
            lblEditStatus.ForeColor = IsModified ? Color.DarkRed : Color.Black;
        }

        private void InitializeImages()
        {
            //this.tabControl.ImageList = IconRepository.ImageList;
            //this.tabPageCode.ImageKey = (base.persistableTarget as CodeFile).Syntax;
            //this.Icon = IconRepository.Get((base.persistableTarget as CodeFile).Syntax).Icon;
            lblEditStatus.Image = this.IconImage;
        }

        private void SetDefaultFont()
        {
            cboFontSize.SelectedIndex = cboFontSize.FindStringExact(ConfigSettings.DefaultFontSize.ToString());
        }
    }
}
