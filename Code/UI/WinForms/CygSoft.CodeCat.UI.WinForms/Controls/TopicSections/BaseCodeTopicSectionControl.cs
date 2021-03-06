﻿using System;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Topics;
using System.Windows.Forms;
using System.Drawing;

namespace CygSoft.CodeCat.UI.WinForms.Controls.TopicSections
{
    public partial class BaseCodeTopicSectionControl : BaseTopicSectionControl
    {
        public event EventHandler FontModified;
        public event EventHandler SyntaxModified;

        private ToolStripLabel lblSyntax = new ToolStripLabel("lblSyntax");
        private ToolStripSyntaxComboBox cboSyntax = new ToolStripSyntaxComboBox();
        private ToolStripFontSizeComboBox cboFontSize = new ToolStripFontSizeComboBox();

        public override int ImageKey { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Index; } }
        public override Icon ImageIcon { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Icon; } }
        public override Image IconImage { get { return IconRepository.Get(cboSyntax.SelectedItem.ToString()).Image; } }

        

        public string Syntax
        {
            get { return cboSyntax.Syntax; }
            set { cboSyntax.Syntax = value; }
        }

        public Single FontSize { get { return Convert.ToSingle(cboFontSize.SelectedItem); } }

        

        public BaseCodeTopicSectionControl()
            : this(null, null, null)
        {

        }

        public BaseCodeTopicSectionControl(AppFacade application, ITopicDocument topicDocument, ICodeTopicSection topicSection)
            : base(application, topicDocument, topicSection)
        {
            InitializeComponent();

            if (topicDocument == null)
                return;

            lblSyntax.Text = "Syntax";

            HeaderToolstrip.Items.Add(lblSyntax);
            HeaderToolstrip.Items.Add(cboSyntax);
            FooterToolstrip.Items.Insert(0, cboFontSize);

            cboFontSize.SetFont(ConfigSettings.DefaultFontSize);

            cboSyntax.LoadSyntaxes(application.GetSyntaxes());
            cboSyntax.Syntax = CodeTopicSection().Syntax;
            base.SetStateImage(IconRepository.Get(Syntax).Image);

            cboFontSize.SelectedIndexChanged += (s, e) => { FontModified?.Invoke(this, new EventArgs()); };
            cboSyntax.SelectedIndexChanged += cboSyntax_SelectedIndexChanged;
        }

        private ICodeTopicSection CodeTopicSection()
        {
            return base.topicSection as ICodeTopicSection;
        }

        private void cboSyntax_SelectedIndexChanged(object sender, EventArgs e)
        {
            SyntaxModified?.Invoke(this, new EventArgs());
            base.SetStateImage(IconRepository.Get(Syntax).Image);
            base.Modify();
        }
    }
}
