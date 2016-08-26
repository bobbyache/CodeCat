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
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.DocumentManager.Infrastructure;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class QikScriptCtrl : UserControl, IDocumentItemControl
    {
        public event EventHandler Modified;

        private AppFacade application;
        private QikFile qikFile;
        private ICodeDocument scriptFile;
        private ICompiler compiler;

        public QikScriptCtrl(AppFacade application, QikFile qikFile)
        {
            InitializeComponent();

            this.application = application;
            this.qikFile = qikFile;
            this.scriptFile = qikFile.ScriptFile;
            this.compiler = qikFile.Compiler;

            syntaxDocument.Text = this.scriptFile.Text;
            syntaxDocument.SyntaxFile = ConfigSettings.QikScriptSyntaxFile;

            SetDefaultFont();
            RegisterDataFieldEvents();
            RegisterFileEvents();
        }

        public int ImageKey { get { return IconRepository.ImageKeyFor(IconRepository.QikKey); } }
        public TabPage ParentTab { get; set; }

        public string Id { get { return this.scriptFile.Id; } }

        public string Title { get { return this.scriptFile.Title; } }

        public string ScriptText
        {
            get { return this.syntaxDocument.Text; }
        }

        public bool IsModified { get; private set; }

        public bool FileExists { get { return qikFile.ScriptFile.Exists; } }

        public void Revert()
        {
            UnregisterDataFieldEvents();
            ResetFieldValues();
            RegisterDataFieldEvents();
        }

        private void ResetFieldValues()
        {
            syntaxDocument.Text = scriptFile.Text;
            this.IsModified = false;
            SetChangeStatus();
        }

        private void RegisterFileEvents()
        {
            qikFile.BeforeContentSaved += qikFile_BeforeContentSaved;
            qikFile.ContentSaved += qikFile_ContentSaved;
            compiler.AfterCompile += compiler_AfterCompile;
            compiler.AfterInput += compiler_AfterInput;
        }

        private void qikFile_ContentSaved(object sender, EventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void qikFile_BeforeContentSaved(object sender, EventArgs e)
        {
            scriptFile.Text = syntaxDocument.Text;
        }

        private void RegisterDataFieldEvents()
        {
            cboFontSize.SelectedIndexChanged += cboFontSize_SelectedIndexChanged;
            syntaxBoxControl.TextChanged += SetModified;
            this.Modified += ScriptCodeCtrl_Modified;
        }

        private void ScriptCodeCtrl_Modified(object sender, EventArgs e)
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
            cboFontSize.SelectedIndexChanged -= cboFontSize_SelectedIndexChanged;
            syntaxBoxControl.TextChanged -= SetModified;
            this.Modified -= ScriptCodeCtrl_Modified;
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.syntaxBoxControl.FontSize = Convert.ToSingle(cboFontSize.SelectedItem);
        }

        private void compiler_AfterInput(object sender, EventArgs e)
        {
        }

        private void compiler_AfterCompile(object sender, EventArgs e)
        {
        }

        private void SetModified(object sender, EventArgs e)
        {
            this.IsModified = true;

            if (this.Modified != null)
                this.Modified(this, new EventArgs());
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
