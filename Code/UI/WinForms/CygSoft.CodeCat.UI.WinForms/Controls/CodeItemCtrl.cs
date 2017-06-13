﻿using System;
using System.Drawing;
using System.Windows.Forms;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Topics;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public partial class CodeItemCtrl : TopicSectionBaseControl, IDocumentItemControl
    {
        public event EventHandler Modified;

        public CodeItemCtrl(AppFacade application, ITopicDocument topicDocument, ICodeTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();

            syntaxDocument.SyntaxFile = ConfigSettings.QikTemplateSyntaxFile;
            
            SetDefaultFont();
            InitializeSyntaxList();

            ResetFieldValues();
            RegisterDataFieldEvents();
            RegisterFileEvents();
        }

        public int ImageKey { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Index; } }
        public Icon ImageIcon { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Icon; } }
        public Image IconImage { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Image; } }
        
        public string Title { get { return this.txtTitle.Text; } }
        public string TemplateText { get { return this.syntaxDocument.Text; } }
        public bool IsModified { get; private set; }
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
            txtTitle.Text = topicSection.Title;
            syntaxBoxControl.Document.Text = CodeTopicSection().Text;
            SelectSyntax(CodeTopicSection().Syntax);

            this.IsModified = false;
            SetChangeStatus();
        }

        private void RegisterFileEvents()
        {
            topicDocument.BeforeSave += codeGroupFile_BeforeContentSaved;
            topicDocument.AfterSave += codeGroupFile_ContentSaved;
        }

        private void codeGroupFile_ContentSaved(object sender, TopicEventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void codeGroupFile_BeforeContentSaved(object sender, TopicEventArgs e)
        {
            this.topicSection.Title = txtTitle.Text;
            this.CodeTopicSection().Text = syntaxDocument.Text;
            this.CodeTopicSection().Syntax = cboSyntax.SelectedItem.ToString();
        }

        private void RegisterDataFieldEvents()
        {
            cboSyntax.SelectedIndexChanged += cboSyntax_SelectedIndexChanged;
            cboFontSize.SelectedIndexChanged += cboFontSize_SelectedIndexChanged;
            txtTitle.TextChanged += SetModified;
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
            this.Modified?.Invoke(this, new EventArgs());
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
            this.syntaxBoxControl.Document.SyntaxFile = syntaxFile;

            this.lblEditStatus.Image = IconRepository.Get(syn).Image;
        }

        private void SetDefaultFont()
        {
            cboFontSize.SelectedIndex = cboFontSize.FindStringExact(ConfigSettings.DefaultFontSize.ToString());
        }
    }
}
