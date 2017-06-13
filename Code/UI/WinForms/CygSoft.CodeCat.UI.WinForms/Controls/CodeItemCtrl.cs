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
            RegisterEvents();
            FontSizeChanged += Base_FontSizeChanged;
            cboSyntax.SelectedIndexChanged += cboSyntax_SelectedIndexChanged;
            txtTitle.TextChanged += SetModified;
            syntaxBoxControl.TextChanged += SetModified;
            this.Modified += CodeItemCtrl_Modified;
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
            cboSyntax.SelectedIndexChanged -= cboSyntax_SelectedIndexChanged;
            txtTitle.TextChanged -= SetModified;
            syntaxBoxControl.TextChanged -= SetModified;
            this.Modified -= CodeItemCtrl_Modified;
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
    }
}
