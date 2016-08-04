using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class QikTemplateCodeCtrl : UserControl
    {
        public event EventHandler Modified;

        public QikTemplateCodeCtrl(string title, string templateCode)
        {
            InitializeComponent();

            txtTitle.Text = title;
            templateSyntaxDocument.Text = templateCode;
            this.IsModified = false;

            RegisterEvents();
        }

        public string Title
        {
            get { return this.txtTitle.Text; }
            set { this.txtTitle.Text = value; }
        }

        public string TemplateText
        {
            get { return this.templateSyntaxDocument.Text; }
            set { this.templateSyntaxDocument.Text = value; }
        }

        public bool IsModified { get; private set; }

        public void Save()
        {
            this.IsModified = false;
        }

        public void Revert(string title, string templateCode)
        {
            txtTitle.TextChanged -= SetModified;
            templateSyntaxBox.TextChanged -= SetModified;

            txtTitle.Text = title;
            templateSyntaxDocument.Text = templateCode;
            this.IsModified = false;

            txtTitle.TextChanged += SetModified;
            templateSyntaxBox.TextChanged += SetModified;
        }

        private void RegisterEvents()
        {
            txtTitle.TextChanged += SetModified;
            templateSyntaxBox.TextChanged += SetModified;
        }

        private void SetModified(object sender, EventArgs e)
        {
            this.IsModified = true;

            if (this.Modified != null)
                this.Modified(this, new EventArgs());
        }
    }
}
