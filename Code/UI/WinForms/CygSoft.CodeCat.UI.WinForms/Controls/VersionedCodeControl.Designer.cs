namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    partial class VersionedCodeControl
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
            this.syntaxDocument = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtTitle = new CygSoft.CodeCat.UI.WinForms.ToolStripSpringTextBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.cboSyntax = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.cboFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.lblEditStatus = new System.Windows.Forms.ToolStripLabel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCode = new System.Windows.Forms.TabPage();
            this.syntaxBox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.tabPageSnapshots = new System.Windows.Forms.TabPage();
            this.snapshotListCtrl1 = new CygSoft.CodeCat.UI.WinForms.SnapshotListCtrl();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageCode.SuspendLayout();
            this.tabPageSnapshots.SuspendLayout();
            this.SuspendLayout();
            // 
            // syntaxDocument
            // 
            this.syntaxDocument.Lines = new string[] {
        ""};
            this.syntaxDocument.MaxUndoBufferSize = 1000;
            this.syntaxDocument.Modified = false;
            this.syntaxDocument.UndoStep = 0;
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
            this.toolStrip1.Size = new System.Drawing.Size(695, 25);
            this.toolStrip1.TabIndex = 7;
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
            this.txtTitle.Size = new System.Drawing.Size(427, 25);
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
            this.toolStrip2.Location = new System.Drawing.Point(0, 508);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(695, 25);
            this.toolStrip2.TabIndex = 8;
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
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageCode);
            this.tabControl.Controls.Add(this.tabPageSnapshots);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 25);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(695, 483);
            this.tabControl.TabIndex = 9;
            // 
            // tabPageCode
            // 
            this.tabPageCode.Controls.Add(this.syntaxBox);
            this.tabPageCode.Location = new System.Drawing.Point(4, 22);
            this.tabPageCode.Name = "tabPageCode";
            this.tabPageCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCode.Size = new System.Drawing.Size(687, 457);
            this.tabPageCode.TabIndex = 0;
            this.tabPageCode.Text = "Code";
            this.tabPageCode.UseVisualStyleBackColor = true;
            // 
            // syntaxBox
            // 
            this.syntaxBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.syntaxBox.AutoListPosition = null;
            this.syntaxBox.AutoListSelectedText = "a123";
            this.syntaxBox.AutoListVisible = false;
            this.syntaxBox.BackColor = System.Drawing.Color.White;
            this.syntaxBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.syntaxBox.CopyAsRTF = false;
            this.syntaxBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syntaxBox.FontName = "Courier new";
            this.syntaxBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.syntaxBox.InfoTipCount = 1;
            this.syntaxBox.InfoTipPosition = null;
            this.syntaxBox.InfoTipSelectedIndex = 1;
            this.syntaxBox.InfoTipVisible = false;
            this.syntaxBox.Location = new System.Drawing.Point(3, 3);
            this.syntaxBox.LockCursorUpdate = false;
            this.syntaxBox.Name = "syntaxBox";
            this.syntaxBox.ShowScopeIndicator = false;
            this.syntaxBox.Size = new System.Drawing.Size(681, 451);
            this.syntaxBox.SmoothScroll = false;
            this.syntaxBox.SplitviewH = -4;
            this.syntaxBox.SplitviewV = -4;
            this.syntaxBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.syntaxBox.TabIndex = 2;
            this.syntaxBox.Text = "syntaxBoxControl1";
            this.syntaxBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // tabPageSnapshots
            // 
            this.tabPageSnapshots.Controls.Add(this.snapshotListCtrl1);
            this.tabPageSnapshots.Location = new System.Drawing.Point(4, 22);
            this.tabPageSnapshots.Name = "tabPageSnapshots";
            this.tabPageSnapshots.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSnapshots.Size = new System.Drawing.Size(813, 370);
            this.tabPageSnapshots.TabIndex = 1;
            this.tabPageSnapshots.Text = "Snapshots";
            this.tabPageSnapshots.UseVisualStyleBackColor = true;
            // 
            // snapshotListCtrl1
            // 
            this.snapshotListCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.snapshotListCtrl1.Location = new System.Drawing.Point(3, 3);
            this.snapshotListCtrl1.Name = "snapshotListCtrl1";
            this.snapshotListCtrl1.Size = new System.Drawing.Size(807, 364);
            this.snapshotListCtrl1.TabIndex = 0;
            // 
            // VersionedCodeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStrip2);
            this.Name = "VersionedCodeControl";
            this.Size = new System.Drawing.Size(695, 533);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageCode.ResumeLayout(false);
            this.tabPageSnapshots.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Alsing.SourceCode.SyntaxDocument syntaxDocument;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private ToolStripSpringTextBox txtTitle;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripComboBox cboSyntax;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripComboBox cboFontSize;
        private System.Windows.Forms.ToolStripLabel lblEditStatus;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageCode;
        private Alsing.Windows.Forms.SyntaxBoxControl syntaxBox;
        private System.Windows.Forms.TabPage tabPageSnapshots;
        private SnapshotListCtrl snapshotListCtrl1;
    }
}
