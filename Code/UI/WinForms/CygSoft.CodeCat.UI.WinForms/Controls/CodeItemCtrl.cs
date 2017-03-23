using System;
using System.Drawing;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.CodeGroup;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class CodeItemCtrl : UserControl, IDocumentItemControl
    {
        public event EventHandler Modified;

        private ICodeTopicSection codeTopicSection;
        private AppFacade application;
        private ICodeGroupDocumentSet codeGroupFile;

        public CodeItemCtrl(AppFacade application, ICodeGroupDocumentSet codeGroupFile, ICodeTopicSection codeFile)
        {
            InitializeComponent();
            
            this.application = application;
            this.codeTopicSection = codeFile;
            this.codeGroupFile = codeGroupFile;
            this.Id = codeFile.Id;

            syntaxDocument.SyntaxFile = ConfigSettings.QikTemplateSyntaxFile;
            
            SetDefaultFont();
            InitializeSyntaxList();

            ResetFieldValues();
            RegisterDataFieldEvents();
            RegisterFileEvents();
        }

        public int ImageKey { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Index; } }
        public Icon ImageIcon { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Icon; } }
        public Image IconImage { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Image; } }
        public string Id { get; private set; }
        public string Title { get { return this.txtTitle.Text; } }
        public string TemplateText { get { return this.syntaxDocument.Text; } }
        public bool IsModified { get; private set; }
        public bool FileExists { get { return codeTopicSection.Exists; } }

        public void Revert()
        {
            UnregisterDataFieldEvents();
            ResetFieldValues();
            RegisterDataFieldEvents();
        }

        private void ResetFieldValues()
        {
            txtTitle.Text = codeTopicSection.Title;
            syntaxBoxControl.Document.Text = codeTopicSection.Text;
            SelectSyntax(codeTopicSection.Syntax);

            this.IsModified = false;
            SetChangeStatus();
        }

        private void RegisterFileEvents()
        {
            codeGroupFile.BeforeSave += codeGroupFile_BeforeContentSaved;
            codeGroupFile.AfterSave += codeGroupFile_ContentSaved;
        }

        private void codeGroupFile_ContentSaved(object sender, TopicEventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void codeGroupFile_BeforeContentSaved(object sender, TopicEventArgs e)
        {
            this.codeTopicSection.Title = txtTitle.Text;
            this.codeTopicSection.Text = syntaxDocument.Text;
            this.codeTopicSection.Syntax = cboSyntax.SelectedItem.ToString();
        }

        private void RegisterDataFieldEvents()
        {
            cboSyntax.SelectedIndexChanged += cboSyntax_SelectedIndexChanged;
            cboFontSize.SelectedIndexChanged += cboFontSize_SelectedIndexChanged;
            txtTitle.TextChanged += SetModified;
            syntaxBoxControl.TextChanged += SetModified;
            this.Modified += CodeItemCtrl_Modified;
        }

        private void CodeItemCtrl_Modified(object sender, EventArgs e)
        {
            SetChangeStatus();
        }

        private void SetChangeStatus()
        {
            lblEditStatus.Text = this.IsModified ? "Edited" : "Saved";
            lblEditStatus.ForeColor = this.IsModified ? Color.DarkRed : Color.Black;
        }

        private void UnregisterDataFieldEvents()
        {
            cboSyntax.SelectedIndexChanged -= cboSyntax_SelectedIndexChanged;
            cboFontSize.SelectedIndexChanged -= cboFontSize_SelectedIndexChanged;
            txtTitle.TextChanged -= SetModified;
            syntaxBoxControl.TextChanged -= SetModified;
            this.Modified -= CodeItemCtrl_Modified;
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.syntaxBoxControl.FontSize = Convert.ToSingle(cboFontSize.SelectedItem);
            this.syntaxBoxControl.FontSize = Convert.ToSingle(cboFontSize.SelectedItem);
        }

        private void cboSyntax_SelectedIndexChanged(object sender, EventArgs e)
        {
            // don't want the syntax box to fire any events here...
            syntaxBoxControl.TextChanged -= SetModified;
            SelectSyntax(cboSyntax.SelectedItem.ToString());
            syntaxBoxControl.TextChanged += SetModified;
            SetModified(this, e);
        }

        private void SetModified(object sender, EventArgs e)
        {
            this.IsModified = true;
            this.Modified?.Invoke(this, new EventArgs());
        }

        private void InitializeSyntaxList()
        {
            cboSyntax.Items.Clear();
            cboSyntax.Items.AddRange(application.GetSyntaxes());
        }

        private void SelectSyntax(string syntax)
        {
            string syn = string.IsNullOrEmpty(syntax) ? ConfigSettings.DefaultSyntax.ToUpper() : syntax.ToUpper();
            int index = cboSyntax.FindStringExact(syn);
            if (index >= 0)
                cboSyntax.SelectedIndex = index;

            string syntaxFile = application.GetSyntaxFile(syn);
            this.syntaxBoxControl.Document.SyntaxFile = syntaxFile;

            this.lblEditStatus.Image = IconRepository.Get(syn).Image;
        }

        private void SetDefaultFont()
        {
            cboFontSize.SelectedIndex = cboFontSize.FindStringExact(ConfigSettings.DefaultFontSize.ToString());
        }
    }
}
