namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    partial class QikTemplateCodeCtrl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtTitle = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.cboSyntax = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.cboFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.lblEditStatus = new System.Windows.Forms.ToolStripLabel();
            this.templateFileTabControl = new System.Windows.Forms.TabControl();
            this.templateTabPage = new System.Windows.Forms.TabPage();
            this.templateSyntaxBox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.templateSyntaxDocument = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.outputTabPage = new System.Windows.Forms.TabPage();
            this.outputSyntaxBox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.outputSyntaxDocument = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.templateFileTabControl.SuspendLayout();
            this.templateTabPage.SuspendLayout();
            this.outputTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtTitle,
            this.toolStripLabel5,
            this.cboSyntax});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(656, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.AutoSize = false;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel1.Text = "Title";
            this.toolStripLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTitle
            // 
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(384, 25);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(41, 22);
            this.toolStripLabel5.Text = "Syntax";
            // 
            // cboSyntax
            // 
            this.cboSyntax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSyntax.Name = "cboSyntax";
            this.cboSyntax.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cboFontSize,
            this.lblEditStatus});
            this.toolStrip2.Location = new System.Drawing.Point(0, 492);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(656, 25);
            this.toolStrip2.TabIndex = 5;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // cboFontSize
            // 
            this.cboFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFontSize.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Size = new System.Drawing.Size(75, 25);
            // 
            // lblEditStatus
            // 
            this.lblEditStatus.AutoSize = false;
            this.lblEditStatus.Name = "lblEditStatus";
            this.lblEditStatus.Size = new System.Drawing.Size(100, 22);
            this.lblEditStatus.Text = "No Changes";
            this.lblEditStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // templateFileTabControl
            // 
            this.templateFileTabControl.Controls.Add(this.templateTabPage);
            this.templateFileTabControl.Controls.Add(this.outputTabPage);
            this.templateFileTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.templateFileTabControl.Location = new System.Drawing.Point(0, 25);
            this.templateFileTabControl.Name = "templateFileTabControl";
            this.templateFileTabControl.SelectedIndex = 0;
            this.templateFileTabControl.Size = new System.Drawing.Size(656, 467);
            this.templateFileTabControl.TabIndex = 6;
            this.templateFileTabControl.SelectedIndexChanged += new System.EventHandler(this.templateFileTabControl_SelectedIndexChanged);
            // 
            // templateTabPage
            // 
            this.templateTabPage.Controls.Add(this.templateSyntaxBox);
            this.templateTabPage.Location = new System.Drawing.Point(4, 22);
            this.templateTabPage.Name = "templateTabPage";
            this.templateTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.templateTabPage.Size = new System.Drawing.Size(648, 441);
            this.templateTabPage.TabIndex = 0;
            this.templateTabPage.Text = "Template";
            this.templateTabPage.UseVisualStyleBackColor = true;
            // 
            // templateSyntaxBox
            // 
            this.templateSyntaxBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.templateSyntaxBox.AutoListPosition = null;
            this.templateSyntaxBox.AutoListSelectedText = "a123";
            this.templateSyntaxBox.AutoListVisible = false;
            this.templateSyntaxBox.BackColor = System.Drawing.Color.White;
            this.templateSyntaxBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.templateSyntaxBox.CopyAsRTF = false;
            this.templateSyntaxBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.templateSyntaxBox.Document = this.templateSyntaxDocument;
            this.templateSyntaxBox.FontName = "Courier new";
            this.templateSyntaxBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.templateSyntaxBox.InfoTipCount = 1;
            this.templateSyntaxBox.InfoTipPosition = null;
            this.templateSyntaxBox.InfoTipSelectedIndex = 1;
            this.templateSyntaxBox.InfoTipVisible = false;
            this.templateSyntaxBox.Location = new System.Drawing.Point(3, 3);
            this.templateSyntaxBox.LockCursorUpdate = false;
            this.templateSyntaxBox.Name = "templateSyntaxBox";
            this.templateSyntaxBox.ShowScopeIndicator = false;
            this.templateSyntaxBox.Size = new System.Drawing.Size(642, 435);
            this.templateSyntaxBox.SmoothScroll = false;
            this.templateSyntaxBox.SplitviewH = -4;
            this.templateSyntaxBox.SplitviewV = -4;
            this.templateSyntaxBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.templateSyntaxBox.TabIndex = 0;
            this.templateSyntaxBox.Text = "syntaxBoxControl1";
            this.templateSyntaxBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // templateSyntaxDocument
            // 
            this.templateSyntaxDocument.Lines = new string[] {
        ""};
            this.templateSyntaxDocument.MaxUndoBufferSize = 1000;
            this.templateSyntaxDocument.Modified = false;
            this.templateSyntaxDocument.UndoStep = 0;
            // 
            // outputTabPage
            // 
            this.outputTabPage.Controls.Add(this.outputSyntaxBox);
            this.outputTabPage.Location = new System.Drawing.Point(4, 22);
            this.outputTabPage.Name = "outputTabPage";
            this.outputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.outputTabPage.Size = new System.Drawing.Size(648, 441);
            this.outputTabPage.TabIndex = 1;
            this.outputTabPage.Text = "Output";
            this.outputTabPage.UseVisualStyleBackColor = true;
            // 
            // outputSyntaxBox
            // 
            this.outputSyntaxBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.outputSyntaxBox.AutoListPosition = null;
            this.outputSyntaxBox.AutoListSelectedText = "a123";
            this.outputSyntaxBox.AutoListVisible = false;
            this.outputSyntaxBox.BackColor = System.Drawing.Color.White;
            this.outputSyntaxBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.outputSyntaxBox.CopyAsRTF = false;
            this.outputSyntaxBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputSyntaxBox.Document = this.outputSyntaxDocument;
            this.outputSyntaxBox.FontName = "Courier new";
            this.outputSyntaxBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.outputSyntaxBox.InfoTipCount = 1;
            this.outputSyntaxBox.InfoTipPosition = null;
            this.outputSyntaxBox.InfoTipSelectedIndex = 1;
            this.outputSyntaxBox.InfoTipVisible = false;
            this.outputSyntaxBox.Location = new System.Drawing.Point(3, 3);
            this.outputSyntaxBox.LockCursorUpdate = false;
            this.outputSyntaxBox.Name = "outputSyntaxBox";
            this.outputSyntaxBox.ReadOnly = true;
            this.outputSyntaxBox.ShowScopeIndicator = false;
            this.outputSyntaxBox.Size = new System.Drawing.Size(642, 435);
            this.outputSyntaxBox.SmoothScroll = false;
            this.outputSyntaxBox.SplitviewH = -4;
            this.outputSyntaxBox.SplitviewV = -4;
            this.outputSyntaxBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.outputSyntaxBox.TabIndex = 0;
            this.outputSyntaxBox.Text = "syntaxBoxControl1";
            this.outputSyntaxBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // outputSyntaxDocument
            // 
            this.outputSyntaxDocument.Lines = new string[] {
        ""};
            this.outputSyntaxDocument.MaxUndoBufferSize = 1000;
            this.outputSyntaxDocument.Modified = false;
            this.outputSyntaxDocument.UndoStep = 0;
            // 
            // QikTemplateCodeCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.templateFileTabControl);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "QikTemplateCodeCtrl";
            this.Size = new System.Drawing.Size(656, 517);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.templateFileTabControl.ResumeLayout(false);
            this.templateTabPage.ResumeLayout(false);
            this.outputTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private ToolStripSpringTextBox txtTitle;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripComboBox cboSyntax;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripComboBox cboFontSize;
        private System.Windows.Forms.ToolStripLabel lblEditStatus;
        private System.Windows.Forms.TabControl templateFileTabControl;
        private System.Windows.Forms.TabPage templateTabPage;
        private System.Windows.Forms.TabPage outputTabPage;
        private Alsing.Windows.Forms.SyntaxBoxControl templateSyntaxBox;
        private Alsing.SourceCode.SyntaxDocument templateSyntaxDocument;
        private Alsing.Windows.Forms.SyntaxBoxControl outputSyntaxBox;
        private Alsing.SourceCode.SyntaxDocument outputSyntaxDocument;
    }
}
