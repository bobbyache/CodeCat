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
using Alsing.SourceCode;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class QikScriptCtrl : UserControl, IDocumentItemControl
    {
        public event EventHandler Modified;

        private AppFacade application;
        private IQikTemplateDocumentSet qikTemplateDocumentSet;
        private ICodeDocument scriptFile;
        private ICompiler compiler;
        private Row selectedRow;

        public QikScriptCtrl(AppFacade application, IQikTemplateDocumentSet qikTemplateDocumentSet)
        {
            InitializeComponent();

            this.application = application;
            this.qikTemplateDocumentSet = qikTemplateDocumentSet;
            this.scriptFile = qikTemplateDocumentSet.ScriptFile;
            this.compiler = qikTemplateDocumentSet.Compiler;

            syntaxDocument.Text = this.scriptFile.Text;
            syntaxDocument.SyntaxFile = ConfigSettings.QikScriptSyntaxFile;

            SetDefaultFont();
            RegisterDataFieldEvents();
            RegisterFileEvents();
        }

        public int ImageKey { get { return IconRepository.Get(IconRepository.Documents.QikGroup).Index; } }
        public Icon ImageIcon { get { return IconRepository.Get(IconRepository.Documents.QikGroup).Icon; } }
        public Image IconImage { get { return IconRepository.Get(IconRepository.Documents.QikGroup).Image; } }

        public string Id { get { return this.scriptFile.Id; } }

        public string Title { get { return this.scriptFile.Title; } }

        public string ScriptText
        {
            get { return this.syntaxDocument.Text; }
        }

        public bool IsModified { get; private set; }

        public bool FileExists { get { return qikTemplateDocumentSet.ScriptFile.Exists; } }

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
            qikTemplateDocumentSet.BeforeSave += qikTemplateDocumentSet_BeforeContentSaved;
            qikTemplateDocumentSet.AfterSave += qikTemplateDocumentSet_ContentSaved;
            compiler.AfterCompile += compiler_AfterCompile;
            compiler.AfterInput += compiler_AfterInput;
            compiler.CompileError += compiler_CompileError;
            compiler.BeforeCompile += compiler_BeforeCompile;
        }

        private void qikTemplateDocumentSet_ContentSaved(object sender, FileEventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void qikTemplateDocumentSet_BeforeContentSaved(object sender, FileEventArgs e)
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

        private void syntaxBoxControl_RowClick(object sender, Alsing.Windows.Forms.SyntaxBox.RowMouseEventArgs e)
        {
            DeselectRow();
        }

        private void compiler_BeforeCompile(object sender, EventArgs e)
        {
            DeselectRow();
            errorListView.Items.Clear();
        }

        private void compiler_CompileError(object sender, CompileErrorEventArgs e)
        {
            AddErrorLine(e.Line, e.Column, e.Message, e.Location, e.OffendingSymbol);
        }

        private void errorListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedRow != null)
                selectedRow.BackColor = Color.White;

            if (errorListView.SelectedItems.Count > 0)
            {
                ListViewItem item = errorListView.SelectedItems[0];
                int lineNumber = int.Parse(item.Text);
                SelectRow(lineNumber);
            }
        }

        private void errorListView_Leave(object sender, EventArgs e)
        {
            DeselectRow();
        }

        private void AddErrorLine(int line, int column, string message, string ruleStack, string symbol)
        {
            ListViewItem item = new ListViewItem();
            item.Text = line.ToString();
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, column.ToString()));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, message));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, ruleStack));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, symbol));

            errorListView.Items.Add(item);
        }

        private Row RowFromLine(int line)
        {
            int index = line > 1 ? line - 1 : line;
            Row row = syntaxBoxControl.Document[index];
            return row;
        }

        private void SelectRow(int line)
        {
            Row row = RowFromLine(line);
            if (row != null)
            {
                syntaxBoxControl.GotoLine(line);
                row.BackColor = Color.Gray;
                selectedRow = row;
            }
            else
                selectedRow = null;
        }

        private void DeselectRow()
        {
            if (selectedRow != null)
            {
                selectedRow.BackColor = Color.White;
                this.selectedRow = null;
            }
        }
    }
}
