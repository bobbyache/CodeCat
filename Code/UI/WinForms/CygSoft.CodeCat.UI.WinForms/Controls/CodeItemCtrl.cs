using System;
using System.Drawing;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Topics;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class CodeItemCtrl : TopicSectionBaseControl, IDocumentItemControl
    {
        



        public CodeItemCtrl(AppFacade application, ITopicDocument topicDocument, ICodeTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();

            syntaxDocument.SyntaxFile = ConfigSettings.QikTemplateSyntaxFile;
            
            ResetFieldValues();
            RegisterDataFieldEvents();
            RegisterFileEvents();
        }

        public string TemplateText { get { return this.syntaxDocument.Text; } }
        public bool FileExists { get { return topicSection.Exists; } }

        public void Revert()
        {
            UnregisterDataFieldEvents();
            ResetFieldValues();
            RegisterDataFieldEvents();
        }

        private ICodeTopicSection CodeTopicSection()
        {
            return base.topicSection as ICodeTopicSection;
        }

        private void ResetFieldValues()
        {
            syntaxBoxControl.Document.Text = CodeTopicSection().Text;
            base.Syntax = CodeTopicSection().Syntax;

            base.IsModified = false;
            SetChangeStatus();
        }

        private void RegisterFileEvents()
        {
            topicDocument.BeforeSave += codeGroupFile_BeforeContentSaved;
            topicDocument.AfterSave += codeGroupFile_ContentSaved;
        }

        private void codeGroupFile_ContentSaved(object sender, TopicEventArgs e)
        {
            base.IsModified = false;
            SetChangeStatus();
        }

        private void codeGroupFile_BeforeContentSaved(object sender, TopicEventArgs e)
        {
            this.topicSection.Title = base.Title;
            this.CodeTopicSection().Text = syntaxDocument.Text;
            this.CodeTopicSection().Syntax = base.Syntax;
        }

        private void RegisterDataFieldEvents()
        {
            RegisterEvents();
            FontSizeChanged += Base_FontSizeChanged;
            SyntaxChanged += Base_SyntaxChanged;
            syntaxBoxControl.TextChanged += SetModified;
            this.Modified += CodeItemCtrl_Modified;
        }

        private void Base_SyntaxChanged(object sender, EventArgs e)
        {
            // don't want the syntax box to fire any events here...
            syntaxBoxControl.TextChanged -= SetModified;
            this.syntaxBoxControl.Document.SyntaxFile = base.SyntaxFile;
            this.lblEditStatus.Image = IconRepository.Get(Syntax).Image;
            syntaxBoxControl.TextChanged += SetModified;
            SetModified(this, e);
        }

        private void Base_FontSizeChanged(object sender, EventArgs e)
        {
            this.syntaxBoxControl.FontSize = base.FontSize;
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
            UnregisterEvents();
            syntaxBoxControl.TextChanged -= SetModified;
            this.Modified -= CodeItemCtrl_Modified;
        }


    }
}
