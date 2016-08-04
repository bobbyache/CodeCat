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

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class QikTemplateCodeCtrl : UserControl
    {
        public event EventHandler Modified;
        //public event EventHandler CloseRequest;

        private QikFile qikFile;

        public QikTemplateCodeCtrl(QikFile qikFile, string templateId)
        {
            InitializeComponent();

            this.qikFile = qikFile;
            this.Id = templateId;

            txtTitle.Text = qikFile.GetTemplateTitle(templateId);
            templateSyntaxDocument.Text = qikFile.GetTemplateText(templateId);

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

            txtTitle.TextChanged += SetModified;
            templateSyntaxBox.TextChanged += SetModified;
        }

        private void qikFile_BeforeContentSaved(object sender, EventArgs e)
        {
            if (qikFile.TemplateExists(this.Id))
            {
                qikFile.SetTemplateTitle(this.Id, txtTitle.Text);
                qikFile.SetTemplateText(this.Id, templateSyntaxDocument.Text);
            }
            //else
            //{
            //    // this template has been removed.
            //    if (this.CloseRequest != null)
            //        CloseRequest(this, new EventArgs());
            //}
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
                this.IsModified = false;

                txtTitle.TextChanged += SetModified;
                templateSyntaxBox.TextChanged += SetModified;
            }
        }

        private void SetModified(object sender, EventArgs e)
        {
            this.IsModified = true;

            if (this.Modified != null)
                this.Modified(this, new EventArgs());
        }
    }
}
