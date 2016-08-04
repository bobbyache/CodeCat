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

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class QikTemplateCodeCtrl : UserControl
    {
        public event EventHandler Modified;

        private QikFile qikFile;
        private AppFacade application;

        public QikTemplateCodeCtrl(AppFacade application, QikFile qikFile, string templateId)
        {
            InitializeComponent();
            

            this.application = application;
            this.qikFile = qikFile;
            this.Id = templateId;

            SetDefaultFont();
            InitializeSyntaxList();

            txtTitle.Text = qikFile.GetTemplateTitle(templateId);
            templateSyntaxDocument.Text = qikFile.GetTemplateText(templateId);
            SelectSyntax(qikFile.GetTemplateSyntax(this.Id));

            this.IsModified = false;

            RegisterEvents();
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

        public bool TemplateExists { get { return qikFile.TemplateExists(this.Id); } }

        private void RegisterEvents()
        {
            qikFile.ContentReverted += qikFile_ContentReverted;
            qikFile.ContentSaved += qikFile_ContentSaved;
            qikFile.BeforeContentSaved += qikFile_BeforeContentSaved;

            cboSyntax.SelectedIndexChanged += cboSyntax_SelectedIndexChanged;
            cboFontSize.SelectedIndexChanged += cboFontSize_SelectedIndexChanged;
            txtTitle.TextChanged += SetModified;
            templateSyntaxBox.TextChanged += SetModified;
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.templateSyntaxBox.FontSize = Convert.ToSingle(cboFontSize.SelectedItem);
            this.outputSyntaxBox.FontSize = Convert.ToSingle(cboFontSize.SelectedItem);
        }

        private void qikFile_BeforeContentSaved(object sender, EventArgs e)
        {
            if (qikFile.TemplateExists(this.Id))
            {
                qikFile.SetTemplateTitle(this.Id, txtTitle.Text);
                qikFile.SetTemplateText(this.Id, templateSyntaxDocument.Text);
                qikFile.SetTemplateSyntax(this.Id, cboSyntax.SelectedItem.ToString());
            }
        }

        private void qikFile_ContentSaved(object sender, EventArgs e)
        {
            this.IsModified = false;
        }

        private void qikFile_ContentReverted(object sender, EventArgs e)
        {
            if (qikFile.TemplateExists(this.Id))
            {
                TabPage tabPage = this.Parent as TabPage;
                tabPage.Show();

                txtTitle.TextChanged -= SetModified;
                templateSyntaxBox.TextChanged -= SetModified;

                txtTitle.Text = qikFile.GetTemplateTitle(this.Id);
                templateSyntaxDocument.Text = qikFile.GetTemplateText(this.Id);
                SelectSyntax(qikFile.GetTemplateSyntax(this.Id));
                this.IsModified = false;

                txtTitle.TextChanged += SetModified;
                templateSyntaxBox.TextChanged += SetModified;
            }
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

            //this.Icon = IconRepository.GetIcon(syntax);
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

    }
}
