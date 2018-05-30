using Alsing.SourceCode;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Files.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using CygSoft.CodeCat.Qik.LanguageEngine.Infrastructure;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class QikTemplateCodeCtrl : UserControl, ITopicSectionBaseControl
    {
        public event EventHandler Modified;

        private IImageResources imageResources;
        private IIconRepository iconRepository;
        private ICodeTopicSection templateFile;
        private IAppFacade application;
        private IQikTemplateDocumentSet qikTemplateDocumentSet;
        private ICompiler compiler;

        public QikTemplateCodeCtrl(IAppFacade application, IImageResources imageResources, IIconRepository iconRepository, IQikTemplateDocumentSet qikTemplateDocumentSet, 
            ICodeTopicSection templateFile)
        {
            InitializeComponent();

            if (iconRepository == null)
                throw new ArgumentNullException("Image Repository is a required constructor parameter and cannot be null");

            this.iconRepository = iconRepository;

            this.imageResources = imageResources;
            this.application = application;
            this.qikTemplateDocumentSet = qikTemplateDocumentSet;
            this.templateFile = templateFile;
            compiler = qikTemplateDocumentSet.Compiler;
            
            Id = templateFile.Id;

            templateSyntaxDocument.SyntaxFile = ConfigSettings.QikTemplateSyntaxFile;

            SetDefaultFont();
            InitializeSyntaxList();

            RegisterSyntaxEditorEvents();
            ResetFieldValues();
            RegisterDataFieldEvents();
            RegisterFileEvents();

            UpdateAutoList();
        }

        public int ImageKey { get { return iconRepository.Get(cboSyntax.SelectedItem.ToString()).Index; } }
        public Icon ImageIcon { get { return iconRepository.Get(cboSyntax.SelectedItem.ToString()).Icon; } }
        public Image IconImage { get { return iconRepository.Get(cboSyntax.SelectedItem.ToString()).Image; } }
        public string Id { get; private set; }
        public string Title { get { return txtTitle.Text; } }
        public string TemplateText { get { return templateSyntaxDocument.Text; } }
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
                string toolTip = string.Format("{0}\n{1}", itemText, Gui.Text.WordWrap(symbolInfo.Description, 150));

                templateSyntaxBox.AutoListAdd(itemText, placeholder, toolTip, 0);
            }
        }

        private void ResetFieldValues()
        {
            txtTitle.Text = templateFile.Title;
            templateSyntaxBox.Document.Text = templateFile.Text;
            outputSyntaxBox.Document.Text = string.Empty;
            SelectSyntax(templateFile.Syntax);

            IsModified = false;
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
            IsModified = false;
            SetChangeStatus();
        }

        private void qikTemplateDocumentSet_BeforeContentSaved(object sender, FileEventArgs e)
        {
            templateFile.Title = txtTitle.Text;
            templateFile.Text = templateSyntaxDocument.Text;
            templateFile.Syntax = cboSyntax.SelectedItem.ToString();
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
            Modified += QikTemplateCodeCtrl_Modified;
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
            lblEditStatus.Text = IsModified ? "Edited" : "Saved";
            lblEditStatus.ForeColor = IsModified ? Color.DarkRed : Color.Black;
        }

        private void UnregisterDataFieldEvents()
        {
            cboSyntax.SelectedIndexChanged -= cboSyntax_SelectedIndexChanged;
            cboFontSize.SelectedIndexChanged -= cboFontSize_SelectedIndexChanged;
            txtTitle.TextChanged -= SetModified;
            templateSyntaxBox.TextChanged -= SetModified;
            Modified -= QikTemplateCodeCtrl_Modified;
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            templateSyntaxBox.FontSize = Convert.ToSingle(cboFontSize.SelectedItem);
            outputSyntaxBox.FontSize = Convert.ToSingle(cboFontSize.SelectedItem);
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
            IsModified = true;

            Modified?.Invoke(this, new EventArgs());
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
            outputSyntaxBox.Document.SyntaxFile = syntaxFile;

            lblEditStatus.Image = iconRepository.Get(syn).Image;
        }

        private void SetDefaultFont()
        {
            cboFontSize.SelectedIndex = cboFontSize.FindStringExact(ConfigSettings.DefaultFontSize.ToString());
        }

        private void templateSyntaxBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!templateSyntaxBox.ReadOnly)
            {
                if (e.KeyData == (Keys.Shift | Keys.F8) || e.KeyData == Keys.F8)
                {
                    if (compiler.Placeholders.Count() > 0)
                    {
                        templateSyntaxBox.AutoListPosition = new TextPoint(templateSyntaxBox.Caret.Position.X, templateSyntaxBox.Caret.Position.Y);
                        templateSyntaxBox.AutoListVisible = true;
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
