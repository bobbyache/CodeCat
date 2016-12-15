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

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class QikTemplateCodeCtrl : UserControl, IDocumentItemControl
    {
        public event EventHandler Modified;

        private ICodeDocument templateFile;
        private AppFacade application;
        private IQikTemplateDocumentSet qikTemplateDocumentSet;
        private ICompiler compiler;

        public QikTemplateCodeCtrl(AppFacade application, IQikTemplateDocumentSet qikTemplateDocumentSet, ICodeDocument templateFile)
        {
            InitializeComponent();
            
            this.application = application;
            this.qikTemplateDocumentSet = qikTemplateDocumentSet;
            this.compiler = qikTemplateDocumentSet.Compiler;
            this.templateFile = templateFile;
            this.Id = templateFile.Id;

            templateSyntaxDocument.SyntaxFile = ConfigSettings.QikTemplateSyntaxFile;

            SetDefaultFont();
            InitializeSyntaxList();

            RegisterSyntaxEditorEvents();
            ResetFieldValues();
            RegisterDataFieldEvents();
            RegisterFileEvents();

            UpdateAutoList();
        }

        public int ImageKey { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Index; } }
        public Icon ImageIcon { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Icon; } }
        public Image IconImage { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Image; } }

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

        public bool FileExists { get { return templateFile.Exists; } }

        public void Revert()
        {
            UnregisterDataFieldEvents();
            ResetFieldValues();
            RegisterDataFieldEvents();
        }

        private void UpdateOutputDocument()
        {
            string input = templateSyntaxBox.Document.Text;
            foreach (string placeholder in compiler.Placeholders)
            {
                string output = compiler.GetValueOfPlaceholder(placeholder);
                input = input.Replace(placeholder, output);
            }

            outputSyntaxBox.Document.Text = input;
        }

        private void UpdateAutoList()
        {
            templateSyntaxBox.AutoListClear();

            foreach (string placeholder in compiler.Placeholders)
            {
                ISymbolInfo symbolInfo = compiler.GetPlaceholderInfo(placeholder);
                string itemText = string.Format("{0} ({1})", symbolInfo.Title, symbolInfo.Placeholder);
                string toolTip = string.Format("{0}\n{1}", itemText, WordWrapper.WordWrap(symbolInfo.Description, 150));

                templateSyntaxBox.AutoListAdd(itemText, placeholder, toolTip, 0);
            }
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
            qikTemplateDocumentSet.BeforeSave += qikTemplateDocumentSet_BeforeContentSaved;
            qikTemplateDocumentSet.AfterSave += qikTemplateDocumentSet_ContentSaved;
            compiler.AfterCompile += compiler_AfterCompile;
            compiler.AfterInput += compiler_AfterInput;
        }

        private void compiler_AfterInput(object sender, EventArgs e)
        {
            UpdateOutputDocument();
            UpdateAutoList();
        }

        private void compiler_AfterCompile(object sender, EventArgs e)
        {
            UpdateOutputDocument();
            UpdateAutoList();
        }

        private void qikTemplateDocumentSet_ContentSaved(object sender, FileEventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void qikTemplateDocumentSet_BeforeContentSaved(object sender, FileEventArgs e)
        {
            this.templateFile.Title = txtTitle.Text;
            this.templateFile.Text = templateSyntaxDocument.Text;
            this.templateFile.Syntax = cboSyntax.SelectedItem.ToString();
        }

        private void RegisterSyntaxEditorEvents()
        {
            templateSyntaxBox.KeyDown += templateSyntaxBox_KeyDown;
            templateSyntaxBox.Leave += templateSyntaxBox_Leave;
            templateSyntaxBox.RowClick += templateSyntaxBox_RowClick;
        }

        private void RegisterDataFieldEvents()
        {
            cboSyntax.SelectedIndexChanged += cboSyntax_SelectedIndexChanged;
            cboFontSize.SelectedIndexChanged += cboFontSize_SelectedIndexChanged;
            txtTitle.TextChanged += SetModified;
            templateSyntaxBox.TextChanged += SetModified;
            this.Modified += QikTemplateCodeCtrl_Modified;
        }

        private void templateSyntaxBox_RowClick(object sender, Alsing.Windows.Forms.SyntaxBox.RowMouseEventArgs e)
        {
            templateSyntaxBox.AutoListVisible = false;
        }

        private void templateSyntaxBox_Leave(object sender, EventArgs e)
        {
            templateSyntaxBox.AutoListVisible = false;
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
            // don't want the syntax box to fire any events here...
            templateSyntaxBox.TextChanged -= SetModified;
            SelectSyntax(cboSyntax.SelectedItem.ToString());
            templateSyntaxBox.TextChanged += SetModified;
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
                    cboSyntax.SelectedItem = item;
            }

            string syntaxFile = application.GetSyntaxFile(syn);
            this.outputSyntaxBox.Document.SyntaxFile = syntaxFile;

            this.lblEditStatus.Image = IconRepository.Get(syn).Image;
        }

        private void SetDefaultFont()
        {
            int index = cboFontSize.FindStringExact(ConfigSettings.DefaultFontSize.ToString());
            if (index >= 0)
                cboFontSize.SelectedIndex = index;
            else
                cboFontSize.SelectedIndex = 4;
        }

        private void templateSyntaxBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.templateSyntaxBox.ReadOnly)
            {
                if (e.KeyData == (Keys.Shift | Keys.F8) || e.KeyData == Keys.F8)
                //if (e.KeyData == (Keys.ControlKey | Keys.F8) || e.KeyData == (Keys.Control | Keys.J))
                {
                    if (this.compiler.Placeholders.Count() > 0)
                    {
                        this.templateSyntaxBox.AutoListPosition = new TextPoint(templateSyntaxBox.Caret.Position.X, templateSyntaxBox.Caret.Position.Y);
                        this.templateSyntaxBox.AutoListVisible = true;
                    }
                }
            }
        }

        private void templateFileTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOutputDocument();
        }
    }
}
