using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Domain;
using Alsing.SourceCode;
using CygSoft.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.CodeGroup;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class CodeItemCtrl : UserControl, IDocumentItemControl
    {
        public event EventHandler Modified;

        private ICodeDocument codeFile;
        private AppFacade application;
        private ICodeGroupDocumentGroup codeGroupFile;

        public CodeItemCtrl(AppFacade application, ICodeGroupDocumentGroup codeGroupFile, ICodeDocument codeFile)
        {
            InitializeComponent();
            
            this.application = application;
            this.codeFile = codeFile;
            this.codeGroupFile = codeGroupFile;
            this.Id = codeFile.Id;

            syntaxDocument.SyntaxFile = ConfigSettings.QikTemplateSyntaxFile;
            
            SetDefaultFont();
            InitializeSyntaxList();

            ResetFieldValues();
            RegisterDataFieldEvents();
            RegisterFileEvents();
        }

        public int ImageKey { get { return IconRepository.ImageKeyFor(cboSyntax.SelectedItem.ToString()); } }
        public Icon ImageIcon { get { return IconRepository.GetIcon(cboSyntax.SelectedItem.ToString()); } }
        public Image IconImage { get { return IconRepository.GetImage(cboSyntax.SelectedItem.ToString()); } }

        public string Id { get; private set; }

        public string Title
        {
            get { return this.txtTitle.Text; }
        }

        public string TemplateText
        {
            get { return this.syntaxDocument.Text; }
        }

        public bool IsModified { get; private set; }

        public bool FileExists { get { return codeFile.Exists; } }

        public void Revert()
        {
            UnregisterDataFieldEvents();
            ResetFieldValues();
            RegisterDataFieldEvents();
        }

        private void ResetFieldValues()
        {
            txtTitle.Text = codeFile.Title;
            syntaxBoxControl.Document.Text = codeFile.Text;
            SelectSyntax(codeFile.Syntax);

            this.IsModified = false;
            SetChangeStatus();
        }

        private void RegisterFileEvents()
        {
            codeGroupFile.BeforeSave += codeGroupFile_BeforeContentSaved;
            codeGroupFile.AfterSave += codeGroupFile_ContentSaved;
        }

        private void codeGroupFile_ContentSaved(object sender, FileEventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void codeGroupFile_BeforeContentSaved(object sender, FileEventArgs e)
        {
            this.codeFile.Title = txtTitle.Text;
            this.codeFile.Text = syntaxDocument.Text;
            this.codeFile.Syntax = cboSyntax.SelectedItem.ToString();
        }

        private void RegisterDataFieldEvents()
        {
            cboSyntax.SelectedIndexChanged += cboSyntax_SelectedIndexChanged;
            cboFontSize.SelectedIndexChanged += cboFontSize_SelectedIndexChanged;
            txtTitle.TextChanged += SetModified;
            //txtTitle.Validated += txtTitle_Validated;
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
            //txtTitle.Validated += txtTitle_Validated;
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

            if (this.Modified != null)
                this.Modified(this, new EventArgs());
        }

        private void InitializeSyntaxList()
        {
            cboSyntax.Items.Clear();
            cboSyntax.Items.AddRange(application.GetSyntaxes());
        }

        private void SelectSyntax(string syntax)
        {
            string syn;
            if (string.IsNullOrEmpty(syntax))
                syn = ConfigSettings.DefaultSyntax.ToUpper();
            else
                syn = syntax.ToUpper();

            foreach (object item in cboSyntax.Items)
            {
                if (item.ToString() == syn)
                {
                    cboSyntax.SelectedItem = item;
                    break;
                }
            }

            string syntaxFile = application.GetSyntaxFile(syn);
            this.syntaxBoxControl.Document.SyntaxFile = syntaxFile;

            this.lblEditStatus.Image = IconRepository.GetIcon(syn).ToBitmap();
        }

        private void SetDefaultFont()
        {
            int index = cboFontSize.FindStringExact(ConfigSettings.DefaultFontSize.ToString());
            if (index >= 0)
                cboFontSize.SelectedIndex = index;
            else
                cboFontSize.SelectedIndex = 4;
        }
    }
}
