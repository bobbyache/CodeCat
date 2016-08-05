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
using CygSoft.CodeCat.Infrastructure.Qik;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class QikTemplateCodeCtrl : UserControl
    {
        public event EventHandler Modified;

        private ITemplateFile templateFile;
        private AppFacade application;
        private QikFile qikFile;
        private TabPage tabPage;

        public QikTemplateCodeCtrl(AppFacade application, QikFile qikFile, ITemplateFile templateFile, TabPage tabPage)
        {
            InitializeComponent();
            
            this.application = application;
            this.qikFile = qikFile;
            this.tabPage = tabPage;
            this.templateFile = templateFile;
            this.Id = templateFile.FileName;

            templateSyntaxDocument.SyntaxFile = ConfigSettings.QikTemplateSyntaxFile;

            SetDefaultFont();
            InitializeSyntaxList();

            ResetFieldValues();
            RegisterDataFieldEvents();
            RegisterFileEvents();
        }

        public string Id { get; private set; }

        public string Title
        {
            get { return this.txtTitle.Text; }
        }

        public string TemplateText
        {
            get { return this.templateSyntaxDocument.Text; }
        }

        public bool IsModified { get; private set; }

        public bool TemplateExists { get { return templateFile.Exists; } }

        public void Revert()
        {
            UnregisterDataFieldEvents();
            ResetFieldValues();
            RegisterDataFieldEvents();
        }

        private void ResetFieldValues()
        {
            txtTitle.Text = templateFile.Title;
            templateSyntaxBox.Document.Text = templateFile.Text;
            outputSyntaxBox.Document.Text = string.Empty;
            SelectSyntax(templateFile.Syntax);

            this.IsModified = false;
            SetChangeStatus();
        }

        private void RegisterFileEvents()
        {
            qikFile.BeforeContentSaved += qikFile_BeforeContentSaved;
            qikFile.ContentSaved += qikFile_ContentSaved;
        }

        private void qikFile_ContentSaved(object sender, EventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void qikFile_BeforeContentSaved(object sender, EventArgs e)
        {
            this.templateFile.Title = txtTitle.Text;
            this.templateFile.Text = templateSyntaxDocument.Text;
            this.templateFile.Syntax = cboSyntax.SelectedItem.ToString();
        }

        private void RegisterDataFieldEvents()
        {
            cboSyntax.SelectedIndexChanged += cboSyntax_SelectedIndexChanged;
            cboFontSize.SelectedIndexChanged += cboFontSize_SelectedIndexChanged;
            txtTitle.TextChanged += SetModified;
            txtTitle.Validated += txtTitle_Validated;
            templateSyntaxBox.TextChanged += SetModified;
            this.Modified += QikTemplateCodeCtrl_Modified;
        }

        private void QikTemplateCodeCtrl_Modified(object sender, EventArgs e)
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
            txtTitle.Validated += txtTitle_Validated;
            templateSyntaxBox.TextChanged -= SetModified;
            this.Modified -= QikTemplateCodeCtrl_Modified;
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.templateSyntaxBox.FontSize = Convert.ToSingle(cboFontSize.SelectedItem);
            this.outputSyntaxBox.FontSize = Convert.ToSingle(cboFontSize.SelectedItem);
        }

        private void cboSyntax_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectSyntax(cboSyntax.SelectedItem.ToString());

            this.IsModified = true;

            if (this.Modified != null)
                this.Modified(this, new EventArgs()); 
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
                    cboSyntax.SelectedItem = item;
            }

            string syntaxFile = application.GetSyntaxFile(syn);
            this.outputSyntaxBox.Document.SyntaxFile = syntaxFile;

            // requires an image list like the one you're using in the list view.
            //this.Icon = IconRepository.GetIcon(syntax);

            this.tabPage.ImageIndex = IconRepository.ImageKeyFor(syn);
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

        private void templateFileTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (templateFileTabControl.SelectedTab.Name == "outputTabPage")
            {
                outputSyntaxDocument.Text = templateSyntaxDocument.Text;
            }
        }

        private void txtTitle_Validated(object sender, EventArgs e)
        {
            tabPage.Text = txtTitle.Text;
        }

    }
}
