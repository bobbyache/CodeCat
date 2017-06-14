﻿using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Topics;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class VersionedCodeTopicSectionControl : UserControl, ITopicSectionBaseControl
    {
        public event EventHandler Modified;

        private AppFacade application;
        private ITopicDocument topicDocument;
        private IVersionedCodeTopicSection topicSection;

        public VersionedCodeTopicSectionControl(AppFacade application, ITopicDocument topicDocument, IVersionedCodeTopicSection topicSection)
        {
            InitializeComponent();

            tabControl.Alignment = TabAlignment.Left;

            this.application = application;
            this.topicDocument = topicDocument;
            this.topicSection = topicSection;
            this.Id = topicSection.Id;

            SetDefaultFont();
            InitializeSyntaxList();

            ResetFieldValues();
            RegisterDataFieldEvents();
            RegisterFileEvents();
        }

        public string Id { get; private set; }
        public string Title { get { return ""; } }
        public int ImageKey { get { return IconRepository.Get(IconRepository.Documents.CodeFile).Index; } }
        public Icon ImageIcon { get { return IconRepository.Get(IconRepository.Documents.CodeFile).Icon; } }
        public Image IconImage { get { return IconRepository.Get(IconRepository.Documents.CodeFile).Image; } }
        public bool IsModified { get; private set; }
        public bool FileExists { get { return false; } }

        public void Revert()
        {
            UnregisterDataFieldEvents();
            ResetFieldValues();
            RegisterDataFieldEvents();
        }

        private void ResetFieldValues()
        {
            txtTitle.Text = topicSection.Title;
            syntaxBox.Document.Text = topicSection.Text;
            SelectSyntax(topicSection.Syntax);

            this.IsModified = false;
            SetChangeStatus();
        }

        private void RegisterFileEvents()
        {
            topicDocument.BeforeSave += codeGroupFile_BeforeContentSaved;
            topicDocument.AfterSave += codeGroupFile_ContentSaved;
        }

        private void RegisterDataFieldEvents()
        {
            cboSyntax.SelectedIndexChanged += cboSyntax_SelectedIndexChanged;
            cboFontSize.SelectedIndexChanged += cboFontSize_SelectedIndexChanged;
            txtTitle.TextChanged += SetModified;
            syntaxBox.TextChanged += SetModified;
            this.Modified += CodeItemCtrl_Modified;
        }

        private void UnregisterDataFieldEvents()
        {
            cboSyntax.SelectedIndexChanged -= cboSyntax_SelectedIndexChanged;
            cboFontSize.SelectedIndexChanged -= cboFontSize_SelectedIndexChanged;
            txtTitle.TextChanged -= SetModified;
            syntaxBox.TextChanged -= SetModified;
            this.Modified -= CodeItemCtrl_Modified;
        }

        private void codeGroupFile_ContentSaved(object sender, TopicEventArgs e)
        {
            this.IsModified = false;
            SetChangeStatus();
        }

        private void CodeItemCtrl_Modified(object sender, EventArgs e)
        {
            SetChangeStatus();
        }

        private void codeGroupFile_BeforeContentSaved(object sender, TopicEventArgs e)
        {
            this.topicSection.Title = txtTitle.Text;
            this.topicSection.Text = syntaxDocument.Text;
            this.topicSection.Syntax = cboSyntax.SelectedItem.ToString();
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.syntaxBox.FontSize = Convert.ToSingle(cboFontSize.SelectedItem);
            this.syntaxBox.FontSize = Convert.ToSingle(cboFontSize.SelectedItem);
        }

        private void cboSyntax_SelectedIndexChanged(object sender, EventArgs e)
        {
            // don't want the syntax box to fire any events here...
            syntaxBox.TextChanged -= SetModified;
            SelectSyntax(cboSyntax.SelectedItem.ToString());
            syntaxBox.TextChanged += SetModified;
            SetModified(this, e);
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
            this.syntaxBox.Document.SyntaxFile = syntaxFile;

            this.lblEditStatus.Image = IconRepository.Get(syn).Image;
        }

        private void SetModified(object sender, EventArgs e)
        {
            this.IsModified = true;
            this.Modified?.Invoke(this, new EventArgs());
        }

        private void SetChangeStatus()
        {
            lblEditStatus.Text = IsModified ? "Edited" : "Saved";
            lblEditStatus.ForeColor = IsModified ? Color.DarkRed : Color.Black;
        }

        private void InitializeImages()
        {
            //this.tabControl.ImageList = IconRepository.ImageList;
            //this.tabPageCode.ImageKey = (base.persistableTarget as CodeFile).Syntax;
            //this.Icon = IconRepository.Get((base.persistableTarget as CodeFile).Syntax).Icon;
            lblEditStatus.Image = this.IconImage;
        }

        private void SetDefaultFont()
        {
            cboFontSize.SelectedIndex = cboFontSize.FindStringExact(ConfigSettings.DefaultFontSize.ToString());
        }
    }
}